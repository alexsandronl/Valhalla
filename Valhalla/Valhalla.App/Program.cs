using System.Configuration;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Blazored.Toast;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PainelDeSenha.Helpers;
using Valhalla.Domain.Interfaces;
using Valhalla.Infraestrutura.Servicos;

var builder = WebApplication.CreateBuilder(args);

Environment.SetEnvironmentVariable("CaminhoDatabase", Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Database"));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.AddAutofacRegistration());

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddBlazoredToast();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddRouting();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        options.LoginPath = "/signin";
        options.LogoutPath = "/signout";
    })
    .AddLinkedIn(options =>
    {
        options.ClientId = builder.Configuration["LinkedIn:ClientId"];
        options.ClientSecret = builder.Configuration["LinkedIn:ClientSecret"];
        options.SaveTokens = true;
        options.Scope.Add("r_liteprofile");
        options.Scope.Add("r_emailaddress");
    });

Environment.SetEnvironmentVariable("NumeroCartasIniciais", builder.Configuration["Tabuleiro:NumeroCartasIniciais"]);
Environment.SetEnvironmentVariable("NumeroDeRodadasPorJogo", builder.Configuration["Tabuleiro:NumeroDeRodadasPorJogo"]);
Environment.SetEnvironmentVariable("NumeroDeTurnosTotaisPorRada", builder.Configuration["Tabuleiro:NumeroDeTurnosTotaisPorRada"]);
Environment.SetEnvironmentVariable("TempoPorTurno", builder.Configuration["Tabuleiro:TempoPorTurno"]);
Environment.SetEnvironmentVariable("SenhaDeAcesso", builder.Configuration["Administracao:SenhaDeAcesso"]);

builder.Services.AddScoped<IFileUpload, ServicoFileUpload>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});

app.UseStaticFiles();

app.UseRouting();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
