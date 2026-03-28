namespace APBD_1;
using System.Text.Json;

public abstract class Device
{
    protected static List<Device> _extent = new List<Device>();
    private static int CurrentId = 0;
    public string Name {get; set;}
    public double LateFeeParameter{ get; }
    public DeviceStatus Status {get; set;}
    public int Id { get; }
    public static int GetNextId()
    {
        return Device.CurrentId++;
    }
    public abstract Device ParseAndValidateDevice(string[] input);

    public static void WriteExtentToFile(string filename)
    {
        string jsonString = JsonSerializer.Serialize(Device._extent);
        File.WriteAllText(filename, jsonString);
    }

    public static void ReadExtentFromFile(string filename)
    {
        string jsonString = File.ReadAllText(filename);
        _extent = JsonSerializer.Deserialize<List<Device>>(jsonString);
        if (_extent == null)
        {
            throw new RentException("Failed to read extent from file");
        }
    }
}