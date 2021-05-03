using System;
using System.Collections.Generic;
using UnityEngine;

public class QuadTree
{
    int capacity;
    bool division;
    public static GameObject Prefab;
    public static GameObject gameObject;
    public static Material LineMaterial;


    List<Point2D> ListOfPoints;
    Boundary boundary;

    QuadTree northEast;
    QuadTree northWest;
    QuadTree southEast;
    QuadTree southWest;

    List<Line> lines = new List<Line>();

    public QuadTree(Boundary boundary, int capacity, IEnumerable<Point2D> PointsInParent)
    {
        this.boundary = boundary;
        this.capacity = capacity;
        ListOfPoints = new List<Point2D>();
        division = false;


        foreach (Point2D p in PointsInParent)
        {
            if (boundary.Contains(p))
            {
                ListOfPoints.Add(p);
            }
        }
    }

    public bool Insert(Point2D point2D)
    {
        if (boundary.Contains(point2D) == false)
        {
            return false;
        }

        if (ListOfPoints.Count < capacity)
        {
            ListOfPoints.Add(point2D);
            return true;
        }
        else
        {
            if (division == false)
            {
                Subdivide();
            }

            if (northEast.Insert(point2D) == true)
            {
                return true;
            }
            else if (northWest.Insert(point2D) == true)
            {
                return true;
            }
            else if (southEast.Insert(point2D) == true)
            {
                return true;
            }
            else if (southWest.Insert(point2D) == true)
            {
                return true;
            }
        }
        return false;
    }

    void Subdivide()
    {
        var x = boundary.centerX;
        var y = boundary.centerY;
        var w = boundary.width;
        var h = boundary.height;


        lines = boundary.DrawCross(gameObject, Prefab, LineMaterial, false);

        var NE = new Boundary(x + w / 2, y + h / 2, w / 2, h / 2);
        northEast = new QuadTree(NE, capacity, ListOfPoints);
        var NW = new Boundary(x - w / 2, y + h / 2, w / 2, h / 2);
        northWest = new QuadTree(NW, capacity, ListOfPoints);
        var SE = new Boundary(x + w / 2, y - h / 2, w / 2, h / 2);
        southEast = new QuadTree(SE, capacity, ListOfPoints);
        var SW = new Boundary(x - w / 2, y - h / 2, w / 2, h / 2);
        southWest = new QuadTree(SW, capacity, ListOfPoints);
        division = true;
    }

    public void Empty()
    {

        foreach (Point2D p in ListOfPoints)
        {
            UnityEngine.Object.Destroy(p);
        }

        ListOfPoints.Clear();

        if (lines != null)
        {
            foreach (Line l in lines)
            {
                UnityEngine.Object.Destroy(l);
            }
        }

        southWest?.Empty();
        southEast?.Empty();
        northWest?.Empty();
        northEast?.Empty();
    }
}
