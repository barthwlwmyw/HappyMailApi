using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyMailApi.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UsersCollectionName { get; set; }
        public string MessagesCollectionName { get; set; }
    }

    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string UsersCollectionName { get; set; }
        string MessagesCollectionName { get; set; }
    }
}
