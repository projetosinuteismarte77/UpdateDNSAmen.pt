﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
namespace UpdateAmenDNSSelenium
{
	public class SeleniumCode
	{
		public SeleniumCode()
		{
		}

		static public void UpdateDNS(bool headless)
		{
            var options = new ChromeOptions();
            if(headless)
                options.AddArgument("--headless=new");
            var driver = new ChromeDriver(options);
            Console.WriteLine(driver.Manage().Window.Size.ToString());
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            var initialURL = "https://controlpanel.amen.pt/welcome.html";
            driver.Navigate().GoToUrl(initialURL);
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".iubenda-cs-reject-btn.iubenda-cs-btn-primary")));
            driver.FindElement(By.CssSelector(".iubenda-cs-reject-btn.iubenda-cs-btn-primary")).Click();
            Console.WriteLine("Fazer Login");
            int count = 0;
            while (driver.Url == initialURL)
            {
                Console.WriteLine("Vou tentar count " + (count++));
                var divInputs = driver.FindElements(By.ClassName("standard-login-module"));
                if (divInputs.Count == 0)
                    continue;
                var inputs = divInputs.First().FindElements(By.TagName("input"));
                if (headless) Thread.Sleep(1000);
                inputs[0].Clear(); inputs[0].SendKeys("ma");
                if (headless) Thread.Sleep(300);
                inputs[0].SendKeys("rto");
                inputs[1].Clear(); inputs[1].SendKeys("");
                if (headless) Thread.Sleep(800);
                driver.FindElements(By.TagName("button")).Where(elem => elem.GetAttribute("type") == "submit").First().Click();
                Thread.Sleep(3000);
            }
            Console.WriteLine("Login Efetuado");
            driver.FindElements(By.TagName("a")).Where(elem => elem.GetAttribute("href") != null && elem.GetAttribute("href") == "https://controlpanel.amen.pt/firstLevel/view.html?domain=martinho.pt").First().Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("webapp_domain")));
            if (headless) Thread.Sleep(300);
            driver.FindElement(By.Id("webapp_domain")).FindElement(By.TagName("a")).Click();
            Console.WriteLine("Selecionei o meu dominio");
            Thread.Sleep(6000);
            driver.FindElements(By.TagName("a")).Where(elem => elem.GetAttribute("href") != null && elem.GetAttribute("href") == "https://controlpanel.amen.pt/domains/dns.html").First().Click();
            Thread.Sleep(5000);
            driver.FindElements(By.TagName("a")).Where(elem => elem.GetAttribute("href") != null && elem.GetAttribute("href") == "https://controlpanel.amen.pt/domains/dnsAdvanced.html").First().Click();
            wait.Until(ExpectedConditions.AlertIsPresent());
            var alert = driver.SwitchTo().Alert();
            alert.Accept();
            Console.WriteLine("Estou na pagina do dns");
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector(".import.btn.btn-sm.btn-line-primary")).Click();
            Console.WriteLine("Vou fazer input");
            var fileInput = driver.FindElement(By.Id("dnsUploadedFile"));
            fileInput.SendKeys(CreateOrUpdateCSV.dnsFile);
            driver.FindElement(By.CssSelector(".btn.btn-sm.btn-primary.apply")).Click();
            Console.WriteLine("Submeti input");
            Thread.Sleep(6000);
            driver.FindElement(By.CssSelector(".submit.btn.btn-md.btn-primary")).Click();
            Console.WriteLine("Apliquei alterações");
            driver.Quit();
        }
	}
}

