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




    public void mirror(Mirror mirror)
    {
        ushort val = (ushort)mirror;

        if((val & 1) == 1)
        {
            X *= -1;
        }

        if((val & 2) == 2)
        {
            Y *= -1;
        }  
    }
}





