using System.Windows;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : Window
    {
        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public EmployeeView()
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel(this);
        }
    }
}