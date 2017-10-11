using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP = TruePlanningApi_16_0;

namespace TemplateUpdaterConsole
{
   public class Updater
   {
      #region Ctor, Members, Properties

      #endregion

      #region Class Methods

      public void Run()
      {
         List<String> files = GetFileList();
         List<TPProj> pList = ProcessFiles(files);
         CreateOutputFile(@"d:\nateList.csv", pList);
      }

      #endregion

      #region Private Methods

      private List<String> GetFileList()
      {
         String templateId = "TEMPLATE";

         List<String> files = new List<String>();

         DirectoryInfo di = new DirectoryInfo(@"d:\Online Template");

         FileInfo[] filelist = di.GetFiles();

         for (Int32 i = 0; i < filelist.Length; i++)
         {
            if (!filelist[i].Name.ToUpper().Contains(templateId))
            {
               files.Add(filelist[i].FullName);
            }
         }

         return (files);
      }

      private List<TPProj> ProcessFiles(List<String> files)
      {
         TP.Application app = new TP.Application();
         TP.Project p = null;
         List<TPProj> pList = new List<TPProj>();
         try
         {
            Int32 cnt = 1;
            Int32 tcnt = files.Count();
            foreach (String s in files)
            {
               try
                    {
                        p = null;
                        Console.WriteLine($"Processing {cnt}/{tcnt}: {s}");
                        p = app.OpenProject(s);

                        p.IsApplyEscalation = false;
                        DateTime dt = new DateTime(2017, 1, 1);
                        p.OutputYear = dt;

                        Console.WriteLine(p.Country.Name);

                        TP.Country myCountry = app.Countries.ItemByName("United States");
                        p.Country = myCountry;

                        Console.WriteLine(p.Country.Name);

                        p.Calculate();

                        TP.CostObject sf = p.CostObjects[1];

                        Double tdc = sf.Metrics.ItemByName("Development Cost").Value;
                        Double upc = sf.Metrics.ItemByName("Unit Production Cost").Value;
                        Double aupc = sf.Metrics.ItemByName("Amortized Unit Production Cost").Value;
                        Double prodq = sf.Metrics.ItemByName("Production Quantity").Value;
                        Double tw = sf.Metrics.ItemByName("Total Weight").Value;

                        String fName = Path.GetFileNameWithoutExtension(s);

                        TPProj curP = new TPProj() { Name = fName, TotalDevelopmentCost = tdc, UnitProductionCost = upc, AmortizedUnitProductionCost = aupc, ProductionQuantity = prodq, TotalWeight = tw };
                        pList.Add(curP);

                        p.Close();
                        p = null;
                    }
                    catch (Exception err)
               {
                  Console.WriteLine($"Error on {cnt}/{tcnt}: {err.Message}");
                  String fName = Path.GetFileNameWithoutExtension(s);
                  TPProj eProj = new TPProj() { Name = fName, Error = $"{s} : {err.Message}" };
                  pList.Add(eProj);
               }
               finally
               {
                  cnt++;
               }
            }
         }
         catch(Exception err)
         {
            if (p != null)
               p.Close();

            Console.WriteLine(err.ToString());
         }
         finally
         {
            if (p != null)
               p.Close();

            if (app != null)
               app.CloseApplication();
         }

         return (pList);
      }

        private static TP.Countries GetCountries(TP.Application app)
        {
            return app.Countries;
        }

        private void CreateOutputFile(String fileName, List<TPProj> pData)
      {
         using (StreamWriter sw = new StreamWriter(fileName))
         {
            foreach(TPProj p in pData)
            {
               sw.WriteLine(p.DumpAsCSVLine());
               sw.Flush();
            }

            sw.Close();
         }
      }
      
      #endregion
   }

}
