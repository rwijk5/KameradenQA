﻿using OpenQA.Selenium;
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
    class FormTests
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
        public void RegisterTest()
        {
            //Fill in first page and continue
            driver.Url = "Kameraden.test:8080/registreren";
            driver.FindElement(By.Id("voornaam")).SendKeys("Jan");
            driver.FindElement(By.Id("achternaam")).SendKeys("Dijk");
            driver.FindElement(By.Id("tussenvoegsel")).SendKeys("van");
            driver.FindElement(By.Id("geboortedatum")).SendKeys("22121980");
            driver.FindElement(By.LinkText("Next")).Click();

            //Fill in second page and continue
            driver.FindElement(By.Id("woonplaats")).SendKeys("Den Bosch");
            driver.FindElement(By.Id("woonwijk")).SendKeys("n.v.t.");
            driver.FindElement(By.Id("adres")).SendKeys("Jan de la barlaan 8");
            driver.FindElement(By.Id("postcode")).SendKeys("5212NX");
            driver.FindElement(By.Id("telefoonnummer")).SendKeys("0612345678");
            driver.FindElement(By.Id("email")).SendKeys("test@test.test");
            driver.FindElement(By.LinkText("Next")).Click();

            //Fill in last page and continue
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
        public void RegisterValidationTest()
        {
            driver.Url = "Kameraden.test:8080/registreren";
            
            //Click next button to see which validations trigger
            driver.FindElement(By.LinkText("Next")).Click();

            //Check if the correct validations trigger
            if (!driver.FindElement(By.Id("voornaam-error")).Enabled ||
                !driver.FindElement(By.Id("achternaam-error")).Enabled ||
                !driver.FindElement(By.Id("geboortedatum-error")).Enabled)
            {
                Assert.Fail();
            }
            //Fill in the form propely and continue
            driver.FindElement(By.Id("voornaam")).SendKeys("Jan");
            driver.FindElement(By.Id("achternaam")).SendKeys("Dijk");
            driver.FindElement(By.Id("tussenvoegsel")).SendKeys("van");
            driver.FindElement(By.Id("geboortedatum")).SendKeys("12221980");
            driver.FindElement(By.LinkText("Next")).Click();

           //Click next button to see which validations trigger
            driver.FindElement(By.LinkText("Next")).Click();

            //Check if the correct validations trigger
            if (!driver.FindElement(By.Id("woonplaats-error")).Enabled ||
               !driver.FindElement(By.Id("adres-error")).Enabled ||
               !driver.FindElement(By.Id("postcode-error")).Enabled ||
                !driver.FindElement(By.Id("telefoonnummer-error")).Enabled ||
                 !driver.FindElement(By.Id("email-error")).Enabled)
            {
                Assert.Fail();
            }
            //Fill in the form properly and continue
            driver.FindElement(By.Id("woonplaats")).SendKeys("Den Bosch");
            driver.FindElement(By.Id("woonwijk")).SendKeys("n.v.t.");
            driver.FindElement(By.Id("adres")).SendKeys("Jan de la barlaan 8");
            driver.FindElement(By.Id("postcode")).SendKeys("5212NX");
            driver.FindElement(By.Id("telefoonnummer")).SendKeys("0612345678");
            driver.FindElement(By.Id("email")).SendKeys("test@test.test");

            //Finish the form properly since there are no fields that are required.
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
            //Enter login credentials and press login button
            driver.Url = "Kameraden.test:8080/inloggen";
            driver.FindElement(By.Name("email")).SendKeys("tim@gmail.com");
            driver.FindElement(By.Name("password")).SendKeys("test");
            driver.FindElement(By.Name("login")).Click();

            if (!driver.Url.Equals("http://kameraden.test:8080/"))
            {
                Assert.Fail();
            }
        }

        [Test]
        public void EditProfileTest()
        {
            //Login
            LoginTest();
            //Go to profile
            builder = new Actions(driver);
            builder.MoveToElement(driver.FindElement(By.ClassName("dropdown"))).Build().Perform();
            driver.FindElement(By.CssSelector("#nav-list > li:nth-child(7) > div > div.dropdown-content > a:nth-child(1)")).Click();
            //Go to edit profile
            driver.FindElement(By.CssSelector("#main > div > div:nth-child(1) > div > div > ul > li:nth-child(1) > a")).Click();

            //Edit profile
            driver.FindElement(By.Name("description")).Clear();
            driver.FindElement(By.Name("description")).SendKeys("beschrijving");
            driver.FindElement(By.Name("birth-date")).SendKeys("12221980");

            //Count total amount of current interest fields
            int interests;
            interests = driver.FindElements(By.Name("interest[]")).Count();

            //If the cap of 5 interests hasn't been reached add an interest field
            if (interests < 5)
            {
                int newInterest = interests + 1;
                driver.FindElement(By.Id("addInterest")).Click();
                driver.FindElement(By.XPath("//*[@id=\"main\"]/div/div/div/form/div[3]/div[1]/div/input[" + newInterest + "]")).SendKeys("dit moet veranderd worden");
                interests = driver.FindElements(By.Name("interest[]")).Count();
            }

            //Replace interest values with a numeric range from 1-5 (depending on how many fields there are)
            for (int i = 1; i < interests + 1; i++)
            {
                string interestValue = i.ToString();
                driver.FindElement(By.XPath("//*[@id=\"main\"]/div/div/div/form/div[3]/div[1]/div/input[" + i + "]")).Clear();
                driver.FindElement(By.XPath("//*[@id=\"main\"]/div/div/div/form/div[3]/div[1]/div/input[" + i + "]")).SendKeys(interestValue);

            }

            //Count the total amount of disability fields
            int disabilities;
            disabilities = driver.FindElements(By.Name("disability[]")).Count();

            //If the cap of 5 disabilities hasn't been reached add an disability field
            if (disabilities < 5)
            {
                int newDisability = disabilities + 1;
                driver.FindElement(By.Id("addDisability")).Click();
                driver.FindElement(By.XPath("//*[@id=\"main\"]/div/div/div/form/div[3]/div[2]/div/input[" + newDisability + "]")).SendKeys("dit moet veranderd worden");
                disabilities = driver.FindElements(By.Name("disability[]")).Count();
            }

            //Replace disability values with a numeric range from 1-5 (depending on how many fields there are)
            for (int i = 1; i < disabilities + 1; i++)
            {
                string disabilityValue = i.ToString();
                driver.FindElement(By.XPath("//*[@id=\"main\"]/div/div/div/form/div[3]/div[2]/div/input[" + i + "]")).Clear();
                driver.FindElement(By.XPath("//*[@id=\"main\"]/div/div/div/form/div[3]/div[2]/div/input[" + i + "]")).SendKeys(disabilityValue);
            }
            //Save changes
            driver.FindElement(By.CssSelector("#main > div > div > div > form > input[type=\"submit\"]:nth-child(5)")).Click();

            //Go to profile
            IWebElement dropdown = driver.FindElement(By.ClassName("dropdown"));
            builder = new Actions(driver);
            builder.MoveToElement(dropdown).Build().Perform();
            driver.FindElement(By.CssSelector("#nav-list > li:nth-child(7) > div > div.dropdown-content > a:nth-child(1)")).Click();


            //Check if the interests results came trough
            for (int i = 2; i < interests + 2; i++)
            {
                if (driver.FindElement(By.XPath("//*[@id=\"main\"]/div/div[2]/div[1]/div/div/ul[1]/li[" + i + "]")).Text != (i - 1).ToString())
                {
                    Assert.Fail();
                }
            }

            //Check if the disability results came trough
            for (int i = 2; i < disabilities + 2; i++)
            {
                if (driver.FindElement(By.XPath("//*[@id=\"main\"]/div/div[2]/div[1]/div/div/ul[2]/li[" + i + "]")).Text != (i - 1).ToString())
                {
                    Assert.Fail();
                }
            }
        }


        [TearDown]
        public void finishTest()
        {
            driver.Close();
        }
    }


}

