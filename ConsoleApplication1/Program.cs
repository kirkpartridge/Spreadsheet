using SS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Kirk Partridge
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            AbstractSpreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("a1", "= a2 + b3");
            ss.SetContentsOfCell("a2", "= b1 + b2");

            ss.SetContentsOfCell("a3", "5.0");
            ss.SetContentsOfCell("b1", "2.0");
            ss.SetContentsOfCell("b2", "3.0");
        }




    }
}

