using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLogger.Entities
{
    public class data_value
    {
        public int id { get; set; }
        public Double var1 { get; set; }
        public int var1_status { get; set; }
        public Double var2 { get; set; }
        public int var2_status { get; set; }
        public Double var3 { get; set; }
        public int var3_status { get; set; }
        public Double var4 { get; set; }
        public int var4_status { get; set; }
        public Double var5 { get; set; }
        public int var5_status { get; set; }
        public Double var6 { get; set; }
        public int var6_status { get; set; }

        public DateTime created { get; set; }
        public DateTime stored_date { get; set; }
        public int stored_hour { get; set; }
        public int stored_minute { get; set; }
        public int push { get; set; }
        public DateTime push_time { get; set; }
        public int MPS_status { get; set; }

        public data_value()
        {
            id = -1;
            var1 = -1;
            var2 = -1;
            var3 = -1;
            var4 = -1;
            var5 = -1;
            var6 = -1;

            MPS_status = -1;
            var1_status = -1;
            var2_status = -1;
            var3_status = -1;
            var4_status = -1;
            var5_status = -1;
            var6_status = -1;

            push = -1;
            push_time = new DateTime();

            created = DateTime.Now;

            stored_date = DateTime.Now;
            stored_hour = -1;
            stored_minute = -1;

        }
    }
    public class measured_data
    {
        public Double var1 { get; set; }
        public int var1_status { get; set; }
        public Double var2 { get; set; }
        public int var2_status { get; set; }
        public Double var3 { get; set; }
        public int var3_status { get; set; }
        public Double var4 { get; set; }
        public int var4_status { get; set; }
        public Double var5 { get; set; }
        public int var5_status { get; set; }
        public Double var6 { get; set; }
        public int var6_status { get; set; }

        public int MPS_status { get; set; }        

        public DateTime created { get; set; }

        public DateTime latest_update_MPS_communication { get; set; }

        public DateTime push_time { get; set; }
        public int push { get; set; }

        public measured_data()
        {
            var1 = -1;
            var2 = -1;
            var3 = -1;
            var4 = -1;
            var5 = -1;
            var6 = -1;

            var1_status = -1;
            var2_status = -1;
            var3_status = -1;
            var4_status = -1;
            var5_status = -1;
            var6_status = -1;

            MPS_status = -1;

            created = DateTime.Now;
            latest_update_MPS_communication = DateTime.Now;

            push = -1;
            push_time = new DateTime();
        }
    }

}
