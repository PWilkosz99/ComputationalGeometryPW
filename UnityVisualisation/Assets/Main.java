import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;


public class Main {

    static ArrayList<String> names;

    public static void main(String[] args) throws IOException
    {            
        // 1
        var point12 = new Point2D(1, 2);
        var point910 = new Point2D(9, 10);
        Line line = new Line(point12, point910);


        var point31 = new Point2D(3, 1);
        var point34 = new Point2D(3, 4.05);


        System.out.println("Punkt: " + point31 + "Na lini: " + line.lineEquationGeneralPrint() + "\nZnajuje się po stronie: "  + line.whichSide(point31));
        System.out.println("Punkt: " + point34 + "Na lini: " + line.lineEquationGeneralPrint() + "\nZnajuje się po stronie: "  + line.whichSide(point34));

        // 2

        var point21 = new Point2D(2, 1);
        var point42 = new Point2D(4, 2);
        Line line2 = new Line(point21, point42);

        System.out.println("\n\nPunkt przeciecia\nLinia1: " + line.lineEquationGeneralPrint() + "\nLinia2: " + line2.lineEquationGeneralPrint());
        System.out.println(line.crossingPointCramer(line2));

        var point11 = new Point2D(1, 1);
        var point22 = new Point2D(2, 2);
        Line line3 = new Line(point11, point22);

        System.out.println("\n\nPunkt przeciecia\nLinia1: " + line.lineEquationGeneralPrint() + "\nLinia3: " + line3.lineEquationGeneralPrint());
        System.out.println(line.crossingPointCramer(line3));

        //3

        System.out.println("\n");

        var trojkat = new Triangle(new Point2D(1, 0), new Point2D(1, 2), new Point2D(3, 0));
        System.out.println(trojkat);
        System.out.println("Pole: " + trojkat.fieldMatrix());

        System.out.println("\n");
        var trojkoat2 = new Triangle(new Point2D(-3, -2), new Point2D(3, 1), new Point2D(-2, 3));
        System.out.println(trojkoat2);
        System.out.println("Pole: " + trojkoat2.fieldMatrix());
    }   
}