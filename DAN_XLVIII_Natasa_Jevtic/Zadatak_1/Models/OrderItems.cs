using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Zadatak_1.Models
{
    class OrderItems
    {
        /// <summary>
        /// This method creates a list of guest ordered items.
        /// </summary>
        /// <param name="username">Username of guest.</param>
        /// <returns>List of ordered item.</returns>
        public List<vwOrderItem> GetOrderedItems(string username)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    return context.vwOrderItems.Where(x => x.CustomerJMBG == username).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method adds ordered item to DbSet and saves changes to database.
        /// </summary>
        /// <param name="menuItem">Item from menu.</param>
        /// <param name="order">Which order to add item.</param>
        /// <param name="quantity">Quatity of item.</param>
        public void AddOrderItem(vwMenu menuItem, vwOrder order, int quantity)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrderItem itemToAdd = new tblOrderItem
                    {
                        FoodID = menuItem.FoodID,
                        Quantity = quantity,
                        OrderID = order.OrderID
                    };
                    context.tblOrderItems.Add(itemToAdd);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// This method deletes ordered item from DbSet and saves changes to database.
        /// </summary>
        /// <param name="orderItemID">ID of order.</param>
        public void RemoveItem(int orderItemID)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrderItem itemToDelete = context.tblOrderItems.Where(x => x.ID == orderItemID).FirstOrDefault();
                    context.tblOrderItems.Remove(itemToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}
