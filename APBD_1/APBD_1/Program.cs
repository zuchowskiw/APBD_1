namespace APBD_1;

public class Program
{
    public static void Main(string[] args)
    {
        Service s = new Service();
        Console.WriteLine(Device._extent.Count);
        s.parseArguments(args);
    }
}