public class LineEquation {
    double A;
    double B;
    double C;

    public LineEquation(Line li){
        this.A = (li.head.y - li.tail.y)/(li.head.x - li.tail.x);
        this.B = li.head.y - A * li.head.x;
        this.C = 0;
    }
}
