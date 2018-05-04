using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KameradenQA
{
    class DriverFactory
    {
        IWebDriver driver;
        public DriverFactory(String browser)
        {
            SetDriver(browser);
        }

        public void SetDriver(string browser)
        {
            switch (browser)
            {

                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
                case "Edge":
                    driver = new EdgeDriver();
                    break;
                default:
                    driver = null;
                    break;
            }
        }

        public IWebDriver getDriver()
        {
            return driver;
        }
    }
}
