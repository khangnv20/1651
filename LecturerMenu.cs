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
                        string s = $"{newi.Name}/line{newi.DateOfbirth}/line{newi.Email}/line{newi.Address}/line{newi.IdNumber}/line{newi.PassportId}/line{newi.Diploma}/line{newi.LecturerId}";
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
                        Lecturer newj = new Lecturer("", "", "", "", "", "", "", "");
                    Console.WriteLine("Insert Lecturer ID: \nId must be LCC+number or you can enter nunber after LCC");
                    insertID: string id = Console.ReadLine();
                        if (int.TryParse(id, out int temp)) id = "LCC" + id;
                        if (!int.TryParse(id, out temp) && !id.Contains("LCC"))
                        {
                            Console.WriteLine("Id must be LCC+number or you can enter nunber after LCC");
                            goto insertID;
                        }
                        if (DuplicateID(id) || id == "")
                        {
                            Console.WriteLine("Duplicate ID!");
                            Console.WriteLine("Insert ID again!");
                            goto insertID;
                        }
                        
                        newj.LecturerId = id;
                        Console.WriteLine("Enter name:");
                        string name = Console.ReadLine();
                        newj.Name = name;
                        Console.WriteLine("Enter Birth date:");
                        string dateOfBirth = Console.ReadLine();
                        newj.DateOfbirth = dateOfBirth;
                        Console.WriteLine("Enter Email:");
                        string email = Console.ReadLine();
                        newj.Email = email;
                        Console.WriteLine("Enter Address:");
                        string address = Console.ReadLine();
                        newj.Address = address;
                        Console.WriteLine("Enter Id Number (ID card):");
                        string idNumber = Console.ReadLine();
                        newj.IdNumber = idNumber;
                        Console.WriteLine("Enter Passport ID:");
                        string passportId = Console.ReadLine();
                        newj.PassportId = passportId;
                        Console.WriteLine("Enter Diploma:");
                        string diploma = Console.ReadLine();
                        newj.Diploma = diploma;
                        AddNew(newj);
                        break;
                    }
                case 2:
                    {
                        Update();
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Lecturer ID to delete: ");
                        string id = Console.ReadLine();
                        if (!DuplicateID(id))
                        {
                            Console.WriteLine("ID is not exist in the system!");
                            Console.ReadKey();
                        }
                        else Delete(id);
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Insert ID (can Lecturer ID, Passport ID, ID number) or Name: ");
                        string id = Console.ReadLine();
                        Console.WriteLine(Search(id));
                        Console.ReadKey();
                        break;
                    }
                case 5:
                    {
                        ViewAll();
                        Console.ReadKey();
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
            if (DuplicateID(newi.LecturerId))
            {
                Console.WriteLine("Duplicate ID!");
                Console.ReadKey();
            }
            if (size == 0)
            {
                this.firstLecturer = this.lastLecturer = newi;
            }
            else
            {
                lastLecturer.Next = newi;
                lastLecturer = newi;
            }
            size++;

        }

        public void ViewAll()
        {
            Console.WriteLine($"The number of lecturer: {GetSize()}\n===============");
            Lecturer newi = firstLecturer;
            while (newi != null)
            {
                Console.WriteLine($"ID: {newi.LecturerId}\nName: {newi.Name}\nDoB: {newi.DateOfbirth}\nEmail: {newi.Email}\nAddress: {newi.Address}\nID card: {newi.IdNumber}\nPassport: {newi.PassportId}\nDeploma: {newi.Diploma}\n===============");
                newi = newi.Next;
            }
        }
        private void updateLastLecturer()
        {
            Lecturer newi = firstLecturer;
            if (newi == null) { lastLecturer = null; return; }
            while (newi.Next != null)
            {
                newi = newi.Next;
            }
            lastLecturer = newi;
        }

        public string Search(string id)
        {
            if (id == "") return "Please enter search keyword!";
            Lecturer newi = firstLecturer;
            string listStudent = "";
            while (newi != null)
            {
                if (newi.LecturerId == id || newi.IdNumber == id || newi.PassportId == id)
                {
                    return $"ID: {newi.LecturerId}\nName: {newi.Name}\nDoB: {newi.DateOfbirth}\nEmail: {newi.Email}\nAddress: {newi.Address}\nID card: {newi.IdNumber}\nPassport: {newi.PassportId}\nDeploma: {newi.Diploma}\n===============";
                }
                else if (newi.Name.Contains(id))
                {
                    listStudent += $"ID: {newi.LecturerId}\nName: {newi.Name}\nDoB: {newi.DateOfbirth}\nEmail: {newi.Email}\nAddress: {newi.Address}\nID card: {newi.IdNumber}\nPassport: {newi.PassportId}\nDeploma: {newi.Diploma}\n===============";
                }
                newi = newi.Next;
            }
            if (listStudent != "") return listStudent;
            else
                return "Cannot find student with ID: " + id;
        }

        public void Delete(string id)
        {
            if (firstLecturer == null) return;
            Lecturer newi = firstLecturer;
            if (firstLecturer.LecturerId == id)
            {
                firstLecturer = firstLecturer.Next;
                size--;
                return;
            }
            while (newi.Next != null)
            {
                if (newi.Next.LecturerId == id)
                {
                    newi.Next = newi.Next.Next;
                    size--;
                    updateLastLecturer();
                    return;
                }
                newi = newi.Next;
            }
            
        }

        public void Update()
        {
            Console.WriteLine("Enter Lecturer ID to update: ");
            string id = Console.ReadLine();
            if (!DuplicateID(id))
            {
                Console.WriteLine("Cannot find lecturer with ID: " + id);
                Console.ReadKey();
                return;
            }
            Console.WriteLine(Search(id));

            Lecturer newi = firstLecturer;
            Lecturer newj = null;

            while (newi != null)
            {
                if (newi.LecturerId == id)
                {
                    newj = newi.GetClone();
                    break;
                }
                newi = newi.Next;
            }
            newj.Next = null;
            Console.WriteLine("All field will not be updated if it's blank! " +
                "\nso, press enter if you don't want to update any field");
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();
            if (name != "") newj.Name = name;
            Console.WriteLine("Enter Birth date:");
            string dateOfBirth = Console.ReadLine();
            if (dateOfBirth != "") newj.DateOfbirth = dateOfBirth;
            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine();
            if (email != "") newj.Email = email;
            Console.WriteLine("Enter Address:");
            string address = Console.ReadLine();
            if (address != "") newj.Address = address;
            Console.WriteLine("Enter Id Number (ID card):");
            string idNumber = Console.ReadLine();
            if (idNumber != "") newj.IdNumber = idNumber;
            Console.WriteLine("Enter Passport ID:");
            string passportId = Console.ReadLine();
            if (passportId != "") newj.PassportId = passportId;
            Console.WriteLine("Enter Diploma:");
            string diploma = Console.ReadLine();
            if (diploma != "") newj.Diploma = diploma;
            Delete(id);
            AddNew(newj);
            Console.WriteLine("===============");
            Console.WriteLine(Search(id));
            Console.ReadKey();
        }

        public int GetSize()
        {
            if (size < 0)
            {
                int i = 0;
                Lecturer newi = firstLecturer;
                while (newi.Next != null)
                {
                    i++;
                    newi = newi.Next;
                }
                return i;
            }
            return this.size;
        }

        public bool DuplicateID(string id)
        {
            Lecturer newi = firstLecturer;
            int n = 0;
            while (newi != null)
            {
                if (newi.LecturerId == id)
                {
                    n = n + 1;
                }
                newi = newi.Next;
            }
            if (n > 0)
                return true;
            else
                return false;
        }
    }
}
