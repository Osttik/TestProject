using Infrastructure.Services.Interfaces;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure.Services.Implementations
{
    public class DataReader: IDataReader
    {
        private readonly string _folderPath = "Data";

        public List<League> GetEnglishLeagues()
        {
            string fullFolderPath = Path.Combine(Environment.CurrentDirectory, _folderPath);

            List<League> leagues = ReadFiles(fullFolderPath);

            return leagues;
        }

        public List<League> ReadFiles(string folder)
        {
            List<League> leagues = new List<League>();

            DirectoryInfo info = new DirectoryInfo(folder);
            FileInfo[] files = info.GetFiles();

            foreach (FileInfo file in files)
            {
                string[] slicedFileName = file.Name.Split('.');

                League league = ReadFile(file.FullName);

                league.LeagueNumber = Convert.ToInt32(slicedFileName[1]);

                leagues.Add(league);
            }

            return leagues;
        }

        public League ReadFile(string path)
        {
            string data = File.ReadAllText(path);

            League league = JsonConvert.DeserializeObject<League>(data);

            return league;
        }
    }
}
