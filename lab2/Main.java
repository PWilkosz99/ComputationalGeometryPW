public class Main {

    public static void main(String[] args) {

        Line line = new Line(new Point(1, 1), new Point(9, 9));
        System.out.println("\nZadanie 2:");
        Calculate.lineEquation(line);

        System.out.println("\nZadanie 3:");
        Calculate.lineBelongig(line, new Point(3, 3));

        System.out.println("\nZadanie 4:");
        Calculate.translation(line, new Vector(2, 2));

        System.out.println("\nZadanie 5:");
        Calculate.rotate(new Point(2,2), 360); //kat w radianach

        System.out.println("\nZadanie 6:");
        Calculate.mirrorXY(new Point(2,2));

    }
}
