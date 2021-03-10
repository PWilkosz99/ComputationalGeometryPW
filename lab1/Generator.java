import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;

public class Generator {

    // dokładność
    private int accuracy = 10;

    // Podanie dokładności jako argument konstruktora
    Generator(int accuracy)
    {
        points = new ArrayList<>();
        this.accuracy = accuracy;
    }
    // Lista punktów
    ArrayList<Point3D> points;

    // centra
    Point2D center2D = new Point2D(0,0);
    Point3D center3D = new Point3D(0,0,1);

    // Metoda tworząca punkty po kole o podanym R co 2Pi/10 naprzemienne z R i R/2
    void tenPoints(double R)
    {        
        double step = 2* Math.PI / 10; // gwiazda ma 5 wierzchołków międzynimi znajduje się wierzchołek o mniejszym promieniu co daje nam 10 wierzchołków

        for (int i = 0; i < 10; i++)
        {
            double r = R - R/2*(i%2);  // r parzystego wierzchołka <- R, nieparzystego <- R/2
            double angle = step*i;
            points.add(new Point2D(r * Math.sin(angle), r * Math.cos(angle)));                        
        }
    }

    // Wypełnienie punktami - linią między podanymi punktami
    void connectBeetwenTwo(Point3D a, Point3D b)
    {
        // podzielenie na "kawałki"
        var xstep = (a.x - b.x)/accuracy;
        var ystep = (a.y - b.y)/accuracy;
        var zstep = (a.z - b.z)/accuracy;

        // tworzenie punktów o współrzędnej a wraz z sumą współrzędnej punktu a i step u
        for(int j = 1; j < accuracy; j++)
        {
            points.add(new Point3D(a.x - xstep*j, a.y - ystep*j, a.z - zstep*j ) );
        }
    }

    // Stworzenie konturu
    void outlines()
    {
        // Pobranie ilości wierzchołków
        var OldSize = points.size();      
        
        //Łączenie linią dwóch sąsiednich punktów 
        for(int i = 0 ;i < OldSize-1; i++ )
        {
            var a = points.get(i);
            var b = points.get(i+1);

            connectBeetwenTwo(a, b);
   
        }
        // Ostatni  z pierwszym
        var a = points.get(OldSize-1);
        var b = points.get(0);

        connectBeetwenTwo(a, b);
    }

    // Łączenie konturu ze środkiem o +z i -z
    void outlinesToCenter()
    {
        var OldSize = points.size();
        for(int i = 0 ;i < OldSize-1; i++ )
        {
            var a = points.get(i);
            connectBeetwenTwo(a, center3D);
            connectBeetwenTwo(a, center3D.zInverse());
        }

        // Ostatni  z pierwszym
        var a = points.get(OldSize-1);
        connectBeetwenTwo(a, center3D);
        connectBeetwenTwo(a, center3D.zInverse());
    }

    // Zapis do pliku
    void saveFile(String FileName) throws IOException {

        BufferedWriter bw = new BufferedWriter(new FileWriter(FileName));

        for(Point3D point : points){

            bw.write("" + point.x + ";" + point.y + ";" + point.z + "\n");

        }

        bw.close();
        System.out.println("Saved");
    }   
}