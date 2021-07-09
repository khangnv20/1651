using System;

namespace MyAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Menu menu = new Menu();
                menu.ShowMenu();
            }
            catch
            {
                Console.WriteLine("There was an occurred with your devices, please try later!\nPress enter for try again!");
            }
        }

    }
}
