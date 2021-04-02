using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main3_4 : MonoBehaviour
{
    public GameObject Prefab;
    public Material LineMaterial;

    private Point2D MakePoint(float x, float y)
    {
        var resp = gameObject.AddComponent<Point2D>();
        resp.PassArgs(x, y, Prefab);
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
    Circle a;
    Line l;
    private void Start()
    {
        a = gameObject.AddComponent<Circle>();
        a.PassArgs(Vector2.zero, 5,  LineMaterial, Prefab);

        //var A = MakePoint(0, 0);
        //var B = MakePoint(4, 5);

        var A = MakePoint(-1, 0);
        var B = MakePoint(1, 0);

        l = MakeDirectLine(A, B);

        print(a.LineTouch(l));

    }
    int count = 0;
    private void Update()
    {
        count++;
        if(count % 30 == 29)
        {
            print(a.LineTouch(l));

        }
    }

}
