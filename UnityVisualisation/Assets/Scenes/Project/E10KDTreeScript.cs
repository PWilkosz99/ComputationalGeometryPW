using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class E10KDTreeScript : MonoBehaviour
{
    public GameObject Prefab;
    public Material LineMaterial;

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
        resp.PassArgs(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Prefab);
        return resp;
    }
    private Line MakeLine(Point2D one, Point2D two)
    {
        var resp = gameObject.AddComponent<Line>();
        resp.PassArgs(one, two, LineMaterial);
        return resp;
    }
    private Line MakeDirectLine(Point2D one, Point2D two)
    {
        var resp = gameObject.AddComponent<DirectLine>();
        resp.PassArgs(one, two, LineMaterial);
        return resp;
    }
    private Triangle MakeTriangle(Point2D a, Point2D b, Point2D c)
    {
        var resp = gameObject.AddComponent<Triangle>();
        resp.PassArgs(a, b, c, LineMaterial);
        return resp;
    }
    private Circle MakeCircle(Vector2 v, float R)
    {
        var resp = gameObject.AddComponent<Circle>();
        resp.PassArgs(v, R, LineMaterial, Prefab);
        return resp;
    }
    private Polygon MakePolygon(ArrayList arr)
    {
        var resp = gameObject.AddComponent<Polygon>();
        resp.PassArgs(arr, LineMaterial);
        return resp;
    }

    #endregion

    Point2D p;
    Point2D tmp;
    Line l;
    KDTree kdt;

    // Start is called before the first frame update
    void Start()
    {
        int numpoints = 20;

        kdt = new KDTree(numpoints);

        for (int i = 0; i < numpoints; i++)
        {
            tmp = MakeRandomPoint();
            kdt.Add(tmp);
        }

        p = MakePoint(1, 1);
        l = MakeLine(p, tmp);
    }

    // Update is called once per frame
    void Update()
    {
        print(kdt.FindNearestPoint(p).x);
        l.head = kdt.FindNearestPoint(p).x;
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "Powrót"))
            SceneManager.LoadScene("ProjectGUI");
    }
}
