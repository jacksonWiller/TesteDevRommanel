using Clientes.Infra.Contexto;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ClienteContext>(
                context => context.UseSqlServer(Configuration.GetConnectionString("SqlServer"))
            );

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
