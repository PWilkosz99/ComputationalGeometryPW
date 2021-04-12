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

    public bool IsInside(Line p)
    {
        int count = 0;
        foreach(Line l in LineList)
        {
            var hm = p.crossingPointCramer(l);
            if(!hm.notExist) // jak przecina
            {
                var hmm = new Vector2((float)hm.X, (float)hm.Y);
                if (p.LineBelongig(hmm))
                {
                    //   count++; // czy pkt nalezy do odcinka
                    if (l.LineBelongig(hmm))
                    {
                        count++; // czy pkt nalezy do odcinka

                        print(l +  " " + hmm);

                    }
                }
            }
        }
        print(count);
        return (count % 2 == 1) ? true : false;
    }
}
