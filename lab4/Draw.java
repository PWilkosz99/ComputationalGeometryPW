import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;

public class Draw {
    private int accuracy = 1;

    Draw(int accuracy) {
        points = new ArrayList<>();
        this.accuracy = accuracy;
    }

    ArrayList<Point> points;

    Point center = new Point(0.0, 0.0);

    void connectPoints(Point a, Point b) {
        var xstep = (a.x - b.x) / accuracy;
        var ystep = (a.y - b.y) / accuracy;

        for (int j = 1; j < accuracy; j++) {
            points.add(new Point(a.x - xstep * j, a.y - ystep * j));
        }
    }

    void addLine(Line l) {
        connectPoints(l.head, l.tail);
    }

    void addPoint(Point a) {
        points.add(a);
    }

    void addTriangle(Traingle t){
        connectPoints(t.a, t.b);
        connectPoints(t.a, t.c);
        connectPoints(t.b, t.c);
    }

    void saveFile(String FileName) throws IOException {

        BufferedWriter bw = new BufferedWriter(new FileWriter(FileName));

        for (Point point : points) {

            bw.write("" + point.x + ";" + point.y + ";" + 0 + "\n");

        }

        bw.close();
    }
}
