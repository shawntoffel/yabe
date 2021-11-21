using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Yabe.Application.Authorization;
using Yabe.Application.Configuration;
using Yabe.Application.Handlers.BlobHandlers.UploadBlobs;
using Yabe.Application.PipelineBehaviors;
using Yabe.Ui.Managers;

namespace Yabe.Console
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages()
                .AddMvcOptions(options =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                }).AddMicrosoftIdentityUI();

            services
                .AddServerSideBlazor();

            services
                .AddCors();

            services
                .AddAzureClients(builder =>
                {
                    builder.AddBlobServiceClient(Configuration.GetConnectionString("AzureBlobStorage"));
                });

            services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
                .AddValidatorsFromAssemblyContaining<UploadBlobsHandler>()
                .AddTransient<IBlobManager, BlobManager>()
                .AddTransient<IAuthorizationManager, AuthorizationManager>()
                .AddMediatR(typeof(UploadBlobsHandler).Assembly)
                .AddHttpClient()
                .AddOptions<YabeOptions>()
                .BindConfiguration(YabeOptions.ConfigurationSection);

            services
                .AddMicrosoftIdentityWebAppAuthentication(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseRouting();
            app.UseCors();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
