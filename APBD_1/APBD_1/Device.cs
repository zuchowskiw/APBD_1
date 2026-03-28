namespace APBD_1;

public abstract class Device
{
    private static int CurrentId = 0;
    public string Name {get; set;}
    public double LateFeeParameter{ get; }
    public DeviceStatus Status {get; set;}
    public int Id { get; }
    public static int GetNextId()
    {
        return Device.CurrentId++;
    }
}