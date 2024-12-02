using System;
using System.Net;
using System.Text;
using A3System.Dbo;
using A3System.Dbo.Dto.Setor;
using A3System.Dbo.Dto.User;
using A3System.Interface;
using A3System.Services;
using A3System.Validation.UserValidations;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UsuariosAPI.Services;
using UsuariosAPI.Validation.UserValidations;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")), ServiceLifetime.Transient);


#region  Scopes
builder.Services.AddScoped<IRegisterService, CadastroService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ISetorService, SetorService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

#endregion

#region Validations Scopes
builder.Services.AddRazorPages();
builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserValidations>();
builder.Services.AddScoped<IValidator<UpdateUserDto>, UpdateUserValidations>();
builder.Services.AddScoped<IValidator<CreateSetorDto>, CreateSetorValidations>();
builder.Services.AddScoped<IValidator<UpdateSetorDto>, UpdateSetorValidations>();
#endregion

#region Auth
var key = Encoding.ASCII.GetBytes("bd65c3-852*f850+0d*e29-588db5d0f85+4.6af++925f81df0As52");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Validar o emissor
            ValidateAudience = true, // Validar a audiência
            ValidateIssuerSigningKey = true, // Validar a chave
            ValidIssuer = "EuMesmo", // Defina o emissor
            ValidAudience = "Clientes", // Defina a audiência
            IssuerSigningKey = new SymmetricSecurityKey(key) // Chave de assinatura
        };
    });
#endregion

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
