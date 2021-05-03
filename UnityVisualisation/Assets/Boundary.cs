using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boundary
{
    public Vector2 center;
    public float centerY => center.y;
    public float centerX => center.x;

    public float width;
    public float height;

    public Boundary(float x, float y, float w, float h)
    {
        center = new Vector2(x, y);
        width = w;
        height = h;
    }

    public Boundary(Vector2 Center, float w, float h)
    {
        center = Center;
        width = w;
        height = h;
    }

    public bool Contains(Point2D point)
    {
        bool contains = (point.X > centerX - width && point.X < centerX + width && point.Y > centerY - height && point.Y < centerY + height);
        return contains;
    }

    public List<Line> DrawCross(GameObject gameObject, GameObject Prefab, Material LineMaterial, bool ShowPoints = true)
    {
        var resp = new List<Line>();
        var square = new Point2D[]
        {
            gameObject.MakePoint(centerX + width, centerY, Prefab),
            gameObject.MakePoint(centerX - width, centerY, Prefab),
            gameObject.MakePoint(centerX, centerY + height, Prefab),
            gameObject.MakePoint(centerX, centerY - height, Prefab),
        };
        resp.Add(gameObject.MakeLine(square[0], square[1], LineMaterial));
        resp.Add(gameObject.MakeLine(square[2], square[3], LineMaterial));

        if (ShowPoints.Equals(false))
        {
            foreach (Line l in resp)
            {
                l.SetActivePoints(ShowPoints);
            }
        }
        return resp;
    }
}

