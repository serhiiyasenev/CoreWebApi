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
            // Arrange
            var postStudents = await _client.PostAsync(_requestUri, _content);

            // Act
            var actualResult = await postStudents.GetModelAsync<StudentEntity>();

            // Assert
            Assert.True(actualResult.Id != 0);
            Assert.True(actualResult.Id.ToString() != null);
            Assert.AreEqual(actualResult.Name, _model.Name);
            Assert.AreEqual(actualResult.Disciplines.Select(d => d.Name), _model.Disciplines);
        }

        [Test]
        public async Task GetStudentModel_ReturnsResultAndOk()
        {
            // Arrange 
            var getStudents = await _client.GetAsync(_requestUri);
            var response = await getStudents.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

            // Act
            var actualResult = JsonHelper.FromJsonToObject<List<StudentModel>>(response);

            // Assert
            Assert.AreEqual(actualResult.Last().Name, _model.Name);
            Assert.AreEqual(actualResult.Last().Disciplines, _model.Disciplines);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}