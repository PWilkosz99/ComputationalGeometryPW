using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main3_2 : MonoBehaviour
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
        resp.PassArgs(Random.Range(-10f,10f), Random.Range(-10f, 10f), Prefab);
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

    Point2D p;
    Triangle tr;

    // Start is called before the first frame update
    void Start()
    {
        tr = MakeTriangle(MakeRandomPoint(), MakeRandomPoint(), MakeRandomPoint());
        p = MakeRandomPoint();


    }

    // Update is called once per frame
    void Update()
    {
        //print(tr.A.Length());
        //print(tr.B.Length());
        //print(tr.C.Length());
        //print(tr.ComputeArea());
        print("A " + tr.A.whichSide(p));
        print("B " + tr.B.whichSide(p));
        print("C " + tr.C.whichSide(p));
        print(tr.IsInside(p));
        //print(tr.pDebug());
    }
}
