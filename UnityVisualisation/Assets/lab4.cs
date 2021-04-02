using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lab4 : MonoBehaviour
{
    public GameObject myPrefab;
    public Material LineMaterial;


    GameObject GameObject;
    public Sprite sprite;
    // Start is called before the first frame update

    List<GameObject> edges = new List<GameObject>();
    List<LineRenderer> lines = new List<LineRenderer>();


    private Point2D MakePoint(float x, float y)
    {
        var resp = gameObject.AddComponent<Point2D>();
        resp.PassArgs(x, y, myPrefab);
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
        Debug.Log("Started");


        //1
        var point12 = MakePoint(1, 2);

        var point910 = MakePoint(9, 10);

        var line = MakeLine(point12, point910);


        var point31 = MakePoint(3, 1);
        var point34 = MakePoint(3, 4.05f);


        print("Punkt: " + point31 + "Na lini: " + line.lineEquationGeneralPrint() + "\nZnajuje siê po stronie: " + line.whichSide(point31));
        print("Punkt: " + point34 + "Na lini: " + line.lineEquationGeneralPrint() + "\nZnajuje siê po stronie: " + line.whichSide(point34));


        // 2

        var point21 = MakePoint(2, 1);
        var point42 = MakePoint(4, 2);
        Line line2 = MakeLine(point21, point42);

        Debug.Log("\n\nPunkt przeciecia\nLinia1: " + line.lineEquationGeneralPrint() + "\nLinia2: " + line2.lineEquationGeneralPrint());
        Debug.Log(line.crossingPointCramer(line2));

        var point11 = MakePoint(1, 1);
        var point22 = MakePoint(2, 2);
        Line line3 = MakeLine(point11, point22);

        Debug.Log("\n\nPunkt przeciecia\nLinia1: " + line.lineEquationGeneralPrint() + "\nLinia3: " + line3.lineEquationGeneralPrint());
        Debug.Log(line.crossingPointCramer(line3));

        //3




        var trojkat = MakeTriangle(MakePoint(1, 0), MakePoint(1, 2), MakePoint(3, 0));
        Debug.Log(trojkat);
        Debug.Log("Pole: " + trojkat.fieldMatrix());

        var trojkoat2 = MakeTriangle(MakePoint(-3, -2), MakePoint(3, 1), MakePoint(-2, 3));
        Debug.Log(trojkoat2);
        Debug.Log("Pole: " + trojkoat2.fieldMatrix());

    }
}
