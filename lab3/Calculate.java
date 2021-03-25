public class Calculate {
   static void whichSide(Line l1, Point p3) {
      LineEquation eq = new LineEquation(l1);
      double res = eq.A * p3.x + eq.B * p3.y + eq.C;
      if (res < -0.3) {
         System.out.println("Punkt z „lewej” strony prostej");
      } else if (res > 0.3) {
         System.out.println("Punkt z prawej strony prostej");
      } else {
         System.out.println("Punkt na prostej");
      }
   }

   static void crossingPointCramer(double A1, double B1, double C1, double A2, double B2, double C2) {
      double W = (A1 * B2) - (B1 * A2);
      double Wx = ((-C1) * B2) - ((-C2) * B1);
      double Wy = (A1 * (-C2)) - ((-C1) * A2);
      double x = Wx / W;
      double y = Wy / W;
      System.out.println("X: " + x + " y:" + y);
   }

   static double trangleArea(Point p1, Point p2, Point p3) {
      return 0.5 * ((p2.x - p1.x) * (p3.y - p1.y) - (p3.x - p1.x) * (p2.y - p1.y));
   }
}
