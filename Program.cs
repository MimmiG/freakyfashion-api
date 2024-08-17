using FreakyFashionAPI.Data;
using FreakyFashionAPI.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<CartRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
