import java.io.IOException;
import java.util.Random;

public class Main {

    public static void main(String[] args) throws IOException {

        // 1
        Random generator = new Random();

        double randangle = Math.PI * generator.nextDouble();

        Point rp = new Point(10);
        Line l1 = new Line(new Point(5, 2), new Point(8, 9));
        l1 = Calculate.rotateLine(l1, Math.PI);

        // Draw d1 = new Draw(100);
        // d1.addLine(l1);
        // d1.addPoint(rp);
        // d1.saveFile("output1.txt");
        // new ProcessBuilder("D:\\Program Files\\VCG\\MeshLab\\meshlab.exe",
        // "output1.txt").start();

        System.out.println(rp.x + " " + rp.y + " " + randangle * Math.PI);
        Calculate.whichSide(l1, rp);

        // 2
        double gae, ga;
        Draw d2 = new Draw(200);
        do {
            Traingle t1 = new Traingle(new Point(10), new Point(10), new Point(10));
            Point rp2 = new Point(10);

            d2.points.clear();//debug

            ga = t1.getArea();
            gae = t1.getAreaExtended(rp2);

            System.out.println(t1.getArea());
            System.out.println(t1.getAreaExtended(rp2));

            d2.addTriangle(t1);
            d2.addPoint(rp2);
        } while (gae > ga);

        // d2.addTriangle(t1);
        // d2.addPoint(rp2);
         //t1.printInsideStatus(rp2);
         //d2.saveFile("output1.txt");
        new ProcessBuilder("D:\\Program Files\\VCG\\MeshLab\\meshlab.exe", "output1.txt").start();
    }
}
