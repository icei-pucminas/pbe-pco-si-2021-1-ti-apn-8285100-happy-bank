using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using HappyBank.Domain.Repository;
using HappyBank.Data.Repository;
using HappyBank.UseCases.CustomerRegistration;
using HappyBank.UseCases.OpenAccount;
using HappyBank.UseCases.ExtractStatement;
using HappyBank.UseCases.ExtractBalance;
using HappyBank.UseCases.SignIn;
using HappyBank.Api.Services;
using Npgsql;
using System.Data;

namespace HappyBank.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string HappyBankAllowAllOrigins = "_HappyBankAllowAllOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: HappyBankAllowAllOrigins,
                                builder =>
                                {
                                    builder.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader();
                                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HappyBank.api", Version = "v1" });
            });

            services.AddHttpContextAccessor();

            services.AddSingleton<NpgsqlConnection>((sp) => new NpgsqlConnection("Host=127.0.0.1;Port=5432;Username=postgres;Password=postgres;Database=happybank"));
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IBankRepository, BankRepository>();
            services.AddSingleton<IExtractStatementRepository, ExtractStatementRepository>();

            services.AddTransient<ContextService>();
            services.AddTransient<CustomerRegistrationUC>();
            services.AddTransient<ExtractStatementUC>();
            services.AddTransient<ExtractBalanceUC>();
            services.AddTransient<OpenAccountUC>();
            services.AddTransient<SignInUC>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HappyBank.api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
