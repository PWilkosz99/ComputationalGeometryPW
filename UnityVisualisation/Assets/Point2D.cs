using System;
using UnityEngine;


public class Point2D : MonoBehaviour
{  
    private GameObject PointFromTemplate;

    public Vector2 Position { get => PointFromTemplate.transform.position; set { PointFromTemplate.transform.position = value;  } }

    public GameObject Prefab {private set
        {
            PointFromTemplate = Instantiate(value, Vector2.zero, Quaternion.identity);
        }
        get
        {
            return PointFromTemplate;
        }
    }  

    public float X { get { return Position.x; } set { Position = new Vector2(value, Y); }  }
    public float Y { get { return Position.y; } set { Position = new Vector2(X, value); } }


    public void PassArgs(Vector2 vector, GameObject GameObjectToCopy)
    {
        this.Prefab = GameObjectToCopy;
        Position = vector;
    }

    /// <summary>
    /// You are trying to create a MonoBehaviour using the 'new' keyword.  This is not allowed.  MonoBehaviours can only be added using AddComponent().
    /// </summary>
    public void PassArgs(float x, float y, GameObject GameObjectToCopy)
    {
        this.Prefab = GameObjectToCopy;
        Position = new Vector2(x, y);
    }


    public override string ToString()
    {
        return "Punkt " + Position.ToString();
    }

    public Point2D Clone(GameObject GameObjectParm)
    {
        var resp = GameObjectParm.AddComponent<Point2D>();
        resp.PassArgs(Position, Prefab);
        return resp;
    }


    public void rotate(float angle){
        var p = new Vector2();

        p.x = X * Mathf.Cos(angle) - Y * Mathf.Sin(angle);
        p.y = Y * Mathf.Cos(angle) + X * Mathf.Sin(angle);
        Position = p;
    }


    public enum Mirror : ushort
    {
        X =1,
        Y =2,
        XY = 3,
    }

    public void mirror(Mirror mirror)
    {
        ushort val = (ushort)mirror;

        if ((val & 1) == 1)
        {
            X *= -1;
        }

        if ((val & 2) == 2)
        {
            Y *= -1;
        }
    }

    public void EquationWithSecondPoint(Point2D P)
    {
        double a = P.Y - Y;
        double b = X - P.X;
        double c = a * (P.X) + b * (Y);
        print("Wzór funkcji:" + a + "x - " + b + "y = " + c);

    }


    public float AngleBetweenPoints(Point2D l1, Point2D l2)
    {
        Vector2 v1 = new Vector2(X - l1.X, Y - l1.Y);
        Vector2 v2 = new Vector2(X - l2.X, Y - l2.Y);
        return Vector2.Angle(v1, v2); //I'm using the built-in unity function 
    }


    private void Update()
    { 
        if(PointFromTemplate != null) // aktualizacja nazwy aby zawsze przedstawia³a wspó³rzêdne
        {
            if(PointFromTemplate.name != ToString())
            {
                PointFromTemplate.name = ToString();
            }
        }
    }
}