using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssignment
{
    interface FileIO
    {
        bool Save(string path);
        bool Read(string path);
    }
}
