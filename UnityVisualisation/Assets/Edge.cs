
public class Edge
{
    public Point2D Point1 { get; }
    public Point2D Point2 { get; }

    public Edge(Point2D Point1, Point2D Point2)
    {
        this.Point1 = Point1;
        this.Point2 = Point2;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != GetType()) return false;
        var edge = obj as Edge;

        var samePoint2Ds = Point1 == edge.Point1 && Point2 == edge.Point2;
        var samePoint2DsReversed = Point1 == edge.Point2 && Point2 == edge.Point1;
        return samePoint2Ds || samePoint2DsReversed;
    }

    public override int GetHashCode()
    {
        int hCode = (int)Point1.X ^ (int)Point1.Y ^ (int)Point2.X ^ (int)Point2.Y;
        return hCode.GetHashCode();
    }
}
