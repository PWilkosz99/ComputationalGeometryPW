using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VirtualTriangle
{
    private GameObject Prefab;

    public Point2D[] Vertices { get; } = new Point2D[3];
    public Point2D Circumcenter { get; private set; }
    public double RadiusSquared;

    public VirtualTriangle(Point2D Point2D1, Point2D Point2D2, Point2D Point2D3, GameObject prefab)
    {
        Prefab = prefab;
        if (Point2D1 == Point2D2 || Point2D1 == Point2D3 || Point2D2 == Point2D3)
        {
            throw new ArgumentException("Must be 3 distinct Point2Ds");
        }

        if (!IsCounterClockwise(Point2D1, Point2D2, Point2D3))
        {
            Vertices[0] = Point2D1;
            Vertices[1] = Point2D3;
            Vertices[2] = Point2D2;
        }
        else
        {
            Vertices[0] = Point2D1;
            Vertices[1] = Point2D2;
            Vertices[2] = Point2D3;
        }

        Vertices[0].AdjacentTriangles.Add(this);
        Vertices[1].AdjacentTriangles.Add(this);
        Vertices[2].AdjacentTriangles.Add(this);
        UpdateCircumcircle();
    }

    public IEnumerable<VirtualTriangle> TrianglesWithSharedEdge
    {
        get
        {
            var neighbors = new HashSet<VirtualTriangle>();
            foreach (var vertex in Vertices)
            {
                var trianglesWithSharedEdge = vertex.AdjacentTriangles.Where(o =>
                {
                    return o != this && SharesEdgeWith(o);
                });
                neighbors.UnionWith(trianglesWithSharedEdge);
            }

            return neighbors;
        }
    }

    private void UpdateCircumcircle()
    {
        var p0 = Vertices[0];
        var p1 = Vertices[1];
        var p2 = Vertices[2];
        var dA = p0.X * p0.X + p0.Y * p0.Y;
        var dB = p1.X * p1.X + p1.Y * p1.Y;
        var dC = p2.X * p2.X + p2.Y * p2.Y;

        var aux1 = (dA * (p2.Y - p1.Y) + dB * (p0.Y - p2.Y) + dC * (p1.Y - p0.Y));
        var aux2 = -(dA * (p2.X - p1.X) + dB * (p0.X - p2.X) + dC * (p1.X - p0.X));
        var div = (2 * (p0.X * (p2.Y - p1.Y) + p1.X * (p0.Y - p2.Y) + p2.X * (p1.Y - p0.Y)));

        if (div == 0)
        {
            throw new DivideByZeroException();
        }

        var center = new Point2D();
        center.PassArgs(new Vector2(aux1 / div, aux2 / div), Prefab);

        center.SetActive(false);
        Circumcenter = center;
        RadiusSquared = (center.X - p0.X) * (center.X - p0.X) + (center.Y - p0.Y) * (center.Y - p0.Y);
    }

    private bool IsCounterClockwise(Point2D P1, Point2D P2, Point2D P3)
    {
        float result = (P2.X - P1.X) * (P3.Y - P1.Y) -(P3.X - P1.X) * (P2.Y - P1.Y);
        return result > 0;
    }

    public bool SharesEdgeWith(VirtualTriangle triangle)
    {
        int sharedVertices = Vertices.Where(o => triangle.Vertices.Contains(o)).Count();
        return sharedVertices == 2;
    }

    public bool IsPoint2DInsideCircumcircle(Point2D Point2D)
    {
        var d_squared = (Point2D.X - Circumcenter.X) * (Point2D.X - Circumcenter.X) + (Point2D.Y - Circumcenter.Y) * (Point2D.Y - Circumcenter.Y);
        return d_squared < RadiusSquared;
    }

    public bool ISPointExists(List<Point2D> tst)
    {
        foreach (var vert in Vertices)
        {
            if (tst.Contains(vert))
            {
                return true;
            }
        }
        return false;
    }
}
