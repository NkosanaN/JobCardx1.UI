using TimeCard.Service.Repository;
using TimeCard.Service.Repository.Interfaces;

namespace TimeCard.UI.Extension;
public static class ServicesApplication
{
    public static IServiceCollection ConfigureService(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddHttpClient<IJobService, JobService>();
        services.AddHttpClient<IJobCardService, JobCardService>();
        services.AddHttpClient<IEmployeeService, EmployeeService>();

        services.AddControllersWithViews();
        return services;
    }
}
