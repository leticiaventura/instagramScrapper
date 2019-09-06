using App.Domain.Features.Places;
using App.Domain.Features.Users;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Infrastructure.Scraping
{
    public class InstagramScraper
    {
        private readonly string _url = ConfigurationManager.AppSettings.Get("InstagramURL");
        private readonly string _user = ConfigurationManager.AppSettings.Get("InstagramLogin");
        private readonly string _password = ConfigurationManager.AppSettings.Get("InstagramPass");
        private ChromeDriver _browser;
        private IJavaScriptExecutor _console;

        private readonly int _mainTab = 0;
        private readonly int _userTab = 1;

        public void Initialize()
        {
            _browser = new ChromeDriver(ConfigurationManager.AppSettings.Get("ChromeDriver"));
            _console = (IJavaScriptExecutor)_browser;
        }

        public void FindVisitorByVenue(Venue venue)
        {
            SearchPlace(venue.Name);

            Refresh();

            var article = Wait().Until(ExpectedConditions.ElementExists(By.TagName("article")));            

            var pageLocation = GetLocation();
            var distance = venue.Location.CalculateDistance(pageLocation);
            if (distance > 150) throw new Exception("Distância maior que 150 metros");

            var allPosts = GetAllPostsUrl();

            GetVisitors(venue, allPosts);
        }

        public void Close()
        {
            _browser.Close();
        }

        private void Refresh()
        {
            _browser.Navigate().GoToUrl(_browser.Url);
        }

        private List<string> GetAllPostsUrl()
        {
            List<string> allPosts = GetDisplayedPostsUrl();
            long scrollPosition = 0;
            long scrollPositionBefore = -1;

            while (scrollPositionBefore != scrollPosition)
            {
                _console.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                Thread.Sleep(2000);
                allPosts.AddRange(GetDisplayedPostsUrl());

                scrollPositionBefore = scrollPosition;
                scrollPosition = (long)_console.ExecuteScript("return window.scrollY");
            }

            return allPosts.Distinct().ToList();
        }

        private void SearchPlace(string name)
        {
            Wait().Until(ExpectedConditions.ElementToBeClickable(
                                    _browser.FindElement(By.CssSelector("input[placeholder='Busca']"))))
                                    .SendKeys(name);
            Wait().Until(ExpectedConditions.ElementExists(By.ClassName("coreSpriteLocation"))).Click();
        }

        public void Login()
        {
            //Vai até a pagina inicial e faz o login
            _browser.Navigate().GoToUrl(new Uri(_url + "accounts/login/"));
            Wait().Until(ExpectedConditions.ElementExists(By.Name("username"))).SendKeys(_user);
            new Actions(_browser)
                .MoveToElement(_browser.FindElement(By.Name("password")))
                .Click()
                .SendKeys(_password)
                .SendKeys(Keys.Enter)
                .Perform();

            //espera a abrir um dialog e clica no botão para fechar
            Wait().Until(ExpectedConditions.ElementToBeClickable(By.ClassName("HoLwm"))).Click();
        }

        private Location GetLocation()
        {
            var lat = (string)_console.ExecuteScript("return document.querySelector('head > meta:nth-child(39)').content");
            var lng = (string)_console.ExecuteScript("return document.querySelector('head > meta:nth-child(40)').content");
            return new Location { Lat = lat, Lng = lng };
        }

        private WebDriverWait Wait()
        {
            return new WebDriverWait(_browser, new TimeSpan(0, 0, 10));
        }

        private long GetInstagramIdFromProfilePage()
        {
            return (long)_console.ExecuteScript("return window._sharedData.entry_data.ProfilePage[0].graphql.user.id");
        }

        private InstagramUser GetInstagramUserFromPost()
        {
            var instagramId = Convert.ToInt64(_console.ExecuteScript("return window._sharedData.entry_data.PostPage[0].graphql.shortcode_media.owner.id"));
            var username = (string)_console.ExecuteScript("return window._sharedData.entry_data.PostPage[0].graphql.shortcode_media.owner.username");
            return new InstagramUser { InstagramId = instagramId, Username = username};
        }

        private List<string> GetDisplayedPostsUrl()
        {
            string script = "return Array.from(document.getElementsByTagName('a')).map(x => x.href).filter(x => x.match(new RegExp(/(?:\\/p\\/)/)))";
            var posts = (IList<object>)_console.ExecuteScript(script);

            return posts.Cast<string>().ToList();
        }

        private void GetVisitors(Venue venue, List<string> posts)
        {
            foreach (var post in posts)
            {
                SwitchTabs(_userTab, post);
                venue.Visitors.Add(GetInstagramUserFromPost());
                CloseTab(_userTab);
                SwitchTabs(_mainTab);
            }
            venue.Visitors.Distinct();
        }

        private void SwitchTabs(int tabIndex, string url = null)
        {
            if (!string.IsNullOrEmpty(url))
            {
                _browser.SwitchTo().Window(_browser.WindowHandles.First());
                _console.ExecuteScript(string.Format("window.open('{0}')", url));
            }
            _browser.SwitchTo().Window(_browser.WindowHandles[tabIndex]);
        }

        private void CloseTab(int tabIndex)
        {
            _console.ExecuteScript("window.close()");
        }
    }
}
