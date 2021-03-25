public class Main {

    public static void main(String[] args) {

        Calculate.whichSide(new Line(new Point(1, 1), new Point(2, 5)), new Point(-1, 3));

        Calculate.whichSide(new Line(new Point(1, 1), new Point(2, 5)), new Point(5, 3));

        Calculate.whichSide(new Line(new Point(1, 1), new Point(2, 5)), new Point(3, 4));
        //todo

        Calculate.crossingPointCramer(2, 4, 5, 2, 3, 7);

        System.out.println(Calculate.trangleArea(new Point(1, 2), new Point(3, 7), new Point(6,1)));
    }
}
