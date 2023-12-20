using Teste.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICreditService, CreditService>();
builder.Services.AddScoped<ICreditConsignadoService, CreditConsignadoService>();
builder.Services.AddScoped<ICreditDiretoService, CreditDiretoService>();
builder.Services.AddScoped<ICreditImobiliarioService, CreditImobiliarioService>();
builder.Services.AddScoped<ICreditPessoaFisicaService, CreditPessoaFisicaService>();
builder.Services.AddScoped<ICreditPessoaJuridicaService, CreditPessoaJuridicaService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
