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
    double error = 0.1;


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
        return "Okr�g r=" + R + " " + center;
    }

    void Update()
    {
        int i = 0;
        var rad = Mathf.Deg2Rad * (i * 360f / segments);
        var point =  new Vector2(center.X + Mathf.Sin(rad) * R, center.Y + Mathf.Cos(rad) * R);
        if ( (Vector2) line.GetPosition(0) !=   point) // gdy w obiekcie punkt nastapi�a zmiana
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

        float a = Mathf.Pow(l.head.X, 2) + Mathf.Pow(l.head.Y, 2) + Mathf.Pow(l.tail.X, 2) + Mathf.Pow(l.tail.Y, 2)
            - 2f * (l.head.X * l.tail.X + l.head.X * l.tail.Y);

        float b = 2f * ( center.X * (l.tail.X - l.head.X) + center.Y * (l.tail.Y - l.head.Y) +
            l.head.X * l.tail.X + l.head.Y * l.tail.Y - Mathf.Pow(l.tail.X, 2) - Mathf.Pow(l.tail.Y, 2));

        float c = -1f * Mathf.Pow(R, 2) + Mathf.Pow(l.tail.X, 2) + Mathf.Pow(l.tail.Y, 2) + Mathf.Pow(center.X, 2) + Mathf.Pow(center.Y, 2) -
            2f * (center.X * l.tail.X + center.Y * l.tail.Y);




        float delta = Mathf.Pow(b, 2) - 4 * a * c;

        int respone = -1;

        if (Mathf.Abs(delta) < error)
        {
            respone = 1;

            float X0 = (-1f*b-Mathf.Sqrt(delta))/(2f*a);

        }
        else if (delta < error)
        {
            respone = 0;
        }
        else
        {
            respone = 2;
            float X1 = (-1f * b + Mathf.Sqrt(delta)) / (2f * a);
            float X2 = (-1f * b - Mathf.Sqrt(delta)) / (2f * a);

            float Y1 = (float)l.Solve((double)X1);
            float Y2 = (float)l.Solve((double)X2);
            print(X1 + ":" + Y1 + " -- " + X2 + ":" + Y2);
        }
        return respone;
    }
}