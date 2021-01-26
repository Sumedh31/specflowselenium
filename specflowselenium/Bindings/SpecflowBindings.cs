namespace specflowselenium.Bindings
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Support.UI;
    using System;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.MSTest;
    using NUnit.Framework;
    using System.Collections.Generic;

    [Binding]
    class SpecflowBindings
    {
        private IWebDriver Driver;

        [When(@"I start the browser")]
        public void WhenIStartTheBrowser()
        {
            Driver = new FirefoxDriver();
        }

        [When(@"I navigate to '(.*)'")]
        public void WhenINavigateToHttpExample_Com(string Url)
        {
            Driver.Navigate().GoToUrl(Url);                        
        }
        [When(@"I click on the '(.*)'")]
        public void WhenIClickOnMoreInformation(string reqtext)
        {
            //Click on More information link
            Driver.FindElement(By.XPath("//a[contains(text(),'More information')]")).Click();            

        }
        [Then(@"a link with text '(.*)' must be present")]
        public void ThenTheExpectedTextShouldBePresent(string reqtext)
        {
            IWebElement elemRFC2606 = FindElement(Driver, By.XPath("//a[contains(text(),'RFC 2606')]"), 5);
            IWebElement elemRFC6761 = FindElement(Driver, By.XPath("//a[contains(text(),'RFC 6761')]"), 5);
            Assert.IsTrue(elemRFC2606.Displayed, "Text RFC2006 was not present");
            Assert.IsTrue(elemRFC6761.Displayed, "Text RFC 6761 was not present");
        }
        [Then(@"the 'Domain Names' box must contain '(.*)' at index '2'")]
        public void ThenTheExpectedElemetShouldBePresent(string reqtext)
        {
            IWebElement domainNameBox = Driver.FindElement(By.CssSelector("#sidebar_left>.navigation_box"));
            //Get all Iwebelement list of all present domain names 
            IList < IWebElement > domainNames = domainNameBox.FindElements(By.TagName("li"));

            //Ensure the webelement present at second index have required text 
            Assert.IsTrue(domainNames[1].GetAttribute("innerHTML").Contains(reqtext), "The required text was not present at index 2");
            Driver.Quit();
        }
        public static IWebElement FindElement(IWebDriver driver, By by, int timeoutInSeconds)
        {
            try
            {
                if (timeoutInSeconds > 0)
                {
                    SystemClock clock = new SystemClock();
                    var wait = new WebDriverWait(clock, driver, TimeSpan.FromSeconds(timeoutInSeconds), TimeSpan.FromMilliseconds(100));
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                    return wait.Until(drv => drv.FindElement(by));
                }
                return driver.FindElement(by);
            }
            catch (WebDriverTimeoutException)
            {

                return null;
            }
        }

    }
}
