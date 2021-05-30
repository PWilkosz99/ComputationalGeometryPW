using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab7 : MonoBehaviour
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
    public GameObject Prefab;
    public Material LineMaterial;
    // Start is called before the first frame update
    void Start()
    {
        var SPoints = new List<Point2D>() { MakePoint(-20, -10), MakePoint(0, 30), MakePoint(20, -10) };
        var SuperTriangle = MakeTriangle(SPoints[0], SPoints[1], SPoints[2]);

        List<Point2D> ListOfPoints = new List<Point2D>();

        for (int i = 0; i < 75; i++)
        {
            ListOfPoints.Add(gameObject.MakeRandomPoint(Prefab));
        }

        DelaunayTriangulator DelaOBJ = new DelaunayTriangulator(Prefab, SPoints, ListOfPoints);
        HashSet<VirtualTriangle> Triangles = DelaOBJ.BowyerWatson();

        foreach (VirtualTriangle vtrs in Triangles)
        {
            if (!vtrs.ISPointExists(SPoints))
            {
                MakeTriangle(vtrs.Vertices[0], vtrs.Vertices[1], vtrs.Vertices[2]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
