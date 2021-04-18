using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab3_1 : MonoBehaviour
{
    public GameObject Prefab;
    public Material LineMaterial;
    private Point2D probe;
    private Line ab;

    //A set of universal methods used to create objects 
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
    private Triangle MakeTriangle(Point2D a, Point2D b, Point2D c)
    {
        var resp = gameObject.AddComponent<Triangle>();
        resp.PassArgs(a, b, c, LineMaterial);
        return resp;
    }

    void Start()
    {
        var a = MakePoint(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        var b = MakePoint(Random.Range(-10f, 10f), Random.Range(-10f, 10f));

        ab = MakeLine(a, b);

        probe = MakePoint(Random.Range(-10f, 10f), Random.Range(-10f, 10f));

        print(probe + "\nNa lini: " + ab.lineEquationGeneralPrint() + "\nZnajduje siê po stronie: " + ab.whichSide(probe));

    }
    private void Update()
    {
        if (ab)
        {
            var a = ab.lineEquation();
            print(a[Line.EquationAB.A] + " - " + a[Line.EquationAB.B]);
            print(ab.whichSide(probe));
        }
    }

}
