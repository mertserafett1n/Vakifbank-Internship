using LoanPlan.Data;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers()
.AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Thanks to gpt, Problem: If your API serializes objects using the default .NET JSON serializer (System.Text.Json), the JSON output will use camelCase. This means the property names in the JSON will not match the PascalCase names in your TypeScript model.

    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<LoanPlanDbContext>(options => 
options.UseInMemoryDatabase("LoansDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.UseCors(CorsPolicy => CorsPolicy.AllowAnyHeader().AllowAnyMethod(). AllowAnyOrigin());

app.MapControllers();

app.Run();
