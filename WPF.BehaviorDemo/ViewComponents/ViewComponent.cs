using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.BehaviorDemo.ViewComponents
{
  public  class ViewComponent
    {
        public Guid  ComponentId  { get; set; }
        public IComponentView  View { get; set; }

        public IComponentViewModel ViewModel { get; set; }


        public void InitializeComponent()
        {
            ViewModel.InitializeComponent();
            View.DataContext = ViewModel;
        }
    }
}
