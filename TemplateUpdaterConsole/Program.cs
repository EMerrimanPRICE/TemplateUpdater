using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateUpdaterConsole
{
   class Program
   {
      static void Main(string[] args)
      {
         Updater u = new Updater();
         u.Run();
         Console.ReadLine();
      }
   }
}
