using System;

public class Matrix {

    public static XY crossingPointCramer(double A1, double A2, double B1, double B2, double C1, double C2)
    {
        var w = cross(A1, A2, B1, B2);
        var result = new XY();
        
        if(w == 0)
        {
            result.notExist = true;
            return result;
        }

        result.X = cross(-C1, -C2, B1, B2)/w;
        result.Y = cross(A1, A2, -C1, -C2)/w;
        
        return result;
    }

    //kolumny najpierw
    // [A1][B2]
    // [A1][B2]
    public static double cross(double A1, double A2, double B1, double B2)
    {
        return A1*B2-B1*A2;
    }
}



public class XY
{
    public double X = 0;
    public double Y = 0;
    public bool notExist = false;

    public override string ToString()
    {
        return String.Format("[" + X + ", " + Y + "]");
    }
}