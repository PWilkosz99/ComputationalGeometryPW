using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;

public class Project2 : MonoBehaviour
{
    #region utility
    //A set of universal methods used to create objects 
    private Point2D MakePoint(float x, float y)
    {
        var resp = gameObject.AddComponent<Point2D>();
        resp.PassArgs(x, y, Prefab);
        return resp;
    }

    private Point2D MakeRandomPoint()
    {
        var resp = gameObject.AddComponent<Point2D>();
        resp.PassArgs(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), Prefab);
        return resp;
    }

    private Line MakeLine(Point2D one, Point2D two)
    {
        var resp = gameObject.AddComponent<Line>();
        resp.PassArgs(one, two, LineMaterial);
        return resp;
    }
    private Triangle MakeTriangle(Point2D a, Point2D b, Point2D c)
    {
        var resp = gameObject.AddComponent<Triangle>();
        resp.PassArgs(a, b, c, LineMaterial);
        return resp;
    }
    private Polygon MakePolygon(ArrayList arr)
    {
        var resp = gameObject.AddComponent<Polygon>();
        resp.PassArgs(arr, LineMaterial);
        return resp;
    }
    #endregion
    public int Step = 2;
    public int BoundarySize = 50;
    public int MeshedLayer = 1;
    public Material LineMaterial;
    public Texture2D InputTexture;
    public GameObject Prefab;
    public ComputeShader computeShader;
    public struct Pixel
    {
        public Vector2 coords;
        public float color;
    }

    private ComputeBuffer computeBuffer;
    private RenderTexture renderTexture;
    private List<Point2D> ListofPoints = new List<Point2D>();
    private bool[] meshed = new bool[200];
    void Start()
    {
        computeBuffer = new ComputeBuffer(InputTexture.width * InputTexture.height + InputTexture.height, 3 * sizeof(float));
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "←"))
            MeshedLayer--;
        if (GUI.Button(new Rect(70, 10, 50, 50), "→"))
            MeshedLayer++;
        if (GUI.Button(new Rect(10, 70, 110, 50), "Screenshot")) 
        {
            ScreenCapture.CaptureScreenshot("ScreenShot"+DateTime.Now.ToShortDateString().Replace('.','-')+".png");
            Debug.Log("Zrobiono zrzut ekranu");
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (computeBuffer == null)
            Start();

        if (renderTexture == null)
        {
            renderTexture = new RenderTexture(InputTexture.width, InputTexture.height, 24)
            {
                enableRandomWrite = true
            };
            renderTexture.Create();
        }

        computeShader.SetInt("Step", Step);
        computeShader.SetInt("Size", InputTexture.width);
        computeShader.SetBuffer(0, "Output", computeBuffer);
        computeShader.SetTexture(0, "Input", InputTexture);
        computeShader.SetTexture(0, "Color", renderTexture);
        computeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);

        Pixel[] res = new Pixel[InputTexture.width * InputTexture.height + InputTexture.height];
        computeBuffer.GetData(res);

        var ResultDictionary = new Dictionary<float, List<Pixel>>();

        foreach (Pixel px in res)
        {
            if (px.color != 0) 
            {
                try
                {
                    ResultDictionary[px.color].Add(px);
                }
                catch (Exception)
                {
                    List<Pixel> list = new List<Pixel>
                    {
                        px
                    };
                    ResultDictionary.Add(px.color, list);
                }
            }
        }
        int i = 0;
        foreach (var p in ResultDictionary)
        {
            if (MeshedLayer == i)
            {
                var last = p.Value[p.Value.Count - 1];
                print(MeshedLayer + " of " + ResultDictionary.Count);
                MeshLayer(p.Value, i);
            }
            i++;
        }
        Graphics.Blit(renderTexture, dest);
    }

    void MeshLayer(List<Pixel> layer, int i)
    {
        if(!meshed[i])
        {
            var line = new Dictionary<float, List<Pixel>>();

            List<float> row = layer.Select(a => a.coords.y).Distinct().ToList();

            foreach (float r in row)
                line.Add(r, GetMaximumRange(layer.Where(a => a.coords.y == r)));

            ListofPoints = new List<Point2D>();
            foreach (var l in line)
                foreach (Pixel px in l.Value)
                    ListofPoints.Add(MakePoint(px.coords.x, px.coords.y));

            Vector2 middle = new Vector2(layer.Min(a => a.coords.x), layer.Min(a => a.coords.y));
            float height = layer.Max(a => a.coords.x) - middle.x;
            float width = layer.Max(a => a.coords.y) - middle.y;

            Boundary2 boundaryy = new Boundary2(middle, Mathf.Max(height, width), Mathf.Max(height, width));

            List<Vector2> res = new List<Vector2>();

            SilentQuadTree quadTree = new SilentQuadTree(boundaryy, 4, new Vector2[] { }, res);

            foreach (KeyValuePair<float, List<Pixel>> l in line)
                foreach (Pixel v in l.Value)
                    quadTree.Insert(v.coords);

            quadTree.Deep(ref BoundarySize);


            foreach (var item in res)
            {
                float lval = line.Keys.First();

                foreach (var k in line.Keys)
                {
                    if (Mathf.Abs(k - item.y) < Mathf.Abs(lval - item.y))
                    {
                        lval = k;
                    }
                }

                List<Pixel> range = line[lval];

                if(range.Count > 1) 
                {
                    if (range[1].coords.x > item.x && range[0].coords.x < item.x)
                    {
                        var pktt = gameObject.MakePoint(item.x, item.y, Prefab);
                        ListofPoints.Add(pktt);
                    }
                }
            }
            meshed[i] = true;

            DelaunayTriangulation(ListofPoints);
        }
    }

    void DelaunayTriangulation(List<Point2D> RandomPoints)
    {
        List<Point2D> SPoints = new List<Point2D>() { MakePoint(- 2100, -1100), MakePoint(0, 3100), MakePoint(2100, -1100) };
        DelaunayTriangulator Triangulator = new DelaunayTriangulator(Prefab, SPoints, RandomPoints);
        HashSet<VirtualTriangle> Triangles = Triangulator.BowyerWatson();

        foreach (VirtualTriangle tr in Triangles)
        {
            if (!tr.ISPointExists(SPoints))
            {
                MakeTriangle(tr.Vertices[0], tr.Vertices[1], tr.Vertices[2]);
            }
        }
    }

    List<Pixel> GetMaximumRange(IEnumerable<Pixel> PXs)
    {
        Pixel min = PXs.First();
        Pixel max = PXs.First();

        foreach (Pixel p in PXs)
        {
            if (p.coords.x < min.coords.x)
                min = p;
            if (p.coords.x > max.coords.x)
                max = p;
        }
        List<Pixel> list = new List<Pixel> { min };
        if (min.coords != max.coords)
            list.Add(max);
        return list;
    }
}