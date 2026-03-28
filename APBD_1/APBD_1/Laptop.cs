using Microsoft.VisualBasic.CompilerServices;

namespace APBD_1;

public class Laptop : Device
{
    private string manufacturer { get; }
    private int weight { get; }

    public Laptop(string name, DeviceStatus status, string manufacturer, int weight)
    {
        this.Name = name;
        this.Status = status;
        this.Id = Device.GetNextId();
        this.manufacturer=manufacturer;
        this.weight=weight;
        Device._extent.Add(this);
    }

    public override void DescribeSelf()
    {
        Console.WriteLine($"Id: {Id}" +
                          $"Name: {Name}," +
                          $"Manufacturer: {manufacturer}" +
                          $"Weight: {weight}");
    }

    public static Laptop ParseAndValidateDevice(string[] input)
    {
        if (input.Length != 4)
        {
            throw new RentException("Registering a laptop requires at least 4 parameters");
        }

        var name = input[0];
        var status = input[1];
        int weight = 0;
        try
        {
            weight = int.Parse(input[2]);
        }
        catch (Exception)
        {
            throw new RentException("Wrong weight provided");
        }
        var manufacturer = input[3];
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
                throw new RentException("Unknown laptop status");
        }
        
        return new Laptop(name, s.Value, manufacturer, weight);
    }
}