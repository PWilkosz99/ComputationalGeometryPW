import java.util.Random;

public class Point {

    Point(double x, double y) {
        this.x = x;
        this.y = y;
    }

    Point(int acc) {
        Random generator = new Random();
        this.x = acc * generator.nextDouble();
        this.y = acc * generator.nextDouble();
    }

    double x;
    double y;
}
