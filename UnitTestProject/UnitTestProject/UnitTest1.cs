using System;
using System.ComponentModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace UnitTestProject
{
    public class UnitTest1
    {
        IWebDriver driver;
        private const string DriverDirectory = "C:\\Users\\Kamil\\source\\repos\\UnitTestProject\\UnitTestProject";

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--start-fullscreen");
            driver = new ChromeDriver(DriverDirectory, options);
            //driver = new FirefoxDriver(DriverDirectory);
        }

        [Test]
        public void Test01_TitleTest()
        {
            driver.Url = "http://192.168.100.10/";
            String title = driver.Title;

            Assert.True(title == "Księgarnia internetowa");
            driver.Quit();
        }

        [Test]
        public void Test02_GoToLoginPageTest()
        {
            driver.Url = "http://192.168.100.10/";

            driver.FindElement(By.XPath("/html/body/main/header/section/div/div/div[1]/a")).Click();

            Assert.True(driver.FindElement(By.XPath("//*[@id=\"email\"]")).GetAttribute("placeholder") == "Adres e-mail");
            Assert.True(driver.FindElement(By.XPath("//*[@id=\"password\"]")).GetAttribute("placeholder") == "Hasło");
            driver.Quit();
        }

        [Test]
        public void Test03_LoginFailTest()
        {
            String email = "//*[@id=\"email\"]";
            String password = "//*[@id=\"password\"]";
            String button = "//*[@class=\"buttons\"]/button";

            driver.Url = "http://192.168.100.10/login";
            driver.FindElement(By.XPath(email)).SendKeys("login_test@test.test");
            driver.FindElement(By.XPath(password)).SendKeys("12345");
            driver.FindElement(By.XPath(button)).Submit();

            Assert.True(driver.FindElement(By.XPath("//*[@id=\"password\"]/following-sibling::span")).Text == "Niepoprawny login lub hasło");
            driver.Quit();
        }

        [Test]
        public void Test04_LoginSuccesTest()
        {
            String email = "//*[@id=\"email\"]";
            String password = "//*[@id=\"password\"]";
            String button = "//*[@class=\"buttons\"]/button";

            driver.Url = "http://192.168.100.10/login";
            driver.FindElement(By.XPath(email)).SendKeys("user1@ksiegarnia-internetowa.licencjat");
            driver.FindElement(By.XPath(password)).SendKeys("zaq1@WSX");
            driver.FindElement(By.XPath(button)).Submit();

            Assert.True(driver.FindElement(By.XPath("//*[@class=\"userName\"]")).Text == "KORNELIA");
            driver.Quit();
        }

        [Test]
        public void Test05_LogoutSuccess()
        {
            String email = "//*[@id=\"email\"]";
            String password = "//*[@id=\"password\"]";
            String button = "//*[@class=\"buttons\"]/button";

            driver.Url = "http://192.168.100.10/login";
            driver.FindElement(By.XPath(email)).SendKeys("user1@ksiegarnia-internetowa.licencjat");
            driver.FindElement(By.XPath(password)).SendKeys("zaq1@WSX");
            driver.FindElement(By.XPath(button)).Submit();

            driver.FindElement(By.XPath("/html/body/main/header/section/div/div/div[1]/span[2]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            driver.FindElement(By.XPath("/html/body/main/header/section/div/div/div[1]/span[2]/span[2]/ul/li[4]/a")).Click();

            Assert.True(driver.FindElement(By.XPath("//*[@class=\"user-name\"]")).Text == "LOGOWANIE / REJESTRACJA");
            driver.Quit();
        }

        [Test]
        public void Test06_FindProduct()
        {
            driver.Url = "http://192.168.100.10/";

            driver.FindElement(By.XPath("//*[@class=\"search-trigger\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"phrase\"]")).SendKeys("zaginiona");
            driver.FindElement(By.XPath("//*[@id=\"phrase\"]/following-sibling::button")).Click();

            Assert.True(driver.FindElement(By.XPath("//*[@class=\"title\"]/a")).Text == "Zaginiona");
            driver.Quit();
        }

        [Test]
        public void Test07_FindProductByFiltr()
        {
            driver.Url = "http://192.168.100.10/";

            driver.FindElement(By.XPath("/html/body/div[1]/div/a")).Click();       
            driver.FindElement(By.XPath("/html/body/main/header/div/div/nav/ul/li[1]/a")).Click();
            driver.FindElement(By.XPath("/html/body/main/div[2]/section/div/div/div/aside/div/nav/ul/li/ul/li[1]/ul/li[2]/a")).Click();
            driver.FindElement(By.XPath("/html/body/main/div[2]/section/div/div/div/aside/div/form/div[2]/div[2]/ul/li[3]/div/label/span[2]/span")).Click();
            driver.FindElement(By.XPath("/html/body/main/div[2]/section/div/div/div/aside/div/form/div[3]/div[2]/ul/li[1]/div/label/span[2]/span")).Click();
            driver.FindElement(By.XPath("//*[@id=\"filtr\"]")).Click();        

            bool staleElement = true;
            while (staleElement)
            {           
                try
                {
                    Assert.True(driver.FindElement(By.XPath("//*[@class=\"title\"]/a")).Text == "Zwiadowcy. Oblężenie Macindaw");
                    staleElement = false;
                }
                catch (StaleElementReferenceException e)
                {
                    staleElement = true;
                }        
            }
 
            driver.Quit();
        }

        [Test]
        public void Test08_SortProduct()
        {
            driver.Url = "http://192.168.100.10/product/list";

            driver.FindElement(By.XPath("//*[@id=\"sort\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"sort\"]/option")).Click();

            bool staleElement = true;
            while (staleElement)
            {
                try
                {
                    Assert.True(driver.FindElement(By.XPath("//*[@class=\"title\"]/a")).Text == "Aleja starych topoli");
                    staleElement = false;
                }
                catch (StaleElementReferenceException e)
                {
                    staleElement = true;
                }
            }

            driver.Quit();
        }

        [Test]
        public void Test09_ProductByPageAndChangePage()
        {
            driver.Url = "http://192.168.100.10/product/list";

            driver.FindElement(By.XPath("/html/body/div[1]/div/a")).Click();
            driver.FindElement(By.XPath("//*[@id=\"limit\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"limit\"]/option")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            bool staleElement = true;
            while (staleElement)
            {
                try
                {
                    driver.FindElement(By.XPath("//*[@class=\"pagination-next\"]/a")).Click();
                    staleElement = false;
                }
                catch (StaleElementReferenceException e)
                {
                    staleElement = true;
                }
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            Assert.True(driver.FindElement(By.XPath("//*[@class=\"title\"]/a")).Text == "Szanse");
            driver.Quit();
        }

        [Test]
        public void Test10_Buy()
        {
            String email = "//*[@id=\"email\"]";
            String password = "//*[@id=\"password\"]";
            String button = "//*[@class=\"buttons\"]/button";

            driver.Url = "http://192.168.100.10/login";
            driver.FindElement(By.XPath("/html/body/div[1]/div/a")).Click();
            driver.FindElement(By.XPath(email)).SendKeys("user1@ksiegarnia-internetowa.licencjat");
            driver.FindElement(By.XPath(password)).SendKeys("zaq1@WSX");
            driver.FindElement(By.XPath(button)).Submit();

            driver.FindElement(By.XPath("/html/body/main/header/div/div/nav/ul/li[1]/a")).Click();
            driver.FindElement(By.XPath("/html/body/main/div[2]/section/div/div/div/main/ul/li[1]/div")).Click();
            driver.FindElement(By.XPath("//*[@id=\"date_start\"]")).SendKeys("09-03-2020" + Keys.Enter + Keys.Enter);
            driver.FindElement(By.XPath("//*[@id=\"date_end\"]")).SendKeys("09-03-2020");
            driver.FindElement(By.XPath("//*[@id=\"product-add-to-cart\"]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            driver.FindElement(By.XPath("//*[@class=\"productSummary__item-buttons\"]/a[2]")).Click();
            driver.FindElement(By.XPath("/html/body/main/div[2]/section/div/div/div/div/form/div[2]/div/div[2]/div[2]/div/div[2]/input")).Submit();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.FindElement(By.XPath("//*[@class=\"button overLoad\"]")).Click();

            Assert.True(driver.FindElement(By.XPath("//*[@class=\"cart-info\"]/p[1]")).Text == "Dziękujemy za dokonanie zamówienia!");
            driver.Quit();
        }

        [Test]
        public void Test11_CheckBuy()
        {
            String email = "//*[@id=\"email\"]";
            String password = "//*[@id=\"password\"]";
            String button = "//*[@class=\"buttons\"]/button";

            driver.Url = "http://192.168.100.10/login";
            driver.FindElement(By.XPath("/html/body/div[1]/div/a")).Click();
            driver.FindElement(By.XPath(email)).SendKeys("user1@ksiegarnia-internetowa.licencjat");
            driver.FindElement(By.XPath(password)).SendKeys("zaq1@WSX");
            driver.FindElement(By.XPath(button)).Submit();

            driver.FindElement(By.XPath("/html/body/main/header/section/div/div/div[1]/span[2]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            driver.FindElement(By.XPath("/html/body/main/header/section/div/div/div[1]/span[2]/span[2]/ul/li[1]/a")).Click();
            driver.FindElement(By.XPath("/html/body/main/div[2]/section/div/div/main/div/table/tbody/tr[last()-1]")).Click();

            Assert.True(driver.FindElement(By.XPath("/html/body/main/div[2]/section/div/div/main/div/table/tbody/tr[last()]/td/div/div/table/tbody/tr[2]/td[2]")).Text == "09-03-2020 - 09-03-2020");
            driver.Quit();
        }
    }
}