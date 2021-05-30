using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class DelaunayTriangulator
{
    public GameObject Prefab;
    List<Point2D> ListOfPoints;
    List<VirtualTriangle> Selvage;

    public DelaunayTriangulator(GameObject Prefab, List<Point2D> SP, List<Point2D> ListOfPoints)
    {
        this.Prefab = Prefab;
        this.ListOfPoints = ListOfPoints;
        Selvage = new List<VirtualTriangle>() { new VirtualTriangle(SP[0], SP[1], SP[2], Prefab)};
    }

    private List<Edge> FindHoleBoundaries(ISet<VirtualTriangle> badTriangles)
    {
        List<Edge> edges = new List<Edge>();
        foreach (VirtualTriangle Triangle in badTriangles)
        {
            edges.Add(new Edge(Triangle.Vertices[0], Triangle.Vertices[1]));
            edges.Add(new Edge(Triangle.Vertices[1], Triangle.Vertices[2]));
            edges.Add(new Edge(Triangle.Vertices[2], Triangle.Vertices[0]));
        }
        return new List<Edge>(edges.GroupBy(o => o).Where(o => o.Count() == 1).Select(o => o.First()));
    }

    private HashSet<VirtualTriangle> FindBadTriangles(Point2D Point2D, HashSet<VirtualTriangle> Triangles)
    {
        return new HashSet<VirtualTriangle>(Triangles.Where(o => o.IsPoint2DInsideCircumcircle(Point2D)));
    }

    public HashSet<VirtualTriangle> BowyerWatson()
    {
        HashSet<VirtualTriangle> res = new HashSet<VirtualTriangle>(Selvage);

        foreach (var p in ListOfPoints)
        {
            var badTriangles = FindBadTriangles(p, res);
            var polygon = FindHoleBoundaries(badTriangles);

            foreach (var tr in badTriangles)
            {
                foreach (var v in tr.Vertices)
                {
                    v.AdjacentTriangles.Remove(tr);
                }
            }
            res.RemoveWhere(o => badTriangles.Contains(o));

            foreach (var ed in polygon.Where(possibleEdge => possibleEdge.Point1 != p && possibleEdge.Point2 != p))
            {
                var Triangle = new VirtualTriangle(p, ed.Point1, ed.Point2, Prefab);
                res.Add(Triangle);
            }
        }
        return res;
    }
}
