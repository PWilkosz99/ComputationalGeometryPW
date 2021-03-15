public class Calculate {
    // 1
    static void lineEquation(Line li) {

        double a = (li.head.y - li.tail.y) / (li.head.x - li.tail.x);
        double b = li.head.y - a * li.head.x;

        System.out.println("y = " + a + "x + " + b);
    }

    // 2
    static void lineBelongig(Line li, Point p) {
        double a = (li.head.y - li.tail.y) / (li.head.x - li.tail.x);
        double b = li.head.y - a * li.head.x;
        double ey = a * p.x + b;
        if (p.y == ey) {
            System.out.println("Punkt leży na prostej");
            if (p.x >= Math.min(li.head.x, li.tail.x) && p.x <= Math.max(li.head.x, li.tail.x)
                    && p.y >= Math.min(li.head.y, li.tail.y) && p.y <= Math.max(li.head.y, li.tail.y)) {
                System.out.println("Punkt leży na odnciku");
            }
        }
    }

    // 3
    static Line translation(Line li, Vector v) {
        li.head.x += v.x;
        li.tail.x += v.x;
        li.head.y += v.y;
        li.tail.y += v.y;

        System.out.print("x1:" + li.head.x + " y1:" + li.head.y + "\nx2:" + li.tail.x + " y2: " + li.tail.y + "\n");
        return li;
    }

    // 4
    static Point rotate(Point p, double angle) {

        p.x = p.x * Math.cos(angle) - p.y * Math.sin(angle);
        p.y = p.y * Math.cos(angle) + p.x * Math.sin(angle);

        System.out.println("After rotate: x = " + p.x + " y = " + p.y);

        return p;
    }

    // 5 \/
    static Point mirrorX(Point p) {
        p.x *= -1;

        System.out.println("After mirroring X: x = " + p.x + " y = " + p.y);
        return p;
    }

    static Point mirrorY(Point p) {
        p.y *= -1;

        System.out.println("After mirroring Y: x = " + p.x + " y = " + p.y);
        return p;
    }

    static Point mirrorXY(Point p) {
        p.x *= -1;
        p.y *= -1;

        System.out.println("After mirroring XY: x = " + p.x + " y = " + p.y);
        return p;
    }
}
