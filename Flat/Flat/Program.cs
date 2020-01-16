using System;
using System.Collections.Generic;

namespace Flat
{
    class Program
    {
        static void Main(string[] args)
        {
            string choice;
            string colorScheme;

            Console.WriteLine("You are about to make home improvements.");
            Console.WriteLine("Now you have to choose a color scheme.");
            Console.WriteLine("Please, type 1 for light style or 2 for dark style.");
            Console.Write("Enter your choice: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    colorScheme = "light";
                    break;
                case "2":
                    colorScheme = "dark";
                    break;
                default:
                    colorScheme = "dark";
                    break;
            }

            Flat flat = new Flat(colorScheme);

            Console.WriteLine("Your flat color scheme is {0}.", flat._colourScheme);

            Console.WriteLine("Your Room1 wallpaper is {0}.", flat._room1._wallpaper);
            Console.WriteLine("Your Room1 chandelier is {0}.", flat._room1._chandelier);

            Console.WriteLine("Your Room2 wallpaper is {0}.", flat._room2._wallpaper);
            Console.WriteLine("Your Room2 chandelier is {0}.", flat._room2._chandelier);

            Console.Write("Press any key to close the application: ");
            Console.ReadLine();
        }
    }

    public class Room
    {
        public string _wallpaper;
        public string _chandelier;

        public Room(string colourScheme)
        {
            switch (colourScheme)
            {
                case "light":
                    _wallpaper = "light";
                    _chandelier = "light";
                    break;
                case "dark":
                    _wallpaper = "dark";
                    _chandelier = "dark";
                    break;
                default:
                    _wallpaper = "light";
                    _chandelier = "light";
                    break;
            }
        }
    }

    public class Flat
    {
        public string _colourScheme;
        public Room _room1;
        public Room _room2;

        public Flat(string colourScheme)
        {
            _colourScheme = colourScheme;
            _room1 = new Room(colourScheme);
            _room2 = new Room(colourScheme);
        }
    }
}