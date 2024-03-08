using Eyeglasses.DAO.DbContext2024;
using Eyeglasses.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<Eyeglasses2024DbContext, Eyeglasses2024DbContext>();
builder.Services.AddScoped<StoreAccountRepository, StoreAccountRepository>();
builder.Services.AddScoped<LensTypeRepository, LensTypeRepository>();
builder.Services.AddScoped<EyeglassesRepository, EyeglassesRepository>();


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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.MapRazorPages();

app.Run();
