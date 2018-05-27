using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace KameradenQA.Tests
{
    class CMSTests
    {
        static IWebDriver driver;
        DriverFactory df;
        Actions builder;

        [SetUp]
        public void initialize()
        {
            df = new DriverFactory("Chrome");
            driver = df.getDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void test()
        {

        }

        [TearDown]
        public void finishTest()
        {
            driver.Close();
        }
    }
}
