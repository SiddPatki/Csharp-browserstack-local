using System;
using System.Collections.Generic;
using BrowserStack;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
namespace csharp_selenium_browserstack
{
    class LocalTest
    {
        public static void execute()
        {
            // Update your credentials
            String? BROWSERSTACK_USERNAME = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
            if (BROWSERSTACK_USERNAME is null)
                BROWSERSTACK_USERNAME = "BROWSERSTACK_USERNAME";

            String? BROWSERSTACK_ACCESS_KEY = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
            if (BROWSERSTACK_ACCESS_KEY is null)
                BROWSERSTACK_ACCESS_KEY = "BROWSERSTACK_ACCESS_KEY";
            IWebDriver driver;
            SafariOptions capabilities = new SafariOptions();
            Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();
            browserstackOptions.Add("osVersion", "14");
            browserstackOptions.Add("deviceName", "iPhone 12");
            browserstackOptions.Add("realMobile", "true");
            browserstackOptions.Add("local", "true");
            browserstackOptions.Add("buildName", "browserstack-local-c#");
            browserstackOptions.Add("userName", BROWSERSTACK_USERNAME);
            browserstackOptions.Add("accessKey", BROWSERSTACK_ACCESS_KEY);
            capabilities.AddAdditionalOption("bstack:options", browserstackOptions);

            // Creates an instance of Local
            Local local = new Local();

            // You can also set an environment variable - "BROWSERSTACK_ACCESS_KEY".
            List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>();
            // Starts the Local instance with the required arguments
            bsLocalArgs.Add(new KeyValuePair<string, string>("key", BROWSERSTACK_ACCESS_KEY));

            // Starts the Local instance with the required arguments
            local.start(bsLocalArgs);
            driver = new RemoteWebDriver(new Uri("https://hub.browserstack.com/wd/hub/"), capabilities);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
           
            try
            {
                driver.Navigate().GoToUrl("http://bs-local.com:45691/check");
                // getting body content
                String body_text = driver.FindElement(By.TagName("body")).Text;
                if (body_text == "Up and running")
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \"Local test ran successful\"}}");
                }
            }
            catch
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \" Issues connecting local\"}}");
            }
            driver.Quit();
            local.stop();

        }
    }
}
