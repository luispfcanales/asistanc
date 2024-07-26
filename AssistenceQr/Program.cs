using AssistanceQr;
using AssistanceQr.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddServerSideBlazor().AddCircuitOptions(e => {
    e.DetailedErrors = true;
});

try
{
    builder.Services.AddDbContext<MyContext>(item =>
        item.UseSqlServer(builder.Configuration.GetConnectionString("akdemic")),
        ServiceLifetime.Transient
    );
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

var app = builder.Build();


try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<MyContext>();
    db.Database.Migrate();
}
catch (Exception e)
{

    Console.WriteLine(e.Message);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
//app.MapControllerRoute();
app.MapControllers();
app.Run();
