using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium.Chrome;
using FluentAssertions;
using Polly;
using System.IO;
using System.ComponentModel;
using OpenQA.Selenium;

namespace Calculator.Test.e2e
{
    [TestClass]
    public class EndToEndTest : IDisposable
    {
        private readonly DockerSettings _settings = new DockerSettings();


        [TestInitialize]
        public void SetUp()
        {
            //this._settings.DockerComposePull();
            //this._settings.DockerComposeUp();
        }

        [Ignore]
        [TestMethod]
        public void Test()
        {

            var co = new ChromeOptions();
            co.AddArgument("no-sandbox");
            co.AddArgument("headless");


            co.PageLoadStrategy = PageLoadStrategy.Normal;

            Policy.Handle<Exception>().Retry(4).Execute(() =>
            {
                using (var driver = new ChromeDriver(Directory.GetCurrentDirectory(), co))
                {
                    driver.Manage().Window.Maximize();
                    driver.Navigate().GoToUrl(_settings.Url + "/signup");

                    var email = driver.FindElementByName("email");
                    email.SendKeys("myMail@mail.com");

                    var name = driver.FindElementByName("name");
                    name.SendKeys("myName");

                    var password = driver.FindElementByName("password");
                    password.SendKeys("myPassword");

                    var signupBtn = driver.FindElementByClassName("btn");
                    signupBtn.Submit();

                    var oneDig = driver.FindElementByClassName("digit-1");
                    var fiveDig = driver.FindElementByClassName("digit-5");
                    var SevenDig = driver.FindElementByClassName("digit-7");
                    var subtract = driver.FindElementByClassName("operator-subtract");
                    var plus = driver.FindElementByClassName("operator-plus");
                    var equal = driver.FindElementByClassName("operator-equals");


                    oneDig.Click();
                    oneDig.Click();
                    subtract.Click();
                    SevenDig.Click();
                    equal.Click();
                    var display = driver.FindElementByClassName("display");

                    plus.Click();
                    fiveDig.Click();
                    subtract.Click();
                    oneDig.Click();
                    equal.Click();
                    display.Text.Should().Be("8");
                }
            });

        }

        public void Dispose()
        {
            this._settings.DockerComposeDown();
        }
    }
}
