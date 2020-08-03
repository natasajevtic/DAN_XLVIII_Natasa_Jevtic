using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class EmployeeViewModel : BaseViewModel
    {
        EmployeeView employeeView;
        Order orders = new Order();

        private vwOrder ordered;

        public vwOrder Ordered
        {
            get
            {
                return ordered;
            }
            set
            {
                ordered = value;
                OnPropertyChanged("Ordered");
            }
        }

        private List<vwOrder> orderList;

        public List<vwOrder> OrderList
        {
            get
            {
                return orderList;
            }
            set
            {
                orderList = value;
                OnPropertyChanged("OrderList");
            }
        }

        private ICommand deleteOrder;

        public ICommand DeleteOrder
        {
            get
            {
                if (deleteOrder == null)
                {
                    deleteOrder = new RelayCommand(param => DeleteOrderExecute(), param => CanDeleteOrderExecute());
                }
                return deleteOrder;
            }
        }

        private ICommand approveOrder;

        public ICommand ApproveOrder
        {
            get
            {
                if (approveOrder == null)
                {
                    approveOrder = new RelayCommand(param => ApproveOrderExecute(), param => CanApproveOrderExecute());
                }
                return approveOrder;
            }
        }

        private ICommand rejectOrder;

        public ICommand RejectOrder
        {
            get
            {
                if (rejectOrder == null)
                {
                    rejectOrder = new RelayCommand(param => RejectOrderExecute(), param => CanRejectOrderExecute());
                }
                return rejectOrder;
            }
        }

        public EmployeeViewModel(EmployeeView employeeView)
        {
            this.employeeView = employeeView;
            OrderList = orders.GetAllOrders();
        }
        /// <summary>
        /// This method invokes method for deleting order.
        /// </summary>
        public void DeleteOrderExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure that you want to delete this order?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bool isDeleted = orders.CancelOrder(Ordered.OrderID);
                    if (isDeleted == true)
                    {
                        MessageBox.Show("Order is deleted.", "Notification", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Order cannot be deleted.", "Notification", MessageBoxButton.OK);
                    }
                    //invokes method to update a list of orders
                    OrderList = orders.GetAllOrders();
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// This method checks if order is selected and status of order.
        /// </summary>
        /// <returns>True if status different from on hold, false if not.</returns>
        public bool CanDeleteOrderExecute()
        {
            try
            {
                if (Ordered != null)
                {
                    if (Ordered.OrderStatus == "on hold")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// This method invokes method for approving order.
        /// </summary>
        public void ApproveOrderExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure that you want to approve this order?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bool isApproved = orders.ApproveOrder(Ordered);
                    if (isApproved == true)
                    {
                        MessageBox.Show("Order is approved.", "Notification", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Order cannot be approved.", "Notification", MessageBoxButton.OK);
                    }
                }                
                //invokes method to update a list of orders
                OrderList = orders.GetAllOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// This method checks can order be approved.
        /// </summary>
        /// <returns>True if can, false if not.</returns>
        public bool CanApproveOrderExecute()
        {
            try
            {
                if (Ordered != null)
                {
                    if (Ordered.OrderStatus == "approved" || Ordered.OrderStatus == "rejected")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// This method invokes method for rejecting order.
        /// </summary>
        public void RejectOrderExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure that you want to reject this order?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bool isRejected = orders.RejectOrder(Ordered);
                    if (isRejected == true)
                    {
                        MessageBox.Show("Order is rejected.", "Notification", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Order cannot be rejected.", "Notification", MessageBoxButton.OK);
                    }
                }                
                //invokes method to update a list of orders
                OrderList = orders.GetAllOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// This method checks can order be rejected.
        /// </summary>
        /// <returns>True if can, false if not.</returns>
        public bool CanRejectOrderExecute()
        {
            try
            {
                if (Ordered != null)
                {
                    if (Ordered.OrderStatus == "approved" || Ordered.OrderStatus == "rejected")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}