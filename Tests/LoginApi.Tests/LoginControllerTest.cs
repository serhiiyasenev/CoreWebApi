using System;
using Xunit;

namespace LoginApi.Tests
{
    public class LoginControllerTest : IClassFixture<LoginWebFactory>
    {
        private readonly LoginWebFactory _factory;

        public LoginControllerTest(LoginWebFactory factory)
        {
            _factory = factory;

        }


        [Fact]
        public void Test1()
        {

        }
    }
}
