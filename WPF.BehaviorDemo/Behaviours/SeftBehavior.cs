using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.BehaviorDemo.Behaviours
{
    public abstract class SeftBehavior
    {
        public abstract Type  AssociatedType { get;  }

        public abstract void Attach(DependencyObject dependencyObject);

        public abstract void Detach(DependencyObject dependencyObject);
    }


    public class SeftBehavior<T> : SeftBehavior where T : DependencyObject
    {
        public override Type AssociatedType => typeof(T);

        public T AssociatedObject { get;private set; }
        public override void Attach(DependencyObject dependencyObject)
        {
            if (dependencyObject is T associatedObject)
            {
                AssociatedObject = associatedObject;
                OnAttached();
            }
        }

        public virtual void OnAttached() { }

        public virtual void OnDetaching() { }

        public override void Detach(DependencyObject dependencyObject)
        {
            if (dependencyObject is T associatedObject && AssociatedObject == associatedObject)
            {
                OnDetaching();
                AssociatedObject = null;
            }
        }
    }
}
