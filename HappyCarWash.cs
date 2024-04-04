using System;
using System.Collections.Generic;

namespace HappyCarWash {

    // Classes

    public enum UserType {
        Administrator,
        Standard
    }

    public class User {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
    }

    public enum VechileType {
        Car = 1,
        Van = 2,
        Truck = 3
    }

    public class Vehicle {
        public string Brand { get; set; }
        public string Model { get; set; }
        public VechileType Type { get; set; }
    }

    // Main Program

    class Program {

        static double defaultPrice = 50;

        // Users
        static List<User> users = new List<User> {
            new User { FirstName = "Petko", LastName = "Petkov", Username = "petkovp", Password = "p123", Type = UserType.Administrator },
            new User { FirstName = "Trajko", LastName = "Trajkov", Username = "trajkovt", Password = "t123", Type = UserType.Standard },
            new User { FirstName = "Marko", LastName = "Markov", Username = "markovm", Password = "m123", Type = UserType.Standard }
        };

        // Washed vehicles
        static List<Vehicle> washedVehiclesList = new List<Vehicle>();

        static int washedVechicles = 0;
        static double moneyMade = 0;

        // Calculate price
        static double CalculatePrice(VechileType type) {
            switch (type) {
                case VechileType.Car: return defaultPrice * 0.5;
                case VechileType.Van: return defaultPrice * 0.7;
                case VechileType.Truck: return defaultPrice;
                default: return 0;
            }
        }

        // Menu for Administrators
        static void AdminMenu() {
            Console.WriteLine("----------------------");
            Console.WriteLine("In order to change the default price press 1");
            Console.WriteLine("In order to generate a report press 2");
            Console.WriteLine("In order to log out press 3");
            Console.WriteLine("----------------------");
        }

        static void AdminActions() {
            while (true) {
                AdminMenu();
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice) {
                    case 1:
                        Console.Write("Enter the new price: ");
                        defaultPrice = Convert.ToDouble(Console.ReadLine());
                        break;
                    case 2:
                        Console.WriteLine($"Total number of washed vehicles: {washedVechicles}");
                        Console.WriteLine($"Total ammount earned: {moneyMade}");
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // Menu for Standard Users
        static void UserMenu() {
            Console.WriteLine("----------------------");
            Console.WriteLine("In order to enter a car to wash press 1");
            Console.WriteLine("In order to log out press 2");
            Console.WriteLine("----------------------");
        }

        static void UserActions() {
            while (true) {
                UserMenu();
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice) {
                    case 1:
                        Console.Write("Enter the brand name: ");
                        string brand = Console.ReadLine();
                        Console.Write("Enter the model name: ");
                        string model = Console.ReadLine();
                        Console.WriteLine("Enter the type of vehicle (1 - car; 2 - van; 3 - truck): ");
                        VechileType type = (VechileType)Convert.ToInt32(Console.ReadLine());

                        double price = CalculatePrice(type);
                        Console.WriteLine($"{price}");

                        var vehicle = new Vehicle { Brand = brand, Model = model, Type = type };
                        washedVehiclesList.Add(vehicle);

                        washedVechicles++;
                        moneyMade += price;

                        break;
                    case 2:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // Main menu
        static void Main(string[] args) {
            Console.WriteLine("----------------------");
            Console.WriteLine("----Happy-Car-Wash----");
            Console.WriteLine("----------------------");

            while (true) {
                Console.Write("Enter Username: ");
                string username = Console.ReadLine();
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                User currentUser = null;
                foreach (var user in users) {
                    if (user.Username == username && user.Password == password) {
                        currentUser = user;
                        break;
                    }
                }

                if (currentUser == null) {
                    Console.WriteLine("Invalid username or password.");
                    return;
                }

                Console.WriteLine($"Welcome, {currentUser.FirstName} {currentUser.LastName}!");

                if (currentUser.Type == UserType.Administrator) {
                    AdminActions();
                } else {
                    UserActions();
                }
            }
        }
    }
}