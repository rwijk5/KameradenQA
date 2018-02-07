using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KameradenQA
{
    class FirstTestCase
    {
        static IWebDriver driver;
        static void Main(string[] args)
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://kameraden.nl");
        }

        public static void Hello()
        {
           new System.OutOfMemoryException();
        }
    }
    
}
