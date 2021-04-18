using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab3_4 : MonoBehaviour
{
    public GameObject Prefab;
    public Material LineMaterial;

    //A set of universal methods used to create objects 
    private Point2D MakePoint(float x, float y)
    {
        var resp = gameObject.AddComponent<Point2D>();
        resp.PassArgs(x, y, Prefab);
        return resp;
    }

    private Line MakeDirectLine(Point2D one, Point2D two)
    {
        var resp = gameObject.AddComponent<DirectLine>();
        resp.PassArgs(one, two, LineMaterial);
        return resp;
    }

    Circle circle;
    Line line;

    private void Start()
    {
        circle = gameObject.AddComponent<Circle>();
        circle.PassArgs(Vector2.zero, 5,  LineMaterial, Prefab);

        Point2D A = MakePoint(-1, 0);
        Point2D B = MakePoint(1, 0);


        line = MakeDirectLine(A, B);

        print(circle.LineTouch(line));

    }

    private short count = 0;

    private void Update()
    {
        count++;
        if(count % 30 == 29)
        {
            print(circle.LineTouch(line));

        }
    }

}
