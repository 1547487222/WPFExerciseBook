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
        private static Dictionary<string, Type> _viewComponentTypes  = new Dictionary<string, Type>();
        private static Dictionary<Guid, ViewComponent> _viewComponents = new Dictionary<Guid, ViewComponent>();

        public static void RegisterViewComponent(string name, Type type)
        {
            _viewComponentTypes.Add(name, type);
        }

        public static Dictionary<string, Type> GetViewComponentDescriptions()
        {
            return _viewComponentTypes;
        }
        public static ViewComponent CrateViewComponent(string name)
        {
            if (_viewComponentTypes.ContainsKey(name))
            {
                var  viewComponent= new ViewComponent { ComponentId= Guid.NewGuid(), View= (IComponentView)Activator.CreateInstance(_viewComponentTypes[name]) };
                _viewComponents.Add(viewComponent.ComponentId, viewComponent);
                return viewComponent;
            }
            throw new Exception("未找到组件");
        }
    }
}
