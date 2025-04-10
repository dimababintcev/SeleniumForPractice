using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace firstSeleniumTest;

public class Tests
{
    private ChromeDriver driver;
    [SetUp]
    public void SetUp()
    {
        driver = new();
        Authorize("", "");
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }

    [Test]
    public void CheckAuthorizationTest()
    {
        var wait = DoExplicitWait();

        Assert.That(driver.Title, Does.Contain("Новости"));
    }

    private WebDriverWait DoExplicitWait()
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[data-tid='Title']")));
        return wait;
    }

    [Test]
    public void CheckNavigationTest()
    {
        var wait = DoExplicitWait();

        var enterCommunities = driver.FindElement(By.CssSelector("[data-tid='Community']"));
        enterCommunities.Click();

        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[data-tid='Title']")));

        Assert.That(driver.Title, Does.Contain("Сообщества"));
    }

    private void Authorize(string userLogin, string userPassword)
    {
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/");

        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys(userLogin);

        var password = driver.FindElement(By.Id("Password"));
        password.SendKeys(userPassword);

        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
    }
}
