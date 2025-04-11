using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using FluentAssertions;

namespace firstSeleniumTest;

[Parallelizable(ParallelScope.All)]
public class Tests
{
    private readonly ChromeDriver driver;
    private readonly WebDriverWait wait;

    public Tests()
    {
        driver = new();
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
    }

    [SetUp]
    public void SetUp()
    {
        Authorize("dimababintcev@gmail.com", "65spWCM73gfr.");
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }

    [Test]
    public void CheckAuthorizationTest()
    {
        driver.Title.Should().Contain("Новости");
    }

    [Test]
    public void CheckNavigationToCommunitiesTest()
    {
        var enterCommunities = driver.FindElement(By.CssSelector("[data-tid='Community']"));
        enterCommunities.Click();

        DoExplicitWait();

        Assert.That(driver.Title, Does.Contain("Сообщества"));
    }

    [Test]
    public void AddCommunityTest()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities");
        DoExplicitWait();
        var addCommunity = driver.FindElement(By.CssSelector("[class='sc-juXuNZ sc-ecQkzk WTxfS vPeNx']"));
        addCommunity.Click();
        DoExplicitWait();
        var communityName = driver.FindElement(By.CssSelector("[placeholder='Название сообщества']"));
        communityName.Click();
        communityName.SendKeys("TestCommunity");
        var create = driver.FindElement(By.CssSelector("[data-tid='CreateButton']"));
        create.Click();
        DoExplicitWait();
        var titleElement = driver.FindElement(By.CssSelector("[data-tid='DeleteButton']"));
        Assert.That(titleElement.Text, Does.Contain("Удалить сообщество"));
    }

    //[Test]
    //public void LeaveCommunityTest()
    //{
    //    driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities/4eb36d5c-faeb-460e-b055-9f7e09f9d9c5");
    //    DoExplicitWait();
    //    var join = driver.FindElement(By.CssSelector("[data-tid=\"Join\"]"));
    //    join.Click();
    //    DoExplicitWait("[data-tid=\"PopupMenu__caption\"]");
    //    var dropDown = driver.FindElement(By.ClassName("sc-kLojOw"));
    //    dropDown.Click();
    //    var leave = driver.FindElement(By.CssSelector("[data-tid=\"Quit\"]"));
    //    leave.Click();
    //    DoExplicitWait();
    //    var joinAgain = driver.FindElement(By.CssSelector("[class=\"sc-eWnToP fEDxD\"]"));
    //    Assert.That(joinAgain, Does.Contain("Вступить"));
    //}

    private void DoExplicitWait(string locator = "[data-tid='Title']")
    {
        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(locator)));
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

        DoExplicitWait();
    }
}
