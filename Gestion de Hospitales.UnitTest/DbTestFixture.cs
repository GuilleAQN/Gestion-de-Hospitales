using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Primer_Parcial.DTOs;
using Primer_Parcial.Models;
using System;

namespace Gestion_de_Hospitales.UnitTest
{
    public class DbTestFixture<TContext> : IDisposable where TContext : DbContext
    {
        public TContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public DbTestFixture()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Configurar opciones para la base de datos en memoria
            var options = new DbContextOptionsBuilder<TContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            // Configurar el contexto de base de datos
            Context = (TContext)Activator.CreateInstance(typeof(TContext), options);

            // Configurar AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            Mapper = mappingConfig.CreateMapper();
        }

        public void Dispose()
        {
            // Liberar recursos al finalizar las pruebas
            Context.Dispose();
        }
    }
}
