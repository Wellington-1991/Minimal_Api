using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minimal_Api.Dominio;
using Minimal_Api.Dominio.DTOs;
using Minimal_Api.Dominio.Servicos;
using Minimal_Api.Infraestrutura.Db;
using Minimal_Api.Infraestrutura.Interfaces;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();

// Configurar o DbContext para usar PostgreSQL
builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("pg"));
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/login", ( [FromBody]LoginDTO loginDTO,  IAdministradorServico administradorServico) => {
    if (administradorServico.Login(loginDTO) != null)
    {
        return Results.Ok("Login com sucesso");
    }
    else
    {
        return Results.Unauthorized();
    }

});

app.Run();
