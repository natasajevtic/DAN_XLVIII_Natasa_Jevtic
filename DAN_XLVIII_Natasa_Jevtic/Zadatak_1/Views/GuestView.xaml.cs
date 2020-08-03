using System.Windows;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for GuestView.xaml
    /// </summary>
    public partial class GuestView : Window
    {
        /// <summary>
        /// Constructor with one parameter.
        /// </summary>
        /// <param name="username">Username of guest.</param>
        public GuestView(string username)
        {
            InitializeComponent();
            this.DataContext = new GuestViewModel(this, username);
        }
    }
}