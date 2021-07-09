using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssignment
{
    class Menu
    {
        private IFunction subjectMenu = new SubjectMenu();
        private IFunction assignmentMenu = new AssignmentMenu();
        private IFunction lecturerMenu = new LecturerMenu();
        private IFunction studentMenu = new StudentMenu();
        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=================== Menu ====================");
                Console.WriteLine(">> 1. Student management");
                Console.WriteLine(">> 2. Lecturer management");
                Console.WriteLine(">> 3. Subject management");
                Console.WriteLine(">> 4. Assignment management");
                Console.WriteLine(">> 5. Exit");
                Console.WriteLine("=============================================");
                Console.Write("Enter your option:");
                bool optionBool = int.TryParse(Console.ReadLine(), out int option);
                if (!optionBool || option < 0 || option > 5) 
                { 
                    Console.WriteLine("please try again!");
                    Console.ReadKey();
                    continue; 
                }
                if (option == 5)
                {
                    Console.WriteLine("Are you sure to exit? [y/n]\n(All data in memory will be deleted, however, in the next open it will be imported.)");
                    string confirm = Console.ReadLine();
                    if (confirm.ToLower() == "yes" || confirm.ToLower() == "y") break;
                    else
                        continue;
                }
                ShowSubMenu(option);
            }
        }
        public void ShowSubMenu(int option)
        {
            switch (option)
            {
                case 1:
                    studentMenu.ShowSubMenu();
                    break;
                case 2:
                    lecturerMenu.ShowSubMenu();
                    break;
                case 3:
                    subjectMenu.ShowSubMenu();
                    break;
                case 4:
                    assignmentMenu.ShowSubMenu();
                    break;
                default:
                    Console.WriteLine("Error occured! please try again!");
                    break;
            }

        }
    }
}
