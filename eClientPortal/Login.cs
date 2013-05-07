using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace eClientPortal
{
    [TestFixture]
    public class Login
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;
        private string testResult;
        private string testResultReason;
        private static string baseURL = Config.startURL;
        private static string testUserName = Config.testUserName;
        private static string testPassWord = Config.testPassWord;
        private static string auxUserName = Config.auxUserName;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            verificationErrors = new StringBuilder();
            testResult = null;
            testResultReason = null;
            Config.ReadConfig("config.xml");
        }

        [Test]
        public void LoginLogOut()
        {
            driver.Navigate().GoToUrl(Config.startURL + "Login.aspx");
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ctrlLogin_Login1_UserName")).Clear();
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ctrlLogin_Login1_UserName")).SendKeys(Config.testUserName);
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ctrlLogin_Login1_Password")).Clear();
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ctrlLogin_Login1_Password")).SendKeys(Config.testPassWord);
            driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ctrlLogin_Login1_BtnSubmit")).Click();
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text,
                                            "^[\\s\\S]* MY ACCOUNT OVERVIEW[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("ctl00_ctlRightNavbar_btnLogout")).Click();


        }
    }
}
