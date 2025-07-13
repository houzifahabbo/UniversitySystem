using Autofac;
using Autofac.Extensions.DependencyInjection;
using University.API.Modules;
using University.Core.Services;
using University.Data.Entities;
using University.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterType<UniversityDbContext>().AsSelf().InstancePerLifetimeScope();
    container.RegisterModule<RepositoryModule>();
    container.RegisterModule<ServiceModule>();
});

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
