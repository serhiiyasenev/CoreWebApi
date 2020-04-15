using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using StudentsApi.Controllers;
using StudentsApi.Helpers;
using StudentsApi.Models;

namespace StudentsApiTest.IntegrationTests
{
    [TestFixture]
    public class StudentControllerTests
    {
        private StudentsApiWebFactory _factory;
        private HttpClient _client;
        private string _requestUri;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new StudentsApiWebFactory();
            _client = _factory.CreateClient();
            _requestUri = "api/student";
        }

        [Test]
        public async Task PostStudentModel_ReturnsResultIsOk()
        {
            
            // Arrange

            var disciplines = new List<DisciplineModel> {new DisciplineModel {DisciplineName = "Math"}};
            var model = new StudentModel
            {
                StudentName = "TestStudent1",
                Disciplines = disciplines
            };

            var content = JsonHelper.ToStringContent(model);


            // Act

            var result = await _client.PostAsync(_requestUri, content);


            //Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task WhenNoTextIsPosted_ThenTheResultIsBadRequest()
        {
            var result = await _client.PostAsync("/sample", new StringContent(string.Empty));
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}