﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium.Chrome;
using FluentAssertions;
using Polly;
using System.IO;
using OpenQA.Selenium;
using ServiceStack;
using Microsoft.Extensions.Configuration;
using Calculator.Test.common;
using System.ComponentModel;

namespace Calculator.Test.e2e
{
    [TestCategory("E2E")]
    [TestClass]
    public class EndToEndTest : IDisposable
    {
        private DockerSettings _setting;

        [TestInitialize]
        public void SetUp()
        {
            var config = TestHelper.InitConfiguration().Get<TestConfiguration>();
            _setting = new DockerSettings(config);
            this._setting.DockerComposePull();
            this._setting.DockerComposeUp();
        }

        [TestMethod]
        public void Test()
        {
            var co = new ChromeOptions();
            co.AddArgument("no-sandbox");
            if (!this._setting.OpenChrome)
                co.AddArgument("headless");


            co.PageLoadStrategy = PageLoadStrategy.Normal;

            Policy.Handle<Exception>().Retry(this._setting.RetryAttemps).Execute(() =>
            {
                using (var driver = new ChromeDriver(Directory.GetCurrentDirectory(), co))
                {
                    driver.Manage().Window.Maximize();
                    driver.Navigate().GoToUrl(this._setting.Url + "/signup");

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
            this._setting.DockerComposeDown();
        }
    }
}
