using Accounting.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eSoft.CashBank.Services;
using eSoft.CashBank.Data;

using eSoft.Ledger.Services;
using eSoft.Ledger.Data;

using eSoft.Hutang.Services;
using eSoft.Hutang.Data;

using eSoft.Piutang.Data;
using eSoft.Piutang.Services;

using eSoft.Persediaan.Data;
using eSoft.Persediaan.Services;

using eSoft.Pembelian.Data;
using eSoft.Pembelian.Services;

using eSoft.Penjualan.Data;
using eSoft.Penjualan.Services;

using eSoft.Order.Data;
using eSoft.Order.Services;

using eSoft.Asset.Data;
using eSoft.Asset.Services;

using eSoft.Company.Data;
using eSoft.Company.Services;

using eSoft.LaporanStock.Services;

using Microsoft.EntityFrameworkCore;
using BlazorFluentUI;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Accounting.Areas.Identity;
using Accounting.Services;

namespace Accounting
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //  services.AddDefaultIdentity<IdentityUser, IdentityRole>();    /* tambahan authorise */
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
               // options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            services.AddDbContext<DbContextBank>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Accounting")));
            services.AddDbContext<DbContextLedger>(options =>
                  options.UseSqlServer(
                      Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Accounting")));
            services.AddDbContext<DbContextPiutang>(options =>
                  options.UseSqlServer(
                      Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Accounting")));
            services.AddDbContext<DbContextHutang>(options =>
                 options.UseSqlServer(
                     Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Accounting")));
            services.AddDbContext<DbContextPersediaan>(options =>
                 options.UseSqlServer(
                     Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Accounting")));

            services.AddDbContext<DbContextBeli>(options =>
                 options.UseSqlServer(
                     Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Accounting")));

            services.AddDbContext<DbContextJual>(options =>
                 options.UseSqlServer(
                     Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Accounting")));

            services.AddDbContext<DbContextOrder>(options =>
                 options.UseSqlServer(
                     Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Accounting")));

            services.AddDbContext<DbContextCompany>(options =>
                 options.UseSqlServer(
                     Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Accounting")));

            services.AddDbContext<DbContextAssets>(options =>
                 options.UseSqlServer(
                     Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Accounting")));

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazorFluentUI();

            services.AddSingleton<WeatherForecastService>();
            services.AddTransient<ICashBankServices, CashBankServices>();

            services.AddTransient<ILedgerServices, LedgerServices>();
            services.AddTransient<IReceivableServices, ReceivableServices>();
            services.AddTransient<IPaymentArServices, PaymentArServices>();
            services.AddTransient<IPaymentArDpServices, PaymentArDpServices>();
            services.AddTransient<IPayableServices, PayableServices>();
            services.AddTransient<IPaymentApServices, PaymentApServices>();
            services.AddTransient<IPaymentApDpServices, PaymentApDpServices>();
            services.AddTransient<IInventoryServices, InventoryServices>();
            services.AddTransient<IIcAdjustServices, IcAdjustServices>();
            services.AddTransient<IPurchaseServices, PurchaseServices>();
            services.AddTransient<ISalesServices, SalesServices>();
            services.AddTransient<IOrderPurchaseServices, OrderPurchaseServices>();
            services.AddTransient<ILaporanStockServices, LaporanStockServices>();
            services.AddTransient<ICompanyServices, CompanyServices>();
            services.AddTransient<IAdministrationServices,AdministrationServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();  /* tambahan authorise */
            app.UseAuthorization();   /* tambahan authorise */

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();   /* tambahan authorise */
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
