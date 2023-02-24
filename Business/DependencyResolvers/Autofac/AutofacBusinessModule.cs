using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Conrete;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System.IdentityModel.Tokens.Jwt;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<UserOperaitonClaimManager>().As<IUserOperaitonClaimService>();
            builder.RegisterType<EfUserOperationDal>().As<IUserOperationDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();


            builder.RegisterType<TokenHandler>().As<ITokenHandler>();

            builder.RegisterType<FileManager>().As<IFileService>();
            
            builder.RegisterType<EmailParameterManager>().As<IEmailParameterService>();
            builder.RegisterType<EfEmailParameterDal>().As<IEmailParameterDal>();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();

        }
    }
}
