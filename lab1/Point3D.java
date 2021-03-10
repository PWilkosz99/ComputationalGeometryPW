public class Point3D {

    Point3D(double x, double y, double z){

        this.x = x;
        this.y = y;
        this.z = z;

    }

    double x = 0;
    double y = 0;
    double z = 0;

    Point3D zInverse()
    {
        return new Point3D(x, y, -z);
    }
}