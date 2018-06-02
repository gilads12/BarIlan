using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using FluentAssertions;

namespace Calculator.Test.e2e
{
    [TestClass]
    public class End2EndTest : IDisposable
    {
        private ContainerManager _container;
        private string _url;


        [TestInitialize]
        public void SetUp()
        {
            _container = new ContainerManager(@"../../../e2e/docker-compose-e2e.yml", "5002", "test");
            _container.Init();
            this._url = _container.Url;
        }

        [TestMethod]
        public void Test()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(_url+"/signup");

                var email = driver.FindElementByName("email");
                email.SendKeys("myMail@mail.com");

                var name = driver.FindElementByName("name");
                name.SendKeys("myName");

                var password = driver.FindElementByName("password");
                password.SendKeys("myPassword");

                var signupBtn = driver.FindElementByClassName("btn");
                signupBtn.Submit();

                var oneDig = driver.FindElementByClassName("digit-1");
                var SevenDig = driver.FindElementByClassName("digit-7");
                var subtract = driver.FindElementByClassName("operator-subtract");
                var equeal = driver.FindElementByClassName("operator-equals");


                oneDig.Click();
                oneDig.Click();

                subtract.Click();

                SevenDig.Click();

                equeal.Click();

                var display = driver.FindElementByClassName("display");

                display.Text.Should().Be("4");
            }
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
