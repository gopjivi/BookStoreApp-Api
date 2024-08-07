using BookStoreApp.Api.DbContexts;
using BookStoreApp.Api.Repositories;
using BookStoreApp.Api.Repositories.Interfaces;
using BookStoreApp.Api.Services;
using BookStoreApp.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{ 
    //configuring swagger documenation
      var xmlCommentsFile = "BookStoreApp.xml";
var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
setupAction.IncludeXmlComments(xmlCommentsFullPath);
});
//database
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositories
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

//Services
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

//CORS
builder.Services.AddCors(options => options.AddPolicy("MyTestCORS", policy =>
{
policy.WithOrigins("http://localhost:3000")
.AllowAnyMethod()
.AllowAnyHeader();
    // allow all origin
    //policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyTestCORS");

app.UseAuthorization();

app.MapControllers();

app.Run();
