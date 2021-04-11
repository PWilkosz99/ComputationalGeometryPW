using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main3_3 : MonoBehaviour
{
    public GameObject Prefab;
    public Material LineMaterial;

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

    Point2D p;
    Polygon pol;

    // Start is called before the first frame update
    void Start()
    {
        ArrayList a = new ArrayList();
        a.Add(MakeRandomPoint());
        a.Add(MakeRandomPoint());
        a.Add(MakeRandomPoint());
        a.Add(MakeRandomPoint());
        a.Add(MakeRandomPoint());
        a.Add(MakeRandomPoint());
        pol = MakePolygon(a);
        p = MakeRandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
        print(pol.IsInside(p));
    }
}
