using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boundary2
{
    public float Width;
    public float Height;
    public Vector2 Center;

    public Boundary2(Vector2 Center, float Width, float Height)
    {
        this.Center = Center;
        this.Width = Width;
        this.Height = Height;
    }

    public Boundary2(float x, float y, float Width, float Height)
    {
        Center = new Vector2(x, y);
        this.Width = Width;
        this.Height = Height;
    }


    public bool Contains(Vector2 point)
    {
        bool contains = (point.x > Center.x - Width && point.x < Center.x + Width && point.y > Center.y - Height && point.y < Center.y + Height);
        return contains;
    }
}

