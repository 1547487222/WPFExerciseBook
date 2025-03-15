using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF.BehaviorDemo.Behaviours
{
   public class DoDropbehavior:Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var slectedItem = this.AssociatedObject.SelectedItem;
                DragDrop.DoDragDrop(this.AssociatedObject, slectedItem, DragDropEffects.Copy);
            }
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
        }
    }
}
