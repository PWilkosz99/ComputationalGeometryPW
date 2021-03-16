public class Calculate {
    // 2
    static void lineEquation(Line li) {

        double a = (li.head.y - li.tail.y) / (li.head.x - li.tail.x);
        double b = li.head.y - a * li.head.x;
        System.out.println("Dla punktów x1 = "+li.head.x+ " y1 = "+li.head.y+", x2 = "+li.tail.x+" y2 = "+li.tail.y);
        System.out.println("Równanie lini to y = " + a + "x + " + b);
    }

    // 3
    static void lineBelongig(Line li, Point p) {
        double a = (li.head.y - li.tail.y) / (li.head.x - li.tail.x);
        double b = li.head.y - a * li.head.x;
        double ey = a * p.x + b;
        System.out.println("Dla prostej oraz odcinka wyznaczanych przez punkty x1 = "+li.head.x+ " y1 = "+li.head.y+", x2 = "+li.tail.x+" y2 = "+li.tail.y);
        if (p.y == ey) {
            System.out.println("Punkt x = "+p.x+" y = "+p.y+" leży na prostej");
            if (p.x >= Math.min(li.head.x, li.tail.x) && p.x <= Math.max(li.head.x, li.tail.x)
                    && p.y >= Math.min(li.head.y, li.tail.y) && p.y <= Math.max(li.head.y, li.tail.y)) {
                System.out.println("Punkt x = "+p.x+" y = "+p.y+" leży na odcinku");
            }
        }else{
            System.out.println("Punkt nie leży na prostej ani na odcniku");
        }
    }

    // 4
    static Line translation(Line li, Vector v) {
        System.out.println("Przed translacją:\n x1 = "+li.head.x+ " y1 = "+li.head.y+", x2 = "+li.tail.x+" y2 = "+li.tail.y);
        li.head.x += v.x;
        li.tail.x += v.x;
        li.head.y += v.y;
        li.tail.y += v.y;
        System.out.println("Po translacji o wektor x = "+v.x+" y = "+v.y+":\n x1 = "+li.head.x+ " y1 = "+li.head.y+", x2 = "+li.tail.x+" y2 = "+li.tail.y);
        return li;
    }

    // 5
    static Point rotate(Point p, double angle) {

        
        System.out.println("Przed rotacją: x = " + p.x + " y = " + p.y);

        p.x = p.x * Math.cos(angle) - p.y * Math.sin(angle);
        p.y = p.y * Math.cos(angle) + p.x * Math.sin(angle);

        System.out.println("Po rotacji o kąt "+angle+" rad : x = " + p.x + " y = " + p.y);

        return p;
    }

    // 6 \/
    static Point mirrorX(Point p) {
        System.out.println("Pred obrotem OX: x = " + p.x + " y = " + p.y);
        p.x *= -1;
        System.out.println("Po obrocie OX: x = " + p.x + " y = " + p.y);
        return p;
    }

    static Point mirrorY(Point p) {
        System.out.println("Pred obrotem OY: x = " + p.x + " y = " + p.y);
        p.y *= -1;
        System.out.println("Po obrocie OY: x = " + p.x + " y = " + p.y);
        return p;
    }

    static Point mirrorXY(Point p) {
        System.out.println("Pred obrotem OXY: x = " + p.x + " y = " + p.y);
        p.x *= -1;
        p.y *= -1;
        System.out.println("Po obrocie OXY: x = " + p.x + " y = " + p.y);
        return p;
    }
}
