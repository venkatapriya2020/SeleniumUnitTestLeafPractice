using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumUnitTestLeafPractice
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver webDriver = new ChromeDriver();

        [TestMethod]
        public void ImageInteraction()
        {
            
            webDriver.Navigate().GoToUrl("http://www.leafground.com/pages/Image.html");

            //Click on this image to go home page
            webDriver.FindElement(By.XPath("//img[@src='../images/home.png']")).Click();
            webDriver.Navigate().GoToUrl("http://www.leafground.com/pages/Image.html");

            //Am I Broken Image?
            var imageDisplayed = webDriver.FindElement(By.XPath("//img[@src='../images/abcd.jpg']")).GetAttribute("naturalWidth");
            
            if(imageDisplayed!="0")
            {
                Console.WriteLine("Image displayed");
            }
            else
            {
                Console.WriteLine("Image broken");
            }

            //Click me using Keyboard or Mouse
            Actions action = new Actions(webDriver);
            webDriver.FindElement(By.XPath("//img[@src='../images/keyboard.png']"));
            action.KeyDown(webDriver.FindElement(By.XPath("//img[@src='../images/keyboard.png']")), Keys.Control)
               .Build()
               .Perform();

            webDriver.Quit();
         }

        [TestMethod]
        public void LearnListboxes()
        {
            webDriver.Navigate().GoToUrl("http://www.leafground.com/pages/Dropdown.html");

            //Select training program using Index
            IWebElement listItems1 =webDriver.FindElement(By.Id("dropdown1"));
            new SelectElement(listItems1).SelectByIndex(2);
            
            //Select training program using Text
            IWebElement listItems2 = webDriver.FindElement(By.Name("dropdown2"));
            new SelectElement(listItems2).SelectByText("Selenium");

            //Select training program using Value
            IWebElement listItems3 = webDriver.FindElement(By.Name("dropdown3"));
            new SelectElement(listItems3).SelectByValue("4");
            

            //Get the number of dropdown options
            IWebElement listItems4 = webDriver.FindElement(By.ClassName("dropdown"));
            Console.WriteLine( new SelectElement(listItems4).Options.Count());

            //You can also use sendKeys to select
            IWebElement listItems5 = webDriver.FindElement(By.XPath("(//select)[5]"));
            listItems5.SendKeys("UFT/QTP");

            //Select your programs multiple
            IWebElement listItems6 = webDriver.FindElement(By.XPath("(//select)[6]"));
            new SelectElement(listItems6).SelectByValue("1");
            new SelectElement(listItems6).SelectByValue("2");
           
            Thread.Sleep(2000);

            webDriver.Quit();
            
        }

        [TestMethod]
        public void PlayWithRadioButtons()
        {
           webDriver.Navigate().GoToUrl("http://www.leafground.com/pages/radio.html");

            //Are you enjoying the classes?
            webDriver.FindElement(By.XPath("//*[@id='yes']")).Click();

            //Find default selected radio button
            bool radio1= webDriver.FindElement(By.XPath("//*[@for='Unchecked']")).Selected;
            bool radio2= webDriver.FindElement(By.XPath("//*[@for='Checked']")).Selected;
            
            if(radio1==true)
            {
                Console.WriteLine("Radio 1 is checked");
            }
            else
            {
                Console.WriteLine("Radio 2 is checked");
            }

            //Select your age group (Only if choice wasn't selected)
            if(!webDriver.FindElement(By.XPath("//div/input[@value='2']")).Selected)
            {
                webDriver.FindElement(By.XPath("//div/input[@value='2']")).Click();
            }
            Thread.Sleep(2000);
            webDriver.Quit();
        }

        [TestMethod]
        public void InteractWithCheckboxes()
        {
            webDriver.Navigate().GoToUrl("http://www.leafground.com/pages/checkbox.html");

            //Select the languages that you know?
            webDriver.FindElement(By.XPath("(//div/input[@type='checkbox'])[3]")).Click();

            //Confirm Selenium is checked
            if(webDriver.FindElement(By.XPath("(//div/input[@type='checkbox'])[6]")).Selected)
            {
                Console.WriteLine("Selenium is selected");
            }

            //DeSelect only checked
             webDriver.FindElement(By.XPath("(//div/input[@type='checkbox'])[8]")).Click();
            
            //Select all below checkboxes
            webDriver.FindElement(By.XPath("(//div/input[@type='checkbox'])[9]")).Click();
            webDriver.FindElement(By.XPath("(//div/input[@type='checkbox'])[10]")).Click();
            webDriver.FindElement(By.XPath("(//div/input[@type='checkbox'])[11]")).Click();
            webDriver.FindElement(By.XPath("(//div/input[@type='checkbox'])[12]")).Click();
            webDriver.FindElement(By.XPath("(//div/input[@type='checkbox'])[13]")).Click();

            Thread.Sleep(2000);
            webDriver.Quit();


        }

        [TestMethod]
        public void Tables()
        {
            
            webDriver.Navigate().GoToUrl("http://www.leafground.com/pages/table.html");

            //Get the count of number of columns
            List<IWebElement> listColumn = webDriver.FindElements(By.TagName("th")).ToList();
            int totalColumnCount = listColumn.Count;
            Console.WriteLine("The Total Column Count is :" + totalColumnCount);

            //Get the count of number of rows
            List<IWebElement> listRow = webDriver.FindElements(By.TagName("tr")).ToList();
            int totalRowCount = listRow.Count;
            Console.WriteLine("The Total row Count is :" + totalRowCount);

            //Get the progress value of 'Learn to interact with Elements'
            string text =  webDriver.FindElement(By.XPath("//table[@id='table_id']/tbody/tr/td[text()='Learn to interact with Elements']/../td[2]")).Text;
            Console.WriteLine(text);

            //Check the vital task for the least completed progress.
            List<IWebElement> fetchProgress = webDriver.FindElements
                (By.XPath("//table[@id='table_id']/tbody/tr/td[2]")).ToList();

            List<int> count = new List<int>();
            
            foreach (var item in fetchProgress)
            {
                int trimItem = Convert.ToInt32(item.Text.Trim('%'));
                count.Add(trimItem);
            }
            int index = count.IndexOf(count.Min())+1;
            webDriver.FindElement(By.XPath("(//*[@type='checkbox'])["+index+"]")).Click();
            Thread.Sleep(2000);
            webDriver.Quit();
            
        }

        [TestMethod]
        public void AlertBox()
        {
            webDriver.Navigate().GoToUrl("http://www.leafground.com/pages/Alert.html");

            //Click the button to display a alert box.
            webDriver.FindElement(By.XPath("(//*/button)[1]")).Click();
            webDriver.SwitchTo().Alert().Accept();

            //Click the button to display a confirm box.
            webDriver.FindElement(By.XPath("(//*/button)[2]")).Click();
            webDriver.SwitchTo().Alert().Dismiss();

            //Click the button to display a prompt box.
            webDriver.FindElement(By.XPath("(//*/button)[3]")).Click();
            webDriver.SwitchTo().Alert().Accept();

            //Click the button to learn line-breaks in an alert.
            webDriver.FindElement(By.XPath("(//*/button)[4]")).Click();
            string AlertText=webDriver.SwitchTo().Alert().Text;
            Console.WriteLine(AlertText);
            webDriver.SwitchTo().Alert().Accept();

            //Click the below button and click OK.
            webDriver.FindElement(By.XPath("//*/button[@id='btn']")).Click();
            webDriver.FindElement(By.XPath("//*/button[@class='swal-button swal-button--confirm']")).Click();


            Thread.Sleep(2000);
            webDriver.Quit();

        }

        [TestMethod]
        public void Frames()
        {
            webDriver.Navigate().GoToUrl("http://www.leafground.com/pages/frame.html");

            //I am inside a frame
            webDriver.SwitchTo().Frame(webDriver
                            .FindElement(By.XPath("//iframe[@src='default.html']")));
            webDriver.FindElement(By.XPath("//*/button[@id='Click']")).Submit();
            webDriver.SwitchTo().DefaultContent();

            webDriver.SwitchTo().Frame(webDriver
                     .FindElement(By.XPath("//*/iframe[@src='page.html']")));
            webDriver.SwitchTo().Frame(webDriver
                            .FindElement(By.XPath("//*/iframe[@src='nested.html']")));
            webDriver.FindElement(By.Id("Click1")).Submit();
            webDriver.SwitchTo().DefaultContent();

            int frameList = webDriver.FindElements(By.XPath("//*/../iframe")).Count;
            Console.WriteLine("The total frame count is :" + frameList);

            Thread.Sleep(2000);
            webDriver.Quit();
        }

        [TestMethod]
        public void DictionaryMethod1()
        {
            webDriver.Navigate().GoToUrl("http://www.leafground.com/pages/frame.html");
            IDictionary<string, int> keyValuePairs = new Dictionary<string, int> 
            { 
                {"A",1 }, {"B",2 },{"C",3 }, {"D",4 }, {"E",5 },{"F",6 },
                {"G",7 }, {"H",8 },{"I",9 }, {"J",10 }, {"K",11 }, {"L",12 },
                {"M",13 }, {"N",14 }, {"O",15 },{"P",16 }, {"Q",17 }, {"R",18 },
                {"S",19 }, {"T",20 }, {"U",21 }, {"V",22 }, {"W",23 }, {"X",24 },
                {"Y",25 }, {"Z",26 }
            };
            

            int sum = 0;
            
            string InputCharacters = "ZZC";
            
            for(int i=0;i<InputCharacters.Length;i++)
            {
                if(keyValuePairs.ContainsKey(InputCharacters[i].ToString()))
                {
                    
                    int value = keyValuePairs[InputCharacters[i].ToString()];
                    sum = sum + value;
                }
                
            }
            Console.WriteLine("Total:" + sum);
            webDriver.Quit();
        }

        [TestMethod]
        public void DictionaryMethod2()
        {
            webDriver.Navigate().GoToUrl("http://www.leafground.com/pages/frame.html");

            string addPrefix;
            string addSuffix;

            IDictionary<int, string> keyValuePairs = new Dictionary<int, string>
            {
                {1 ,"A"}, {2,"B" },{3,"C" }, {4,"D" }, {5,"E" },{6,"F" },
                {7,"G"}, {8,"H"},{9,"I"}, {10,"J" }, {11,"K" }, {12,"L"},
                {13,"M"}, {14,"N" }, {15,"O" },{16,"P" }, {17,"Q" }, {18,"R"},
                {19,"S" }, {20,"T" }, {21,"U" }, {22,"V" }, {23,"W" }, {24,"X" },
                {25,"Y" }, {26,"Z" }
            };
            int prefix = (100 / 26);
            int suffix = (100 % 26);

            if (prefix > 0)
            {
                addPrefix = new string('Z', Convert.ToInt32(prefix));

                if (keyValuePairs.ContainsKey(suffix))
                {
                    addSuffix = keyValuePairs[suffix];
                    string append = addPrefix + addSuffix;
                    Console.WriteLine(append);
                }
            }
            webDriver.Quit();
        }

        [TestMethod]
        public void Windows()
        {
            webDriver.Navigate().GoToUrl("http://www.leafground.com/pages/Window.html");

            //Click button to open home page in New Window
            string parentWindow = webDriver.CurrentWindowHandle;
            webDriver.FindElement(By.Id("home")).Submit();
            string childWindow1 = webDriver.WindowHandles[1];
            webDriver.SwitchTo().Window(parentWindow);

            //Find the number of opened windows
            webDriver.FindElement(By.XPath("(//*/button)[2]")).Submit();
            string childWindow2 = webDriver.WindowHandles[2];
            string childWindow3 = webDriver.WindowHandles[3];
            Console.WriteLine("Total opened windows:"+webDriver.WindowHandles.Count);
            webDriver.SwitchTo().Window(parentWindow);

            //Close all except this window
            webDriver.FindElement(By.Id("color")).Submit();
            string childWindow4 = webDriver.WindowHandles[4];
            string childWindow5 = webDriver.WindowHandles[5];

            webDriver.SwitchTo().Window(childWindow1).Close();
            webDriver.SwitchTo().Window(childWindow2).Close();
            webDriver.SwitchTo().Window(childWindow3).Close();
            webDriver.SwitchTo().Window(childWindow4).Close();
            webDriver.SwitchTo().Window(childWindow5).Close();
            webDriver.SwitchTo().Window(parentWindow);

            //Wait for 2 new Windows to open
            webDriver.FindElement(By.XPath("(//button)[4]")).Submit();
            webDriver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(5);
            string childWindow6 = webDriver.WindowHandles[1];
            string childWindow7 = webDriver.WindowHandles[2];

            webDriver.Quit();
        }
        [TestMethod]
        public void Calendar()
        {
            webDriver.Navigate().GoToUrl("http://www.leafground.com/pages/Calendar.html");
            webDriver.FindElement(By.Id("datepicker")).Click();
            Thread.Sleep(3000);
            webDriver.FindElement(By.XPath("(//*[contains(text(),'10')])[2]")).Click();
        }

        [TestMethod]
        public void DragAndDrop()
        {
            webDriver.Navigate().GoToUrl("http://www.leafground.com/pages/drag.html");
            Actions dragaction = new Actions(webDriver);
            dragaction.DragAndDropToOffset((webDriver.FindElement(By.Id("draggable"))), 120, 120)
                .Build().Perform();
            Thread.Sleep(2000);
            webDriver.Quit();
        }

        [TestMethod]
        public void Droppable()
        {
            webDriver.Navigate().GoToUrl("http://www.leafground.com/pages/drop.html");
            Actions dragaction = new Actions(webDriver);
            dragaction.DragAndDrop((webDriver.FindElement(By.Id("draggable"))), (webDriver.FindElement(By.Id("droppable"))))
                .Build().Perform();
            Thread.Sleep(2000);
            webDriver.Quit();
        }

        [TestMethod]
        public void Selectable()
        {
            webDriver.Navigate().GoToUrl("http://www.leafground.com/pages/selectable.html");
            webDriver.FindElement(By.XPath("//*[contains(text(),'Item 1')]")).Click();
            webDriver.FindElement(By.XPath("//*[contains(text(),'Item 2')]")).Click();
            webDriver.FindElement(By.XPath("//*[contains(text(),'Item 3')]")).Click();
            webDriver.FindElement(By.XPath("//*[contains(text(),'Item 4')]")).Click();
            webDriver.FindElement(By.XPath("//*[contains(text(),'Item 5')]")).Click();
            webDriver.FindElement(By.XPath("//*[contains(text(),'Item 6')]")).Click();
            webDriver.FindElement(By.XPath("//*[contains(text(),'Item 7')]")).Click();
            Thread.Sleep(2000);
            webDriver.Quit();


        }
    }
}

