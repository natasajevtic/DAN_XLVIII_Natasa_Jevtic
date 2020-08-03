using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Zadatak_1.Models
{
    class Menu
    {
        /// <summary>
        /// This methods creates a list of data from view of menu.
        /// </summary>
        /// <returns>List of menu.</returns>
        public List<vwMenu> GetMenu()
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {                    
                    return context.vwMenus.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
