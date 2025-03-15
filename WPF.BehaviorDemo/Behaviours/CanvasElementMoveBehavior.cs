using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF.BehaviorDemo.Behaviours
{
    public class CanvasElementMoveBehavior : Behavior<UIElement>
    {
        private bool _isMouseDown = false;
        private Canvas _parentCanvas;
        private Point _offset = new Point(0, 0);
        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            _parentCanvas = VisualTreeHelper.GetParent(AssociatedObject) as Canvas;
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_parentCanvas == null)
                return;
            _isMouseDown = true;
            _offset = e.GetPosition(AssociatedObject);
            AssociatedObject.CaptureMouse();
            AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = false;
            AssociatedObject.ReleaseMouseCapture();
            AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                var pos = e.GetPosition(_parentCanvas);
                Canvas.SetLeft(AssociatedObject, pos.X-_offset.X);
                Canvas.SetTop(AssociatedObject, pos.Y-_offset.Y);
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
        }
    }
}
