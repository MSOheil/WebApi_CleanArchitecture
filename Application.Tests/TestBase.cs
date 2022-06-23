using Application.Common.Models.Dtos;
using Domain.Entities.CustomeIdentity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests
{
    [TestFixture]
    public class TestBase
    {
        private static IServiceScopeFactory _scopeFactory;
        [OneTimeSetUp]
        public void RunBeforAnyTests()
        {
            var services = new ServiceCollection();
            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }
        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<ISender>();

            return await mediator.Send(request);
        }
        //public static async Task<string> RunUserAsync(RegisterDto model)
        //{
        //    using var scope = _scopeFactory.CreateScope();

        //    var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();


        //}
    }
}
