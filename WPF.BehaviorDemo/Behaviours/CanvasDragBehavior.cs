using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Shapes;
using WPF.BehaviorDemo.Models;

namespace WPF.BehaviorDemo.Behaviours
{
    public abstract class CanvasDragBehavior<TData>:Behavior<Canvas>
    {
        private DropDecorator? _dropDecorator;
        protected override void OnAttached()
        {
            this.AssociatedObject.DragEnter += AssociatedObject_DragEnter;
            this.AssociatedObject.DragOver += AssociatedObject_DragOver;
            this.AssociatedObject.Drop += AssociatedObject_Drop;
            this.AssociatedObject.AllowDrop = true;
        }

        private void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            _dropDecorator?.UpdatePosition(e.GetPosition(this.AssociatedObject));
        }

        public virtual DropDecorator? GetDropDecorator(TData data)
        {
            return null;
        }
        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(TData)) is not TData data)
                return;
            if (_dropDecorator == null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(AssociatedObject);
                if (adornerLayer != null)
                {
                    _dropDecorator ??= GetDropDecorator(data);
                    if(_dropDecorator != null)
                    adornerLayer.Add(_dropDecorator);
                }
            }
        }
        public abstract UIElement DropElement(TData data);
        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            var pos = e.GetPosition(this.AssociatedObject);
            if (e.Data.GetData(typeof(TData)) is TData data)
            {
                var uIElement = DropElement(data);
                this.AssociatedObject.Children.Add(uIElement);
                Canvas.SetLeft(uIElement, pos.X);
                Canvas.SetTop(uIElement, pos.Y);
                var behaviors = Interaction.GetBehaviors(uIElement);
                behaviors.Add(new CanvasElementMoveBehavior());
            }
            e.Effects = DragDropEffects.None;
            e.Handled = true;
            if (_dropDecorator != null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(AssociatedObject);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(_dropDecorator);
                    _dropDecorator = null;
                }
            }
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.DragEnter -= AssociatedObject_DragEnter;
            this.AssociatedObject.DragOver -= AssociatedObject_DragOver;
            this.AssociatedObject.Drop -= AssociatedObject_Drop;
            this.AssociatedObject.AllowDrop = false;
        }
    }
}
