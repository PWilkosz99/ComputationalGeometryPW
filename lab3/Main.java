public class Main {

    public static void main(String[] args) {

        // 1
        System.out.println("ZAD 1: Położenie względem prostej:\n");
        System.out.println("\nDla lini (1,1), (2,5) i punktu (-1,3):");
        Calculate.whichSide(new Line(new Point(1, 1), new Point(2, 5)), new Point(-1, 3));
        System.out.println("\nDla lini (1,1), (2,5) i punktu (5,3):");
        Calculate.whichSide(new Line(new Point(1, 1), new Point(2, 5)), new Point(5, 3));
        System.out.println("\nDla lini (1,1), (2,5) i punktu (3,4):");
        Calculate.whichSide(new Line(new Point(1, 1), new Point(3, 3)), new Point(4, 4));

        // 2
        System.out.println("\nDla A1 = 2, B1 = 4, C1 = 5 oraz A2 = 2, B2 = 3, C2 = 7 : \n");
        Calculate.crossingPointCramer(2, 4, 5, 2, 3, 7);

        // 3
        System.out.println("\nZAD 3: Pole trójkata:\n");
        System.out.println("Dla trójkąta na wektorach [1,2], [3,-7], [6,1] : ");
        System.out.println(Calculate.trangleArea(new Point(1, 2), new Point(3, -7), new Point(6, 1)));
    }
}
