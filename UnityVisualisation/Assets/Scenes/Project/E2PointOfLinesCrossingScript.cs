using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class E2PointOfLinesCrossingScript : MonoBehaviour
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

    Line line1;
    Line line2;

    // Start is called before the first frame update
    void Start()
    {
        line1 = MakeLine(MakeRandomPoint(), MakeRandomPoint());
        line2 = MakeLine(MakeRandomPoint(), MakeRandomPoint());
    }

    // Update is called once per frame
    void Update()
    {
        print(line1.crossingPointCramer(line2));
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "Powrót"))
            SceneManager.LoadScene("ProjectGUI");
    }
}
