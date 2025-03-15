using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF.BehaviorDemo.Behaviours
{
  public  class ClickBehavior:SeftBehavior<Button>
    {
        public override void OnAttached()
        {
            this.AssociatedObject.Click += AssociatedObject_Click;
        }

        private void AssociatedObject_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("被点击了！");
        }

        public override void OnDetaching()
        {
            this.AssociatedObject.Click -= AssociatedObject_Click;
        }
    }
}
