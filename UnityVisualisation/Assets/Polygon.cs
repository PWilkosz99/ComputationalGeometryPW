using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour
{
    public List<Line> LineList = new List<Line>();

    public void PassArgs(ArrayList arr, Material LineMaterial)
    {
        //A = gameObject.AddComponent<Line>();
        //B = gameObject.AddComponent<Line>();
        //C = gameObject.AddComponent<Line>();

        //A.PassArgs(a, b, LineMaterial);
        //B.PassArgs(b, c, LineMaterial);
        //C.PassArgs(c, a, LineMaterial);
        Point2D lastpoint = null;
        Line tmp = new Line();
        int i = 0;
        foreach(Point2D p in arr)
        {
            //LineList.Add(p);
            tmp = gameObject.AddComponent<Line>();
            if (i != 0)
            {
                tmp.PassArgs(lastpoint, p, LineMaterial);
                LineList.Add(tmp);
            }
            lastpoint = p;
            i++;
        }
        tmp = gameObject.AddComponent<Line>();
        tmp.PassArgs(lastpoint, LineList[0].tail, LineMaterial);
        LineList.Add(tmp);
    }

    public bool IsInside(Point2D p)
    {
        foreach(Line l in LineList)
        {
            if(l.whichSide(p) != Line.Side.Right)
            {
                return false;
            }
        }
        return true;
    }
}
