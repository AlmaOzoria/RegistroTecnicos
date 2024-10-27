global using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RegistroTecnicos.Components;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Services;

namespace RegistroTecnicos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddSingleton<Radzen.NotificationService>();
            builder.Services.AddBlazorBootstrap();

            // Agregar el constructor para obtener la cadena de conexión
            //var ConStr = builder.Configuration.GetConnectionString("SqlConStr");

            // Configurar el contexto con SQL Server
            builder.Services.AddDbContextFactory<Contexto>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



            //Inyectando service
            builder.Services.AddScoped<TecnicoServices>();

            builder.Services.AddScoped<TiposTecnicosServices>();

            builder.Services.AddScoped<ClientesServices>();

            builder.Services.AddScoped<TrabajosServices>();

            builder.Services.AddScoped<PrioridadesServices>();

            builder.Services.AddScoped<ArticuloServices>();

            builder.Services.AddScoped<CotizacionServices>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
