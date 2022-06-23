using Application.Patients.Commands.CreatePatient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.PatientController.Command
{
    [TestFixture]
    public class CreatePatientCommandTest : TestBase
    {
        [Test]
        public async Task CreatePatient_ReturnGuid()
        {
            //Arrage
            var patient = new CreatePatientCommand()
            {
                Birthday = DateTime.Now,
                Name = "Ali",
                NationalCode = 123421112
            };
            //Act
            var result = await SendAsync(patient);
            //Assert
            Assert.AreEqual(typeof(Guid), Is.EqualTo(result.GetType()));
        }


    }
}
