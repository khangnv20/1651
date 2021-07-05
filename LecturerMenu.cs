using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssignment
{
    class LecturerMenu: IFunction, FileIO
    {
        private Lecturer firstLecturer;
        private Lecturer lastLecturer;
        private int size = 0;
        public LecturerMenu()
        {
            firstLecturer = null;
            lastLecturer = null;
            if (!Read("public/LecturerData.txt"))
            {
                Console.WriteLine("An occured when importing lecturer data!");
                Console.ReadKey();
            }
        }
        public bool Save(string path)
        {
            Lecturer newi = firstLecturer;
            using (StreamWriter sw = new StreamWriter(path))
            {
                while (newi != null)
                {
                    try
                    {
                        string s = $"{newi.Name}/line{newi.DateOfbirth}/line{newi.Email}/line{newi.Address}/line{newi.IdNumber}/line{newi.PassportId}/line{newi.Deployma}/line{newi.LecturerId}";
                        sw.WriteLine(s);
                        newi = newi.Next;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

            }
            return true;
        }
        public bool Read(string path)
        {
            string line = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] data = line.Split("/line");
                        Lecturer lineLec = new Lecturer(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7]);
                        AddNew(lineLec);
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public void ShowSubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("================== Lecturer Management =====================");
                Console.WriteLine(">> 1. Add");
                Console.WriteLine(">> 2. Update");
                Console.WriteLine(">> 3. Delete");
                Console.WriteLine(">> 4. Search by ID / Name");
                Console.WriteLine(">> 5. Show all information");
                Console.WriteLine("<< 6. Back");
                Console.WriteLine("============================================================");

                Console.Write("Enter your option:");
                bool optionBool = int.TryParse(Console.ReadLine(), out int option);
                if (!optionBool) continue;
                if (option == 6) {
                    Save("public/LecturerData.txt");
                    return; 
                }
                ExecuteMenu(option);
            }
        }
        public void ExecuteMenu(int option)
        {
            switch (option)
            {
                case 1:
                    {
                       
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                       
                        break;
                    }
                case 4:
                    {
                       
                        break;
                    }
                case 5:
                    {
                       
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Error occured! please try again!");
                        Console.ReadKey();
                        break;
                    }
            }
        }

        public void AddNew(object newe)
        {
            Lecturer newi = (Lecturer)newe;
            return;
            
        }

        public void ViewAll()
        {
            throw new NotImplementedException();
        }

        public string Search(string id)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public int GetSize()
        {
            throw new NotImplementedException();
        }

        public bool DuplicateID(string id)
        {
            throw new NotImplementedException();
        }
    }
}
