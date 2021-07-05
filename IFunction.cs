using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyAssignment
{
    interface IFunction
    {
        void AddNew(object newe);
        void ViewAll();
        string Search(string id);
        void Delete(string id);
        void Update();
        int GetSize();
        bool DuplicateID(string id);
        void ShowSubMenu();
    }
}
