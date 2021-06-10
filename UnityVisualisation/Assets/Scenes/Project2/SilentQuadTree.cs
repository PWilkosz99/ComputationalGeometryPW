using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SilentQuadTree
{
    public static GameObject gameObject;
    public static GameObject Prefab;
    public Vector2 Center;

    private Boundary2 boundary;
    private List<Vector2> Point2Ds;
    private int capacity;
    private bool divided;

    SilentQuadTree topRight;
    SilentQuadTree topLeft;
    SilentQuadTree bottomRight;
    SilentQuadTree bottomLeft;


    public IEnumerable<SilentQuadTree> SilentQuadTrees
    {
        get { return new SilentQuadTree[] { topLeft, topRight, bottomLeft, bottomRight }; }
        
    }
    List<Vector2> Result;

    int deph;
    public SilentQuadTree(Boundary2 _boundary, int _capacity,IEnumerable<Vector2> PointsInParent, List<Vector2> Result ,  int deph = 0)
    {
        Center = new Vector2(_boundary.Center.x + _boundary.Height / 2f, _boundary.Center.y + _boundary.Width / 2f);
        this.Result = Result;
        this.deph = deph;

        boundary = _boundary;
        capacity = _capacity;
        Point2Ds = new List<Vector2>();
        divided = false;

        foreach (var item in PointsInParent)
        {
            if(boundary.Contains(item))
            {
                Point2Ds.Add(item);
            }
        }
    }


    public void Deep(ref int MinBoundary)
    {
        if (boundary.Width < MinBoundary)
        {
            Result.Add(Center);
        }
        foreach (var item in SilentQuadTrees)
        {
            if (item != null)
            {
                item.Deep(ref MinBoundary);
            }
        }
    }


    public bool Insert(Vector2 point2D)
    {
        if (boundary.Contains(point2D) == false)
        {
            return false;
        }

        if (Point2Ds.Count < capacity)
        {
            Point2Ds.Add(point2D);
            return true;
        }
        else
        {
            if (divided == false)
            {
                Subdivide();
            }

            if (topRight.Insert(point2D) == true)
            {
                return true;
            }
            else if (topLeft.Insert(point2D) == true)
            {
                return true;
            }
            else if (bottomRight.Insert(point2D) == true)
            {
                return true;
            }
            else if (bottomLeft.Insert(point2D) == true)
            {
                return true;
            }
        }
        return false;
    }

    void Subdivide()
    {
        var w = boundary.Width;
        var h = boundary.Height;

        var tr = new Boundary2(Center.x , Center.y , w / 2, h / 2);
        topRight = new SilentQuadTree(tr, capacity, Point2Ds,Result , deph + 1);
        var tl = new Boundary2(Center.x -w/2, Center.y , w / 2, h / 2);
        topLeft = new SilentQuadTree(tl, capacity, Point2Ds, Result ,deph + 1);
        var br = new Boundary2(Center.x , Center.y - h / 2, w / 2, h / 2);
        bottomRight = new SilentQuadTree(br, capacity, Point2Ds, Result ,deph + 1);
        var bl = new Boundary2(Center.x - w / 2, Center.y - h / 2, w / 2, h / 2);
        bottomLeft = new SilentQuadTree(bl, capacity, Point2Ds, Result , deph + 1);
        divided = true;
    }

    public void Empty()
    {
        bottomLeft?.Empty();
        bottomRight?.Empty();
        topLeft?.Empty();
        topRight?.Empty();
    }
}
