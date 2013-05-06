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
    public class Accounts
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

            if (testResult == "I")
            {
                Console.WriteLine("TEST INCOMPLETE.  Reason: " + testResultReason + "\r\n");
            }
            else Console.WriteLine("TEST PASSED\r\n");
        }

        [Test]
        public void ChangeSelectedProduct()
        {
            driver.Navigate().GoToUrl(baseURL + "Login.aspx");
            driver.FindElement(By.XPath("//input[contains(@name,'UserName')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'UserName')]")).SendKeys(DealerPortal.Config.testUserName);
            driver.FindElement(By.XPath("//input[contains(@name,'Password')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'Password')]")).SendKeys(DealerPortal.Config.testPassWord);
            driver.FindElement(By.XPath("//input[contains(@id,'BtnSubmit')]")).Click();
            for (int second = 0; ; second++)   // Wait on the DEALERS sub link to become present before proceeding. 
            {
                if (second >= 60) Assert.Fail("Timed out waiting on DEALERS main link to appear");
                try
                {
                    if (IsElementPresent(By.XPath("//td[contains(text(),'DEALERS')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//td[contains(text(),'DEALERS')]")).Click();  // Click Dealers Main Link

            for (int second = 0; ; second++)   // Wait on the DEALERS sub link to become present before proceeding. 
            {
                if (second >= 60) Assert.Fail("Timed out waiting on DEALERS sub link to appear");
                try
                {
                    if (IsElementPresent(By.XPath("//a[contains(text(),'DEALERS')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//a[contains(text(),'DEALERS')]")).Click();  // Click Dealers sub link

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("Timed out waiting on PRODUCT SELECTION DROPDOWN to appear");
                try
                {
                    if (IsElementPresent(By.XPath("//select[@class='product']"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            // select GAP INS dropdown option
            new SelectElement(driver.FindElement(By.Id("SelectionControl_ddlProducts1"))).SelectByText("GAP INS");

            // Make sure dropdown is loaded before eval
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("Timed out waiting on PRODUCT SELECTION DROPDOWN to appear");
                try
                {
                    if (IsElementPresent(By.XPath("//select[@class='product']"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            // Eval assert 
            try
            {
                Assert.IsTrue(IsElementPresent(By.CssSelector("option[value=\"GAP\"]")));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
                Console.WriteLine("Exception occurred on Assertion: option[value=\"GAP\"]" + e.Message);
            }


            // select GAP WAIVER ropdown option
            new SelectElement(driver.FindElement(By.Id("SelectionControl_ddlProducts1"))).SelectByText("Gap Waiver");

            // Make sure dropdown is loaded before eval
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("Timed out waiting on PRODUCT SELECTION DROPDOWN to appear");
                try
                {
                    if (IsElementPresent(By.XPath("//select[@class='product']"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            // Eval assert 
            try
            {
                Assert.IsTrue(IsElementPresent(By.CssSelector("option[value=\"GAPWAIVER\"]")));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
                Console.WriteLine("Exception occurred on Assertion: option[value=\"GAPWAIVER\"]" + e.Message);
            }



            // select dropdown option
            new SelectElement(driver.FindElement(By.Id("SelectionControl_ddlProducts1"))).SelectByText("Preferred");

            // Make sure dropdown is loaded before eval
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("Timed out waiting on PRODUCT SELECTION DROPDOWN to appear");
                try
                {
                    if (IsElementPresent(By.XPath("//select[@class='product']"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            // Eval assert 
            try
            {
                Assert.IsTrue(IsElementPresent(By.CssSelector("option[value=\"PPP\"]")));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
                Console.WriteLine("Exception occurred on Assertion: option[value=\"PPP\"]" + e.Message);
            }


            // select dropdown option
            new SelectElement(driver.FindElement(By.Id("SelectionControl_ddlProducts1"))).SelectByText("Secure Dealer Solutions");

            // Make sure dropdown is loaded before eval
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("Timed out waiting on PRODUCT SELECTION DROPDOWN to appear");
                try
                {
                    if (IsElementPresent(By.XPath("//select[@class='product']"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            // Eval assert 
            try
            {
                Assert.IsTrue(IsElementPresent(By.CssSelector("option[value=\"SDS\"]")));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
                Console.WriteLine("Exception occurred on Assertion: option[value=\"SDS\"]" + e.Message);
            }


            // select dropdown option
            new SelectElement(driver.FindElement(By.Id("SelectionControl_ddlProducts1"))).SelectByText("ICS");

            // Make sure dropdown is loaded before eval
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("Timed out waiting on PRODUCT SELECTION DROPDOWN to appear");
                try
                {
                    if (IsElementPresent(By.XPath("//select[@class='product']"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            // Eval assert 
            try
            {
                Assert.IsTrue(IsElementPresent(By.CssSelector("option[value=\"ICS\"]")));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
                Console.WriteLine("Exception occurred on Assertion: option[value=\"ICS\"]" + e.Message);
            }


            // select dropdown option
            new SelectElement(driver.FindElement(By.Id("SelectionControl_ddlProducts1"))).SelectByText("Credit Insurance");

            // Make sure dropdown is loaded before eval
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("Timed out waiting on PRODUCT SELECTION DROPDOWN to appear");
                try
                {
                    if (IsElementPresent(By.XPath("//select[@class='product']"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            // Eval assert 
            try
            {
                Assert.IsTrue(IsElementPresent(By.CssSelector("option[value=\"CREDITINSURANCE\"]")));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
                Console.WriteLine("Exception occurred on Assertion: option[value=\"CREDITINSURANCE\"]" + e.Message);
            }


            // select dropdown option
            new SelectElement(driver.FindElement(By.Id("SelectionControl_ddlProducts1"))).SelectByText("Choose Product");

            // Make sure dropdown is loaded before eval
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("Timed out waiting on PRODUCT SELECTION DROPDOWN to appear");
                try
                {
                    if (IsElementPresent(By.XPath("//select[@class='product']"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            // Eval assert 
            try
            {
                Assert.IsTrue(IsElementPresent(By.CssSelector("option[value=\"NONE\"]")));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
                Console.WriteLine("Exception occurred on Assertion: option[value=\"NONE\"]" + e.Message);
            }
            testResult  = "P";
        }

        [Test]
        public void ChangePassword()
        {
            // Go to dealer portal and login as test user
            driver.Navigate().GoToUrl(baseURL + "Login.aspx");
            driver.FindElement(By.XPath("//input[contains(@name,'UserName')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'UserName')]")).SendKeys(DealerPortal.Config.testUserName);
            driver.FindElement(By.XPath("//input[contains(@name,'Password')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'Password')]")).SendKeys(DealerPortal.Config.testPassWord);
            driver.FindElement(By.XPath("//input[contains(@id,'BtnSubmit')]")).Click();

            for (int second = 0; ; second++)   // Wait on the ADMIN link to become present before proceeding. 
            {
                if (second >= 60) Assert.Fail("Timed out waiting on ADMIN link to appear");
                try
                {
                    if (IsElementPresent(By.XPath("//a[contains(text(),'MY INFO')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//a[contains(text(),'MY INFO')]")).Click();

            
            for (int second = 0; ; second++)   // Wait on the ADMIN link to become present before proceeding. 
            {
                if (second >= 60) Assert.Fail("Timed out waiting on Change Password link to appear");
                try
                {
                    if (IsElementPresent(By.XPath("//div[contains(text(),'Change Password')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//div[contains(text(),'Change Password')]")).Click();

            driver.FindElement(By.XPath("//input[contains(@name,'tbxCurrentPassword')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'tbxCurrentPassword')]")).SendKeys(DealerPortal.Config.testPassWord);
            driver.FindElement(By.XPath("//input[contains(@name,'tbxNewPassword1')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'tbxNewPassword1')]")).SendKeys(DealerPortal.Config.testPassWord);
            driver.FindElement(By.XPath("//input[contains(@name,'tbxNewPassword2')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'tbxNewPassword2')]")).SendKeys(DealerPortal.Config.testPassWord);
            driver.FindElement(By.Id("ContentPlaceHolder1_AccountInfo1_ChangePassword_btnUpdate")).Click();

            Thread.Sleep(8000);
            
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Your password has been successfully changed[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
                Console.WriteLine("TEST FAILED.  Reason: " +
                                  e.Message + "for the regular expression \"^[\\s\\S]*Your password has been successfully changed[\\s\\S]*$\" :" + "\r\n" + 
                                  "In other words, the test expected to see a success message after it clicked change password but did not.\r\n");
            }
        }

        [Test]
        public void MyInfo()
        {
            driver.Navigate().GoToUrl(baseURL + "Login.aspx");
            driver.FindElement(By.Id("ContentPlaceHolder1_ctrlLogin_Login1_UserName")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolder1_ctrlLogin_Login1_UserName")).SendKeys(DealerPortal.Config.testUserName);
            driver.FindElement(By.Id("ContentPlaceHolder1_ctrlLogin_Login1_Password")).Clear();
            driver.FindElement(By.Id("ContentPlaceHolder1_ctrlLogin_Login1_Password")).SendKeys(DealerPortal.Config.testPassWord);
            driver.FindElement(By.Id("ContentPlaceHolder1_ctrlLogin_Login1_BtnSubmit")).Click();

 

            for (int second = 0; ; second++)  
            {
                if (second >= 60) Assert.Fail("Timeout waiting on login return");
                try
                {
                    if (IsElementPresent(By.Id("SiteMenu1_SiteMenu1__ctl0_MY INFO"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.Id("SiteMenu1_SiteMenu1__ctl0_MY INFO")).Click();
    
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout waiting on my info page");
                try
                {
                    if (IsElementPresent(By.Id("ContentPlaceHolder1_AccountInfo1_tbxUserName"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            try
            {
                Assert.AreEqual(DealerPortal.Config.testUserName, driver.FindElement(By.Id("ContentPlaceHolder1_AccountInfo1_tbxUserName")).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
 
            driver.FindElement(By.Id("ctlRightNavbar_A2")).Click();
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
