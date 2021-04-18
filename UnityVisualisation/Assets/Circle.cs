using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    Point2D center;
    private GameObject ThisGameObject;
    public Material Material;

    float R;
    LineRenderer line;

    int segments = 360;
    double error = 0.5;


    public void PassArgs(Vector2 Center,float r, Material LineMaterial, GameObject PointPrefab)
    {
        R = r;
        Material = LineMaterial;
        ThisGameObject = new GameObject();

        center = gameObject.AddComponent<Point2D>();
        center.PassArgs(Center, PointPrefab);


        line = ThisGameObject.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = .2f;
        line.endWidth = .2f;
        line.positionCount = segments+1;
        line.material = Material;

        for (int i = 0; i <= segments; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            line.SetPosition(i, new Vector2( center.X + Mathf.Sin(rad) * R, center.Y+  Mathf.Cos(rad) * R));
        }

        ThisGameObject.name = ToString();
    }

    public override string ToString()
    {
        return "Cicrle r=" + R + " " + center;
    }

    void Update()
    {
        int i = 0;
        var rad = Mathf.Deg2Rad * (i * 360f / segments);
        var point =  new Vector2(center.X + Mathf.Sin(rad) * R, center.Y + Mathf.Cos(rad) * R);
        if ( (Vector2) line.GetPosition(0) !=   point) //when is the change
        {
            for (; i <= segments; i++)
            {
                rad = Mathf.Deg2Rad * (i * 360f / segments);
                line.SetPosition(i, new Vector2(center.X + Mathf.Sin(rad) * R, center.Y + Mathf.Cos(rad) * R));
            }
            ThisGameObject.name = ToString();
        }
    }

    public int LineTouch(Line l)
    {
        float dx = l.tail.X - l.head.X;
        float dy = l.tail.Y - l.head.Y;
        float t;
        float A = dx * dx + dy * dy;
        float B = 2 * (dx * (l.A.X - center.X) + dy * (l.A.Y - center.Y));
        float C = (l.A.X - center.X) * (l.A.X - center.X) + (l.A.Y - center.Y) * (l.A.Y - center.Y) - R * R;

        float det = B * B - 4 * A * C;

        if ((A <= error) || (det < 0))
        {
            return 0;
        }
        else if (det == 0)
        {
            t = -B / (2 * A);
            float X0 = l.A.X + t * dx;
            float Y0 = l.A.Y + t * dy;
            print(X0 + ":" + Y0);
            return 1;
        }
        else
        {
            t = (float)((-B + Mathf.Sqrt(det)) / (2 * A));
            float X1 = l.A.X + t * dx;
            float Y1 = l.A.Y + t * dy;
            t = (float)((-B - Mathf.Sqrt(det)) / (2 * A));
            float X2 = l.A.X + t * dx;
            float Y2 = l.A.Y + t * dy;
            print(X1 + ":" + Y1 + " -- " + X2 + ":" + Y2);
            return 2;
        }
    }
}