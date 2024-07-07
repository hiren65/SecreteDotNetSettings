using HiddenSetting.Models;
using Microsoft.Extensions.Configuration;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMvc();

//Get String For Twillio using Bind with Class and appsettings 
var ConstrTwillio = builder.Configuration.GetSection("Twilio");

builder.Services.Configure<TwilioSettings>(ConstrTwillio);

var constrSocialLoginSettings = builder.Configuration.GetSection("SocialLoginSettings");
builder.Services.Configure<SocialLoginSettings>(constrSocialLoginSettings);



builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
