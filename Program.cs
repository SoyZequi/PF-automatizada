using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    static void Main()
    {
        try
        {
            // Configuración para Edge
            var options = new EdgeOptions();
            var driver = new EdgeDriver(options);

            // Carpeta para screenshots
            string captureFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CaptureFolder");
            if (!Directory.Exists(captureFolder))
            {
                Directory.CreateDirectory(captureFolder);
            }

            // Entra a Edge
            driver.Navigate().GoToUrl("https://www.google.com/?hl=es");
            CaptureSs(driver, Path.Combine(captureFolder, "1.png"));

            // Entra al SearchBox
            var googlesearch = driver.FindElement(By.Id("APjFqb"));
            googlesearch.SendKeys("amazon.com");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            CaptureSs(driver, Path.Combine(captureFolder, "2.png"));

            // Te redirigue a la pagina
            driver.Navigate().GoToUrl("https://www.amazon.com/");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            CaptureSs(driver, Path.Combine(captureFolder, "3.png"));

            // Entra al SearchBox de Amazon
            var amazonsearch = driver.FindElement(By.Id("twotabsearchtextbox"));
            amazonsearch.SendKeys("Lamine Yamal");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            CaptureSs(driver, Path.Combine(captureFolder, "4.png"));

            // Presiona el boton de búsqueda
            var searchbutton = driver.FindElement(By.Id("nav-search-submit-button"));
            if (searchbutton != null)
            {
                ExecuteJavaSclick(driver, searchbutton);
                Thread.Sleep(TimeSpan.FromSeconds(5)); // Aumenta el tiempo de espera
                CaptureSs(driver, Path.Combine(captureFolder, "5.png"));
            }
            else
            {
                Console.WriteLine("El botón de búsqueda no se encontró.");
            }

            // Te redirigue a la pagina
            driver.Navigate().GoToUrl(" https://www.amazon.com/Lamine-Yamal-Biography-Untapped-Football/dp/B0CXMXQMVD/ref=sr_1_2?crid=8LMKIDSSQUII&dib=eyJ2IjoiMSJ9.4ctB3AQH_M-j2gLI21glmqNXOj6O5ujzuPbGMpNTCeRfrP50-m8uALrC9-WuFtp6_vhoX5urBqrcCsLiaFgm3Ih5XD7r1MYUhYDZ799zB4hehZdYoQaDuS-Su-aLZa252xauSHOK5cPjEhuARaqNwPWnzclKfQCgIXoJgfo8HQQGdAJiJ8u9wHnxgb8pPIRJcZD7OotYYmkh0Oy4F2ZoYfGen6_vDjkErrdfkVRYx4rRorML66rjlvMIz2dSJQIUdW-a3mSli-214EpR4V354R6-20V5DdbR73n23TdlS3c.UbggTxGmDJGU6Q6bTqOI21CBDDykkZeUYGgJKcdgWio&dib_tag=se&keywords=lamine+yamal&qid=1723300527&sprefix=lamine+yamal%2Caps%2C112&sr=8-2");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            CaptureSs(driver, Path.Combine(captureFolder, "6.png"));

            //abrir el readmore
            var readMoreSpan = driver.FindElement(By.XPath("//span[@class='a-expander-prompt' and contains(text(),'Read more')]"));
            readMoreSpan.Click();
            Thread.Sleep(TimeSpan.FromSeconds(3));
            CaptureSs(driver, Path.Combine(captureFolder, "7.png"));


            // Agrega el producto al carrito
            var addcartbutton = driver.FindElement(By.Id("add-to-cart-button"));
            ExecuteJavaSclick(driver, addcartbutton);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            CaptureSs(driver, Path.Combine(captureFolder, "8.png"));

            // Presiona el boton que redirige a la pagina principal
            var homebutton = driver.FindElement(By.Name("proceedToRetailCheckout"));
            ExecuteJavaSclick(driver, homebutton);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            CaptureSs(driver, Path.Combine(captureFolder, "9.png"));

            // Te redirigue a la pagina
            driver.Navigate().GoToUrl("https://www.amazon.com/");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            CaptureSs(driver, Path.Combine(captureFolder, "10.png"));


            // Redirige a Amazon Music
            driver.Navigate().GoToUrl("https://music.amazon.com/?ref=dm_lnd_nw_listn_fd44f942_nav_em__dm_nav_nw_0_2_2_3");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            CaptureSs(driver, Path.Combine(captureFolder, "11.png"));

            // Crea un informe HTML con las capturas de pantalla
            string report = Path.Combine(captureFolder, "Reporte_de_ss.html");
            GeneraHtmlReporte(captureFolder, report);

            Console.WriteLine($"Informe HTML creado en: {report}");

            Thread.Sleep(TimeSpan.FromSeconds(5));

            // Abre el informe HTML en el navegador predeterminado
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = report,
                UseShellExecute = true
            });
        }
        catch (Exception pro)
        {
            Console.WriteLine($"Error: {pro.Message}");
        }
    }

    // Método para ejecutar clics mediante JavaScript
    static void ExecuteJavaSclick(IWebDriver driver, IWebElement element)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("arguments[0].click();", element);
    }

    // Método que captura capturas de pantalla
    static void CaptureSs(IWebDriver driver, string filePath)
    {
        try
        {
            ITakesScreenshot screenshotsave = (ITakesScreenshot)driver;
            Screenshot screenshot = screenshotsave.GetScreenshot();
       
        }
        catch (Exception prom)
        {
            Console.WriteLine($"Error al capturar ss en la pantalla: {prom.Message}");
        }
    }

    // Método que genera un informe HTML con las capturas de pantalla
    static void GeneraHtmlReporte(string captureFolder, string reportFilePath)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(reportFilePath))
            {
                sw.WriteLine("<!DOCTYPE html>");
                sw.WriteLine("<html lang=\"es\">");
                sw.WriteLine("<head>");
                sw.WriteLine("    <meta charset=\"UTF-8\">");
                sw.WriteLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                sw.WriteLine("    <title>Informe de Capturas</title>");
                sw.WriteLine("    <style>");
                sw.WriteLine("        body { font-family: 'Arial', sans-serif; background-color: #f4f4f4; margin: 20px; }");
                sw.WriteLine("        h1 { color: #333; }");
                sw.WriteLine("        img { max-width: 100%; height: auto; border: 1px solid #ddd; margin-bottom: 10px; }");
                sw.WriteLine("    </style>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");
                sw.WriteLine("    <h1>Informe de Capturas:</h1>");

                string[] imageFiles = Directory.GetFiles(captureFolder, "*.png");

                foreach (string imageFile in imageFiles)
                {
                    sw.WriteLine($"    <img src=\"{Path.GetFileName(imageFile)}\" alt=\"Captura de pantalla\">");
                }

                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
            }

            Console.WriteLine($"Informe HTML creado en: {reportFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al generar el informe HTML: {ex.Message}");
        }
    }
}
