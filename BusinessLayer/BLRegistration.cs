using BusinessLayer.Profiles.CategoryProfiles;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BusinessLayer.Services.Abstractions;
using BusinessLayer.Services.Implementations;
using BusinessLayer.ExternalServices.Abstractions;
using BusinessLayer.ExternalServices.Implementations;

namespace BusinessLayer;

public static class BLRegistration
{
    public static void AddBlServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAutoMapper(typeof(CategoryProfile).Assembly);
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IDebtRecordService, DebtRecordService>();
        services.AddScoped<IDepoService, DepoService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<ILossService, LossService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductInDepoService, ProductInDepoService>();
        services.AddScoped<IReturnService, ReturnService>();
        services.AddScoped<IReturnItemService, ReturnItemService>();
        services.AddScoped<ISaleService, SaleService>();
        services.AddScoped<ISaleItemService, SaleItemService>();
        services.AddScoped<ITransferService, TransferService>();
        services.AddScoped<IUserService, UserService>();

        services.AddAuthentication(cfg => {
            cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x => {

            x.TokenValidationParameters = new TokenValidationParameters
            {

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8
                        .GetBytes(configuration["Jwt:SecretKey"]!)
                ),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = configuration["Jwt:Audience"],
                ValidIssuer = configuration["Jwt:Issuer"]
            };
        });
    }
}
