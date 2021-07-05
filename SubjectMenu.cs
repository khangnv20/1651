using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyAssignment
{
    class SubjectMenu:IFunction, FileIO
    {
        private Subject firstSubject;
        private Subject lastSubject;
        private int size = 0;

        public SubjectMenu()
        {
            firstSubject = null;
            lastSubject = null;
            if (Read("public/SubjectData.txt"))
                Console.WriteLine("Import Subject data successfully!");
            else
                Console.WriteLine("An occured when importing Subject data!");
        }
        public bool Save(string path)
        {
            Subject newi = firstSubject;
            using (StreamWriter sw = new StreamWriter(path))
            {
                while(newi != null)
                {
                    try
                    {
                        string s = $"{newi.SubjectId}/line{newi.SubjectTitle}/line{newi.Slots}/line{newi.NumberOfAssignment}";
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
                        bool conv = int.TryParse(data[2], out int slots);
                        if (!conv) return false;
                        conv = int.TryParse(data[2], out int assignments);
                        if (!conv) return false;
                        Subject lineSubject = new Subject(data[0], data[1], slots, assignments);
                        AddNew(lineSubject);
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
                Console.WriteLine("==================== Subject Management ===================");
                Console.WriteLine(">> 1. Add");
                Console.WriteLine(">> 2. Update");
                Console.WriteLine(">> 3. Delete");
                Console.WriteLine(">> 4. Search by id");
                Console.WriteLine(">> 5. Show all subject");
                Console.WriteLine("<< 6. Back");
                Console.WriteLine("===========================================================");
                Console.Write("Enter your option:");
                bool optionBool = int.TryParse(Console.ReadLine(), out int option);
                if (!optionBool) continue;
                if (option == 6)
                {
                    if (!Save("public/SubjectData.txt"))
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
                        Subject subject;
                        Console.WriteLine("Insert Subject ID: ");
                        string id = Console.ReadLine();
                        if (DuplicateID(id))
                        {
                            Console.WriteLine("Duplicate ID!");
                            Console.ReadKey();
                            break;
                        }
                        Console.WriteLine("Insert Subject Title: ");
                        string title = Console.ReadLine();
                        slots: Console.WriteLine("Insert the number of slots: ");
                        bool convertInt = int.TryParse(Console.ReadLine(), out int slots);
                        if (!convertInt || slots < 0) goto slots;
                        assignment: Console.WriteLine("Insert the number of assignments: ");
                        convertInt = int.TryParse(Console.ReadLine(), out int assignment);
                        if (!convertInt || assignment < 0) goto assignment;
                        subject = new Subject(id, title, slots, assignment);
                        AddNew(subject);
                        break;
                    }
                case 2:
                    {
                        Update();
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Subject ID to delete: ");
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
                        Console.WriteLine("Insert Subject ID: ");
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
            Subject newi = (Subject) newe;
            if (DuplicateID(newi.SubjectId)) 
            {
                Console.WriteLine("Duplicate ID!");
                Console.ReadKey();
            } 
            if (size == 0)
            {
                this.firstSubject = this.lastSubject = newi;
            }
            else
            {
                lastSubject.Next = newi;
                lastSubject = newi;
            }
            size++;
        }

        public void Delete(string id)
        {
            if (firstSubject == null) return;
            Subject newi = firstSubject;
            if (firstSubject.SubjectId == id)
            {
                firstSubject = firstSubject.Next;
                size--;
                return;
            }
            while (newi.Next != null)
            {
                if (newi.Next.SubjectId == id)
                {
                    newi.Next = newi.Next.Next;
                    size--;
                    return;
                }
                newi = newi.Next;
            }
            updateLastSubject();
        }

        private void updateLastSubject()
        {
            Subject newi = firstSubject;
            if (newi == null) { lastSubject = null; return; }
            while (newi.Next != null)
            {
                newi = newi.Next;
            }
            lastSubject = newi;
        }

        public int GetSize()
        {
            if (size < 0)
            {
                int i = 0;
                Subject subject = firstSubject;
                while(subject.Next != null)
                {
                    i++;
                    subject = subject.Next;
                }
                return i;
            }
            return this.size;
        }

        public string Search(string id)
        {
            Subject newi = firstSubject;
            while (newi != null)
            {
                if (newi.SubjectId == id)
                {
                    return $"ID: {newi.SubjectId}\nTitle: {newi.SubjectTitle}\nThe number of slots: {newi.Slots}\nThe number of assignment: {newi.NumberOfAssignment}\n===============";  
                }
                newi = newi.Next;
            }
            return "Cannot find subject with ID: "+id;
        }

        public void Update()
        {
            Console.WriteLine("Enter id to update: ");
            string id = Console.ReadLine();
            if (Search(id).Contains("Cannot find subject with ID: "))
            {
                Console.WriteLine(Search(id));
                Console.ReadKey();
                return;
            }
            Console.WriteLine(Search(id));

            Subject newi = firstSubject;
            Subject subject = null;
            while (newi != null)
            {
                if (newi.SubjectId == id)
                {
                    subject = newi.GetClone();
                    break;
                }
                newi = newi.Next;
            }
            Console.WriteLine("Update Subject Title: \nIf you don't update this field, keep blank and please enter!");
            string title = Console.ReadLine();
            if (title != "") subject.SubjectTitle = title; 
            slots: Console.WriteLine("Update the number of slots: ");
            bool convertInt = int.TryParse(Console.ReadLine(), out int slots);
            if (!convertInt || slots < 0) goto slots;
            subject.Slots = slots;
        assignment: Console.WriteLine("Update the number of assignments: ");
            convertInt = int.TryParse(Console.ReadLine(), out int assignment);
            if (!convertInt || assignment < 0) goto assignment;
            subject.NumberOfAssignment = assignment;
            Delete(id);
            AddNew(subject);
            Console.WriteLine("===============");
            Console.WriteLine(Search(id));
            Console.ReadKey();


        }

        public void ViewAll()
        {
            Console.WriteLine($"The number of subjects: {GetSize()}\n===============");
            Subject newi1 = firstSubject;
            while (newi1 != null)
            {
                Console.WriteLine($"ID: {newi1.SubjectId}\nTitle: {newi1.SubjectTitle}\nThe number of slots: {newi1.Slots}\nThe number of assignment: {newi1.NumberOfAssignment}\n===============");
                newi1 = newi1.Next;
            }
        }
        public bool DuplicateID(string id)
        {
            Subject newi = firstSubject;
            int n = 0;
            while (newi != null)
            {
                if (newi.SubjectId.ToLower() == id.ToLower())
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
