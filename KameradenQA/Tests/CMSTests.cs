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
        public void ApproveRegisteredUser()
        {
            //Enter login credentials and press login button
            driver.Url = Globals.address + "/inloggen";
            driver.FindElement(By.Name("email")).SendKeys("tim@gmail.com");
            driver.FindElement(By.Name("password")).SendKeys("test");
            driver.FindElement(By.Name("login")).Click();

            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.ClassName("dropdown"))).Build().Perform();
            driver.FindElement(By.CssSelector("#nav-list > li:nth-child(7) > div > div.dropdown-content > a:nth-child(2)")).Click();
            driver.FindElement(By.CssSelector("body > header > a:nth-child(2)")).Click();
            //driver.FindElement(By.LinkText("Jan van Dijk")).Click();
            //Je kan niet het juiste element selecteren.
            driver.Url = "http://kameraden.test:8080/cms/gebruiker/keuren/1";
            driver.FindElement(By.LinkText("Goedkeuren")).Click();
        }

        [TearDown]
        public void finishTest()
        {
            driver.Close();
        }
    }
}
