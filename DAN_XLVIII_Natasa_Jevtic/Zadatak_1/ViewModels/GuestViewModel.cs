using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class GuestViewModel : BaseViewModel
    {
        GuestView guestView;
        Menu menu = new Menu();
        Order newOrder = new Order();
        OrderItems orderItems = new OrderItems();
        BackgroundWorker backgroundWorker;

        private string username;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private vwMenu menuItem;

        public vwMenu MenuItem
        {
            get
            {
                return menuItem;
            }
            set
            {
                menuItem = value;
                OnPropertyChanged("MenuItem");
            }
        }

        private List<vwMenu> menuList;

        public List<vwMenu> MenuList
        {
            get
            {
                return menuList;
            }
            set
            {
                menuList = value;
                OnPropertyChanged("MenuList");
            }
        }

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

        private vwOrderItem orderItem;

        public vwOrderItem OrderItem
        {
            get
            {
                return orderItem;
            }
            set
            {
                orderItem = value;
                OnPropertyChanged("OrderItem");
            }
        }

        private List<vwOrderItem> orderList;

        public List<vwOrderItem> OrderList
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

        private int quantity;

        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        private ICommand removeItem;

        public ICommand RemoveItem
        {
            get
            {
                if (removeItem == null)
                {
                    removeItem = new RelayCommand(param => RemoveItemExecute(), param => CanRemoveItemExecute());
                }
                return removeItem;
            }
        }

        private ICommand addItem;

        public ICommand AddItem
        {
            get
            {
                if (addItem == null)
                {
                    addItem = new RelayCommand(param => AddItemExecute(), param => CanAddItemExecute());
                }
                return addItem;
            }
        }

        private ICommand cancelOrder;

        public ICommand CancelOrder
        {
            get
            {
                if (cancelOrder == null)
                {
                    cancelOrder = new RelayCommand(param => CancelOrderExecute(), param => CanCancelOrderExecute());
                }
                return cancelOrder;
            }
        }

        private ICommand confirmOrder;

        public ICommand ConfirmOrder
        {
            get
            {
                if (confirmOrder == null)
                {
                    confirmOrder = new RelayCommand(param => ConfirmOrderExecute(), param => CanConfirmOrderExecute());
                }
                return confirmOrder;
            }
        }

        private Visibility isVisibleMenu;
        public Visibility IsVisibleMenu
        {
            get
            {
                return isVisibleMenu;
            }
            set
            {
                isVisibleMenu = value;
                OnPropertyChanged("IsVisibleMenu");
            }
        }

        private Visibility isVisibleOrderStatus = Visibility.Hidden;
        public Visibility IsVisibleOrderStatus
        {
            get
            {
                return isVisibleOrderStatus;
            }
            set
            {
                isVisibleOrderStatus = value;
                OnPropertyChanged("IsVisibleOrderStatus");
            }
        }

        private int totalPrice;

        public int TotalPrice
        {
            get
            {
                return totalPrice;
            }
            set
            {
                totalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        private Visibility isConfirmed;
        public Visibility IsConfirmed
        {
            get
            {
                return isConfirmed;
            }
            set
            {
                isConfirmed = value;
                OnPropertyChanged("IsConfirmed");
            }
        }

        public GuestViewModel(GuestView guestView, string username)
        {
            this.guestView = guestView;
            MenuList = menu.GetMenu();
            Username = username;
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += CheckIfApprovedOrRejected;
            //checking whether the guest has an order
            if (newOrder.CheckIfUserOrdered(username) == false)
            {
                newOrder.CreateOrder(Username);
                Ordered = newOrder.ViewOrder(Username);
                totalPrice = Ordered.TotalPrice;
            }
            //if has order
            else
            {
                IsVisibleMenu = Visibility.Hidden;
                OrderList = orderItems.GetOrderedItems(Username);
                IsVisibleOrderStatus = Visibility.Visible;
                IsConfirmed = Visibility.Hidden;
                Ordered = newOrder.ViewOrder(Username);
                totalPrice = Ordered.TotalPrice;
                //running background worker
                backgroundWorker.RunWorkerAsync();
            }            
        }
        /// <summary>
        /// This method invokes method for deleting ordered item.
        /// </summary>
        public void RemoveItemExecute()
        {
            try
            {
                if (MenuItem != null)
                {
                    //invokes method to delete item
                    orderItems.RemoveItem(OrderItem.ID);
                    //invokes method to update list of order
                    OrderList = orderItems.GetOrderedItems(Username);
                    TotalPrice = newOrder.CalculateTotalPrice(Ordered.OrderID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanRemoveItemExecute()
        {
            return true;
        }
        /// <summary>
        /// This method invokes method for adding item to order.
        /// </summary>
        public void AddItemExecute()
        {
            try
            {
                orderItems.AddOrderItem(MenuItem, Ordered, Quantity);
                //invokes method to update a list of ordered items
                OrderList = orderItems.GetOrderedItems(Username);
                TotalPrice = newOrder.CalculateTotalPrice(Ordered.OrderID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// This method checks if user interface element for quantity filled and if data is valid.
        /// </summary>
        /// <returns>True if valid, false if not.</returns>
        public bool CanAddItemExecute()
        {
            if (Int32.TryParse(Quantity.ToString(), out int number) && number > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// This method invokes method for deleting order.
        /// </summary>
        public void CancelOrderExecute()
        {
            try
            {
                bool isCanceled = newOrder.CancelOrder(Ordered.OrderID);
                if (isCanceled == true)
                {
                    MessageBox.Show("Order is canceled.", "Notification", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Order cannot be canceled.", "Notification", MessageBoxButton.OK);
                }
                OrderList = orderItems.GetOrderedItems(username);
                guestView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanCancelOrderExecute()
        {
            return true;
        }
        /// <summary>
        /// This method invokes method to confirm order.
        /// </summary>
        public void ConfirmOrderExecute()
        {
            try
            {
                bool isConfirmed = newOrder.ConfirmOrder(Ordered);
                if (isConfirmed == true)
                {
                    MessageBox.Show("Order is created. Please wait to be approved.", "Notification", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Order cannot be created.", "Notification", MessageBoxButton.OK);
                }
                IsVisibleMenu = Visibility.Hidden;
                IsVisibleOrderStatus = Visibility.Visible;
                IsConfirmed = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanConfirmOrderExecute()
        {
            if (OrderList != null)
            {
                if (OrderList.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// This method is handler for backgroundWorker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CheckIfApprovedOrRejected(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Ordered.OrderStatus != "on hold")
                {
                    Thread.Sleep(2000);
                    IsVisibleMenu = Visibility.Visible;
                    IsConfirmed = Visibility.Visible;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}