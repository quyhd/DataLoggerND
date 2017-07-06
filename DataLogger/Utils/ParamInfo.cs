using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogger.Utils
{
    public class ParamInfo
    {
        public string NameDB { get; set; }
        public string  NameDisplay { get; set; }
        public bool HasStatus { get; set; }
        public string StatusNameDB { get; set; }
        public string StatusNameDisplay { get; set; }
        public string StatusNameVisible { get; set; }
        public bool Selected { get; set; }
        public Color GraphColor { get; set; }
    }

    public static class DataLoggerParam
    {
        public static List<ParamInfo> PARAMETER_LIST = new List<ParamInfo>()
        {
             new ParamInfo(){ NameDB = "mps_ph",                    NameDisplay = "MPS pH",         HasStatus = true,  StatusNameDB = "mps_ph_status",          StatusNameDisplay = "MPS pH Status", StatusNameVisible = "MPS_pH_Status_Val" , Selected = false, GraphColor = Color.OrangeRed}
            ,new ParamInfo(){ NameDB = "mps_ec",                    NameDisplay = "MPS EC",         HasStatus = true,  StatusNameDB = "mps_ec_status",          StatusNameDisplay = "MPS EC Status", StatusNameVisible = "MPS_EC_Status_Val" , Selected = false, GraphColor = Color.Blue }
            ,new ParamInfo(){ NameDB = "mps_do",                    NameDisplay = "MPS DO",         HasStatus = true,  StatusNameDB = "mps_do_status",          StatusNameDisplay = "MPS DO Status", StatusNameVisible = "MPS_DO_Status_Val" , Selected = false, GraphColor = Color.BlueViolet }
            ,new ParamInfo(){ NameDB = "mps_turbidity",             NameDisplay = "MPS Turbidity",  HasStatus = true,  StatusNameDB = "mps_turbidity_status",   StatusNameDisplay = "MPS Turbidity Status", StatusNameVisible = "MPS_Turbidity_Status_Val" , Selected = false, GraphColor = Color.Brown }
            ,new ParamInfo(){ NameDB = "mps_orp",                   NameDisplay = "MPS ORP",        HasStatus = true,  StatusNameDB = "mps_orp_status",         StatusNameDisplay = "MPS ORP Status", StatusNameVisible = "MPS_ORP_Status_Val" , Selected = false, GraphColor = Color.Chocolate }
            ,new ParamInfo(){ NameDB = "mps_temp",                  NameDisplay = "MPS Temp",       HasStatus = true,  StatusNameDB = "mps_temp_status",        StatusNameDisplay = "MPS Temp Status", StatusNameVisible = "MPS_Temp_Status_Val" , Selected = false, GraphColor = Color.Coral }
            ,new ParamInfo(){ NameDB = "tn",                        NameDisplay = "TN",             HasStatus = true,  StatusNameDB = "tn_status",              StatusNameDisplay = "TN Status", StatusNameVisible = "TN_Status_Val" , Selected = false, GraphColor = Color.CornflowerBlue }
            ,new ParamInfo(){ NameDB = "tp",                        NameDisplay = "TP",             HasStatus = true,  StatusNameDB = "tp_status",              StatusNameDisplay = "TP Status", StatusNameVisible = "TP_Status_Val" , Selected = false, GraphColor = Color.Crimson }
            ,new ParamInfo(){ NameDB = "toc",                       NameDisplay = "TOC",            HasStatus = true,  StatusNameDB = "toc_status",             StatusNameDisplay = "TOC Status", StatusNameVisible = "TOC_Status_Val" , Selected = false, GraphColor = Color.DarkBlue }
            ,new ParamInfo(){ NameDB = "refrigeration_temperature", NameDisplay = "SAM00(Temp)",    HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.DarkCyan }
            ,new ParamInfo(){ NameDB = "bottle_position",           NameDisplay = "SAM01(Bottle)",  HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.DarkGoldenrod }
            ,new ParamInfo(){ NameDB = "door_status",               NameDisplay = "SAM02(Door)",    HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.DarkGreen }
            ,new ParamInfo(){ NameDB = "module_power",              NameDisplay = "Power",          HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.DarkKhaki }
            ,new ParamInfo(){ NameDB = "module_ups",                NameDisplay = "UPS",            HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.DarkMagenta }
            ,new ParamInfo(){ NameDB = "module_door",               NameDisplay = "Door",           HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.DarkOliveGreen }
            ,new ParamInfo(){ NameDB = "module_fire",               NameDisplay = "Fire",           HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.DarkOrange }
            ,new ParamInfo(){ NameDB = "module_flow",               NameDisplay = "Flow",           HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.DarkOrchid }
            ,new ParamInfo(){ NameDB = "module_pumplam",            NameDisplay = "Pump (L) A/M",   HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.DarkRed }
            ,new ParamInfo(){ NameDB = "module_pumplrs",            NameDisplay = "Pump (L) R/S",   HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.DarkSlateBlue }
            ,new ParamInfo(){ NameDB = "module_pumplflt",           NameDisplay = "Pump (L) FLT",   HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.DarkSlateGray }
            ,new ParamInfo(){ NameDB = "module_pumpram",            NameDisplay = "Pump (R) A/M",   HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.Firebrick }
            ,new ParamInfo(){ NameDB = "module_pumprrs",            NameDisplay = "Pump (R) R/S",   HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.Fuchsia }
            ,new ParamInfo(){ NameDB = "module_pumprflt",           NameDisplay = "Pump (R) FLT",   HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.Indigo }
            ,new ParamInfo(){ NameDB = "module_temperature",        NameDisplay = "Temperature",    HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.Red }
            ,new ParamInfo(){ NameDB = "module_humidity",           NameDisplay = "Humidity",       HasStatus = false, StatusNameDB = "", StatusNameDisplay = "", StatusNameVisible = "" , Selected = false, GraphColor = Color.Magenta }
        };

    }
}
