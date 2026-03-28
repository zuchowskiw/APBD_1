namespace APBD_1;

public class Camera : Device
{
    public int PixelCount { get; }
    public int FocalLength { get; }

    public Camera(string name, DeviceStatus status, int pixelCount, int focalLength)
    {
        Name = name;
        Status = status;
        PixelCount = pixelCount;
        FocalLength = focalLength;
        Device._extent.Add(this);
    }

    public override Camera ParseAndValidateDevice(string[] input)
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