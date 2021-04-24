using System;
using UnityEngine;

public class KDNode
{
    public GameObject Prefab;
    public Material Material;

    public int axis;
    public int id;
    public bool isChecked;
    public bool orientation;
    public Point2D x;

    public KDNode Parent;
    public KDNode Left;
    public KDNode Right;

    public KDNode(Point2D x0, int axis0)
    {
        x = x0;
        axis = axis0;
        Left = Right = Parent = null;
        isChecked = false;
        id = 0;
    }

    public KDNode FindParent(Point2D x0)
    {
        KDNode parent = null;
        KDNode next = this;
        int split;
        while (next != null)
        {
            split = next.axis;
            parent = next;
            if (split == 0)
            {
                if (x0.X > next.x.X)
                {
                    next = next.Right;
                }
                else
                {
                    next = next.Left;
                }
            }
            else
            {
                if (x0.Y > next.x.Y)
                {
                    next = next.Right;
                }
                else
                {
                    next = next.Left;
                }
            }
        }
        return parent;
    }

    public KDNode Insert(Point2D p)
    {
        KDNode parent = FindParent(p);
        if (Equal(p, parent.x, 2) == true)
        {
            return null;
        }

        KDNode newNode = new KDNode(p, parent.axis + 1 < 2 ? parent.axis + 1 : 0);
        newNode.Parent = parent;

        if (parent.axis == 0)
        {
            if (p.X > parent.x.X)
            {
                parent.Right = newNode;
                newNode.orientation = true;
            }
            else
            {
                parent.Left = newNode;
                newNode.orientation = false;
            }
        }
        else
        {
            if (p.Y > parent.x.Y)
            {
                parent.Right = newNode;
                newNode.orientation = true;
            }
            else
            {
                parent.Left = newNode;
                newNode.orientation = false;
            }
        }
        return newNode;
    }

    public bool Equal(Point2D x1, Point2D x2, int dim)
    {
        if (dim == 1 || dim == 2)
        {
            if (x1.X != x2.X)
                return false;
            if (dim == 2)
            {
                if (x1.Y != x2.Y)
                    return false;
            }
        }

        return true;
    }

    public double Distance(Point2D x1, Point2D x2, int dim)
    {
        double S = 0;
        if (dim == 1 || dim == 2)
        {
            S += (x1.X - x2.X) * (x1.X - x2.X);
            if (dim == 2)
            {
                S += (x1.Y - x2.Y) * (x1.Y - x2.Y);
            }
        }
        return S;
    }
}


public class KDTree
{
    KDNode Root;
    KDNode nearestPoint;
    KDNode[] checkedNodes;
    KDNode[] List;

    double minDistanceValue;
    int checkedNodesNum;
    int KD_id;
    int nList;

    double[] xMin;
    double[] xMax;
    bool[] maxBoundary;
    bool[] minBoundary; 
    int boundaryNumber;

    public KDTree(int i)
    {
        Root = null;
        KD_id = 1;
        nList = 0;
        List = new KDNode[i];
        checkedNodes = new KDNode[i];
        maxBoundary = new bool[2];
        minBoundary = new bool[2];
        xMin = new double[2];
        xMax = new double[2];
    }

    public bool Add(Point2D x)
    {
        if (Root == null)
        {
            Root = new KDNode(x, 0);
            Root.id = KD_id++;
            List[nList++] = Root;
        }
        else
        {
            KDNode pNode;
            if ((pNode = Root.Insert(x)) != null)
            {
                pNode.id = KD_id++;
                List[nList++] = pNode;
            }
        }

        return true;
    }

    public KDNode FindNearestPoint(Point2D x)
    {
        if (Root == null)
            return null;

        checkedNodesNum = 0;
        KDNode parent = Root.FindParent(x);
        nearestPoint = parent;
        minDistanceValue = Root.Distance(x, parent.x, 2);
        if (parent.Equal(x, parent.x, 2) == true)
            return nearestPoint;

        SearchParent(parent, x);
        Uncheck();

        return nearestPoint;
    }

    public void CheckSubtree(KDNode node, Point2D x)
    {
        if ((node == null) || node.isChecked)
            return;

        checkedNodes[checkedNodesNum++] = node;
        node.isChecked = true;
        SetBoundingCube(node, x);

        int dim = node.axis;
        double d;
        if (dim == 0)
        {
            d = node.x.X - x.X;
        }
        else
        {
            d = node.x.Y - x.Y;
        }

        if (d * d > minDistanceValue)
        {
            if (dim == 0)
            {
                if (node.x.X > x.X)
                    CheckSubtree(node.Left, x);
                else
                    CheckSubtree(node.Right, x);
            }
            else
            {
                if (node.x.Y > x.X)
                    CheckSubtree(node.Left, x);
                else
                    CheckSubtree(node.Right, x);
            }
        }
        else
        {
            CheckSubtree(node.Left, x);
            CheckSubtree(node.Right, x);
        }
    }

    public void SetBoundingCube(KDNode node, Point2D x)
    {
        if (node == null)
            return;
        double d = 0;
        double dx;
        int k = 0;
        dx = node.x.X - x.X;
        if (dx > 0)
        {
            dx *= dx;
            if (!maxBoundary[k])
            {
                if (dx > xMax[k])
                    xMax[k] = dx;
                if (xMax[k] > minDistanceValue)
                {
                    maxBoundary[k] = true;
                    boundaryNumber++;
                }
            }
        }
        else
        {
            dx *= dx;
            if (!minBoundary[k])
            {
                if (dx > xMin[k])
                    xMin[k] = dx;
                if (xMin[k] > minDistanceValue)
                {
                    minBoundary[k] = true;
                    boundaryNumber++;
                }
            }
        }
        d += dx;
        if (d > minDistanceValue)
            return;

        k = 1;
        dx = node.x.Y - x.Y;
        if (dx > 0)
        {
            dx *= dx;
            if (!maxBoundary[k])
            {
                if (dx > xMax[k])
                    xMax[k] = dx;
                if (xMax[k] > minDistanceValue)
                {
                    maxBoundary[k] = true;
                    boundaryNumber++;
                }
            }
        }
        else
        {
            dx *= dx;
            if (!minBoundary[k])
            {
                if (dx > xMin[k])
                    xMin[k] = dx;
                if (xMin[k] > minDistanceValue)
                {
                    minBoundary[k] = true;
                    boundaryNumber++;
                }
            }
        }
        d += dx;
        if (d > minDistanceValue)
            return;

        if (d < minDistanceValue)
        {
            minDistanceValue = d;
            nearestPoint = node;
        }
    }

    public KDNode SearchParent(KDNode parent, Point2D x)
    {
        for (int k = 0; k < 2; k++)
        {
            xMin[k] = xMax[k] = 0;
            maxBoundary[k] = minBoundary[k] = false;
        }
        boundaryNumber = 0;

        KDNode search_root = parent;
        while (parent != null && (boundaryNumber != 2 * 2))
        {
            CheckSubtree(parent, x);
            search_root = parent;
            parent = parent.Parent;
        }

        return search_root;
    }

    public void Uncheck()
    {
        for (int n = 0; n < checkedNodesNum; n++)
            checkedNodes[n].isChecked = false;
    }
}
