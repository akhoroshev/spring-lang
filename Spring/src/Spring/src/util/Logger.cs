using System.IO;

namespace JetBrains.ReSharper.Plugins.Spring.Util
{
    public class Logger
    {
        public static Logger Default = new Logger("/home/akhoroshev/spring.log");

        private readonly string _path;

        public Logger(string path)
        {
            _path = path;
        }

        public void Log(string line)
        {
            var writer = File.AppendText(_path);
            writer.WriteLine(line);
            writer.Close();
        }
    }
}