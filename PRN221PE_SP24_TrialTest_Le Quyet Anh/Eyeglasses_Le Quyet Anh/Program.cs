using Eyeglasses_Repo.DbContext2024;
using Eyeglasses_Repo.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var cnString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Eyeglasses2024DBContext>(op =>
op.UseSqlServer(cnString));
builder.Services.AddScoped<Eyeglasses2024DBContext>();

builder.Services.AddScoped<AccountRepo>();
builder.Services.AddScoped<EyeglassesRepo>();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
