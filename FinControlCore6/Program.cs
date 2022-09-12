using FinControlCore6.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CapitalizeCamelCaseNamingStrategy()
        };
    });
builder.Services.AddDbContext<FinControlDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("FinControlDatabase")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// конвейер обработки запросов
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public class CapitalizeCamelCaseNamingStrategy : NamingStrategy
{
    protected override string ResolvePropertyName(string name)
    {
        return name[0].ToString().ToUpperInvariant() + name.Substring(1);
    }
}