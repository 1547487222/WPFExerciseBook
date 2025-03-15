using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.BehaviorDemo.Behaviours
{
   public class BehaviorAccessor
    {
        public static SeftBehavior GetSelfBehavior(DependencyObject obj)
        {
            return (SeftBehavior)obj.GetValue(SeftBehaviorProperty);
        }

        public static void SetSelfBehavior(DependencyObject obj, SeftBehavior value)
        {
            obj.SetValue(SeftBehaviorProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeftBehaviorProperty =
            DependencyProperty.RegisterAttached("SeftBehaviorProperty", typeof(SeftBehavior), typeof(BehaviorAccessor), new PropertyMetadata(null, OnSelfBehaviorChanged));

        private static void OnSelfBehaviorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldseftBehavior = e.OldValue as SeftBehavior;
            var newseftBehavior = e.NewValue as SeftBehavior;
            oldseftBehavior?.Detach(d);
            newseftBehavior?.Attach(d);
        }
    }
}
