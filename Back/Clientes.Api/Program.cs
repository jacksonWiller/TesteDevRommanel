using Clientes.Aplicacao.Commands.CreateCliente;
using Clientes.Aplicacao.Queries.GetAllClientes;
using Clientes.Dominio.Interfaces;
using Clientes.Infra.Contexto;
using Clientes.Infra.Repositorios;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IClienteContext, ClienteContext>();

builder.Services.AddDbContext<ClienteContext>(options =>
{
    //options.UseNpgsql("Host=postgres;Port=5432;Database=postgres;Username=postgres;Password=postgres"); //docker

    options.UseNpgsql("Host=localhost;Port=5434;Database=postgres;Username=postgres;Password=postgres"); //local
});

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateClienteCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetAllClientesQuery).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(CreateClienteCommandValidator).Assembly);

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

app.Run();
