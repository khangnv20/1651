using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssignment
{
    class AssignmentMenu : IFunction, FileIO
    {
        private Assignment firstAssignment;
        private Assignment lastAssignment;
        private int size = 0;
        public AssignmentMenu()
        {
            firstAssignment = null;
            lastAssignment = null;
            if (!Read("public/AssignmentData.txt"))
            {
                Console.WriteLine("An occured when importing assignment data!");
                Console.ReadKey();
            }
        }

        public void ShowSubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("================== Assignment Management =====================");
                Console.WriteLine(">> 1. Add");
                Console.WriteLine(">> 2. Update");
                Console.WriteLine(">> 3. Delete");
                Console.WriteLine(">> 4. Search by ID/title");
                Console.WriteLine(">> 5. Show all information");
                Console.WriteLine("<< 6. Back");
                Console.WriteLine("============================================================");

                Console.Write("Enter your option:");
                bool optionBool = int.TryParse(Console.ReadLine(), out int option);
                if (!optionBool) continue;
                if (option == 6)
                {
                    Save("public/AssignmentData.txt");
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
                        Assignment newj = new Assignment("","","","","","");
                     Console.WriteLine("Insert assignment ID: \nId must be ASM+number or you can enter nunber after ASM");

                    insertID: string id = Console.ReadLine();
                        if (int.TryParse(id, out int temp)) id = "ASM" + id;
                        if (!int.TryParse(id, out temp) && !id.Contains("ASM"))
                        {
                            Console.WriteLine("Id must be ASM+number or you can enter nunber after ASM");
                            goto insertID;
                        }
                        if (DuplicateID(id) || id == "")
                        {
                            Console.WriteLine("Duplicate ID!");
                            Console.WriteLine("Insert ID again!");
                            goto insertID;
                        }
                        
                        newj.AssignmentID = id;
                        Console.WriteLine("Enter title:");
                        string title = Console.ReadLine();
                        newj.AssignmentTitle = title;
                        grade:Console.WriteLine("Enter Grade:");
                        string grade = Console.ReadLine();
                        if (grade.ToLower() == "pass" || grade.ToLower() == "faile")
                            newj.Grade = grade;
                        else
                        {
                            Console.WriteLine("Grade must be Pass or Fail!");
                            goto grade;
                        }
                        Console.WriteLine("Enter Feedback:");
                        string feedback = Console.ReadLine();
                        newj.Feedback = feedback;
                        Console.WriteLine("Enter Student name:");
                        string sname = Console.ReadLine();
                        newj.StudentName = sname;
                        Console.WriteLine("Enter Subject");
                        string subject = Console.ReadLine();
                        newj.SubjectTitle = subject;
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
                        Console.WriteLine("Assignment ID to delete: ");
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
                        Console.WriteLine("Insert Assignment ID: ");
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
            Assignment newi = (Assignment)newe;
            if (DuplicateID(newi.AssignmentID))
            {
                Console.WriteLine("Duplicate ID!");
                Console.ReadKey();
            }
            if (size == 0)
            {
                this.firstAssignment = this.lastAssignment = newi;
            }
            else
            {
                lastAssignment.Next = newi;
                lastAssignment = newi;
            }
            size++;
        }

        public void Delete(string id)
        {
            if (firstAssignment == null) return;
            Assignment newi = firstAssignment;
            if (firstAssignment.AssignmentID == id)
            {
                firstAssignment = firstAssignment.Next;
                size--;
                return;
            }
            while (newi.Next != null)
            {
                if (newi.Next.AssignmentID == id)
                {
                    newi.Next = newi.Next.Next;
                    size--;
                    updateLastAssinment();
                    return;
                }
                newi = newi.Next;
            }
            
        }
        private void updateLastAssinment()
        {
            Assignment newi = firstAssignment;
            if (newi == null) { lastAssignment = null; return; }
            while (newi.Next != null)
            {
                newi = newi.Next;
            }
            lastAssignment = newi;
        }

        public bool DuplicateID(string id)
        {
            Assignment newi = firstAssignment;
            int n = 0;
            while (newi != null)
            {
                if (newi.AssignmentID == id)
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
                Assignment newi = firstAssignment;
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
                        Assignment lineAssignment = new Assignment(data[0], data[1], data[2], data[3], data[4], data[5]);
                        AddNew(lineAssignment);
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
            Assignment newi = firstAssignment;
            using (StreamWriter sw = new StreamWriter(path))
            {
                while (newi != null)
                {
                    try
                    {
                        string s = $"{newi.AssignmentID}/line{newi.Grade}/line{newi.SubjectTitle}/line{newi.StudentName}/line{newi.Feedback}/line{newi.AssignmentTitle}";
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
            Assignment newi = firstAssignment;
            string listStudent = "";
            while (newi != null)
            {
                if (newi.AssignmentID == id)
                {
                    return $"ID: {newi.AssignmentID}\nTitle: {newi.AssignmentTitle}\nGrade: {newi.Grade}\nFeedback {newi.Feedback}\nStudent Name: {newi.StudentName}\nSubject: {newi.SubjectTitle}\n===============";
                }
                else if (newi.AssignmentTitle.Contains(id) || newi.SubjectTitle.Contains(id) || newi.StudentName.Contains(id))
                {
                    listStudent += $"ID: {newi.AssignmentID}\nTitle: {newi.AssignmentTitle}\nGrade: {newi.Grade}\nFeedback {newi.Feedback}\nStudent Name: {newi.StudentName}\nSubject: {newi.SubjectTitle}\n===============";
                }
                newi = newi.Next;
            }
            if (listStudent != "") return listStudent;
            else
                return "Cannot find student with ID: " + id;
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

            Assignment newi = firstAssignment;
            Assignment newj = null;

            while (newi != null)
            {
                if (newi.AssignmentID == id)
                {
                    newj = newi.GetClone();
                    break;
                }
                newi = newi.Next;
            }
            newj.Next = null;
            Console.WriteLine("All field will not be updated if it's blank! " +
                "\nso, press enter if you don't want to update any field");
            Console.WriteLine("Enter assignment title:");
            string assignmentTitle = Console.ReadLine();
            if (assignmentTitle != "") newj.AssignmentTitle = assignmentTitle;
            Console.WriteLine("Enter grade:");
            string grade = Console.ReadLine();
            if(grade!="")
                if (grade.ToLower() == "pass" || grade.ToLower() == "fail") 
                    newj.Grade = grade;
            Console.WriteLine("Enter assignment feedback; ");
            string feedback = Console.ReadLine();
            if (feedback != "") newj.Feedback = feedback;
            Console.WriteLine("Enter Student Name");
            string studentName = Console.ReadLine();
            if (studentName != "") newj.StudentName = studentName;
            Console.WriteLine("Enter Subject title");
            string subjectTitle = Console.ReadLine();
            if (subjectTitle != "") newj.SubjectTitle = subjectTitle;
            Delete(id);
            AddNew(newj);
            Console.WriteLine("===============");
            Console.WriteLine(Search(id));
            Console.ReadKey();
        }

        public void ViewAll()
        {
            Console.WriteLine($"The number of assignment: {GetSize()}\n===============");
            Assignment newi1 = firstAssignment;
            while (newi1 != null)
            {
                Console.WriteLine($"ID: {newi1.AssignmentID}\nTitle: {newi1.AssignmentTitle}\nGrade: {newi1.Grade}\nFeedback {newi1.Feedback}\nStudent Name: {newi1.StudentName}\nSubject: {newi1.SubjectTitle}\n===============");
                newi1 = newi1.Next;
            }
            return;
        }
    }
}
