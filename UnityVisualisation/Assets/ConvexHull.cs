using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvexHull : MonoBehaviour
{
    public LineRenderer LineRendererObj;
    public Material Material;

    public void PassArgs(Material material)
    {
        GameObject gm = new GameObject();
        LineRendererObj = gm.AddComponent<LineRenderer>();
        LineRendererObj.name = ToString();
        LineRendererObj.positionCount = 0;
        LineRendererObj.startWidth = .5f;
        LineRendererObj.endWidth = .5f;
        LineRendererObj.material = material;
        LineRendererObj.loop = true;
        Material = material;
    }

    public void AddPoint(Point2D point, int i)
    {
        LineRendererObj.positionCount = i + 1;
        LineRendererObj.SetPosition(i, point.Position);
    }

    public override string ToString()
    {
        return $"Convex Hull contain {LineRendererObj.positionCount} points";
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LineRendererObj.name = ToString();
    }
}
