using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.BehaviorDemo.ViewComponents
{
   public static class ViewComponentManager
    {
        //初始化组件描述
        private static Dictionary<string, (Type viewType, Type viewModelType)> _viewComponentTypes = [];
        private static Dictionary<Guid, ViewComponent> _viewComponents = new Dictionary<Guid, ViewComponent>();
        private static IServiceProvider _serviceProvider;
        private static ServiceCollection serviceDescriptors=new ServiceCollection();
        public static void RegisterViewComponent(string name, Type viewType,Type viewModelType)
        {
            _viewComponentTypes.Add(name, (viewType, viewModelType));
            serviceDescriptors.AddKeyedTransient(viewType,name);
            serviceDescriptors.AddKeyedTransient(viewModelType,name);
        }
        public static void Build()
        {
            _serviceProvider = serviceDescriptors.BuildServiceProvider();
        }
        public static Dictionary<string, (Type viewType, Type viewModelType)> GetViewComponentDescriptions()
        {
            return _viewComponentTypes;
        }
        public static ViewComponent CrateViewComponent(string name)
        {
            if (_viewComponentTypes.ContainsKey(name))
            {

                var view = _serviceProvider.GetRequiredKeyedService(_viewComponentTypes[name].viewType, name) as IComponentView;
                var viewModel = _serviceProvider.GetRequiredKeyedService(_viewComponentTypes[name].viewModelType, name) as IComponentViewModel;
                var viewComponent = new ViewComponent { ComponentId = Guid.NewGuid(), View = view, ViewModel = viewModel };
                _viewComponents.Add(viewComponent.ComponentId, viewComponent);
                return viewComponent;
            }
            throw new Exception("未找到组件");
        }
    }
}
