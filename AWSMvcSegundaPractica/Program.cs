using Amazon.S3;
using AWSMvcSegundaPractica.Helpers;
using AWSMvcSegundaPractica.Models;
using AWSMvcSegundaPractica.Services;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

string jsonSecrets = await HelperSecretManager.GetSecretsAsync();
KeysModel keysModel = JsonConvert.DeserializeObject<KeysModel>(jsonSecrets);

builder.Services.AddSingleton<KeysModel>(x => keysModel);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAWSService<IAmazonS3>();

builder.Services.AddTransient<ServiceStorageAWS>();

builder.Services.AddTransient<ServicePractica>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
