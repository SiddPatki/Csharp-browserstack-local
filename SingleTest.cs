using System;
using System.Collections.Generic;
using BrowserStack;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
namespace csharp_selenium_browserstack
{
    class SingleTest
    {
        public static void execute()
        {
             // Update your credentials
            String? BROWSERSTACK_USERNAME = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
            String? BROWSERSTACK_ACCESS_KEY = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
            String? BROWSERSTACK_LOCAL_IDENTIFIER = Environment.GetEnvironmentVariable("BROWSERSTACK_LOCAL_IDENTIFIER");
            String? BROWSERSTACK_LOCAL = Environment.GetEnvironmentVariable("BROWSERSTACK_LOCAL");
            String? BROWSERSTACK_BUILD_NAME = Environment.GetEnvironmentVariable("BROWSERSTACK_BUILD_NAME");
            
            if (BROWSERSTACK_LOCAL_IDENTIFIER is null)
                BROWSERSTACK_LOCAL_IDENTIFIER = "HelloMyWorld";
            if (BROWSERSTACK_LOCAL is null)
                BROWSERSTACK_LOCAL = "true";
            
            
            IWebDriver driver;
            SafariOptions capabilities = new SafariOptions();
            Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();
            browserstackOptions.Add("osVersion", "14");
            browserstackOptions.Add("deviceName", "iPhone 12");
            browserstackOptions.Add("realMobile", "true");
            browserstackOptions.Add("local", BROWSERSTACK_LOCAL);
            browserstackOptions.Add("localIdentifier", BROWSERSTACK_LOCAL_IDENTIFIER);
            browserstackOptions.Add("buildName", BROWSERSTACK_BUILD_NAME);
            browserstackOptions.Add("sessionName", "Local Test");
            browserstackOptions.Add("userName", BROWSERSTACK_USERNAME);
            browserstackOptions.Add("accessKey", BROWSERSTACK_ACCESS_KEY);
            capabilities.AddAdditionalOption("bstack:options", browserstackOptions);


            // Creates an instance of Local
            Local local = new Local();

            // You can also set an environment variable - "BROWSERSTACK_ACCESS_KEY".
            List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>();
            // Starts the Local instance with the required arguments
            bsLocalArgs.Add(new KeyValuePair<string, string>("key", BROWSERSTACK_ACCESS_KEY));
            bsLocalArgs.Add(new KeyValuePair<string, string>("binarypath", "./BrowserStackLocal"));
            bsLocalArgs.Add(new KeyValuePair<string, string>("v", "true"));
            bsLocalArgs.Add(new KeyValuePair<string, string>("logfile", "./logs.txt"));
            bsLocalArgs.Add(new KeyValuePair<string, string>("forcelocal", "true"));
            bsLocalArgs.Add(new KeyValuePair<string, string>("-use-system-installed-ca", "true"));
            bsLocalArgs.Add(new KeyValuePair<string, string>("localIdentifier", BROWSERSTACK_LOCAL_IDENTIFIER));


            // Starts the Local instance with the required arguments
            local.start(bsLocalArgs);
            driver = new RemoteWebDriver(new Uri("https://hub.browserstack.com/wd/hub/"), capabilities);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                driver.Navigate().GoToUrl("https://bstackdemo.com/");
                // getting name of the product
                String product_on_page = wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"1\"]/p"))).Text;
                // clicking the 'Add to Cart' button
                IWebElement cart_btn = driver.FindElement(By.XPath("//*[@id='1']/div[4]"));
                wait.Until(driver => cart_btn.Displayed);
                cart_btn.Click();
                // waiting for the Cart pane to appear
                wait.Until(driver => driver.FindElement(By.ClassName("float-cart__content")).Displayed);
                // getting name of the product in the cart
                String product_in_cart = driver.FindElement(By.XPath("//*[@id='__next']/div/div/div[2]/div[2]/div[2]/div/div[3]/p[1]")).Text;
                if (product_on_page == product_in_cart)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \" Product has been successfully added to the cart!\"}}");
                }
            }
            catch
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \" Some elements failed to load.\"}}");
            }
            driver.Quit();
            local.stop();

        }
    }
}
