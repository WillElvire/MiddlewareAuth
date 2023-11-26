namespace MiddlewareAuth
{
    internal class LogHandler
    {
        internal static void WriteLog(object logMessage, string actionType)
        {
            try
            {

                string path = Path.Combine(actionType+"logs", DateTime.Today.ToString("dd-MM-yy") + ".txt");
                string filePath = path;// Path.Combine(_hostingEnvironment.ContentRootPath, path);

                if (!File.Exists(filePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    File.Create(filePath).Close();
                }

                using (StreamWriter w = File.AppendText(filePath))
                {
                    w.WriteLine("\r\n::::::::::::::::::::::::::::::::: LOG ENTRY :::::::::::::::::::::::::::::::::");
                    w.WriteLine("{0}", DateTime.Now.ToString("dd/MM/yyyy H:m:s"));
                    w.WriteLine("LOG DETAILS");
                    w.WriteLine("\t|==> LOG FROM : " + filePath);
                    w.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                // Gérer l'exception ici, vous pouvez ajouter un journal d'erreurs.
                Console.WriteLine(ex.Message);
            }
        }

        internal static void WriteLog(string v)
        {
        }

        internal static void WriteLog(string logAccessMessage, string v1, bool v2)
        {
        }
    }
}