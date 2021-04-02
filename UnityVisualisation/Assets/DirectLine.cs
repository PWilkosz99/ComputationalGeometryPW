using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectLine : Line
{
    float MaxScale = 100;
    protected override void MakeLine(Point2D start, Point2D stop, Material material)
    {
        var go = new GameObject();
        print(gameObject.transform.position);

        LineRendererObj = go.AddComponent<LineRenderer>();
        LineRendererObj.positionCount = 4;

        var ab = lineEquation();

        LineRendererObj.startWidth = .2f;
        LineRendererObj.endWidth = .2f;

        LineRendererObj.SetPosition(0,new Vector2(-1 * MaxScale, (float)(ab[EquationAB.A] * -1 * MaxScale + ab[EquationAB.B]))); 
        LineRendererObj.SetPosition(1, start.Position);
        LineRendererObj.SetPosition(2, stop.Position);
        LineRendererObj.SetPosition(3, new Vector2(MaxScale, (float)(ab[EquationAB.A] * MaxScale + ab[EquationAB.B])));

        LineRendererObj.material = material; 
        LineRendererObj.name = ToString();
    }

    private void Update()
    {
        if ((Vector2)LineRendererObj.GetPosition(1) != head.Position)
        {
            var ab = lineEquation();
            LineRendererObj.SetPosition(0, new Vector2(-1 * MaxScale, (float)(ab[EquationAB.A] * -1 * MaxScale + ab[EquationAB.B])));
            LineRendererObj.SetPosition(1, head.Position);
            LineRendererObj.SetPosition(3, new Vector2(MaxScale, (float)(ab[EquationAB.A] * MaxScale + ab[EquationAB.B])));
            LineRendererObj.name = ToString();
        }

        if ((Vector2)LineRendererObj.GetPosition(2) != tail.Position)
        {
            var ab = lineEquation();
            LineRendererObj.SetPosition(0, new Vector2(-1 * MaxScale, (float)(ab[EquationAB.A] * -1 * MaxScale + ab[EquationAB.B])));
            LineRendererObj.SetPosition(2, tail.Position);
            LineRendererObj.SetPosition(3, new Vector2(MaxScale, (float)(ab[EquationAB.A] * MaxScale + ab[EquationAB.B])));
            LineRendererObj.name = ToString();
        }
    }

    public override string ToString()
    {
        return "Prosta " + tail + " -> " + head;
    }
}
