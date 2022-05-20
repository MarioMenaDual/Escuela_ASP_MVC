using Microsoft.EntityFrameworkCore;
using platzi_asp_net_core.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EscuelaContext>( 
    options => options.UseInMemoryDatabase(databaseName: "testDB")
    );

var app = builder.Build();

using(var scope=app.Services.CreateScope()){
    var serv=scope.ServiceProvider;
    try
    {
        var contex=serv.GetRequiredService<EscuelaContext>();
        contex.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
                        var logger = serv.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Ocurrio un error");

    }

}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Escuela}/{action=Index}/{id?}");

    

app.Run();
