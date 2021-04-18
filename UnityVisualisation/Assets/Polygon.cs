using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour
{
    public List<Line> LineList = new List<Line>();

    public void PassArgs(ArrayList arr, Material LineMaterial)
    {

        Point2D lastpoint = null;
        Line tmp = new Line();
        int i = 0;
        foreach(Point2D p in arr)
        {
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

    public bool IsInside(Line p)
    {
        int count = 0;
        foreach(Line l in LineList)
        {
            var hm = p.crossingPointCramer(l);
            if(!hm.notExist) // when it cross
            {
                var hmm = new Vector2((float)hm.X, (float)hm.Y);
                if (p.LineBelongig(hmm))
                {
                    if (l.LineBelongig(hmm))
                    {
                        count++; // if it belong
                        print(l +  " " + hmm);
                    }
                }
            }
        }
        print(count);
        return (count % 2 == 1);
    }
}
