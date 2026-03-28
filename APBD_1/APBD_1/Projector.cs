namespace APBD_1;

public class Projector : Device
{
    public int Lumens { get; }
    public bool WithSound { get; }
    
    public override void DescribeSelf()
    {
        Console.WriteLine($"Name: {Name}," +
                          $"Lumens: {Lumens}" +
                          $"With sound: {WithSound}");
    }
    public static Projector ParseAndValidateDevice(string[] input)
    {
        if (input.Length != 4)
        {
            throw new RentException("Registering a projector requires 4 parameters");
        }
        var name = input[0];
        var status = input[1];
        int lumens = 0;
        try
        {
            lumens = int.Parse(input[2]);
        }
        catch (Exception)
        {
            throw new RentException("Wrong lumens provided");
        }

        var withSound = false;
        switch (input[3].ToUpper())
        {
            case "TRUE":
                withSound = true;
                break;
            case "FALSE":
                withSound = false;
                break;
            default:
                throw new RentException("Expected true/false for withSound parameter");
        }
        
        DeviceStatus? s = null;
        switch (status.ToUpper())
        {
            case "AVAILABLE":
                s = DeviceStatus.Available;
                break;
            case "UNAVAILABLE":
                s = DeviceStatus.Unavailable;
                break;
            default:
                throw new RentException("Unknown projector status");
        }
        
        return new Projector(name, s.Value, lumens, withSound);
    }

    public Projector(string name, DeviceStatus status, int lumens, bool withSound)
    {
        this.Name = name;
        this.Status = status;
        this.Id = Device.GetNextId();
        this.Lumens = lumens;  
        this.WithSound = withSound;
        Device._extent.Add(this);
    }
}