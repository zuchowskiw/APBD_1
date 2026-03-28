using System.Runtime.CompilerServices;

namespace APBD_1;

public class User
{
    private static int _idCounter = 0;
    private static List<User> _extent = new List<User>();
    public int  Id {get;}
    public string Name {get; set;}
    public string Surname {get; set;}
    public UserType UserType { get; set; }

    private static int GetNextiD()
    {
        User._idCounter += 1;
        return User._idCounter;
    }

    public User(string name, string surname, UserType userType)
    {
        this.Name = name;
        this.Surname = surname;
        this.UserType = userType;
        this.Id = User.GetNextiD();
        User._extent.Add(this);
    }

    public static User ParseAndValidateUser(string[] input)
    {
        if (input.Length != 3)
        {
            throw new RentException("Expected at least 3 seperate parameters for creating user");
        }

        User? returnedUser = null;
        var userName = input[0];
        var userSurname = input[1];
        if (input[2].ToUpper().Equals("EMPLOYEE"))
        {
            returnedUser = new User(userName, userSurname, UserType.Employee);
        } else if ((input[2].ToUpper().Equals("STUDENT")))
        {
            returnedUser = new User(userName, userSurname, UserType.Student);
        }
        else
        {
            throw new RentException("Unknown user type entered");
        }

        return returnedUser;
    }
}