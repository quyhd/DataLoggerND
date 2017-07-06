using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger.Entities
{
    public static class CommonInfo
    {
        public const int INT_STATUS_MEASURING = 0;

        public const int TRANSACTION_ADD_NEW = 1;
        public const int TRANSACTION_UPDATE = 2;

        public const int PERIOD_CHECK_COMMUNICATION_ERROR = 35;

        public const string CODE_MPS_PH = "CODE_MPS_PH";
        public const string CODE_MPS_EC = "CODE_MPS_EC";
        public const string CODE_MPS_DO = "CODE_MPS_DO";
        public const string CODE_MPS_TURBIDITY = "CODE_MPS_TURBIDITY";
        public const string CODE_MPS_ORP = "CODE_MPS_ORP";
        public const string CODE_MPS_TEMP = "CODE_MPS_TEMP";


        public const int INT_STATUS_NORMAL = 0;
        public const int INT_STATUS_MEASURING_STOP = 1;
        public const int INT_STATUS_EMPTY_SAMPLER_RESERVOIR = 2;
        public const int INT_STATUS_CALIBRATING = 3;
        public const int INT_STATUS_MAINTENANCE = 4;
        public const int INT_STATUS_COMMUNICATION_ERROR = 5;
        public const int INT_STATUS_INSTRUMENT_ERROR = 6;       

        public const string STATUS_ERROR = "Error";
        public const string STATUS_Normal = "Normal";
        public const string STATUS_WARNING = "Warning";
        public const string STATUS_MEASURING = "Measuring";

        public const int MAINTENANCE_PERIOD = 0;
        public const int MAINTENANCE_INCIDENT = 1;

    }
}
