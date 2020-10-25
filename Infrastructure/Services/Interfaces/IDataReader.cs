using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services.Interfaces
{
    public interface IDataReader
    {
        public List<League> GetEnglishLeagues();
    }
}
