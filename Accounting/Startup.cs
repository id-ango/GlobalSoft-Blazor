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

using Microsoft.EntityFrameworkCore;


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
            services.AddDbContext<DbContextBank>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), b=>b.MigrationsAssembly("Accounting")));
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


            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddTransient<ICashBankServices, CashBankServices>();
         
            services.AddTransient<ILedgerServices,LedgerServices>();
            services.AddTransient<IReceivableServices, ReceivableServices>();
            services.AddTransient<IPaymentArServices, PaymentArServices>();
            services.AddTransient<IPaymentArDpServices, PaymentArDpServices>();
            services.AddTransient<IPayableServices, PayableServices>();
            services.AddTransient<IPaymentApServices, PaymentApServices>();
            services.AddTransient<IPaymentApDpServices, PaymentApDpServices>();
            services.AddTransient<IInventoryServices, InventoryServices>();
            services.AddTransient<IIcAdjustServices, IcAdjustServices>();
            services.AddTransient<IPurchaseServices, PurchaseServices>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
