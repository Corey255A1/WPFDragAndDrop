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
using System.Runtime.InteropServices;
namespace DragAndDropBareMinimum
{
    /// <summary>
    /// Interaction logic for Draggable.xaml
    /// </summary>
    public partial class Draggable : UserControl
    {
        public Draggable()
        {
            InitializeComponent();
        }

        #region Adornments
        //Adorner subclass specific to this control
        private class DraggableAdorner : Adorner
        {
            Rect renderRect;
            Brush renderBrush;
            public Point CenterOffset;
            public DraggableAdorner(Draggable adornedElement) : base(adornedElement)
            {
                renderRect = new Rect(adornedElement.RenderSize);
                this.IsHitTestVisible = false;
                //Clone so that it can be modified with on modifying the original
                renderBrush = adornedElement.Background.Clone();
                CenterOffset = new Point(-renderRect.Width / 2, -renderRect.Height / 2);
            }
            protected override void OnRender(DrawingContext drawingContext)
            {
                drawingContext.DrawRectangle(renderBrush, null, renderRect);
            }
        }

        //Struct to use in the GetCursorPos function
        private struct PInPoint
        {
            public int X;
            public int Y;
            public PInPoint(int x, int y)
            {
                X = x; Y = y;
            }
            public PInPoint(double x, double y)
            {
                X = (int)x; Y = (int)y;
            }
            public Point GetPoint(double xOffset=0, double yOffet=0)
            {
                return new Point(X + xOffset, Y + yOffet);
            }
            public Point GetPoint(Point offset)
            {
                return new Point(X + offset.X, Y + offset.Y);
            }
        }

        [DllImport("user32.dll")]
        static extern void GetCursorPos(ref PInPoint p);

        private DraggableAdorner myAdornment;
        private PInPoint pointRef = new PInPoint();

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var obj = new DataObject("COLOR", this.Background);
                var adLayer = AdornerLayer.GetAdornerLayer(this);
                myAdornment = new DraggableAdorner(this);
                adLayer.Add(myAdornment);
                DragDrop.DoDragDrop(this, obj, DragDropEffects.Copy);
                adLayer.Remove(myAdornment);
            }
        }

        private void UserControl_PreviewGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {            
            GetCursorPos(ref pointRef);
            Point relPos = this.PointFromScreen(pointRef.GetPoint(myAdornment.CenterOffset));
            myAdornment.Arrange(new Rect(relPos, myAdornment.DesiredSize));
        }

        #endregion


    }
}
