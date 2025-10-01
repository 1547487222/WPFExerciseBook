using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LaboratoryManagementSystem.Models;

namespace LaboratoryManagementSystem.Controls
{
    /// <summary>
    /// Draggable item control for workflow composition
    /// </summary>
    public class DraggableItem : ContentControl
    {
        public static readonly DependencyProperty IsDraggingProperty =
            DependencyProperty.Register(nameof(IsDragging), typeof(bool), typeof(DraggableItem),
                new PropertyMetadata(false));

        public bool IsDragging
        {
            get => (bool)GetValue(IsDraggingProperty);
            set => SetValue(IsDraggingProperty, value);
        }

        private Point _startPoint;
        private bool _isDragStarted;

        public DraggableItem()
        {
            MouseLeftButtonDown += OnMouseLeftButtonDown;
            MouseMove += OnMouseMove;
            MouseLeftButtonUp += OnMouseLeftButtonUp;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(null);
            _isDragStarted = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !_isDragStarted)
            {
                Point currentPosition = e.GetPosition(null);
                Vector diff = _startPoint - currentPosition;

                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    _isDragStarted = true;
                    IsDragging = true;

                    var data = new DataObject();
                    data.SetData("DraggableItem", Content);

                    DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);

                    IsDragging = false;
                }
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDragStarted = false;
            IsDragging = false;
        }
    }

    /// <summary>
    /// Drop target control for workflow composition
    /// </summary>
    public class DropTarget : ContentControl
    {
        public static readonly DependencyProperty IsDropTargetProperty =
            DependencyProperty.Register(nameof(IsDropTarget), typeof(bool), typeof(DropTarget),
                new PropertyMetadata(false));

        public bool IsDropTarget
        {
            get => (bool)GetValue(IsDropTargetProperty);
            set => SetValue(IsDropTargetProperty, value);
        }

        public event EventHandler<DragEventArgs>? ItemDropped;

        public DropTarget()
        {
            AllowDrop = true;
            DragEnter += OnDragEnter;
            DragLeave += OnDragLeave;
            Drop += OnDrop;
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("DraggableItem"))
            {
                IsDropTarget = true;
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void OnDragLeave(object sender, DragEventArgs e)
        {
            IsDropTarget = false;
            e.Handled = true;
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            IsDropTarget = false;

            if (e.Data.GetDataPresent("DraggableItem"))
            {
                ItemDropped?.Invoke(this, e);
            }

            e.Handled = true;
        }
    }
}
