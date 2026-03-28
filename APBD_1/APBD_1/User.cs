using System.Runtime.CompilerServices;
using System.Text.Json;

namespace APBD_1;

public class User
{
    private static int _idCounter = 0;
    public static List<User> _extent = new List<User>();
    public int  Id {get;}
    public string Name {get; set;}
    public string Surname {get; set;}
    public UserType UserType { get; set; }

    private static int GetNextiD()
    {
        User._idCounter += 1;
        return User._idCounter;
    }

    public static User GetById(int userId)
    {
        return _extent.Find(x => x.Id == userId);
    }

    public static void ListAllUsers()
    {
        foreach (var user in User._extent)
        {
            Console.WriteLine($"Name: {user.Name}, Surname: {user.Surname},  UserType: {user.UserType}");
        }
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
        Console.WriteLine("Adding user!");
        return returnedUser;
    }
    
    public static void WriteExtentToFile(string filename)
    {
        string jsonString = JsonSerializer.Serialize(User._extent);
        File.WriteAllText(filename, jsonString);
    }

    public static void ReadExtentFromFile(string filename)
    {
        string jsonString = File.ReadAllText(filename);
        _extent = JsonSerializer.Deserialize<List<User>>(jsonString);
        if (_extent == null)
        {
            throw new RentException("Failed to read extent from file");
        }

        User._idCounter = User._extent.MaxBy(x => x.Id).Id;
    }
}