using Autofac;
using System.Reflection;

namespace webapi.Config
{
    /// <summary>
    /// Dependency Injection Container
    /// 重写Autofac管道Load方法，在这里注册注入
    /// </summary>
    public class AutofacModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Interface Library Name
            Assembly interfaceAssembly = Assembly.Load("Interface");
            //Service Library Name
            Assembly serviceAssembly = Assembly.Load("Service");
            builder.RegisterAssemblyTypes(interfaceAssembly, serviceAssembly).AsImplementedInterfaces();
        }
    }
}
