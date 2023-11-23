using System;
namespace MomoApi.Services
{
    public class LogHandler
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private static IWebHostEnvironment _hostingEnvironment;




        public LogHandler(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
        }

        public static void WriteLog(string logMessage, string directory = null, bool isAccessLog = false, string fileName = null)
        {
            try
            {

                string path = Path.Combine("logs", DateTime.Today.ToString("dd-MM-yy") + ".txt");
                string filePath = path;// Path.Combine(_hostingEnvironment.ContentRootPath, path);

                if (!File.Exists(filePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    File.Create(filePath).Close();
                }

                using (StreamWriter w = File.AppendText(filePath))
                {
                    if (isAccessLog)
                    {
                        w.WriteLine(logMessage);
                    }
                    else
                    {
                        w.WriteLine("\r\n::::::::::::::::::::::::::::::::: LOG ENTRY :::::::::::::::::::::::::::::::::");
                        w.WriteLine("{0}", DateTime.Now.ToString("dd/MM/yyyy H:m:s"));
                        w.WriteLine("LOG DETAILS");
                        w.WriteLine("\t|==> LOG FROM : " + filePath);
                        w.WriteLine(logMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                // Gérer l'exception ici, vous pouvez ajouter un journal d'erreurs.
                Console.WriteLine(ex.Message);
            }
        }
    }
}

