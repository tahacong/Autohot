using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace Auto_Reg_Hotmail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void MoChrome()
        {
            // Thiết lập đường dẫn đến tệp exe của ChromeDriver
            var chromeOptions = new ChromeOptions();
            chromeOptions.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            chromeOptions.AddArgument("disable-infobars");
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
        }
        //Tạo acc hotmail
        public static void RegisterHotmailAccount(string email, string password, int waitTimeSeconds)
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--incognito");
            options.AddArgument(@"--start-maximized");

            using (var driver = new ChromeDriver(driverService, options))
            {
                driver.Navigate().GoToUrl("https://signup.live.com/signup.aspx");

                var emailInput = driver.FindElement(By.Id("MemberName"));
                emailInput.SendKeys(email);

                var passwordInput = driver.FindElement(By.Id("PasswordInput"));
                passwordInput.SendKeys(password);

                var confirmPasswordInput = driver.FindElement(By.Id("RetypePassword"));
                confirmPasswordInput.SendKeys(password);

                var firstNameInput = driver.FindElement(By.Id("FirstName"));
                firstNameInput.SendKeys("First");

                var lastNameInput = driver.FindElement(By.Id("LastName"));
                lastNameInput.SendKeys("Last");

                var birthMonthInput = driver.FindElement(By.Id("BirthMonth"));
                var birthDayInput = driver.FindElement(By.Id("BirthDay"));
                var birthYearInput = driver.FindElement(By.Id("BirthYear"));

                // Set birth date to 18 years ago
                var birthDate = DateTime.Now.AddYears(-18);
                birthMonthInput.SendKeys(birthDate.Month.ToString());
                birthDayInput.SendKeys(birthDate.Day.ToString());
                birthYearInput.SendKeys(birthDate.Year.ToString());

                var countrySelect = driver.FindElement(By.Id("Country"));
                var selectElement = new SelectElement(countrySelect);
                selectElement.SelectByText("United States");

                var nextButton = driver.FindElement(By.Id("iSignupAction"));
                nextButton.Click();

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(waitTimeSeconds));
            }
        }
        public static string CreateUserName()        {            HashSet<string> usernames = new HashSet<string>();            Random rand = new Random();
            string username = "";
            while (true)
            {
                for (int j = 0; j < 10; j++)
                {
                    int type = rand.Next(4);
                    username += (char)(rand.Next(26) + 97);
                }
                if (usernames.Contains(username) == false)
                {
                    usernames.Add(username);
                    break;
                }
            }
            return username;
        }
        //Tạo password tự động
        public static string CreatePassword()        {            HashSet<string> passwords = new HashSet<string>();            Random rand = new Random();
            string password = "";
            while (true)
            {
                password += (char)(rand.Next(26) + 97);
                for (int j = 0; j < 8; j++)
                {
                    int type = rand.Next(4);
                    if (type == 0)
                    {
                        password += (char)(rand.Next(26) + 97);
                    }
                    else if (type == 1)
                    {
                        password += (char)(rand.Next(26) + 65);
                    }
                    else if (type == 2)
                    {
                        password += (char)(rand.Next(10) + 48);
                    }
                    else
                    {
                        password += (char)(rand.Next(15) + 33);
                    }
                }
                if (passwords.Contains(password) == false)
                {
                    passwords.Add(password);
                    break;
                }
            }
            return password;
        }
        //Tạo họ từ list
        public static string TaoHo()
        {
            string filePath = "ho.txt"; // đường dẫn đến file ho.txt
            string[] lines = System.IO.File.ReadAllLines(filePath); // đọc tất cả các dòng trong file vào một mảng
            int randomIndex = new Random().Next(0, lines.Length); // sinh một số ngẫu nhiên từ 0 đến số lượng dòng trong file
            return lines[randomIndex]; // trả về dòng tương ứng với số ngẫu nhiên đã sinh
        }
        //Tao tên từ list
        public static string TaoTen()
        {
            string filePath = "ten.txt"; // đường dẫn đến file ten.txt
            string[] lines = System.IO.File.ReadAllLines(filePath); // đọc tất cả các dòng trong file vào một mảng
            int randomIndex = new Random().Next(0, lines.Length); // sinh một số ngẫu nhiên từ 0 đến số lượng dòng trong file
            return lines[randomIndex]; // trả về dòng tương ứng với số ngẫu nhiên đã sinh
        }
        //Ghi log()
        public void Ghilog(string userID, string email, string pass, string ho, string ten)
        {
            string logLine = userID + "|" + email + "|" + pass + "|" + ho + "|" + ten;
            using (StreamWriter sw = File.AppendText("log.txt"))
            {
                sw.WriteLine(logLine);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MoChrome();
        }
    }
}
