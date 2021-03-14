
import java.io.IOException;

public class Main {

    public static void main(String[] args) throws IOException {

        Generator gen = new Generator(1000); //argument: precyzja
                
        gen.makeCone(1,3); //argumenty: promien podstawy, wysokosc.


        gen.saveFile("output1.txt");//zapis do pliku

        new ProcessBuilder("D:\\Program Files\\VCG\\MeshLab\\meshlab.exe", "output1.txt").start();//przekierowanie danych do meshlaba
    }
}
