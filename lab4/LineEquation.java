public class LineEquation {
    double A;
    double B;
    double C;

    public LineEquation(Line li) {
        this.A = (li.head.y - li.tail.y) / (li.head.x - li.tail.x);
        this.B = li.head.y - (this.A * li.head.x);
        this.C = 0;
    }
    public LineEquation(double A, double B, double C){
        this.A=A;
        this.B=B;
        this.C=C;
    }
    public void printYeq() {
        System.out.println("y = "+(-A/B)+" + "+C/B);
    }
}
