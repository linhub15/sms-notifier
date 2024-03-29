﻿using Hangfire;
using Hangfire.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.OpenApi.Models;
using Notifier.Core.Entities;
using Notifier.Core.Gateways;
using Notifier.Core.UseCases;
using Notifier.Infrastructure;
using Notifier.Infrastructure.Data;

namespace Notifier
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            
            if (env.IsDevelopment())
            {
                builder.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
                builder.AddUserSecrets<Startup>();
            }
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Default");
            var dbName = Configuration.GetValue<string>("MongoDbName");

            // Dependency Injection
            services.AddSingleton<IDbContext, NotifierDb>(
                context => new NotifierDb(connectionString, dbName)
            );
            services.AddSingleton<ISmsGateway, TwilioSmsGateway>();
            services.AddSingleton<ISchedulerGateway, HangFireSchedulerGateway>();
            services.AddSingleton<IRepositoryGateway<string, Message>, MessageRepository>();
            services.AddSingleton<IRepositoryGateway<string, Community>, CommunityRepository>();

            UseCaseInjection(services);

            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Swagger
            services.AddSwaggerDocument();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notifier Api", Version = "v1" });
            });

            // Hangfire services
            var migrationOptions = new MongoMigrationOptions()
            {
                Strategy = MongoMigrationStrategy.Drop
            };

            services.AddHangfire(
                configuration => configuration
                .UseMongoStorage(
                    connectionString,
                    dbName,
                    new MongoStorageOptions { MigrationOptions = migrationOptions })
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHangfireServer();
            app.UseHangfireDashboard();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }

        private void UseCaseInjection(IServiceCollection services)
        {
            services.AddSingleton<IGetMessage, GetMessageInteractor>();
            services.AddSingleton<IListMessages, ListMessagesInteractor>();
            services.AddSingleton<IModifyMessage, ModifyMessageInteractor>();
            services.AddSingleton<IRescheduleMessage, RescheduleMessageInteractor>();
            services.AddSingleton<IScheduleMessage, ScheduleMessageInteractor>();
            services.AddSingleton<ISendMessage, SendMessageInteractor>();
            services.AddSingleton<ISubscribe, SubscribeInteractor>();
            services.AddSingleton<IUnsubscribe, UnsubscribeInteractor>();
            services.AddSingleton<IUnscheduleMessage, UnscheduleMessageInteractor>();
        }
    }
}
