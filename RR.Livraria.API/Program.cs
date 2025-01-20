using Microsoft.Data.SqlClient;
using RR.Livraria.API.Filters;
using RR.Livraria.Application.Mapping;
using RR.Livraria.Application.Services;
using RR.Livraria.Domain.Interfaces.Notification;
using RR.Livraria.Domain.Interfaces.Repositories;
using RR.Livraria.Domain.Interfaces.Services;
using RR.Livraria.Domain.Notification;
using RR.Livraria.Infra.Contexts;
using RR.Livraria.Infra.Repositories;
using System.Data.Common;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IAssuntoService, AssuntoService>();
builder.Services.AddScoped<IAssuntoRepository, AssuntoRepository>();
builder.Services.AddScoped<ILivroAssuntoRepository, LivroAssuntoRepository>();
builder.Services.AddScoped<ILivroAutorRepository, LivroAutorRepository>();
builder.Services.AddScoped<IDomainNotification, DomainNotification>();
builder.Services.AddScoped<IVendaService, VendaService>();
builder.Services.AddScoped<IVendaRepository, VendaRepository>();
builder.Services.AddScoped<ILivroVendaRepository, LivroVendaRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_policyAllowSpecificOrigins",
                      policy =>
                      {
                          policy
                            .WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});
builder.Services.AddControllers();
builder.Services.AddMvc
    (
        options =>
        {
            options.Filters.Add<DomainNotificationFilter>();
        }
    )
    .AddJsonOptions
    (
        options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        }
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database connection info
var connectionString = builder.Configuration.GetConnectionString("LivrariaDB");
builder.Services.AddScoped<DbConnection>(conn => new SqlConnection(connectionString));
builder.Services.AddScoped<DapperContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("_policyAllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
