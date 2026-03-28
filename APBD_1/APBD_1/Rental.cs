namespace APBD_1;

public class Rental
{
    public int RenterId { get;}
    public int DeviceId { get; }
    public DateTime RentalDate { get; }
    public int RentalDaysCount { get; }
    public bool ItemReturned { get; set; }
    public bool ReturnedInTime { get; set; }
    private static List<Rental> _extent = new List<Rental>();

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

    // private void _updateExtentTimings()
    // {
    //     foreach (var rent in Rental._extent)
    //     {
    //     }
    // }

    public List<Rental> GetOverdueRentals()
    {
        return Rental._extent.FindAll(x => x.IsOverdue());
    }
    
}