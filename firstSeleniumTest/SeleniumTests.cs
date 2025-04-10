using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace firstSeleniumTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CheckAuthorizationTest()
    {
        var driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/");

        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("");

        var password = driver.FindElement(By.Id("Password"));
        password.SendKeys("");

        var enter = driver.FindElement(By.Name("button"));
        enter.Click();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[data-tid='Title']")));

        Assert.That(driver.Title, Does.Contain("Новости"));

        driver.Quit();
    }

    [Test]
    public void CheckNavigationTest()
    {
        var driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/");

        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("");

        var password = driver.FindElement(By.Id("Password"));
        password.SendKeys("");

        var enter = driver.FindElement(By.Name("button"));
        enter.Click();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[data-tid='Title']")));

        var enterCommunities = driver.FindElement(By.CssSelector("[data-tid='Community']"));
        enterCommunities.Click();

        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[data-tid='Title']")));

        Assert.That(driver.Title, Does.Contain("Сообщества"));
        driver.Quit();
    }
}
