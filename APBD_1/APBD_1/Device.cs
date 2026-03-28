namespace APBD_1;
using System.Text.Json;

public class Device
{
    protected static List<Device> _extent = new List<Device>();
    private static int CurrentId = 0;
    public string Name {get; set;}
    public double LateFeeParameter{ get; }
    public DeviceStatus Status {get; set;}
    public int Id { get; set; }

    public virtual void DescribeSelf()
    {
    }

    public static void ListAllDevices()
    {
        foreach (var dev in Device._extent)
        {
            dev.DescribeSelf();
        }
    }

    public static Device GetById(int deviceId)
    {
        return _extent.Find(x => x.Id == deviceId);
    }
    public static int GetNextId()
    {
        Device.CurrentId += 1;
        return Device.CurrentId;
    }
    // public abstract Device ParseAndValidateDevice(string[] input);

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

        var res = Device._extent.MaxBy(x => x.Id);
        if (res == null)
        {
            Device.CurrentId = 0;
        }
        else
        {
            Device.CurrentId = res.Id;
        }
    }
}