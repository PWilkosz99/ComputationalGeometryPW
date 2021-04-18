
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    protected LineRenderer LineRendererObj;
    double error = 0.5; //precision to assess distance from the line 

    public Point2D A => tail;
    public Point2D B => head;


    public enum EquationABC
    {
        A,
        B,
        C,
    }

    public enum EquationAB
    {
        A,
        B,
    }


    //Method witch i use for passing arngs to which let me pass proper args to make unity's line
    public void PassArgs(Point2D tail, Point2D head, Material material)
    {
        this.head = head;
        this.tail = tail;
        Material = material;
        MakeLine(head, tail, material);
    }


    public Line Clone(GameObject GameObjectParm)
    {
        var resp = GameObjectParm.AddComponent<Line>();
        resp.PassArgs(tail.Clone(GameObjectParm), head.Clone(GameObjectParm), Material);
        return resp;
    }


    public Point2D head;
    public Point2D tail;
    public Material Material;

    public Dictionary<EquationAB, Double> lineEquation()
    {
        var ans = new Dictionary<EquationAB, Double>();
        if (A.X == B.X)
        {
            throw new LineIsNotFunctionException(this);
        }
        double a = (A.Y - B.Y) / (A.X - B.X);
        ans.Add(EquationAB.A, a);
        ans.Add(EquationAB.B, A.Y - a * A.X);
        return ans;
    }

    public override string ToString()
    {
        return "Odcinek " + tail + " -> " + head;
    }

    public bool DirectLineBelongig(Point2D p) => DirectLineBelongig(p.Position);
    public bool LineBelongig(Point2D p) => LineBelongig(p.Position);


    public bool DirectLineBelongig(Vector2 p)
    {
        double a = (head.Y - tail.Y) / (head.X - tail.X);
        double b = head.Y - a * head.X;
        double ey = a * p.x + b;
        if ((p.y - ey) < error)
        {
            return true;
        }
        return false;
    }

    public bool LineBelongig(Vector2 p)
    {
        if (DirectLineBelongig(p))
        {
            if (p.y >= (Math.Min(head.X, tail.X) - error) && p.x <= (Math.Max(head.X, tail.X) + error)
                                && p.y >= (Math.Min(head.Y, tail.Y) - error) && p.y <= (Math.Max(head.Y, tail.Y) + error))
            {
                //if true it's on line
                return true;
            }
        }
        return false;
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
    public Dictionary<EquationABC, Double> lineEquationGeneral()
    {
        var ab = lineEquation();
        var res = new Dictionary<EquationABC, Double>();

        res.Add(EquationABC.A, ab[EquationAB.A] * -1); // -A
        res.Add(EquationABC.C, ab[EquationAB.B] * -1); // -C (było b);
        res.Add(EquationABC.B, 1.0);  // 1b -> y= ...
        return res;
    }

    public Side whichSide2(Point2D point)
    {
        throw new Exception("Not working");
        var ab = lineEquationGeneral();
        double res = ab[EquationABC.A] * point.X + ab[EquationABC.B] * point.Y + ab[EquationABC.C];

        if (error > Math.Abs(res))
        {
            return Side.On;
        }
        else if (res > error)
        {
            return Side.Left;
        }
        else
        {
            return Side.Right;
        }

    }

    public Side whichSide(Point2D p)
    {
        //d = (x - x1)(y2 - y1) - (y - y1)(x2 - x1)
        double d = (p.X - head.X) * (tail.Y - head.Y) - (p.Y - head.Y) * (tail.X - head.X);
        if (d < -error)
        {
            return Side.Right;
        }
        else if (d > error)
        {
            return Side.Left;
        }
        else
        {
            return Side.On;
        }
    }

    public XY crossingPointCramer(Line line2)
    {

        var lcl = this.lineEquationGeneral();
        var parm = line2.lineEquationGeneral();


        var result = Matrix.crossingPointCramer(lcl[EquationABC.A], parm[EquationABC.A], lcl[EquationABC.B], parm[EquationABC.B], lcl[EquationABC.C], parm[EquationABC.C]);

        if (result.notExist)
        {
            //MonoBehaviour - This class doesn't support the null-conditional operator (?.) and the null-coalescing operator (??).

            Debug.Log("Proste równoległe");
            result = new XY() { X = double.NaN, Y = double.NaN, notExist = true };
        }

        return result;
    }


    public String lineEquationGeneralPrint()
    {
        var linclc = lineEquationGeneral();
        return linclc[EquationABC.A] + "x + " + linclc[EquationABC.B] + "y + " + linclc[EquationABC.C];
    }

    protected virtual void MakeLine(Point2D start, Point2D stop, Material material)
    {
        var go = new GameObject();
        LineRendererObj = go.AddComponent<LineRenderer>();
        LineRendererObj.name = ToString();
        LineRendererObj.startWidth = .10f;
        LineRendererObj.endWidth = .7f;
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
        if (LineRendererObj)
        {
            if ((Vector2)LineRendererObj.GetPosition(0) != head.Position)
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
    }

    public double Length()
    {
        return Math.Sqrt((Math.Pow((tail.X - head.X), 2) + Math.Pow((tail.Y - tail.X), 2)));
    }
}