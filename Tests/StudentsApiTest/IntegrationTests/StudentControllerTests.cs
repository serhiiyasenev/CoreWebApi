using Common.Helpers;
using NUnit.Framework;
using StudentsApi.Entities;
using StudentsApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StudentsApiTest.IntegrationTests
{
    [TestFixture]
    public class StudentControllerTests
    {
        private StudentsApiWebFactory _factory;
        private HttpClient _client;
        private string _requestUri;
        private StringContent _content;
        private StudentModel _model;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new StudentsApiWebFactory();
            _client = _factory.CreateClient();
            _requestUri = "api/student";

            _model = new StudentModel
            {
                Name = "TestStudent1",
                Disciplines = new HashSet<string>
                {
                    "Math",
                    "Chemistry"
                }
            };

            _content = JsonHelper.ToStringContent(_model);
        }

        [Test]
        public async Task PostStudentModel_ReturnsResultAndOk()
        {
            // Act
            var actualResult = await _client.PostAsync(_requestUri, _content);
            var response = await actualResult.GetModelAsync<StudentEntity>();

            // Assert
            Assert.True(response.Id != 0);
            Assert.True(response.Id.ToString() != null);
            Assert.AreEqual(response.Name, _model.Name);
            Assert.AreEqual(response.Disciplines.Select(d => d.Name), _model.Disciplines);
        }

        [Test]
        public async Task GetStudentModel_ReturnsResultAndOk()
        {
            // Act
            var result = await _client.GetAsync(_requestUri);
            var response = JsonHelper.FromJsonToObject<List<StudentModel>>(result.EnsureSuccessStatusCode().Content.ReadAsStringAsync().Result);

            // Assert
            Assert.AreEqual(response.Last().Name, _model.Name);
            Assert.AreEqual(response.Last().Disciplines, _model.Disciplines);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}