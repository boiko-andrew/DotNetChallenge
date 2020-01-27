using System;

namespace Flat
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("You are about to make home improvements.");
            Console.WriteLine("Now you have to choose a color scheme.");
            Console.WriteLine("Please, type 1 for light style or 2 for dark style.");
            Console.Write("Enter your choice: ");

            string colorScheme = Console.ReadLine();

            IRoomFactory factory;

            switch (colorScheme)
            {
                case "1":
                    factory = new LightRoomFactory();
                    break;
                case "2":
                    factory = new DarkRoomFactory();
                    break;
                default:
                    factory = new DarkRoomFactory();
                    break;
            }

            var flat = new Flat {Room1 = factory.CreateRoom(), Room2 = factory.CreateRoom()};

            Console.WriteLine("Your Room1 wallpaper is {0}.", flat.Room1.Wallpaper.Color);
            Console.WriteLine("Your Room1 chandelier is {0}.", flat.Room1.Chandelier.Color);

            Console.WriteLine("Your Room2 wallpaper is {0}.", flat.Room2.Wallpaper.Color);
            Console.WriteLine("Your Room2 chandelier is {0}.", flat.Room2.Chandelier.Color);

            Console.Write("Press any key to close the application: ");
            Console.ReadLine();
        }
    }
}