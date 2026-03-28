namespace APBD_1;

public class Camera : Device
{
    public int PixelCount { get; }
    public int FocalLength { get; }
    
    public override void DescribeSelf()
    {
        Console.WriteLine($"Id: {Id}" + "Name: {Name}," +
                          $"Pixel Cound: {PixelCount}" +
                          $"Focal Length: {FocalLength}");
    }

    public Camera(string name, DeviceStatus status, int pixelCount, int focalLength)
    {
        this.Name = name;
        this.Status = status;
        this.Id = Device.GetNextId();
        this.PixelCount = pixelCount;
        this.FocalLength = focalLength;
        Device._extent.Add(this);
    }

    public static Camera ParseAndValidateDevice(string[] input)
    {
        if (input.Length != 4)
        {
            throw new RentException("Registering a projector requires 4 parameters");
        }
        var name = input[0];
        var status = input[1];
        int PixelCount = 0;
        try
        {
            PixelCount = int.Parse(input[2]);
        }
        catch (Exception)
        {
            throw new RentException("Wrong PixelCount provided");
        }

        int FocalLength = 0;
        try
        {
            FocalLength = int.Parse(input[3]);
        }
        catch (Exception)
        {
            throw new RentException("Wrong FocalLength provided");
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
        
        return new Camera(name, s.Value, PixelCount, FocalLength);
    }
}