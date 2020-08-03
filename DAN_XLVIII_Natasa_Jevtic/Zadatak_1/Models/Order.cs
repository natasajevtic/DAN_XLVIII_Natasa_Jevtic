using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Zadatak_1.Models
{
    class Order
    {
        /// <summary>
        /// This method adds new order to DbSet and then saves changes to database.
        /// </summary>
        /// <param name="username">Username of quest.</param>        
        public void CreateOrder(string username)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrder order = new tblOrder
                    {
                        DateAndTimeOfOrder = DateTime.Now,
                        TotalPrice = 0,
                        CustomerJMBG = username,
                        OrderStatus = "on hold"
                    };
                    context.tblOrders.Add(order);
                    context.SaveChanges();                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());                
            }
        }
        /// <summary>
        /// This method finds order based on forwarded username.
        /// </summary>
        /// <param name="username">Username of guest.</param>
        /// <returns>Order of guest.</returns>
        public vwOrder ViewOrder(string username)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    return context.vwOrders.Where(x => x.CustomerJMBG == username).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method calculates total price of order and then saves changes to database.
        /// </summary>
        /// <param name="orderID">ID of order.</param>
        /// <returns>Total price of order.</returns>
        public int CalculateTotalPrice(int orderID)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    //finding order with forwarded id
                    tblOrder order = context.tblOrders.Where(x => x.OrderID == orderID).FirstOrDefault();
                    //finding order items with forwarded id
                    List<vwOrderItem> orders = context.vwOrderItems.Where(x => x.OrderID == orderID).ToList();
                    int sum = 0;
                    //calculating total sum of order
                    foreach (var item in orders)
                    {
                        sum += item.Price * item.Quantity;
                    }
                    order.TotalPrice = sum;
                    context.SaveChanges();
                    return sum;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return 0;
            }
        }
        /// <summary>
        /// This method deletes order and every ordered item in that order from database.
        /// </summary>
        /// <param name="orderID">Id of order.</param>
        /// <returns>True if order is deleted, false if not.</returns>
        public bool CancelOrder(int orderID)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    //finding order with forwarded id
                    tblOrder order = context.tblOrders.Where(x => x.OrderID == orderID).FirstOrDefault();
                    //finding order items with forwarded id
                    List<tblOrderItem> orders = context.tblOrderItems.Where(x => x.OrderID == orderID).ToList();
                    //removing order items from DbSet
                    foreach (var item in orders)
                    {
                        context.tblOrderItems.Remove(item);
                        context.SaveChanges();
                    }
                    context.tblOrders.Remove(order);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        ///  This method changes time of order and saves changes to database.
        /// </summary>
        /// <param name="order">Order.</param>
        /// <returns>True if order is created, false if not.</returns>
        public bool ConfirmOrder(vwOrder order)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrder orderToEdit = context.tblOrders.Where(x => x.OrderID == order.OrderID).FirstOrDefault();
                    orderToEdit.DateAndTimeOfOrder = DateTime.Now;
                    orderToEdit.OrderStatus = "on hold";
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// This method creates a list of all orders in database.
        /// </summary>
        /// <returns>List of all orders.</returns>
        public List<vwOrder> GetAllOrders()
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {                   
                    return context.vwOrders.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method changes order status to approved and saves changes to database.
        /// </summary>
        /// <param name="order">Order to be approved.</param>
        /// <returns>True if order is approved, false if not.</returns>
        public bool ApproveOrder(vwOrder order)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrder orderToApprove = context.tblOrders.Where(x => x.OrderID == order.OrderID).FirstOrDefault();
                    orderToApprove.OrderStatus = "approved";
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// This method changes order status to rejected and saves changes to database.
        /// </summary>
        /// <param name="order">Order to be rejected.</param>
        /// <returns>True if order is rejected, false if not.</returns>
        public bool RejectOrder(vwOrder order)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrder orderToReject = context.tblOrders.Where(x => x.OrderID == order.OrderID).FirstOrDefault();
                    orderToReject.OrderStatus = "rejected";
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// This method checks if quest already ordered.
        /// </summary>
        /// <param name="username">Username of guest.</param>
        /// <returns>True if ordered, false if not.</returns>
        public bool CheckIfUserOrdered(string username)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    List<vwOrder> orders = context.vwOrders.Where(x => x.CustomerJMBG == username).ToList();
                    if (orders.Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
    }
}
