using Mechatronica.DB.Models;
using Mechatronica.WPF.Models;
using Mechatronica.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;



namespace Mechatronica.WPF.Views
{
 
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
   
          
            InitializeComponent();
            lvDb.Items.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
        }
   
     
    }
}