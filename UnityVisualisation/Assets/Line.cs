
using System;
using System.Collections.Generic;
using UnityEngine;

public class Line :MonoBehaviour
{
    protected LineRenderer LineRendererObj;
    double error = 0.5;

    public enum EquationABC {
        A,
        B,
        C,    
    }

    public enum EquationAB {
        A,
        B,
    }

    public void PassArgs(Point2D head, Point2D tail, Material material)
    {
        this.head = head;
        this.tail = tail;
        Material = material;
        MakeLine(head, tail, material);
    }


    public Line Clone(GameObject GameObjectParm)
    {
        var resp = GameObjectParm.AddComponent<Line>();        
        resp.PassArgs(head.Clone(GameObjectParm), tail.Clone(GameObjectParm), Material);
        return resp;
    }


    public Point2D head;
    public Point2D tail;
    public Material Material;

    public Dictionary<EquationAB, Double> lineEquation() {
        var ans = new Dictionary<EquationAB, Double>();
        double a = (head.Y - tail.Y) / (head.X - tail.X);
        ans.Add(EquationAB.A, a);
        ans.Add(EquationAB.B, head.Y - a * head.X);
        return ans;
    }

    public override string ToString()
    {
        return "Odcinek " + tail + " -> " + head;
    }



    public void lineBelongig(Point2D p)
    {
        double a = (head.Y - tail.Y) / (head.X - tail.X);
        double b = head.Y - a * head.X;
        double ey = a * p.X + b;
        if ((p.Y - ey) < error)
        {
            Debug.LogWarning("Punkt leży na prostej");
            if (p.X >= (Math.Min(head.X, tail.X) - error) && p.X <= (Math.Max(head.X, tail.X) + error)
                    && p.Y >= (Math.Min(head.Y, tail.Y) - error) && p.Y <= (Math.Max(head.Y, tail.Y) + error))
            {
                Debug.LogWarning("Punkt leży na odnciku");
            }
        }
    }



    public void translation(Vector2 v)
    {
        head.X += v.x;
        tail.X += v.x;
        head.Y += v.y;
        tail.Y += v.y;
    }

    public enum Side
    {
        Left,
        Right,
        On,
    }
    

    // Ax+Bx+C
    public Dictionary<EquationABC, Double> lineEquationGeneral () {
        var ab = lineEquation();  
        var res = new Dictionary<EquationABC, Double>();

        res.Add(EquationABC.A, ab[EquationAB.A] *-1); // -A
        res.Add(EquationABC.C, ab[EquationAB.B] *-1); // -C (było b);
        res.Add(EquationABC.B, 1.0);  // 1b -> y= ...
        return res;
    }

    //public Side whichSide(Point2D point, int ttt)
    //{
    //    var ab =lineEquationGeneral();
    //    var res = ab[EquationABC.A]*point.X + ab[EquationABC.B]*point.Y+ ab[EquationABC.C];

    //    if(res < error )
    //    {
    //        return Side.Left;
    //    }
    //    else if (res > error)
    //    {
    //        return Side.Right;
    //    }
    //    else
    //    {
    //        return Side.On;
    //    }

    //}

    //public Side whichSide(Point2D p3)
    //{
    //    double A = (head.Y - tail.Y) / (head.X - tail.X);
    //    //double B = head.Y - (A * head.X);
    //    // double res = eq.A * p3.x + eq.B * p3.y + eq.C;
    //    double B = 1.0;//mam tylko A i B -> C
    //    double C = 0.0;
    //    double res = -1 * A * p3.X + B * p3.Y - C;
    //    if (res < -0.001)
    //    {
    //        return Side.Left;
    //        //System.out.println("Punkt z „lewej” strony prostej");
    //    }
    //    else if (res > 0.001)
    //    {
    //        return Side.Right;
    //        //System.out.println("Punkt z prawej strony prostej");
    //    }
    //    else
    //    {
    //        return Side.On;
    //        //System.out.println("Punkt na prostej");
    //    }
    //}

    //public void LineEquation()
    //{
    //    double A = (head.Y - tail.Y) / (head.X - tail.X);
    //    double B = head.Y - (A * head.X);
    //    double C = 0;
    //}

    public Side whichSide(Point2D p)
    {
        //d = (x - x1)(y2 - y1) - (y - y1)(x2 - x1)
        double d = (p.X - head.X) * (tail.Y - head.Y) - (p.Y - head.Y) * (tail.X - head.X);
        if (d < -error)
        {
            return Side.Right;
        }else if (d>error)
        {
            return Side.Left;
        }
        else
        {
            return Side.On;
        }
    }

#nullable enable

    public XY crossingPointCramer(Line line2)
    {

        var lcl = this.lineEquationGeneral();
        var parm = line2.lineEquationGeneral();


        var result = Matrix.crossingPointCramer(lcl[EquationABC.A], parm[EquationABC.A], lcl[EquationABC.B], parm[EquationABC.B], lcl[EquationABC.C], parm[EquationABC.C]);

        if (result.notExist)
        {
            //MonoBehaviour - This class doesn't support the null-conditional operator (?.) and the null-coalescing operator (??).

            Debug.Log("Proste równoległe");
            result = new XY() { X = double.NaN, Y = double.NaN };
        }

        return result;
    }


    public String lineEquationGeneralPrint()
    {
        var linclc = lineEquationGeneral();
        return linclc[EquationABC.A] + "x + " + linclc[EquationABC.B] + "y + " + linclc[EquationABC.C];
    }

    protected virtual void MakeLine(Point2D start, Point2D stop,Material material)
    {
        var go = new GameObject();
        LineRendererObj = go.AddComponent<LineRenderer>();
        LineRendererObj.name = ToString();
        LineRendererObj.startWidth = .01f;
        LineRendererObj.endWidth = .10f;
        LineRendererObj.SetPosition(0, start.Position);
        LineRendererObj.SetPosition(1, stop.Position);
        LineRendererObj.material = material;
    }

    public double Solve(double x)
    {
        var fun = lineEquation();

        return fun[EquationAB.A] * x + fun[EquationAB.B];

    }

    private void Update()
    {
        if((Vector2)LineRendererObj.GetPosition(0) != head.Position)
        {
            LineRendererObj.SetPosition(0, head.Position);
            LineRendererObj.name = ToString();
        }

        if ((Vector2)LineRendererObj.GetPosition(1) != tail.Position)
        {
            LineRendererObj.SetPosition(1, tail.Position);
            LineRendererObj.name = ToString();
        }
    }

    //public static double Length(Point2D A, Point2D B)
    //{
    //    return Math.Sqrt(Math.Pow((B.X - A.X), 2) + Math.Pow((B.Y - A.Y), 2));
    //}

    public double Length()
    {
        return Math.Sqrt((Math.Pow((tail.X - head.X), 2) + Math.Pow((tail.Y - tail.X), 2)));
    }
}