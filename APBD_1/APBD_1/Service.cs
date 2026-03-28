using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace APBD_1;

public class Service
{
    private static string _userFilePath = "users.json";
    private static string _deviceFilePath = "devices.json";
    private static string _rentalFilePath = "rentals.json";
    public Service()
    {
        if(!File.Exists(_userFilePath))
        {
            File.AppendAllText(_userFilePath, "[]");
        }
        if(!File.Exists(_deviceFilePath))
        {
            File.AppendAllText(_deviceFilePath, "[]");
        }
        if(!File.Exists(_rentalFilePath))
        {
            File.AppendAllText(_rentalFilePath, "[]");
        }
        User.ReadExtentFromFile(_userFilePath);
        Device.ReadExtentFromFile((_deviceFilePath));
        Rental.ReadExtentFromFile((_rentalFilePath));
    }

    private void _cleanup()
    {
        Console.WriteLine(User._extent);
        User.WriteExtentToFile(_userFilePath);
        Device.WriteExtentToFile(_deviceFilePath);
        Rental.WriteExtentToFile(_rentalFilePath);
    }

    public void GenerateReport()
    {  
        User.ListAllUsers();
        Device.ListAllDevices();
    }

    public void ReturnDevice(string[] input)
    {
        if (input.Length < 1)
        {
            throw new RentException("At least one input required to return device");
        }

        int deviceId = 0;
        try
        {
            deviceId = int.Parse(input[0]);
        }
        catch (Exception)
        {
            throw new RentException("Invalid device id");
        }
        Rental.UpdateById(deviceId, x => x.ItemReturned = true);
        if (Rental.DaysOverdue(deviceId) > 0)
        {
            Console.WriteLine($"Overdue amount:  {Rental.DaysOverdue(deviceId) * Device.GetById(deviceId).LateFeeParameter}"); 
        }
        Console.WriteLine("Not overdue yet");
    }

    public void MarkAsUnavailable(string[] input)
    {
        if (input.Length < 1)
        {
            throw new RentException("At least one input required to update device");
        }
        int deviceId = 0;
        try
        {
            deviceId = int.Parse(input[0]);
        }
        catch (Exception)
        {
            throw new RentException("Invalid device id");
        }
        
        Device.GetById(deviceId).Status = DeviceStatus.Unavailable;
    }

    public void ListUserRentals(string[] input)
    {
        if (input.Length < 1)
        {
            throw new RentException("At least one input required to list user rentals");
        }
        int userId = 0;
        try
        {
            userId = int.Parse(input[0]);
        }
        catch (Exception)
        {
            throw new RentException("Invalid user id");
        }
        var rentals = Rental.getByUserId(userId);
        foreach (var rental in rentals)
        {
            Device.GetById(rental.DeviceId).DescribeSelf();
        }
    }

    public void RentDevice(string[] input)
    {
        if (input.Length < 3)
        {
            throw new RentException("At least two inputs required to rent device");
        }
        int userId = 0;
        int deviceId = 0;
        int days = 0;
        try
        {
            userId = int.Parse(input[0]);
            deviceId = int.Parse(input[1]);
            days = int.Parse(input[2]);
        }
        catch (Exception)
        {
            throw new RentException("Invalid id");
        }
        new Rental(User.GetById(userId), Device.GetById(deviceId), days);
    }

    public void AddDevice(string[] input)
    {
        if (input.Length < 1)
        {
            throw new RentException("At least one input required to add device");
        }

        switch (input[0].ToUpper())
        {
            case "LAPTOP":
                Laptop.ParseAndValidateDevice(input.Skip(1).ToArray());
                break;
            case "PROJECTOR":
                Projector.ParseAndValidateDevice(input.Skip(1).ToArray());
                break;
            case "CAMERA":
                Camera.ParseAndValidateDevice(input.Skip(1).ToArray());
                break;
            default:
                throw new RentException("Device type specified not supported");
        }
    }
    
    public void parseArguments(string[] input)
    {
        if (input.Length == 0)
        {
            Console.WriteLine("Rental service console app requires at least one command parameter");
        }
        var parameters = input.Skip(1).ToArray();
        switch (input[0])
        {
            case "add-user":
                User.ParseAndValidateUser(parameters);
                break;
            case "add-device":
                AddDevice(parameters);
                break;
            case "list-users":
                User.ListAllUsers();
                break;
            case "list-devices":
                Device.ListAllDevices();
                break;
            case "list-available-devices":
                // Device.ListAvailableDevices():
                break;
            case "list-user-rentals":
                ListUserRentals(parameters);
                break;
            case "list-overdue-rentals":
                break;
            case "mark-as-unavailable":
                MarkAsUnavailable(parameters);
                break;
            case "rent-device":
                RentDevice(parameters);
                break;
            case "return-device":
                ReturnDevice(parameters);
                break;
            case "generate-report":
                GenerateReport();
                break;
            default:
                Console.WriteLine("Invalid command detected - closing the application");
                break;
        }
        _cleanup();
    }
}