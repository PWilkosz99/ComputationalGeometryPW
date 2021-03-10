
import java.io.IOException;

public class Main {

    public static void main(String[] args) throws IOException {

        // Ustawinie ilości punktów łączących linie
        Generator gen = new Generator(100);
        // Wierzchołki
        gen.tenPoints(4);
        // Kontur między  wierzchołkami
        gen.outlines();
        // Kontur ze środkiem
        gen.outlinesToCenter();

        // Zapis do pliku
        gen.saveFile("out1.txt");

        // Uruchomienie 
        new ProcessBuilder("C:\\Program Files\\VCG\\MeshLab\\meshlab.exe", "out1.txt").start();
    }
}
