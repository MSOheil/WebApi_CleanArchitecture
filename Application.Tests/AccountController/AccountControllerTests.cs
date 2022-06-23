//using Application.Common.Interfaces;
//using Application.Common.Models.Dtos;
//using Domain.Entities.CustomeIdentity;
//using Infrastructure.Identity;
//using Microsoft.AspNetCore.Identity;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebApiWithClean.Controllers;

//namespace Application.Tests.AccountControllerTests
//{
//    [TestFixture]
//    public class AccountControllerTests
//    {
//        private IIdentityService _identity;
//        [OneTimeSetUp]
//        public void ArrageForController()
//        {


//            _identity = new IdentityService();
//        }
//        [Test]
//        public async void Register_RegisterDto_ReturntrueAsync()
//        {
//            //Arrage
//            var user = new RegisterDto()
//            {
//                Email = "alia@gmail.com",
//                Password = "soheyl@M",
//                UserName = "ofking601"
//            };
//            //Act
//            var result = await _identity.RegisterAsync(user);
//            //Assert
//            Assert.AreEqual(200, result);
//        }

//    }
//}
