using ApiConsumer.Infrastructure.Startup;
using Microsoft.AspNetCore.Mvc;

[assembly: ApiController]
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
WebApplication.CreateBuilder(args)
              .ConfigureService()
              .Configure();