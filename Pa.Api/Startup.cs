using Microsoft.OpenApi.Models;
using Para.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Para.Data.UnitOfWork;
using Para.Business.MapperConfig;
using AutoMapper;
using MediatR;
using System.Reflection;
using FluentValidation.AspNetCore;
using Para.Business.Cqrs;
using FluentValidation;
using Para.Data.Validator;
using Autofac;
using Pa.Api.AutoFac;

namespace Pa.Api
{
    public class Startup
    {
        public IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pa.Api", Version = "v1" });
            });

            var connectionStringSql = Configuration.GetConnectionString("MsSqlConnection");
            services.AddDbContext<ParaDbContext>(options => options.UseSqlServer(connectionStringSql));

            services.AddValidatorsFromAssemblyContaining<CustomerValidator>();
            services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CustomerValidator>());
            services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CustomerAddressValidator>());

            services.AddFluentValidationClientsideAdapters();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfig());
            });
            services.AddSingleton(config.CreateMapper());
            services.AddMediatR(typeof(CreateCustomerCommand).GetTypeInfo().Assembly);

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Para.Api v1"));
            }

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
