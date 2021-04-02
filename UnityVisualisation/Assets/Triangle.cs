using System;
using UnityEngine;

public class Triangle: MonoBehaviour {
    public Line A, B, C;


    public void PassArgs(Point2D a, Point2D b, Point2D c, Material LineMaterial)
    {
        //this.a = a;
        //this.b = b;
        //this.c = c;


        A = gameObject.AddComponent<Line>();
        B = gameObject.AddComponent<Line>();
        C = gameObject.AddComponent<Line>();

        A.PassArgs(a, b, LineMaterial);
        B.PassArgs(b, c, LineMaterial);
        C.PassArgs(c, a, LineMaterial);


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
        bool status = true;
        if (A.whichSide(p) == Line.Side.Left)
        {
            status = false;
        }        
        if (B.whichSide(p) == Line.Side.Left)
        {
            status = false;
        }
        if (C.whichSide(p) == Line.Side.Left)
        {
            status = false;
        }
        return status;
    }
}
