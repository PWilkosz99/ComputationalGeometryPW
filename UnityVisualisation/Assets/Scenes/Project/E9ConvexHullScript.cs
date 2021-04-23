using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E9ConvexHullScript : MonoBehaviour
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

    ConvexHull Hull;
    List<Point2D> Points = new List<Point2D>();
    Point2D ReferencePoint;

    private Point2D FindMinPoint(List<Point2D> PointsList)
    {
        float MinValue = float.PositiveInfinity;
        Point2D MinPoint = PointsList[0];
        for (int i = 0; i < PointsList.Count; i++)
        {
            if (MinValue > PointsList[i].Y)
            {
                MinPoint = PointsList[i];
                MinValue = PointsList[i].Y;
            }
            else if (MinValue == PointsList[i].Y)
            {
                if (MinPoint.X > PointsList[i].X)
                {
                    MinPoint = PointsList[i];
                }
            }
        }
        return MinPoint;
    }

    Point2D GetMaxAngle(Point2D p1, Point2D p2)
    {
        float maxangle = float.NegativeInfinity;
        int maxindex = 0;

        for (int i = 0; i < Points.Count; i++)
        {
            float angle = p2.AngleBetweenPoints(p1, Points[i]);
            if (maxangle < angle)
            {
                maxangle = angle;
                maxindex = i;
            }
        }
        return Points[maxindex];
    }

    void Wrap()
    {
        Point2D bottompoint = FindMinPoint(Points);
        ReferencePoint.Y = bottompoint.Y;
        ReferencePoint.X = -100;//inf for scene which i using

        Point2D selected = GetMaxAngle(ReferencePoint, bottompoint);
        Point2D prev = bottompoint;

        int i = 0;
        Hull.AddPoint(bottompoint, i++);
        Hull.AddPoint(selected, i++);

        while (bottompoint != selected)
        {
            Point2D next = GetMaxAngle(prev, selected);
            Hull.AddPoint(next, i++);
            prev = selected;
            selected = next;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        int AmoutOfPoints = Random.Range(50, 75);
        Hull = gameObject.AddComponent<ConvexHull>();
        Hull.PassArgs(LineMaterial);
        for (int i = 0; i < AmoutOfPoints; i++)
        {
            Points.Add(MakeRandomPoint());
        }
        ReferencePoint = MakePoint(-100, -100);//Offstage for safety reason 
    }

    // Update is called once per frame
    void Update()
    {
        Wrap();//It let me dynamically generate outline after change position of point
    }
}

