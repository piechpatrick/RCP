using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RCP.ClientLite.Controls
{
    
    /// <summary>
    /// Interaction logic for ActivitiesContainer.xaml
    /// </summary>
    public partial class ActivitiesContainer : UserControl
    {
        public ActivityContainerViewModel ViewModel { get; private set; }

        public ActivitiesContainer()
        {
            InitializeComponent();
            this.ViewModel = new ActivityContainerViewModel();
            this.DataContext = this.ViewModel;
        }
    }
}
