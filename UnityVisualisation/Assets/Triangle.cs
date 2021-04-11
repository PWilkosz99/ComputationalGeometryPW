using System;
using System.Collections.Generic;
using UnityEngine;

public class Triangle: MonoBehaviour {
  //  public Line A, B, C;

    public Line A => points[0];
    public Line B => points[1];
    public Line C => points[2];


    private List<Line> points = new List<Line>();

    public void PassArgs(Point2D a, Point2D b, Point2D c, Material LineMaterial)
    {

        points.Add(gameObject.AddComponent<Line>());
        points.Add(gameObject.AddComponent<Line>());
        points.Add(gameObject.AddComponent<Line>());


        points[0].PassArgs(a, b, LineMaterial);
        points[1].PassArgs(b, c, LineMaterial);
        points[2].PassArgs(c, a, LineMaterial);
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
        //bool status = true;

        //foreach (var item in points)
        //{
        //    if(item.whichSide(p) != Line.Side.Left)
        //    {
        //        status = false;
        //    }
        //}
        //return status;

        Line.Side firstpointside = points[0].whichSide(p);

        if(!(points[1].whichSide(p) == firstpointside))
        {
            return false;
        }
        else if(!(points[2].whichSide(p) == firstpointside))
        {
            return false;
        }
        return true;
    }
}
