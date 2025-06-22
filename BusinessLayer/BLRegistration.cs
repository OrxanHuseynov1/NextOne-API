using BusinessLayer.Profiles.CategoryProfiles;
using DAL.SqlServer.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BusinessLayer;

public static class BLRegistration
{
    public static void AddBlServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAutoMapper(typeof(CategoryProfile).Assembly);


    }
}
