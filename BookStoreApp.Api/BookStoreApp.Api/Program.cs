using BookStoreApp.Api.DbContexts;
using BookStoreApp.Api.Repositories;
using BookStoreApp.Api.Repositories.Interfaces;
using BookStoreApp.Api.Services;
using BookStoreApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using Serilog.Events;
using Serilog;
using System.Net;
using System.Reflection;
using BookStoreApp.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);




// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console(LogEventLevel.Information) // Log to console
   .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Information)  // Log Information and above to file
    .CreateLogger();

builder.Host.UseSerilog();


builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders(); // Clears default providers
    logging.AddSerilog(); // Add Serilog as the logging provider
}); 




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{ 
    //configuring swagger documenation
      var xmlCommentsFile = "BookStoreApp.xml";
var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
setupAction.IncludeXmlComments(xmlCommentsFullPath);

    //swagger authentication
    setupAction.AddSecurityDefinition("BookStoreApiBearerAuth", new()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API"
    });
    setupAction.AddSecurityRequirement(new()
    {
        {
            new ()
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "BookStoreApiBearerAuth" }
            },
            new List<string>()
        }
    });
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

//authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
               Convert.FromBase64String(builder.Configuration["Authentication:SecretForKey"]))
        };
    }
    );



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// for unauthorized errors
app.UseMiddleware<CustomUnauthorizedMiddleware>();


//for uuhandled errors

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
{
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    context.Response.ContentType = "application/json";
    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
    if (contextFeature != null)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();

        // Log the exception
        logger.LogError(contextFeature.Error, "An unhandled exception has occurred.");

        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
        {
            statusCode = context.Response.StatusCode,
            Message = "Something went wrong"
        }));
    }

});
});

app.UseHttpsRedirection();

app.UseCors("MyTestCORS");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
