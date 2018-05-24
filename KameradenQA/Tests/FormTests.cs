using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KameradenQA.Tests
{
    class FormTests
    {
        static IWebDriver driver;
        DriverFactory df;

        [SetUp]
        public void initialize()
        {
            df = new DriverFactory("Chrome");
            driver = df.getDriver();
        }

        [Test]
        public void RegisterTest()
        {
            driver.Url = "Kameraden.test:8080/registreren";
            driver.FindElement(By.Id("voornaam")).SendKeys("Jan");
            driver.FindElement(By.Id("achternaam")).SendKeys("Dijk");
            driver.FindElement(By.Id("tussenvoegsel")).SendKeys("van");
            driver.FindElement(By.Id("geboortedatum")).SendKeys("22121980");
            driver.FindElement(By.LinkText("Next")).Click();

            driver.FindElement(By.Id("woonplaats")).SendKeys("Den Bosch");
            driver.FindElement(By.Id("woonwijk")).SendKeys("n.v.t.");
            driver.FindElement(By.Id("adres")).SendKeys("Jan de la barlaan 8");
            driver.FindElement(By.Id("postcode")).SendKeys("5212NX");
            driver.FindElement(By.Id("telefoonnummer")).SendKeys("0612345678");
            driver.FindElement(By.Id("email")).SendKeys("test@test.test");
            driver.FindElement(By.LinkText("Next")).Click();

            driver.FindElement(By.Id("beroep")).SendKeys("Metselaar");
            driver.FindElement(By.Id("sport")).SendKeys("Cricket");
            driver.FindElement(By.Id("beperkingen")).SendKeys("Slecht zicht");
            driver.FindElement(By.Name("beschrijving")).SendKeys("Ik vind het leuk om piano te spelen");
            driver.FindElement(By.LinkText("Finish")).Click();

            if (!driver.Url.Equals("http://kameraden.test:8080/inloggen"))
            {
                Assert.Fail();
            }
        }

        [Test]
        public void LoginTest()
        {
            driver.Url = "Kameraden.test:8080/inloggen";
            driver.FindElement(By.Name("email")).SendKeys("tim@gmail.com");
            driver.FindElement(By.Name("password")).SendKeys("test");
            driver.FindElement(By.Name("login")).Click();

            if (!driver.Url.Equals("http://kameraden.test:8080/"))
            {
                Assert.Fail();
            }
        }

        [TearDown]
        public void finishTest()
        {
            driver.Close();
        }
    }


}

