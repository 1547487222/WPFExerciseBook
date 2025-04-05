using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.BehaviorDemo.ViewComponents
{
  public  interface IComponentViewModel
    {
        void InitializeComponent();
        object ViewOptions { get; set; }

        void OnViewOptionsChanged(object viewOptions);
    }


    public abstract class ComponentViewModelBase : ObservableObject,IComponentViewModel
    {
        protected ComponentViewModelBase()
        {
            ViewOptionsChangedCommand = new RelayCommand<object>(OnViewOptionsChanged);
        }
        public object ViewOptions { get; set; }

        public abstract void InitializeComponent();

        public RelayCommand<object> ViewOptionsChangedCommand { get; }

        public  void OnViewOptionsChanged(object viewOptions)
        {
            ViewOptions = viewOptions;
            HandleViewOptionsChanged(viewOptions);
        }

        public virtual void HandleViewOptionsChanged(object viewOptions)
        {

        }
    }
}
