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
using WPF.BehaviorDemo.Controls;
using WPF.BehaviorDemo.ViewComponents;

namespace WPF.BehaviorDemo.Behaviours
{
    public class ViewComponentCanvasDragBehavior : CanvasDragBehavior<string>
    {
        public override UIElement DropElement(string data)
        {
            var container = new ViewComponentControl(data);
            container.UpdateMaskContextMenu(MaskContextMenu);
            return container;
        }







        public ContextMenu  MaskContextMenu 
        {
            get { return (ContextMenu)GetValue(MaskContextMenuProperty); }
            set { SetValue(MaskContextMenuProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaskContextMenuProperty =
            DependencyProperty.Register("MyProperty", typeof(ContextMenu), typeof(ViewComponentCanvasDragBehavior), new PropertyMetadata(null));



    }
}
