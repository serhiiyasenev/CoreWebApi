using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using StudentsApi.Entities;
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
                StudentName = "TestStudent1",
                Disciplines = new HashSet<DisciplineModel>
                {
                    new DisciplineModel
                    {
                        DisciplineName = "Math"
                    },
                    new DisciplineModel
                    {
                        DisciplineName = "Chemistry"
                    }
                }
            };

            _content = JsonHelper.ToStringContent(_model);
        }

        [Test]
        public async Task PostStudentModel_ReturnsResultAndOk()
        {
            // Act
            var result = await _client.PostAsync(_requestUri, _content);
            var response = JsonHelper.FromJsonToObject<StudentEntity>(result.EnsureSuccessStatusCode().Content.ReadAsStringAsync());

            // Assert
            Assert.True(response.StudentId != 0);
            Assert.True(response.StudentId.ToString() != null);
            Assert.AreEqual(response.StudentName, _model.StudentName);
            Assert.AreEqual(response.Disciplines, _model.Disciplines);
        }

        [Test]
        public async Task GetStudentModel_ReturnsResultAndOk()
        {
            // Act
            var result = await _client.GetAsync(_requestUri);
            var response = JsonHelper.FromJsonToObject<StudentModel>(result.EnsureSuccessStatusCode().Content.ReadAsStringAsync());

            // Assert
            Assert.True(response.StudentId != 0);
            Assert.True(response.StudentId.ToString() != null);
            Assert.AreEqual(response.StudentName, _model.StudentName);
            Assert.AreEqual(response.Disciplines, _model.Disciplines);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}