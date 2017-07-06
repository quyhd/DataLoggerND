using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLogger.Entities
{
    public class station
    {
        public int id { get; set; }
        public string station_name { get; set; }
        public string station_id { get; set; }
        public int socket_port { get; set; }
        public DateTime modified { get; set; }
        public string ftpserver { get; set; }
        public string ftpusername { get; set; }
        public string ftpfolder { get; set; }
        public string ftppassword { get; set; }
        public int ftpflag { get; set; }
        public station()
        {
            id = -1;
            station_name = "Station No.1";
            station_id = "BLVTRS0001";
            socket_port = 3001;
            modified = new DateTime();
            ftpserver = "";
            ftpusername = "";
            ftpfolder = "";
            ftppassword = "";
            ftpflag = -1;
        }
    }
}
