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
        public DropDecorator(UIElement adornedElement) : base(adornedElement)
        {
  

        }

        protected override Visual GetVisualChild(int index)
        {
            return AdornedElement;
        }
        protected override int VisualChildrenCount => 1;


        protected override Size ArrangeOverride(Size finalSize)
        {
            AdornedElement.Arrange(new Rect(new Point(0,0), AdornedElement.DesiredSize));
            return finalSize;
        }

        public void UpdatePosition(Point position)
        {
            AdornedElement.Arrange(new Rect(position, AdornedElement.DesiredSize));
        }
    }
}
