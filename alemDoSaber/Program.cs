
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Data;
using RedeSocial.Model;
using RedeSocial.Service.Implements;
using RedeSocial.Service;
using RedeSocial.Validator;

namespace alemDoSaber
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

           
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); 

            
            builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(connectionString)
            );

            
            builder.Services.AddTransient<IValidator<Tema>, TemaValidator>();
            builder.Services.AddTransient<IValidator<Postagem>, PostagemValidator>();
            builder.Services.AddTransient<IValidator<User>, UserValidator>();


            builder.Services.AddScoped<ITemaService, TemaService>();
            builder.Services.AddScoped<IPostagemService, PostagemValidator>();

            // Add services(Service) to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Configuração do CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    policy =>
                    {
                        policy.AllowAnyOrigin()    
                        .AllowAnyMethod()         
                        .AllowAnyHeader();         
                    });
            });

            var app = builder.Build();

            //Criar o Banco de Dados e as Tabelas
            using (var scope = app.Services.CreateAsyncScope()) 
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated();
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            //Inicializa o CORS
            app.UseCors("MyPolicy");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}