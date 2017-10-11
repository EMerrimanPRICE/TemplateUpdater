using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateUpdaterConsole
{
   public class TPProj
   {     
      #region CTor, Members, Properites

      private String _name;
      private Double _totalDevCost;
      private Double _UnitProdCost;
      private Double _AmtUnitProdCost;
      private Double _ProdQuantity;
      private Double _TotalWeight;
      private String _err;

      public String Name
      {
         get => _name;
         set => this._name = value;
      }

      public Double TotalDevelopmentCost
      {
         get => _totalDevCost;
         set => _totalDevCost = value;
      }

      public Double UnitProductionCost
      {
         get => _UnitProdCost;
         set => _UnitProdCost = value;
      }

      public Double AmortizedUnitProductionCost
      {
         get => _AmtUnitProdCost;
         set => _AmtUnitProdCost = value;
      }

      public Double TotalWeight
      {
         get => _TotalWeight;
         set => _TotalWeight = value;
      }

      public Double ProductionQuantity
      {
         get => _ProdQuantity;
         set => _ProdQuantity = value;
      }

      public String Error
      {
         get => _err;
         set => _err = value;
      }


      #endregion

      #region Class Methods

      public String DumpAsCSVLine()
      {
         String s = $"\"{_name}\",\"{_totalDevCost}\",\"{_UnitProdCost}\",\"{_AmtUnitProdCost}\",\"{_ProdQuantity}\",\"{_TotalWeight}\",\"{_err}\"";
         return (s);
      }

      #endregion

      #region Private Methods


      #endregion

   }
}
