using MySite.Services;

namespace MySite.Infrastructure.Extensions
{
    public static class ServersesCollectEstantion
    {
        public static IServiceCollection AddServises(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IEmailSender, EmailSender>();
            return serviceCollection;
        }
    }
}
