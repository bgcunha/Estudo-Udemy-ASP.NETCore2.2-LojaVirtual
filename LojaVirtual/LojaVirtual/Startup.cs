using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Libraries.Middleware;
using LojaVirtual.Libraries.Sessao;
using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;
using LojaVirtual.Libraries.Cookie;
using LojaVirtual.Libraries.CarrinhoCompra;
using AutoMapper;
using LojaVirtual.Libraries.AutoMapper;
using WSCorreios;

namespace LojaVirtual
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            /*
            * AutoMapper
            */

            services.AddAutoMapper(config => config.AddProfile<MappingProfile>());

            services.AddHttpContextAccessor();
            services.AddScoped<IRepositoryCliente, RepositoryCliente>();
            services.AddScoped<IRepositoryColaborador, RepositoryColaborador>();
            services.AddScoped<IRepositoryNewsLatterEmail, NewsLatterRepository>();
            services.AddScoped<IRepositoryCategoria, RepositoryCategoria>();
            services.AddScoped<IRepositoryProduto, RepositoryProduto>();
            services.AddScoped<IRepositoryImagem, RepositoryImagem>();


            /*
             * SMTP
             */

            services.AddScoped<SmtpClient>(options => {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Configuration.GetValue<string>("Email:ServerSMTP"),
                    Port = Configuration.GetValue<int>("Email:ServerPort"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("Email:Username"), Configuration.GetValue<string>("Email:Password")),
                    EnableSsl = true,
                };

                return smtp;
            });

            services.AddScoped<CalcPrecoPrazoWSSoap>(options => {
                var servico = new CalcPrecoPrazoWSSoapClient(CalcPrecoPrazoWSSoapClient.EndpointConfiguration.CalcPrecoPrazoWSSoap);
                return servico;
            });

            services.AddScoped<GerenciarEmail>();            


            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMemoryCache();
            services.AddSession(options =>
            {

            });

            services.AddScoped<GerenciarSessao>();
            services.AddScoped<GerenciarCookie>();
            services.AddScoped<GerenciarCarrinho>();
            services.AddScoped<LoginCliente>();
            services.AddScoped<LoginColaborador>();
            services.AddScoped<NewsLatterEmail>();
            services.AddScoped<Categoria>();

            services.AddMvc(options => { options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "O campo deve ser preenchido!"); }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddSessionStateTempDataProvider();
            services.AddSession(options => { options.Cookie.IsEssential = true; });

            //var connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LojaVirtual;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var source = Configuration.GetValue<string>("ConnectionStrings:Source");
            var connection = $@"Data Source={source};Initial Catalog=LojaVirtual;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            services.AddDbContext<LojaVirtualContext>(options => options.UseSqlServer(connection));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();

            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
