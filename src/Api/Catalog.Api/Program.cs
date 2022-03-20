using Catalog.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger(builder.Configuration);
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseMiddlewares();

app.MapControllers();

app.Run();
