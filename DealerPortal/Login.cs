using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace DealerPortalTests
{
    [TestFixture]
    public class Login
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;
        private string testResult;
        private string testResultReason;
        private static string baseURL = DealerPortal.Config.startURL;
        private static string testUserName = DealerPortal.Config.testUserName;
        private static string testPassWord = DealerPortal.Config.testPassWord;
        private static string auxUserName = DealerPortal.Config.auxUserName;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            verificationErrors = new StringBuilder();
            testResult = null;
            testResultReason = null;
            DealerPortal.Config.ReadConfig("config.xml");
        }
   
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());

            if (testResult == "I") // it's either gonna be "I" or otherwise pass. fails are dealt with in their exceptions.
            {
                Console.WriteLine("TEST INCOMPLETE.  Reason: " + testResultReason + "\r\n");
            }
            else Console.WriteLine("TEST PASSED\r\n");
        }

        [Test]
        public void LoginLogout()
        {
            driver.Navigate().GoToUrl(baseURL + "Login.aspx?ReturnUrl=%2fDealerPortal%2f");
            driver.FindElement(By.Id("ContentPlaceHolder1_ctrlLogin_Login1_UserName")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolder1_ctrlLogin_Login1_UserName")).SendKeys(testUserName);
            driver.FindElement(By.Id("ContentPlaceHolder1_ctrlLogin_Login1_Password")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolder1_ctrlLogin_Login1_Password")).SendKeys(testPassWord);
            driver.FindElement(By.Id("ContentPlaceHolder1_ctrlLogin_Login1_BtnSubmit")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 60)
                {
                    testResult = "I";
                    testResultReason = "timeout waiting on login to return";
                    Assert.Fail(testResultReason);               
                }
                try
                {
                    if (IsElementPresent(By.Id("ctlRightNavbar_ctlUserName"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, testUserName));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            
            driver.FindElement(By.XPath("//a[contains(text(),'Logout')]")).Click();  // Put the seat back down
        }
 
        [Test]
        public void ForgotPassword()
        {
            driver.Navigate().GoToUrl(baseURL + "Login.aspx?ReturnUrl=%2fDealerPortal%2f");
            driver.FindElement(By.Id("ContentPlaceHolder1_ctrlLogin_Login1_resetPassword")).Click();
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout waiting on accounts list to load");
                try
                {
                    if (IsElementPresent(By.Id("ContentPlaceHolder1_ResetPassword1_tbxEmail"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.Id("ContentPlaceHolder1_ResetPassword1_tbxEmail")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolder1_ResetPassword1_tbxEmail")).SendKeys(DealerPortal.Config.auxEmailAddr);
            driver.FindElement(By.Id("ContentPlaceHolder1_ResetPassword1_btnSearch")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout waiting on return or accounts chooser");
                try
                {
                    if (IsElementPresent(By.Id("ContentPlaceHolder1_ResetPassword1_btnSearch"))) break;
                    if (IsElementPresent(By.Id("ContentPlaceHolder1_ResetPassword1_userList")))
                    {
                        new SelectElement(driver.FindElement(By.Id("ContentPlaceHolder1_ResetPassword1_userList"))).SelectByText("selenium2");
                        driver.FindElement(By.Id("ContentPlaceHolder1_ResetPassword1_btnSubmit")).Click();
                        break;
                    }
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*temporary password has been emailed[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
                Console.WriteLine("TEST FAILED. Reason: " + e.Message + " for regular expression \"^[\\s\\S]*temporary password has been emailed[\\s\\S]*$\"");
                Assert.Fail(e.Message); 
            }
        }
 
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alert.Text;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
