using Microsoft.EntityFrameworkCore;
using BookStore.Application.Interfaces;
using BookStore.Application.UseCases;
using BookStore.Infrastructure.Repositories;
using BookStore.Infrastructure.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("BookStore.Api")
    )
);


builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<BookService>();

builder.Services.AddScoped<IReaderRepository, ReaderRepository>();
builder.Services.AddScoped<ReaderService>();

builder.Services.AddScoped<ILendRepository, LendRepository>();
builder.Services.AddScoped<LendService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("Front", policy =>
    {
        policy.WithOrigins("https://localhost:3001", "http://localhost:3001")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();


app.UseSwagger();
app.UseSwaggerUI();
app.MapScalarApiReference();

app.UseCors("Front");
//app.UseAuthorization();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.MapControllers();

app.Run();