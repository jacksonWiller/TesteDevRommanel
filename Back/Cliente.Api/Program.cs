using Clientes.Aplicacao.Commands.CreateCliente;
using Clientes.Aplicacao.Queries;
using Clientes.Dominio.Interfaces;
using Clientes.Infra.Contexto;
using Clientes.Infra.Repositorios;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ClienteContext>(
    context => context.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"))
);


builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateClienteCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetAllClientesQuery).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(CreateClienteCommandValidator).Assembly);

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddControllers();


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

app.Run();
