using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer_PC
{
    public interface ICalbrationFileAcess
    {
        double ReadValue();
        void ReplaceValue(double value);
    }
}
