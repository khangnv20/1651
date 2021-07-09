using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace MyAssignment
{
    class StudentMenu : IFunction, FileIO
    {
        private Student firstStudent;
        private Student lastStudent;
        private int size = 0;

        public StudentMenu()
        {
            firstStudent = null;
            lastStudent = null;
            if (!Read("public/StudentData.txt"))
            {
                Console.WriteLine("An occured when importing Student data!");
                Console.ReadKey();
            }
        }
        public void AddNew(object newe)
        {
            Student newi = (Student) newe;
            if (DuplicateID(newi.StudentId))
            {
                Console.WriteLine("Duplicate ID!");
                Console.ReadKey();
            }
            if (size == 0)
            {
                this.firstStudent = this.lastStudent = newi;
            }
            else
            {
                lastStudent.Next = newi;
                lastStudent = newi;
            }
            size++;
        }

        public void Delete(string id)
        {
            if (firstStudent == null) return;
            Student newi = firstStudent;
            if (firstStudent.StudentId == id)
            {
                firstStudent = firstStudent.Next;
                size--;
                return;
            }

            while (newi.Next != null)
            {
                if (newi.Next.StudentId == id)
                {
                    newi.Next = newi.Next.Next;
                    size--;
                    updateLastStudent();
                    return;
                }
                newi = newi.Next;
            }
            
        }
        private void updateLastStudent()
        {
            Student newi = firstStudent;
            if (newi == null) { lastStudent = null; return; }
            while (newi.Next != null)
            {
                newi = newi.Next;
            }
            lastStudent = newi;
        }

        public bool DuplicateID(string id)
        {
            Student newi = firstStudent;
            int n = 0;
            while (newi != null)
            {
                if (newi.StudentId == id)
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

        public int GetSize()
        {
             if (size < 0)
             {
                int i = 0;
                Student newi = firstStudent;
                while (newi.Next != null)
                {
                    i++;
                    newi = newi.Next;
                }
                return i;
             }
             return this.size;
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
                        Student lineStudent = new Student(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8]);
                        AddNew(lineStudent);
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool Save(string path)
        {
            Student newi = firstStudent;
            using (StreamWriter sw = new StreamWriter(path))
            {
                while (newi != null)
                {
                    try
                    {
                        string s = $"{newi.Name}/line{newi.DateOfbirth}/line{newi.Email}/line{newi.Address}/line{newi.IdNumber}/line{newi.PassportId}/line{newi.Major}/line{newi.AcademicYear}/line{newi.StudentId}";
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

        public string Search(string id)
        {
            if (id == "") return "Please enter search keyword!";
            Student newi = firstStudent;
            string listStudent = "";
            while (newi != null)
            {
                if (newi.StudentId == id || newi.IdNumber == id || newi.PassportId == id)
                {
                    return $"ID: {newi.StudentId}\nName: {newi.Name}\nDoB: {newi.DateOfbirth}\nEmail: {newi.Email}\nAddress: {newi.Address}\nID card: {newi.IdNumber}\nPassport: {newi.PassportId}\nMajor: {newi.Major}\nAcedamic Year: {newi.AcademicYear}\n===============";
                }
                else if (newi.Name.Contains(id))
                {
                    listStudent += $"ID: {newi.StudentId}\nName: {newi.Name}\nDoB: {newi.DateOfbirth}\nEmail: {newi.Email}\nAddress: {newi.Address}\nID card: {newi.IdNumber}\nPassport: {newi.PassportId}\nMajor: {newi.Major}\nAcedamic Year: {newi.AcademicYear}\n===============";
                }
                newi = newi.Next;
            }
            if (listStudent != "") return listStudent; 
            else 
            return "Cannot find student with ID: " + id;
        }

        public void ShowSubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==================== Student Management ===================");
                Console.WriteLine(">> 1. Add");
                Console.WriteLine(">> 2. Update");
                Console.WriteLine(">> 3. Delete");
                Console.WriteLine(">> 4. Search by id (ID number, student ID, Passport ID) or Name");
                Console.WriteLine(">> 5. View all");
                Console.WriteLine("<< 6. Back");
                Console.WriteLine("===========================================================");
                Console.Write("Enter your option:");
                bool optionBool = int.TryParse(Console.ReadLine(), out int option);
                if (!optionBool) continue;
                if (option == 6)
                {
                    if (!Save("public/StudentData.txt"))
                    {
                        Console.WriteLine("An occured when saving Subject data!");
                        Console.ReadKey();
                    }
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
                        Student newj = new Student("","","","","","","","","");
                        Console.WriteLine("Insert Student ID: \nId must be GCC+number or you can enter nunber after GCC");
                    insertStudentID: string id = Console.ReadLine();
                        if (int.TryParse(id, out int temp)) id = "GCC" + id;
                        if (!int.TryParse(id, out temp) && !id.Contains("GCC"))
                        {
                            Console.WriteLine("Id must be GCC+number or you can enter nunber after GCC");
                            goto insertStudentID;
                        }
                        if (DuplicateID(id)||id=="")
                        {
                            Console.WriteLine("Duplicate ID!");
                            Console.WriteLine("Insert ID again!");
                            goto insertStudentID;
                        }
                        newj.StudentId = id;
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
                        Console.WriteLine("Enter Major:");
                        string major = Console.ReadLine();
                        newj.Major = major;
                        Console.WriteLine("Enter Acedamic year:");
                        string acedamicYear = Console.ReadLine();
                        newj.AcademicYear = acedamicYear;
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
                        Console.WriteLine("Student ID to delete: ");
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
                        Console.WriteLine("Insert ID (can Student ID, Passport ID, ID number) or Name: ");
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

        public void Update()
        {
            Console.WriteLine("Enter student ID to update: ");
            string id = Console.ReadLine();
            if (!DuplicateID(id))
            {
                Console.WriteLine("Cannot find student with ID: " + id);
                Console.ReadKey();
                return;
            }
            Console.WriteLine(Search(id));

            Student newi = firstStudent;
            Student newj = null;

            while (newi != null)
            {
                if (newi.StudentId == id)
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
            Console.WriteLine("Enter Major:");
            string major = Console.ReadLine();
            if (major != "") newj.Major = major;
            Console.WriteLine("Enter Acedamic year:");
            string acedamicYear = Console.ReadLine();
            if (acedamicYear != "") newj.AcademicYear = acedamicYear;
            Delete(id);
            AddNew(newj);
            Console.WriteLine("===============");
            Console.WriteLine(Search(id));
            Console.ReadKey();
        }

        public void ViewAll()
        {
            Console.WriteLine($"The number of student: {GetSize()}\n===============");
            Student newi = firstStudent;
            while (newi != null)
            {
                Console.WriteLine($"ID: {newi.StudentId}\nName: {newi.Name}\nDoB: {newi.DateOfbirth}\nEmail: {newi.Email}\nAddress: {newi.Address}\nID card: {newi.IdNumber}\nPassport: {newi.PassportId}\nMajor: {newi.Major}\nAcedamic Year: {newi.AcademicYear}\n===============");
                newi = newi.Next;
            }
        }
    }
}
