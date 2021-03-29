public class Traingle {
    Point a;
    Point b;
    Point c;

    Traingle(Point a, Point b, Point c) {
        this.a = a;
        this.b = b;
        this.c = c;
    }
    // s= sqrt(p(p-a)(p-b)(p-c))
    //

    double getArea() {
        double walla = Math.sqrt(Math.pow((b.x - a.x), 2) + Math.pow((b.y - a.y), 2));
        double wallb = Math.sqrt(Math.pow((c.x - a.x), 2) + Math.pow((c.y - a.y), 2));
        double wallc = Math.sqrt(Math.pow((c.x - b.x), 2) + Math.pow((c.y - b.y), 2));

        double p = 0.5 * (walla + wallb + wallc);
        double S = Math.sqrt(p * (p - walla) * (p - wallb) * (p - wallc));// wzor Herona
        return S;
    }

    double getAreaReplaceA(Point pt) {
        double walla = Math.sqrt(Math.pow((b.x - pt.x), 2) + Math.pow((b.y - pt.y), 2));
        double wallb = Math.sqrt(Math.pow((c.x - pt.x), 2) + Math.pow((c.y - pt.y), 2));
        double wallc = Math.sqrt(Math.pow((c.x - b.x), 2) + Math.pow((c.y - b.y), 2));

        double p = 0.5 * (walla + wallb + wallc);
        double S = Math.sqrt(p * (p - walla) * (p - wallb) * (p - wallc));// wzor Herona
        return S;
    }

    double getAreaReplaceB(Point pt) {
        double walla = Math.sqrt(Math.pow((pt.x - a.x), 2) + Math.pow((pt.y - a.y), 2));
        double wallb = Math.sqrt(Math.pow((c.x - a.x), 2) + Math.pow((c.y - a.y), 2));
        double wallc = Math.sqrt(Math.pow((c.x - pt.x), 2) + Math.pow((c.y - pt.y), 2));

        double p = 0.5 * (walla + wallb + wallc);
        double S = Math.sqrt(p * (p - walla) * (p - wallb) * (p - wallc));// wzor Herona
        return S;
    }

    double getAreaReplaceC(Point pt) {
        double walla = Math.sqrt(Math.pow((pt.x - a.x), 2) + Math.pow((pt.y - a.y), 2));
        double wallb = Math.sqrt(Math.pow((c.x - a.x), 2) + Math.pow((c.y - a.y), 2));
        double wallc = Math.sqrt(Math.pow((c.x - pt.x), 2) + Math.pow((c.y - pt.y), 2));

        double p = 0.5 * (walla + wallb + wallc);
        double S = Math.sqrt(p * (p - walla) * (p - wallb) * (p - wallc));// wzor Herona
        return S;
    }

    double getAreaExtended(Point pt) {
        double sum = getAreaReplaceA(pt) + getAreaReplaceB(pt) + getAreaReplaceC(pt);
        return sum;
    }

    void printInsideStatus(Point pt) {
        if (getAreaExtended(pt) > getArea()) {
            System.out.println("Punkt znajuje sie na zewnatrz trójkąta");
        } else {
            System.out.println("Punkt znajuje sie w trójkącie");
        }
    }
}
