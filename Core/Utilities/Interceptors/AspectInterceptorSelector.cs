using Castle.DynamicProxy;
using System.Reflection;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttribute = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttribute = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttribute.AddRange(methodAttribute);
            return classAttribute.ToArray();
        }
    }
}
