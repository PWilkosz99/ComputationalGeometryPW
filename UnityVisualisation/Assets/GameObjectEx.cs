using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectEx
{
    public static Point2D MakePoint(this GameObject gameObject, float x, float y, GameObject Prefab)
    {
        var resp = gameObject.AddComponent<Point2D>();
        resp.PassArgs(x, y, Prefab);
        return resp;
    }

    public static Point2D MakeRandomPoint(this GameObject gameObject, GameObject Prefab)
    {
        var resp = gameObject.AddComponent<Point2D>();
        resp.PassArgs(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Prefab);
        return resp;
    }

    public static Line MakeLine(this GameObject gameObject, Point2D one, Point2D two, Material LineMaterial)
    {
        var resp = gameObject.AddComponent<Line>();
        resp.PassArgs(one, two, LineMaterial);
        return resp;
    }

    public static Line MakeDirectLine(this GameObject gameObject, Point2D one, Point2D two, Material LineMaterial)
    {
        var resp = gameObject.AddComponent<DirectLine>();
        resp.PassArgs(one, two, LineMaterial);
        return resp;
    }

    public static Triangle MakeTriangle(this GameObject gameObject, Point2D a, Point2D b, Point2D c, Material LineMaterial)
    {
        var resp = gameObject.AddComponent<Triangle>();
        resp.PassArgs(a, b, c, LineMaterial);
        return resp;
    }

    public static Polygon MakePolygon(this GameObject gameObject, ArrayList arr, Material LineMaterial)
    {
        var resp = gameObject.AddComponent<Polygon>();
        resp.PassArgs(arr, LineMaterial);
        return resp;
    }

    public static Circle MakeCircle(this GameObject gameObject, Vector2 v, float R, GameObject Prefab, Material LineMaterial)
    {
        var resp = gameObject.AddComponent<Circle>();
        resp.PassArgs(v, R, LineMaterial, Prefab);
        return resp;
    }
}