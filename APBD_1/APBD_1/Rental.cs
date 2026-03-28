using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
namespace APBD_1;

public class Rental
{
    public int RenterId { get;}
    public int DeviceId { get; }
    public DateTime RentalDate { get; set; }
    public int RentalDaysCount { get; set; }
    public bool ItemReturned { get; set; }
    public bool ReturnedInTime { get; set; }
    private static List<Rental> _extent = new List<Rental>();

    public static List<Rental> getByUserId(int userId)
    {
        return _extent.FindAll(x => x.RenterId == userId);
    }

    public static void UpdateById(int deviceId, Action<Rental> func)
    {
        var res = _extent.FindAll(x => x.DeviceId == deviceId);
        if (res.Count == 1)
        {
            func(res[0]);
        }
    }

    public static int DaysOverdue(int deviceId)
    {
        Rental r = _extent.FindAll(x => x.RenterId == deviceId)[0];
        return DateTime.Now.Subtract(r.RentalDate).Days - r.RentalDaysCount;
    }

    public Rental(int userId, int deviceId, 
            DateTime rentalDate, int rentalDaysCount, bool itemReturned, bool returnedInTime)
    {
        this.RenterId = userId;
        this.DeviceId = deviceId;
        this.RentalDate = rentalDate;
        this.ItemReturned = itemReturned;
        this.ReturnedInTime = returnedInTime;
        this.RentalDaysCount = rentalDaysCount;
    }

    public Rental(User user, Device device, int rentalDays)
    {
        
        if (device.Status == DeviceStatus.Unavailable)
        {
            throw new RentException("Cannot rent an unavailable device");
        }
        var userRentedCount = _extent.FindAll(x => x.RenterId == user.Id).Count;
        switch (user.UserType)
        {
            case UserType.Employee:
                if (userRentedCount >= 5)
                {
                    throw new RentException("Employee's already hit the limit for rented devices");
                }

                break;
            case UserType.Student:
                if (userRentedCount >= 2)
                {
                    throw new RentException("Student's already hit the limit for rented devices");
                }
                break;
            default:
                break;
        }
        this.RenterId = user.Id;
        this.DeviceId = device.Id;
        this.RentalDate = DateTime.Now;
        this.ItemReturned = false;
        this.ReturnedInTime = false;
        this.RentalDaysCount = rentalDays;
        Rental._extent.Add(this);
    }

    private bool IsOverdue()
    {
        var currentDate = DateTime.Now;
        return (!this.ItemReturned &&
                (currentDate.Subtract(this.RentalDate).TotalDays > this.RentalDaysCount));
    }
    

    public List<Rental> GetOverdueRentals()
    {
        return Rental._extent.FindAll(x => x.IsOverdue());
    }
    
    public static void ReadExtentFromFile(string filename)
    {
        string jsonString = File.ReadAllText(filename);
        _extent = JsonSerializer.Deserialize<List<Rental>>(jsonString);
        if (_extent == null)
        {
            throw new RentException("Failed to read extent from file");
        }
    }
    
    public static void WriteExtentToFile(string filename)
    {
        string jsonString = JsonSerializer.Serialize(Rental._extent);
        File.WriteAllText(filename, jsonString);
    }
    
}