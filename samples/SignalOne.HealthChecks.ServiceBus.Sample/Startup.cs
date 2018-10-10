using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Rest;
using System;

namespace SignalOne.HealthChecks.ServiceBus.Sample
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
            services.AddHealthChecks()
                        .AddAzureServiceBusDefaults(options =>
                        {
                            options.BaseUri = new Uri("");
                            options.ServiceCredentials = new BasicAuthenticationCredentials
                            {
                                Password = "",
                                UserName = ""
                            };
                        })
                        .AddAzureServiceBusQueueCheck("my-queue", requiredConfiguration =>
                        {
                            //requiredConfiguration.MaxDeliveryCount = 10; // TODO :: implement
                        })
                        .AddAzureServiceBusTopicCheck("my-topic")
                        .AddAzureServiceBusSubscriptionCheck("my-topic", "my-subscription");
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHealthChecks("/health");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
