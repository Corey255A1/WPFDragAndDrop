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

namespace DragAndDropBareMinimum
{
    /// <summary>
    /// Interaction logic for DropZone.xaml
    /// </summary>
    public partial class DropZone : UserControl
    {
        public DropZone()
        {
            InitializeComponent();
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            var droppedBrush = (Brush)e.Data.GetData("COLOR");
            this.Background = droppedBrush;
            borderRect.StrokeDashArray = null;
            borderRect.Stroke = Brushes.Black;
        }

        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {
            borderRect.StrokeDashArray = new DoubleCollection() { 4, 2 };
            borderRect.Stroke = Brushes.LightBlue;
        }

        private void UserControl_DragLeave(object sender, DragEventArgs e)
        {
            borderRect.StrokeDashArray = null;
            borderRect.Stroke = Brushes.Black;
        }
    }
}
