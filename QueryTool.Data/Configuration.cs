using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.Data
{
    public sealed class Configuration
    {
        private static readonly Configuration INSTANCE = new Configuration();

        public static Configuration Instance { get => INSTANCE; }

        private readonly string AppDataFolderName = "Query Tool Client";

        private readonly string TextLogFileName = "Logs.txt";

        public readonly string AppDataFolderPath;

        public readonly string TextLogsPath;

        private Configuration()
        {
            AppDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + AppDataFolderName;

            TextLogsPath = AppDataFolderPath + "\\" + TextLogFileName;

            //Creates directory only if it doesnt exists yet 
            Directory.CreateDirectory(AppDataFolderPath);
            
        }

    }
}
