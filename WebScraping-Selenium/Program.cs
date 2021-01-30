using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace WebScraping_Selenium
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ChromeDriver driver = new ChromeDriver();
          
            driver.Url = "https://travesiacostatorremolinos.com";
            var window = driver.Manage().Window;

            Thread.Sleep(3000);
            window.Maximize();
            Thread.Sleep(1000);
            //CLASIFICACIONES
             OpenQA.Selenium.IWebElement hipervinculo = driver.FindElementByXPath("//*[@id='menu-item-1924']");

            Actions action = new Actions(driver);

             action.MoveToElement(hipervinculo);
  
            action.Perform();
            Thread.Sleep(1000);
            //PARTICIPANTES
            hipervinculo = driver.FindElementByXPath("//*[@id='menu-item-1919']");
            action.MoveToElement(hipervinculo);
            action.Perform();
            Thread.Sleep(1000);
            //2019
            hipervinculo = driver.FindElementByXPath("//*[@id='menu-item-2172']/a");
            action.MoveToElement(hipervinculo).Click();
            action.Perform();

           //RECORREMOS LA TABLA
          var tabla=  driver.FindElementByXPath("/html/body/div[1]/div[2]/div/div/div/section/div/div/div/div/div/div/div/div/table");
          
            IList<IWebElement> tableRow = tabla.FindElements(By.TagName("tr"));
            IList<IWebElement> rowTD;
            foreach (IWebElement row in tableRow)
            {
                rowTD = row.FindElements(By.TagName("td"));

                Console.WriteLine("{0}:{1}:{2}:{3}", rowTD[0].Text,rowTD[1].Text, rowTD[2].Text, rowTD[3].Text);
            }

            //TOMAMOS CAPTURA DE IMAGEN
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile("Image.png",
            ScreenshotImageFormat.Png);
            NuevoTab(driver);

            Thread.Sleep(5000);

            driver.Close();

        }

        private static  void NuevoTab( ChromeDriver driver )
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            string url2 = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+"/Image.png";
            driver.Url=url2;

            Thread.Sleep(5000);

    
        }

    }
}
