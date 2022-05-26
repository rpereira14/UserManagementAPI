using UserManagementApi.Repositories;

namespace UserManagementApi
{
    public static class ServiceColletionExtension 
    {
        public static void RegisterDbcon(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>();
        }
    }
}
