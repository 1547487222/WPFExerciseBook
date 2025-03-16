using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using WPF.BehaviorDemo.ViewComponents;

namespace WPF.BehaviorDemo.Behaviours
{
    public class ViewComponentCanvasDragBehavior : CanvasDragBehavior<string>
    {
        public override UIElement DropElement(string data)
        {
            var viewComponent= ViewComponentManager.CrateViewComponent(data);
            var container = new Grid();
            var iElement= (UIElement)viewComponent.View;
            iElement.IsHitTestVisible = false;
            container.Children.Add(iElement);

            var mask = new Border
            {
                Background = new SolidColorBrush(Colors.LightGray),
                Opacity = 0.5,
            };
            container.Children.Add(mask);
            var thumb =new Thumb()
            {
                Cursor = Cursors.SizeAll,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
            thumb.DragDelta += (sender, e) =>
            {
                container.Width = Math.Max(container.ActualWidth + e.HorizontalChange, 50);
                container.Height = Math.Max(container.ActualHeight + e.VerticalChange, 50);
            };
            container.Children.Add(thumb);
            return container;
        }
    }
}
