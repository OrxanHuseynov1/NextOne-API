using DAL.SqlServer.Context;
using DAL.SqlServer.Repositories.Abstractions;
using DAL.SqlServer.Repositories.Implementations; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.SqlServer;

public static class DALRegistration
{
    public static void RegisterDAL(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("MSSql"));
        });

        service.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
        service.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

        service.AddScoped<IChatReadRepository, ChatReadRepository>();
        service.AddScoped<IChatWriteRepository, ChatWriteRepository>();

        service.AddScoped<ICompanyReadRepository, CompanyReadRepository>();
        service.AddScoped<ICompanyWriteRepository, CompanyWriteRepository>();

        service.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        service.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

        service.AddScoped<IDebtRecordReadRepository, DebtRecordReadRepository>();
        service.AddScoped<IDebtRecordWriteRepository, DebtRecordWriteRepository>();

        service.AddScoped<IDepoReadRepository, DepoReadRepository>();
        service.AddScoped<IDepoWriteRepository, DepoWriteRepository>();

        service.AddScoped<IExpenseReadRepository, ExpenseReadRepository>();
        service.AddScoped<IExpenseWriteRepository, ExpenseWriteRepository>();

        service.AddScoped<ILossReadRepository, LossReadRepository>();
        service.AddScoped<ILossWriteRepository, LossWriteRepository>();

        service.AddScoped<IProductReadRepository, ProductReadRepository>();
        service.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        service.AddScoped<IProductInDepoReadRepository, ProductInDepoReadRepository>();
        service.AddScoped<IProductInDepoWriteRepository, ProductInDepoWriteRepository>();

        service.AddScoped<IReturnReadRepository, ReturnReadRepository>();
        service.AddScoped<IReturnWriteRepository, ReturnWriteRepository>();

        service.AddScoped<IReturnItemReadRepository, ReturnItemReadRepository>();
        service.AddScoped<IReturnItemWriteRepository, ReturnItemWriteRepository>();

        service.AddScoped<ISaleReadRepository, SaleReadRepository>();
        service.AddScoped<ISaleWriteRepository, SaleWriteRepository>();

        service.AddScoped<ISaleItemReadRepository, SaleItemReadRepository>();
        service.AddScoped<ISaleItemWriteRepository, SaleItemWriteRepository>();

        service.AddScoped<ITransferReadRepository, TransferReadRepository>();
        service.AddScoped<ITransferWriteRepository, TransferWriteRepository>();

        service.AddScoped<IUserReadRepository, UserReadRepository>();
        service.AddScoped<IUserWriteRepository, UserWriteRepository>();
    }
}