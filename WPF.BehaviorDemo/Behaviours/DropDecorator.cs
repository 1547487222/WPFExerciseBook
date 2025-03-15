using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using WPF.BehaviorDemo.Models;

namespace WPF.BehaviorDemo.Behaviours
{
    public class DropDecorator : Adorner
    {
        private Shape shape;
        private TranslateTransform translateTransform;
        public DropDecorator(UIElement adornedElement,ShapeModel data) : base(adornedElement)
        {
            switch (data.ShapeType)
            {
                case ShapeType.Ellipse:
                    shape = new Ellipse { Width = data.Size.Width, Height = data.Size.Height, Fill = data.ShapeColor };
                    break;
                case ShapeType.Rectangle:
                    shape = new Rectangle { Width = data.Size.Width, Height = data.Size.Height, Fill = data.ShapeColor };
                    break;
                case ShapeType.Triangle:
                    shape = new Path() { Data = System.Windows.Media.Geometry.Parse($"M0,0 L{data.Size.Width},0 L{data.Size.Width / 2},{data.Size.Height} Z"), Fill = data.ShapeColor };
                    break;
            }
            shape.Opacity = 0.5;
            translateTransform = translateTransform ?? new TranslateTransform();
            shape.RenderTransform = translateTransform;
            AddVisualChild(shape);
            AddLogicalChild(shape);
            IsHitTestVisible = false;
        }

        protected override Visual GetVisualChild(int index)
        {
            return shape;
        }
        protected override int VisualChildrenCount => 1;


        protected override Size ArrangeOverride(Size finalSize)
        {
            shape.Arrange(new Rect(new Point(0,0),shape.DesiredSize));
            return finalSize;
        }

        public void UpdatePosition(Point position)
        {
            translateTransform.X = position.X - shape.DesiredSize.Width / 2;
            translateTransform.Y = position.Y - shape.DesiredSize.Height / 2;
        }
    }
}
