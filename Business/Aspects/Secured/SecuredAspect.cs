using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Aspects.Secured
{
    public class SecuredAspect : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _contextAccessor;

        public SecuredAspect(string roles)
        {
            _roles = roles.Split(",");
            _contextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _contextAccessor.HttpContext.User.ClaimsRoles();

            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception("Işlem Için Yetki Yok");
            
        }
    }
}
