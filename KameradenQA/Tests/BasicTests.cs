using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace KameradenQA.Tests
{
    class BasicTests
    {

        static IWebDriver driver;
        DriverFactory df;

        public BasicTests()
        {
        }

        [SetUp]
        public void initialize()
        {
            df = new DriverFactory("Chrome");
            driver = df.getDriver();
        }

        [Test]
        public void PageReferenceTest()
        {
            String[] NavBar = new String[] {"Home", "Over ons", "Meedoen", "Profielen", "Partners", "Contact", "Inloggen"};
            String[] PageNames = new String[] {"", "/over-ons", "registreren", "profielen", "partners", "contact", "inloggen" };
           
            for(int i = 0; i < NavBar.Length; i++)
            {
                for(int j = 0; j < PageNames.Length; j++)
                {
                    driver.Url = "Kameraden.test:8080" + PageNames[i];
                    driver.FindElement(By.LinkText(NavBar[j])).Click();
                    if (!driver.Url.Equals("http://kameraden.test:8080" + PageNames[j]))
                    {
                        Assert.Fail();
                    }
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
