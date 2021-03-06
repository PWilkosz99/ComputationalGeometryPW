using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class E7CircleWithLineCrossingPointsScript : MonoBehaviour
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

    Circle circle;
    Line line;

    // Start is called before the first frame update
    void Start()
    {
        circle = MakeCircle(Vector2.zero, 8);
        line = MakeDirectLine(MakePoint(-3, 0), MakePoint(3, 0));
    }

    // Update is called once per frame
    void Update()
    {
        print(circle.LineTouch(line));
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "Powr?t"))
            SceneManager.LoadScene("ProjectGUI");
    }
}
