using OpenQA.Selenium;
//using System;
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
            df = new DriverFactory("Firefox");
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

        [Test]
        public void LoginTest()
        {
            //Enter login credentials and press login button
            driver.Url = Globals.address + "/inloggen";
            driver.FindElement(By.Name("email")).SendKeys("tim@gmail.com");
            driver.FindElement(By.Name("password")).SendKeys("test");
            driver.FindElement(By.Name("login")).Click();

            if (!driver.Url.Equals(Globals.address + "/"))
            {
                Assert.Fail();
            }
        }

        //VOOR REVIEWER: Deze code werkt niet omdat de sortable list op zodanige manier in Javascript is opgebouwd dat Selenium er niet mee kan werken.
        //Je ziet hier wel een versie die zou MOETEN werken als selenium niet ongemakkelijk om gaat met dit soort DOM manipulaties.
        [Test]
        public void MenuAdjustmentTestALWAYSFAILS()
        {
            LoginTest();
            driver.Url = Globals.address + "/";
            var compare1 = driver.FindElement(By.XPath("//*[@id=\"nav-list\"]/li[1]")).Text;
            var compare2 = driver.FindElement(By.XPath("//*[@id=\"nav-list\"]/li[2]")).Text;

            driver.Url = Globals.address + "/cms/menu";

            var element = driver.FindElement(By.XPath("/html/body/section/div/div[1]"));
            var target = driver.FindElement(By.XPath("/html/body/section/div/div[2]"));
            (new Actions(driver)).ClickAndHold(element).MoveByOffset(0, 44).Release(element).Perform();
            driver.FindElement(By.ClassName("success")).Click();
            driver.Url = Globals.address + "/";

            var compareTo1 = driver.FindElement(By.XPath("//*[@id=\"nav-list\"]/li[1]")).Text;
            var compareTo2 = driver.FindElement(By.XPath("//*[@id=\"nav-list\"]/li[2]")).Text;
        }

        [TearDown]
        public void finishTest()
        {
            driver.Close();
        }
    }
}
