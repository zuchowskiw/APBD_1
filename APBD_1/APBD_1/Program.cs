namespace APBD_1;

public class Program
{
    public static void Main(string[] args)
    {
        Service s = new Service();
        s.parseArguments(args);
    }
}