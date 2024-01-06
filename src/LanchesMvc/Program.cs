using LanchesMvc.Data;
using LanchesMvc.Models;
using LanchesMvc.Repositories;
using LanchesMvc.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("LanchesMVC")));

//Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//builder.Services.Configure<IdentityOptions>(opt =>
//{
//    // Default Password settings
//    opt.Password.RequireDigit = true;
//    opt.Password.RequireLowercase = true;
//    opt.Password.RequireNonAlphanumeric = true;
//    opt.Password.RequireUppercase = true;
//    opt.Password.RequiredLength = 6;
//    opt.Password.RequiredUniqueChars = 1;
//});

//Injeção de Dependência
builder.Services.AddTransient<ILancheRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Cria uma instãncia de CarrinhoCompra a cada request, caso sejam 2 clientes diferentes, será criado uma instancia para cada um
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

//Registrando os middleware 
builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Adicionando a sessão
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "categoriaFiltro",
    pattern: "Lanche/{action}/{categoria?}",
    defaults: new { Controller = "Lanche", action = "List" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();