using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebDriver
{
    [TestFixture]
    public class Class1
    {
        IWebDriver webDriver = new ChromeDriver();

        [TestCase]
        public void mainTitle()
        {
            webDriver.Url = "https://translate.google.com/?hl=ru";
            Assert.AreEqual("Google Переводчик", webDriver.Title);
            webDriver.Close();
        }
        [TestCase]
        public void Visibility()
        {
            webDriver.Url = "https://translate.google.com/?hl=ru";
            var wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 30));
            var element = wait.Until(condition =>
            {
                try
                {
                    var elementToBeDisplayed =
                    webDriver.FindElement(By.XPath("/html/body/div[2]/header/div[2]/div[1]/div[4]/div/a/span[1]"));
                    return elementToBeDisplayed.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
            IWebElement elem = webDriver.FindElement(By.XPath("/html/body/div[2]/header/div[2]/div[1]/div[4]/div/a/span[1]")); // надпись google в заголовке
            bool status = elem.Displayed;
            webDriver.Close();
        }
        [TestCase]
        public void Text()
        {
            webDriver.Url = "https://translate.google.com/?hl=ru";
            IWebElement text = webDriver.FindElement(By.XPath("/html/body/c-wiz/div/div[2]/c-wiz/div[2]/c-wiz/div[1]/div[2]/div[3]/c-wiz[1]/span/span/div/textarea"));
            text.SendKeys("переведи этот текст");
            webDriver.Close();
        }
        [TestCase]
        public void Button()
        {
            webDriver.Url = "https://translate.google.com/?hl=ru";
            IWebElement button = webDriver.FindElement(By.XPath("/html/body/c-wiz/div/div[2]/c-wiz/div[1]/nav/div[2]/button/span")); //кнопка документы
            button.Click();
            webDriver.Close();
        }
        [TestCase]
        public void ClearText()
        {
            webDriver.Url = "https://translate.google.com/?hl=ru";
            IWebElement text = webDriver.FindElement(By.XPath("/html/body/c-wiz/div/div[2]/c-wiz/div[2]/c-wiz/div[1]/div[2]/div[3]/c-wiz[1]/span/span/div/textarea"));
            text.SendKeys("переведи этот текст");
            IWebElement clear = webDriver.FindElement(By.XPath("/html/body/c-wiz/div/div[2]/c-wiz/div[2]/c-wiz/div[1]/div[2]/div[3]/c-wiz[1]/span/span/div/textarea"));
            clear.Clear();
            webDriver.Close();
        }
        [TestCase]
        public void Link()
        {
            webDriver.Url = "https://translate.google.com/?hl=ru";
            webDriver.FindElement(By.XPath("//*[@id=\"gb\"]/div[2]/div[1]/div[1]")).Click(); //открытие меню
            IWebElement link = webDriver.FindElement(By.XPath("/html/body/div[2]/header/div[1]/div/div[2]/div/c-wiz/div/div[1]/a")); //переход по ссылке о переводчике google
            link.Click();
            webDriver.Close();
        }
        [TestCase]
        public void GoBack()
        {
            webDriver.Url = "https://translate.google.com/?hl=ru";
            webDriver.FindElement(By.XPath("//*[@id=\"gb\"]/div[2]/div[1]/div[1]")).Click();
            IWebElement link = webDriver.FindElement(By.XPath("/html/body/div[2]/header/div[1]/div/div[2]/div/c-wiz/div/div[1]/a"));
            link.Click();
            webDriver.Navigate().Back();
            webDriver.Close();
        }
        [TestCase]
        public void Translate()
        {
            webDriver.Url = "https://translate.google.com/?hl=ru&sl=ru&tl=en&op=translate";
            IWebElement text = webDriver.FindElement(By.XPath("/html/body/c-wiz/div/div[2]/c-wiz/div[2]/c-wiz/div[1]/div[2]/div[3]/c-wiz[1]/span/span/div/textarea"));
            text.SendKeys("переведи этот текст");
            /*IWebElement round = webDriver.FindElement(By.XPath("/html/body/c-wiz/div/div[2]/c-wiz/div[2]/c-wiz/div[1]/div[1]/c-wiz/div[1]/c-wiz/div[3]/div/span/button"));
            round.Click();
            Actions action = new Actions(webDriver);
            action.SendKeys(OpenQA.Selenium.Keys.Control + OpenQA.Selenium.Keys.LeftShift + "s").Build().Perform();*/
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.XPath("/html/body/c-wiz/div/div[2]/c-wiz/div[2]/c-wiz/div[1]/div[2]/div[3]/c-wiz[2]/div[6]")).Text != null);
            wait.Until(d => d.FindElement(By.XPath("/html/body/c-wiz/div/div[2]/c-wiz/div[2]/c-wiz/div[1]/div[2]/div[3]/c-wiz[2]/div[6]")).Text != "");
            IWebElement returnText = webDriver.FindElement(By.XPath("/html/body/c-wiz/div/div[2]/c-wiz/div[2]/c-wiz/div[1]/div[2]/div[3]/c-wiz[2]/div[6]"));
            string text2 = returnText.Text;
            string word = text2.Substring(0, text2.IndexOf('\r'));
            Assert.AreEqual("Transfer this text", word);
            webDriver.Close();
        }
        [TearDown]
        public void testEnd()
        {
            webDriver.Quit();
        }
    }
}
