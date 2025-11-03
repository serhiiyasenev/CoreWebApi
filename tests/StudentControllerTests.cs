using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Helpers;
using NUnit.Framework;
using StudentsApi.Entities;
using StudentsApi.Models;

namespace StudentsApiTest
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
                Disciplines =
                [
                    new DisciplineEntity { Name = "Math"},
                    new DisciplineEntity { Name = "Chemistry"}
                ]
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
            Assert.That(actualResult.Id != 0);
            Assert.That(actualResult.Name, Is.EqualTo(_model.Name));
            Assert.That(
                actualResult.Disciplines.Select(d => d.Name),
                Is.EquivalentTo(_model.Disciplines.Select(d => d.Name))
            );
        }

        [Test]
        public async Task GetStudentModel_ReturnsResultAndOk()
        {
            // Arrange 
            await _client.PostAsync(_requestUri, _content);

            // Act
            var getStudents = await _client.GetAsync(_requestUri);
            var studentsResult = await getStudents.GetModelAsync<List<StudentEntity>>();
            var actualStudent = studentsResult.Last();

            // Assert
            Assert.That(actualStudent.Name, Is.EqualTo(_model.Name));
            Assert.That(
                actualStudent.Disciplines.Select(d => d.Name),
                Is.EquivalentTo(_model.Disciplines.Select(d => d.Name))
            );
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}