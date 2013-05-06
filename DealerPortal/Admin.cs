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
    public class Admin
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

            // Console.WriteLine("Result: " + testResult);
            if (testResult == "I")
            {
                Console.WriteLine("TEST INCOMPLETE.  Reason: " + testResultReason + "\r\n");
            }
            else Console.WriteLine("TEST PASSED\r\n");
          }

        [Test]
        public void ViewUsers()
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
                    if (IsElementPresent(By.XPath("//td[contains(text(),'ADMIN')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//td[contains(text(),'ADMIN')]")).Click();

            for (int second = 0; ; second++)  // Wait on the VIEW USERS link to become present before proceeding.
            {
                if (second >= 60) Assert.Fail("timed out waiting on admin page");
                try
                {
                    
                    if (IsElementPresent(By.XPath("//a[contains(text(),'VIEW USERS')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//a[contains(text(),'VIEW USERS')]")).Click();
// TODO
             for (int second = 0; ; second++)  // validate this properly. dunno wtf i was thinking.
            {
                if (second >= 60) Assert.Fail("timed out waiting on admin page");
                try
                {

                    if (IsElementPresent(By.XPath("//a[contains(text(),'VIEW USERS')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            } 

            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.XPath("//a[contains(text(),'VIEW USERS')]")).Text, "^[\\s\\S]*VIEW USERS[\\s\\S]*$"));
                // Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*VIEW USERS[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.XPath("//a[contains(text(),'Logout')]")).Click();  // Put the seat back down

        }
 
        [Test]
        public void EditAccount()
        {   // Go to dealer portal and login as test user
            driver.Navigate().GoToUrl(baseURL + "Login.aspx");
            driver.FindElement(By.XPath("//input[contains(@name,'UserName')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'UserName')]")).SendKeys(DealerPortal.Config.testUserName);
            driver.FindElement(By.XPath("//input[contains(@name,'Password')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'Password')]")).SendKeys(DealerPortal.Config.testPassWord);
            driver.FindElement(By.XPath("//input[contains(@id,'BtnSubmit')]")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    
                    if (IsElementPresent(By.XPath("//td[contains(text(),'ADMIN')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//td[contains(text(),'ADMIN')]")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    
                    if (IsElementPresent(By.XPath("//a[contains(text(),'VIEW USERS')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//a[contains(text(),'VIEW USERS')]")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    
                    if (IsElementPresent(By.XPath("//input[contains(@id,'tbxSearchTerm')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//input[contains(@id,'tbxSearchTerm')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@id,'tbxSearchTerm')]")).SendKeys(DealerPortal.Config.auxUserName);
            driver.FindElement(By.XPath("//input[contains(@id,'btnSearch')]")).Click();

                    for (int second = 0; ; second++)
                    {
                        if (second >= 60) Assert.Fail("timeout");
                        try
                        {

                            if (IsElementPresent(By.XPath("//a[contains(@id,'lnkBtnEdit')]"))) break;
                        }
                        catch (Exception)
                        { }
                        Thread.Sleep(1000);
                    }

            
            driver.FindElement(By.XPath("//a[contains(@id,'lnkBtnEdit')]")).Click();
            
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout waiting on edit interface to load");
                try
                {

                    if (IsElementPresent(By.XPath("//input[contains(@id,'btnUpdate')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            // Change to some test information and btnUpdate

            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxFirstName')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxFirstName')]")).SendKeys("Webteam_edited");
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxLastName')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxLastName')]")).SendKeys("Testing_edited");
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxEmail')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxEmail')]")).SendKeys("selenium1_edit@srv0.com");
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxPhone')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxPhone')]")).SendKeys("5126370002");
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxFax')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxFax')]")).SendKeys("5126370001");
            driver.FindElement(By.XPath("//input[contains(@id,'btnUpdate')]")).Click();
            
            // Make sure there are no errors displayed. 

            try
            {
                Assert.IsFalse(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*error[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            // Verify new values present after update

            try
            {
                Assert.AreEqual("Webteam_edited", driver.FindElement(By.Id("ContentPlaceHolder1_AccountInfoScreen_tbxFirstName")).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Testing_edited", driver.FindElement(By.Id("ContentPlaceHolder1_AccountInfoScreen_tbxLastName")).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("selenium1_edit@srv0.com", driver.FindElement(By.Id("ContentPlaceHolder1_AccountInfoScreen_tbxEmail")).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("5126370002", driver.FindElement(By.Id("ContentPlaceHolder1_AccountInfoScreen_tbxPhone")).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            // Write back original values and save. btnUpdate

            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxFirstName')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxFirstName')]")).SendKeys("Webteam");
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxLastName')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxLastName')]")).SendKeys("Testing");
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxEmail')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxEmail')]")).SendKeys("selenium1@srv0.com");
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxPhone')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxPhone')]")).SendKeys("5126370000");
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxFax')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxFax')]")).SendKeys("5126370002");
            driver.FindElement(By.XPath("//input[contains(@id,'btnUpdate')]")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxFirstName')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            // Make sure original values are still present

            try
            {
                Assert.AreEqual("Webteam", driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxFirstName')]")).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Testing", driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxLastName')]")).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("selenium1@srv0.com", driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxEmail')]")).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("5126370000", driver.FindElement(By.XPath("//input[contains(@name,'AccountInfoScreen$tbxPhone')]")).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            
            driver.FindElement(By.XPath("//a[contains(text(),'Logout')]")).Click();  // Make like a tree
        }
        
        [Test]
        public void ActivateDeactivate()
        {
            driver.Navigate().GoToUrl(baseURL + "Login.aspx");
            driver.FindElement(By.XPath("//input[contains(@name,'UserName')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'UserName')]")).SendKeys(DealerPortal.Config.testUserName);
            driver.FindElement(By.XPath("//input[contains(@name,'Password')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'Password')]")).SendKeys(DealerPortal.Config.testPassWord);
            driver.FindElement(By.XPath("//input[contains(@id,'BtnSubmit')]")).Click();
            
            
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout waiting on accounts list to load");
                try
                {
                    if (IsElementPresent(By.XPath("//td[contains(text(),'ADMIN')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
                                                                        
            driver.FindElement(By.XPath("//td[contains(text(),'ADMIN')]")).Click();
                                                                        
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.Id("ContentPlaceHolder1_SearchCriteriaControl1_btnSearch"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//input[contains(@id,'tbxSearchTerm')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@id,'tbxSearchTerm')]")).SendKeys(DealerPortal.Config.auxUserName);
            driver.FindElement(By.XPath("//input[contains(@id,'btnSearch')]")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout waiting on accounts list to load");
                try
                {
                    if (IsElementPresent(By.XPath("//a[contains(@id,'lnkBtnDeactivate')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//a[contains(@id,'lnkBtnDeactivate')]")).Click();


            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout waiting on deactivate");
                try
                {
                    if (IsElementPresent(By.XPath("//img[contains(@id,'lockedout_0')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Activate[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.XPath("//a[contains(@id,'lnkBtnActivate')]")).Click();
            
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout waiting on activate");
                try
                {


                    if (IsElementPresent(By.XPath("//img[contains(@id,'active_0')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            try
            {
                Assert.IsTrue(driver.FindElement(By.Id("ContentPlaceHolder1_ucAccounts__gridView_active_0")).Displayed);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Deactivate[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.XPath("//a[contains(text(),'Logout')]")).Click();
        }

        [Test]
        public void CopyAccount()
        {   
            // Go to dealer portal and login as test user

            driver.Navigate().GoToUrl(baseURL + "Login.aspx");
            driver.FindElement(By.XPath("//input[contains(@name,'UserName')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'UserName')]")).SendKeys(DealerPortal.Config.testUserName);
            driver.FindElement(By.XPath("//input[contains(@name,'Password')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'Password')]")).SendKeys(DealerPortal.Config.testPassWord);
            driver.FindElement(By.XPath("//input[contains(@id,'BtnSubmit')]")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 30)
                {
                    testResult = "I";
                    testResultReason = "timed out waiting on login.";
                    Assert.Fail("timeout");    
                }
                try
                {
                    
                    if (IsElementPresent(By.XPath("//td[contains(text(),'ADMIN')]"))) break;
                }
                catch (Exception)
                { 
                
                }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//td[contains(text(),'ADMIN')]")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 30)
                {
                    testResult = "I";
                    testResultReason = "navigation timed out.";
                    Assert.Fail("timeout");
                }
                try
                {
                    
                    if (IsElementPresent(By.XPath("//a[contains(text(),'VIEW USERS')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//a[contains(text(),'VIEW USERS')]")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 30)
                {
                    testResult = "I";
                    testResultReason = "navigation timed out.";
                    Assert.Fail("timeout");
                }
                try
                {
                    
                    if (IsElementPresent(By.XPath("//input[contains(@id,'tbxSearchTerm')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//input[contains(@id,'tbxSearchTerm')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@id,'tbxSearchTerm')]")).SendKeys(DealerPortal.Config.auxUserName);
            driver.FindElement(By.XPath("//input[contains(@id,'btnSearch')]")).Click();

                    for (int second = 0; ; second++)
                    {
                        if (second >= 60) Assert.Fail("timeout");
                        try
                        {

                            if (IsElementPresent(By.XPath("//a[contains(@id,'lnkBtnEdit')]"))) break;
                        }
                        catch (Exception)
                        { }
                        Thread.Sleep(1000);
                    }
                    driver.FindElement(By.XPath("//a[contains(@id,'lnkBtnEdit')]")).Click();


                    for (int second = 0; ; second++)
                    {
                        if (second >= 60) Assert.Fail("timeout");
                        try
                        {

                            if (IsElementPresent(By.XPath("//div[contains(@id,'LinkCopyThisAccount')]"))) break;
                        }
                        catch (Exception)
                        { }
                        Thread.Sleep(1000);
                    }


                    driver.FindElement(By.XPath("//div[contains(@id,'LinkCopyThisAccount')]")).Click();

                    for (int second = 0; ; second++)
                    {
                        if (second >= 60) Assert.Fail("timeout");
                        try
                        {

                            if (IsElementPresent(By.XPath("//input[contains(@id,'AccountCopyControl_tbxUserName')]"))) break;
                        }
                        catch (Exception)
                        { }
                        Thread.Sleep(1000);
                    }

                    driver.FindElement(By.XPath("//input[contains(@id,'AccountCopyControl_tbxUserName')]")).Clear();
                    driver.FindElement(By.XPath("//input[contains(@id,'AccountCopyControl_tbxUserName')]")).SendKeys("selenium_copy");
                    driver.FindElement(By.XPath("//input[contains(@id,'AccountCopyControl_btnAddAccount')]")).Click();

                    Thread.Sleep(18000);

                    try  // Verify the message presented is not an error.
                    {
                        Assert.IsTrue(IsElementPresent(By.XPath("//span[@class = 'messageGood']")));
                    }
                    catch (AssertionException e)
                    {
                        verificationErrors.Append(e.Message);

                    }

                    try // Verify we got an 'account creation email notification' message
                    {
                        Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Account creation email notification was sent[\\s\\S]*$"));
                    }
                    catch (AssertionException e)
                    {
                        verificationErrors.Append(e.Message);
                        Console.WriteLine("TEST FAILED. Reason: " + e.Message);
                        Assert.Fail("Expected success message but got fail");     
                    }

                    // Close the copy account interface and logout.

                    driver.FindElement(By.CssSelector("div.closeBox")).Click();
                    
                    driver.Navigate().GoToUrl(baseURL + "WebModules/AdminModules/manageUsers.aspx");

                    for (int second = 0; ; second++)
                    {
                        if (second >= 60) Assert.Fail("timeout");
                        try
                        {

                            if (IsElementPresent(By.XPath("//input[contains(@id,'tbxSearchTerm')]"))) break;
                        }
                        catch (Exception)
                        { }
                        Thread.Sleep(1000);
                    }

                    driver.FindElement(By.XPath("//input[contains(@id,'tbxSearchTerm')]")).Clear();
                    driver.FindElement(By.XPath("//input[contains(@id,'tbxSearchTerm')]")).SendKeys("selenium_copy");
                    driver.FindElement(By.XPath("//input[contains(@id,'btnSearch')]")).Click();

                    for (int second = 0; ; second++)
                    {
                        if (second >= 60) Assert.Fail("timeout");
                        try
                        {

                            if (IsElementPresent(By.XPath("//a[contains(@id,'lnkBtnDelete')]"))) break;
                        }
                        catch (Exception)
                        { }
                        Thread.Sleep(1000);
                    }

                    driver.FindElement(By.XPath("//a[contains(@id,'lnkBtnDelete')]")).Click();

                    for (int second = 0; ; second++)
                    {
                        if (second >= 60) Assert.Fail("timeout");
                        try
                        {

                            if (IsElementPresent(By.XPath("//input[@id = \"ContentPlaceHolder1_ucAccounts__gridView_ButtonOk_0\"]"))) break;
                        }
                        catch (Exception)
                        { }
                        Thread.Sleep(1000);
                    }
                    
                driver.FindElement(By.XPath("//input[@id = \"ContentPlaceHolder1_ucAccounts__gridView_ButtonOk_0\"]")).Click();
        }

        [Test]
        public void DeleteAccount()
        {
            // Go to dealer portal and login as test user

            driver.Navigate().GoToUrl(baseURL + "Login.aspx");
            driver.FindElement(By.XPath("//input[contains(@name,'UserName')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'UserName')]")).SendKeys(DealerPortal.Config.testUserName);
            driver.FindElement(By.XPath("//input[contains(@name,'Password')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'Password')]")).SendKeys(DealerPortal.Config.testPassWord);
            driver.FindElement(By.XPath("//input[contains(@id,'BtnSubmit')]")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {

                    if (IsElementPresent(By.XPath("//td[contains(text(),'ADMIN')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//td[contains(text(),'ADMIN')]")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {

                    if (IsElementPresent(By.XPath("//a[contains(text(),'VIEW USERS')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//a[contains(text(),'VIEW USERS')]")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {

                    if (IsElementPresent(By.XPath("//input[contains(@id,'tbxSearchTerm')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//input[contains(@id,'tbxSearchTerm')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@id,'tbxSearchTerm')]")).SendKeys(DealerPortal.Config.auxUserName);
            driver.FindElement(By.XPath("//input[contains(@id,'btnSearch')]")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {

                    if (IsElementPresent(By.XPath("//a[contains(@id,'lnkBtnEdit')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            driver.FindElement(By.XPath("//a[contains(@id,'lnkBtnEdit')]")).Click();


            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {

                    if (IsElementPresent(By.XPath("//div[contains(@id,'LinkCopyThisAccount')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }


            driver.FindElement(By.XPath("//div[contains(@id,'LinkCopyThisAccount')]")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {

                    if (IsElementPresent(By.XPath("//input[contains(@id,'AccountCopyControl_tbxUserName')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//input[contains(@id,'AccountCopyControl_tbxUserName')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@id,'AccountCopyControl_tbxUserName')]")).SendKeys("selenium_copy");
            driver.FindElement(By.XPath("//input[contains(@id,'AccountCopyControl_btnAddAccount')]")).Click();

            Thread.Sleep(8000);

            try  // Verify the message presented is not an error.
            {
                Assert.IsTrue(IsElementPresent(By.XPath("//span[@class = 'messageGood']")));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            try // Verify we got an 'account creation email notification' message
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Account creation email notification was sent[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

            // Close the copy account interface and logout.

            driver.FindElement(By.CssSelector("div.closeBox")).Click();

            driver.Navigate().GoToUrl(baseURL + "WebModules/AdminModules/manageUsers.aspx");

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {

                    if (IsElementPresent(By.XPath("//input[contains(@id,'tbxSearchTerm')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//input[contains(@id,'tbxSearchTerm')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@id,'tbxSearchTerm')]")).SendKeys("selenium_copy");
            driver.FindElement(By.XPath("//input[contains(@id,'btnSearch')]")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {

                    if (IsElementPresent(By.XPath("//a[contains(@id,'lnkBtnDelete')]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//a[contains(@id,'lnkBtnDelete')]")).Click();

            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {

                    if (IsElementPresent(By.XPath("//input[@id = \"ContentPlaceHolder1_ucAccounts__gridView_ButtonOk_0\"]"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }

            driver.FindElement(By.XPath("//input[@id = \"ContentPlaceHolder1_ucAccounts__gridView_ButtonOk_0\"]")).Click();
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
