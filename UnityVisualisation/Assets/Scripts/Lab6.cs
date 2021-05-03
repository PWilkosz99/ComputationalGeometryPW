using System;
using System.Collections;
using UnityEngine;
using Stopwatch = System.Diagnostics.Stopwatch;

public class Lab6 : MonoBehaviour
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
    QuadTree QT;
    public GameObject Prefab;
    public Material LineMaterial;

    Vector3 pos;

    void Start()
    {
        pos = Vector3.zero;

        QuadTree.gameObject = gameObject;
        QuadTree.LineMaterial = LineMaterial;
        QuadTree.Prefab = Prefab;

        var boundary = new Boundary(Vector2.zero, 22, 12);
        int capacity = 4;
        QT = new QuadTree(boundary, capacity, new Point2D[] { });
    }

    void Update()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        if (Input.GetMouseButtonDown(0))
        {
            Point2D p = gameObject.MakePoint(pos.x, pos.y, Prefab);
            QT.Insert(p);
        }
    }
}
