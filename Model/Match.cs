using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model
{
    public class Match
    {
        public string Round { get; set; }
        public DateTime Date { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public Score Score { get; set; }
    }
}
