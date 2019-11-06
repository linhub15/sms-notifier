using Hangfire;
using Hangfire.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.OpenApi.Models;
using Notifier.Core.Entities;
using Notifier.Core.Gateways;
using Notifier.Core.Interfaces;
using Notifier.Core.UseCases;
using Notifier.Infrastructure;
using Notifier.Infrastructure.Data;

namespace Notifier
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDbContext, NotifierDb>(
                context => new NotifierDb("mongodb://localhost:27017", "notifier")
            );
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<ISubscriberService, SubscriberService>();
            services.AddSingleton<IMessageSender, TwilioMessageSender>();
            services.AddSingleton<ISchedulerGateway, HangFireScheduler>();
            services.AddSingleton<IRepository<string, Message>, MessageRepository>();
            services.AddSingleton<IRepository<string, Community>, CommunityRepository>();

            SetupUseCases(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            // Hangfire services
            var migrationOptions = new MongoMigrationOptions()
            {
                Strategy = MongoMigrationStrategy.Drop
            };

            services.AddHangfire(
                configuration => configuration
                .UseMongoStorage(
                    "mongodb://localhost:27017",
                    "notifier",
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notifier-Api");
            });
        }

        private void SetupUseCases(IServiceCollection services)
        {
            services.AddSingleton<IUseCaseInteractor<GetMessageRequest, GetMessageResponse>, GetMessageInteractor>();
            services.AddSingleton<IUseCaseInteractor<ListMessagesRequest, ListMessagesResponse>, ListMessagesInteractor>();
            services.AddSingleton<IUseCaseInteractor<ModifyMessageRequest, ModifyMessageResponse>, ModifyMessageInteractor>();
            services.AddSingleton<IUseCaseInteractor<RescheduleMessageRequest, RescheduleMessageResponse>, RescheduleMessageInteractor>();
            services.AddSingleton<IUseCaseInteractor<ScheduleMessageRequest, ScheduleMessageResponse>, ScheduleMessageInteractor>();
            services.AddSingleton<IUseCaseInteractor<SendMessageRequest, SendMessageResponse>, SendMessageInteractor>();
            services.AddSingleton<IUseCaseInteractor<SubscribeRequest, SubscribeResponse>, SubscribeInteractor>();
            services.AddSingleton<IUseCaseInteractor<UnsubscribeRequest, UnsubscribeResponse>, UnsubscribeInteractor>();
            services.AddSingleton<IUseCaseInteractor<UnscheduleMessageRequest, UnscheduleMessageResponse>, UnscheduleMessageInteractor>();
        }
    }
}
