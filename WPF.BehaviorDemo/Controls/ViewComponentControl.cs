
using Microsoft.Xaml.Behaviors;
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
using System.Xml.Linq;
using WPF.BehaviorDemo.Behaviours;
using WPF.BehaviorDemo.ViewComponents;
using WPF.BehaviorDemo.Views;

namespace WPF.BehaviorDemo.Controls
{
    public class ViewComponentControl : Border
    {
        private readonly ViewComponent _viewComponent;
        private readonly Grid _container;
        private readonly Border _mask;
        private readonly Thumb _thumb;
        private readonly UIElement _view;
        private CanvasElementMoveBehavior? canvasElementMoveBehavior;
        public ViewComponentControl(string name)
        {
            _viewComponent = ViewComponentManager.CrateViewComponent(name);
            _viewComponent.InitializeComponent();
            _container = new Grid();
            _view = (UIElement)_viewComponent.View;
            _view.IsHitTestVisible = false;
            _container.Children.Add(_view);

            _mask = new Border
            {
                Background = new SolidColorBrush(Colors.LightGray),
                Opacity = 0.5,
                DataContext = this,
            };
            _container.Children.Add(_mask);
            _thumb = new Thumb()
            {
                Height = 10,
                Width = 10,
                Cursor = Cursors.SizeAll,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
            };
            _thumb.DragDelta += (sender, e) =>
            {
                _container.Width = Math.Max(_container.ActualWidth + e.HorizontalChange, 0);
                _container.Height = Math.Max(_container.ActualHeight + e.VerticalChange, 0);
            };
            _container.Children.Add(_thumb);
            this.Child = _container;
            this.Loaded += (sender, e) => 
            {
                OpenEdit();
            };
        }
        public void ShowEditOptionView()
        {
            var pupp = new Popup
            {
                PlacementTarget = this,
                AllowsTransparency = true,
                Placement = PlacementMode.Left,
                StaysOpen = false,
                Child = new EditOptionsView() { DataContext = _viewComponent.ViewModel },
                IsOpen = true,
            };
            pupp.Closed += (sender, e) =>
            {
                pupp.Child = null;
                pupp = null;
            };
        }

        public void UpdateMaskContextMenu(ContextMenu contextMenu)
        {
            _mask.ContextMenu = contextMenu;
        }
        public void OpenEdit()
        {
            _mask.Visibility = Visibility.Visible;
            _thumb.Visibility = Visibility.Visible;
            _view.IsHitTestVisible = false;
            if (canvasElementMoveBehavior == null)
            {
                canvasElementMoveBehavior = new CanvasElementMoveBehavior();
                var behaviors = Interaction.GetBehaviors(this);
                behaviors.Add(canvasElementMoveBehavior);
            }
        }

        public void CloseEdit()
        {
            _mask.Visibility = Visibility.Collapsed;
            _thumb.Visibility = Visibility.Collapsed;
            _view.IsHitTestVisible = true;
            if (canvasElementMoveBehavior != null)
            {
                var behaviors = Interaction.GetBehaviors(this);
                behaviors.Remove(canvasElementMoveBehavior);
                canvasElementMoveBehavior = null;
            }
        }
    }
}
