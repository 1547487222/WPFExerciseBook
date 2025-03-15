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
    public class CanvasDragBehavior:Behavior<Canvas>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.DragEnter += AssociatedObject_DragEnter;
            this.AssociatedObject.DragLeave += AssociatedObject_DragLeave;
            this.AssociatedObject.DragOver += AssociatedObject_DragOver;
            this.AssociatedObject.Drop += AssociatedObject_Drop;
            this.AssociatedObject.AllowDrop = true;
        }

        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            
        }

        private void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            if (_dropDecorator != null)
            {
                    _dropDecorator.UpdatePosition(e.GetPosition(this.AssociatedObject));
            }
        }
        private DropDecorator _dropDecorator;
        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ShapeModel)) is not ShapeModel data)
                return;
            if (_dropDecorator == null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(AssociatedObject);
                if (adornerLayer != null)
                {
                    _dropDecorator ??= new DropDecorator(AssociatedObject, data);
                    adornerLayer.Add(_dropDecorator);
                }
            }
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            var pos = e.GetPosition(this.AssociatedObject);
            if (e.Data.GetData(typeof(ShapeModel)) is ShapeModel data)
            {
                switch (data.ShapeType)
                {
                    case ShapeType.Ellipse:
                        var ellipse = new Ellipse { Width = data.Size.Width, Height = data.Size.Height, Fill = data.ShapeColor };
                        this.AssociatedObject.Children.Add(ellipse);
                        Canvas.SetLeft(ellipse, pos.X);
                        Canvas.SetTop(ellipse, pos.Y);
                        var behaviors = Interaction.GetBehaviors(ellipse);
                        behaviors.Add(new CanvasElementMoveBehavior());
                        break;
                    case ShapeType.Rectangle:
                        var rectangle = new Rectangle { Width = data.Size.Width, Height = data.Size.Height, Fill = data.ShapeColor };
                        this.AssociatedObject.Children.Add(rectangle);
                        Canvas.SetLeft(rectangle, pos.X);
                        Canvas.SetTop(rectangle, pos.Y);
                        var behaviors2 = Interaction.GetBehaviors(rectangle);
                        behaviors2.Add(new CanvasElementMoveBehavior());
                        break;
                    case ShapeType.Triangle:
                        //TODO:
                        var triangle = new Path() { Data = System.Windows.Media.Geometry.Parse($"M0,0 L{data.Size.Width},0 L{data.Size.Width / 2},{data.Size.Height} Z"), Fill = data.ShapeColor };
                        this.AssociatedObject.Children.Add(triangle);
                        Canvas.SetLeft(triangle, pos.X);
                        Canvas.SetTop(triangle, pos.Y);
                        var behaviors3 = Interaction.GetBehaviors(triangle);
                        behaviors3.Add(new CanvasElementMoveBehavior());
                        break;
                    default:
                        break;
                }
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
            this.AssociatedObject.DragLeave -= AssociatedObject_DragLeave;
            this.AssociatedObject.DragOver -= AssociatedObject_DragOver;
            this.AssociatedObject.Drop -= AssociatedObject_Drop;
            this.AssociatedObject.AllowDrop = false;
        }
    }
}
