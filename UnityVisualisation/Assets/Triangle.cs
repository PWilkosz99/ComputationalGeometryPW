using System;
using System.Collections.Generic;
using UnityEngine;

public class Triangle: MonoBehaviour {

    public Line A => lines[0];
    public Line B => lines[1];
    public Line C => lines[2];

    private List<Line> lines = new List<Line>();

    public void PassArgs(Point2D a, Point2D b, Point2D c, Material LineMaterial)
    {

        lines.Add(gameObject.AddComponent<Line>());
        lines.Add(gameObject.AddComponent<Line>());
        lines.Add(gameObject.AddComponent<Line>());


        lines[0].PassArgs(a, b, LineMaterial);
        lines[1].PassArgs(b, c, LineMaterial);
        lines[2].PassArgs(c, a, LineMaterial);
    }

    public override string ToString()
    {
        return "T: " + A + B + C;
    }

    public double fieldMatrix()
    {
        var res  = 0.5 * Math.Abs(Matrix.cross(B.head.X - A.head.X, B.head.Y-A.head.Y, C.head.X-A.head.X, C.head.Y-A.head.Y));
        return res;
    }
    
    public double ComputeArea()
    {
        double p = 0.5 * (A.Length() + B.Length() + C.Length());
        return Math.Sqrt(Math.Abs(p * ((p - A.Length()) * (p - B.Length()) * (p - C.Length()))));
    }
    
    public bool IsInside(Point2D p)
    {
        Line.Side firstpointside = lines[0].whichSide(p);

        if(!(lines[1].whichSide(p) == firstpointside))
        {
            return false;
        }
        else if(!(lines[2].whichSide(p) == firstpointside))
        {
            return false;
        }
        return true;
    }
}
