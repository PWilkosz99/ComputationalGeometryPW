import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;

public class Generator {

    private int accuracy = 1;

    Generator(int accuracy) {
        points = new ArrayList<>();
        this.accuracy = accuracy;
    }

    ArrayList<Point3D> points;

    Point3D center3D = new Point3D(0, 0, 1);
    Point3D center2D = new Point3D(0, 0, 0);

    void connectPoints(Point3D a, Point3D b) {
        var xstep = (a.x - b.x) / accuracy;
        var ystep = (a.y - b.y) / accuracy;
        var zstep = (a.z - b.z) / accuracy;

        for (int j = 1; j < accuracy; j++) {
            points.add(new Point3D(a.x - xstep * j, a.y - ystep * j, a.z - zstep * j));
        }
    }

    void makeBaseCircle(int R) {
        double step = 2 * Math.PI / 1000;

        for (int i = 0; i < 1000; i++) {
            double angle = step * i;
            points.add(new Point3D(R * Math.sin(angle), R * Math.cos(angle), 0));
        }
    }

    void fillBase(int quantity) {
        for (int i = 0; i < quantity - 1; i++) {
            var a = points.get(i);
            connectPoints(a, center2D);
        }
        var a = points.get(quantity - 1);
        connectPoints(a, center2D);
    }

    void makeSide(int quantity) {
        for (int i = 0; i < quantity - 1; i++) {
            var a = points.get(i);
            connectPoints(a, center3D);
        }

        var a = points.get(quantity - 1);
        connectPoints(a, center3D);
    }

    void makeCone(int R, int h) {
        Point3D tmpPoint = new Point3D(0, 0, h);
        center3D = tmpPoint;
        makeBaseCircle(R);
        var quantity = points.size();
        fillBase(quantity);
        makeSide(quantity);
    }

    void saveFile(String FileName) throws IOException {

        BufferedWriter bw = new BufferedWriter(new FileWriter(FileName));

        for (Point3D point : points) {

            bw.write("" + point.x + ";" + point.y + ";" + point.z + "\n");

        }

        bw.close();
    }
}