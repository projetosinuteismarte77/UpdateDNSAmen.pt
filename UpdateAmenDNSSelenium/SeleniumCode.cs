using System;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace UpdateAmenDNSSelenium
{
	public class SeleniumCode
	{
		public SeleniumCode()
		{
		}

        public static string findCommand(string whatToFind)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo() { FileName = "find", Arguments = "/snap/firefox -name "+whatToFind };
            Process proc = new Process() { StartInfo = startInfo, };
            proc.Start();
            proc.WaitForExit();
            var output = proc.StandardOutput.ReadToEnd();
            return output.Split("\n").Last();
        }

        public static void Log(string log)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[MARTINHO] " + log);
            Console.ResetColor();
        }

		static public void UpdateDNS(bool headless, string user, string password)
		{
            
            var options = new FirefoxOptions();
            if(headless)
                options.AddArgument("-headless");
            options.AddArgument("-log-level=fatal");
            var geckodriverPath = OperatingSystem.IsMacOS() ? "/Users/marte/Documents/RandomProjeots/UpdateAmenDNSSelenium/UpdateAmenDNSSelenium/geckodriver" : "/snap/firefox/4038/usr/lib/firefox/geckodriver";//findCommand("geckodriver");//"/home/pi/noderedstuff/update_godaddy/UpdateDNSAmen.pt/geckodriver";
            if (OperatingSystem.IsLinux())
                options.BinaryLocation = "/snap/firefox/4038/usr/lib/firefox/firefox";//findCommand("firefox");
            options.LogLevel = FirefoxDriverLogLevel.Fatal;
            options.SetLoggingPreference(LogType.Driver, LogLevel.Off);
            var driver = new FirefoxDriver(geckodriverPath, options);//new ChromeDriver(options);
            Log(driver.Manage().Window.Size.ToString());
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            var initialURL = "https://controlpanel.amen.pt/welcome.html";
            driver.Navigate().GoToUrl(initialURL);
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".iubenda-cs-reject-btn.iubenda-cs-btn-primary")));
            driver.FindElement(By.CssSelector(".iubenda-cs-reject-btn.iubenda-cs-btn-primary")).Click();
            Log("Fazer Login");
            int count = 0;
            Thread.Sleep(15000);
            while (driver.Url == initialURL)
            {
                driver.Navigate().Refresh();
                Log("Vou tentar count " + (count++) + " " + driver.Url);
                var divInputs = driver.FindElements(By.ClassName("standard-login-module"));
                if (divInputs.Count == 0)
                    continue;
                else if (count > 10) throw new Exception("Demasiadas tentativas nas credenciais!");
                var inputs = divInputs.First().FindElements(By.TagName("input"));
                if (headless) Thread.Sleep(1000);
                inputs[0].Clear(); inputs[0].SendKeys(user);
                if (headless) Thread.Sleep(300);
                inputs[1].Clear(); inputs[1].SendKeys(password);
                if (headless) Thread.Sleep(2000);
                driver.FindElements(By.TagName("button")).Where(elem => elem.GetAttribute("type") == "submit").First().Click();
                Thread.Sleep(3000);
            }
            Log("Login Efetuado");
            driver.FindElements(By.TagName("a")).Where(elem => elem.GetAttribute("href") != null && elem.GetAttribute("href") == "https://controlpanel.amen.pt/firstLevel/view.html?domain=martinho.pt").First().Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("webapp_domain")));
            if (headless) Thread.Sleep(300);
            driver.FindElement(By.Id("webapp_domain")).FindElement(By.TagName("a")).Click();
            Log("Selecionei o meu dominio");
            Thread.Sleep(6000);
            driver.FindElements(By.TagName("a")).Where(elem => elem.GetAttribute("href") != null && elem.GetAttribute("href") == "https://controlpanel.amen.pt/domains/dns.html").First().Click();
            Thread.Sleep(5000);
            driver.FindElements(By.TagName("a")).Where(elem => elem.GetAttribute("href") != null && elem.GetAttribute("href") == "https://controlpanel.amen.pt/domains/dnsAdvanced.html").First().Click();
            wait.Until(ExpectedConditions.AlertIsPresent());
            var alert = driver.SwitchTo().Alert();
            alert.Accept();
            Log("Estou na pagina do dns");
            Thread.Sleep(5000);
            driver.FindElement(By.CssSelector(".import.btn.btn-sm.btn-line-primary")).Click();
            Log("Vou fazer input");
            var fileInput = driver.FindElement(By.Id("dnsUploadedFile"));
            fileInput.SendKeys(CreateOrUpdateCSV.dnsFile);
            driver.FindElement(By.CssSelector(".btn.btn-sm.btn-primary.apply")).Click();
            Log("Submeti input");
            Thread.Sleep(6000);
            driver.FindElement(By.CssSelector(".submit.btn.btn-md.btn-primary")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.Id("modalDNS")).FindElement(By.CssSelector(".pribttn.nm.apply")).Click();
            Thread.Sleep(6000);
            Log("Apliquei alterações");
            Thread.Sleep(1000);
            driver.Quit();
        }

    }
}

