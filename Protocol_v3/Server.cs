using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.IO.Ports;
using System.Web.Helpers;
using DataLogger.Data;
using Npgsql;
using System.Data;
using DataLogger.Entities;

namespace Protocol
{
    public static class MyTcpListener
    {
        private const int BUFFER_SIZE = 1024;
        public static measured_data objMeasuredDataGlobal = new measured_data();
        //public static TcpListener tcpListener = null;
        public static System.IO.Ports.SerialPort serialPortTN;
        public static System.IO.Ports.SerialPort serialPortTOC;
        public static System.IO.Ports.SerialPort serialPortTP;
        public static System.IO.Ports.SerialPort serialPortSAMP;
        //public static NpgsqlConnection _conn;
        private const string connstring = @"Server=localhost;Port=5432;User Id=postgres;Password=123;Database=DataLoggerDB;";
        static int counter = 0;
        static Thread newThread = null;
        //TcpClient client = null;
        public static void Main1()
        {
            String msg = "\x02" + "BLVTRS0001" + "20160831152201" + "DATAM" + "\x03" + "\x0D";
            Console.WriteLine(msg);
            Console.WriteLine(CalculateChecksum(msg));
            Console.ReadLine();
        }
        public static string CalculateChecksum(string dataToCalculate)
        {
            byte[] byteToCalculate = Encoding.ASCII.GetBytes(dataToCalculate);

            int checksum = 0;

            foreach (byte chData in byteToCalculate)
            {
                checksum += chData;
            }

            checksum &= 0xff;
            //Console.WriteLine(checksum);
            string hexString = String.Format("\\" + "x{0:X2}", checksum);
            //return hexString; //X2 de chuyen sang hexa
            return checksum.ToString("X2");
        }
        public static void sendMsg(NetworkStream nwStream, String msg)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(msg);
            String data = System.Text.Encoding.ASCII.GetString(buffer, 0, buffer.Length);
            try
            {
                nwStream.Write(buffer, 0, buffer.Length);
                nwStream.Flush();
                Console.WriteLine("Sended: {0}", msg);
            }
            catch
            {
                Console.WriteLine("SENDING: " + "\"" + msg + "\"" + " BUT");
                Console.WriteLine("ERROR : CAN NOT LISTEN ANY CONNECT, CHECK CONNECT IN CENTER.");
            }
        }
        public static void sendByte(NetworkStream nwStream, byte[] msg)
        {
            //byte[] buffer = Encoding.ASCII.GetBytes(msg);
            //String data = System.Text.Encoding.ASCII.GetString(buffer, 0, buffer.Length);
            try
            {
                nwStream.Write(msg, 0, msg.Length);
                nwStream.Flush();
                Console.WriteLine(" Sended: {0}", _encoder.GetString(msg));
            }
            catch
            {
                Console.WriteLine("SENDING: " + "\"" + _encoder.GetString(msg) + "\"" + " BUT");
                Console.WriteLine("ERROR : CAN NOT LISTEN ANY CONNECT, CHECK CONNECT IN CENTER.");
            }
        }
        public static ASCIIEncoding _encoder = new ASCIIEncoding();
        //public static string Au(string _privateKey, string _publicKey, String enc, RSACryptoServiceProvider rsa)
        //{
        //    //var rsa = new RSACryptoServiceProvider();
        //    //_privateKey = rsa.ToXmlString(true);
        //    //_publicKey = rsa.ToXmlString(false);
        //    //var text = "Test1";
        //    //Console.WriteLine("RSA // Text to encrypt: " + text);
        //    //var enc = Encrypt(text);
        //    //Console.WriteLine("RSA // Encrypted Text: " + enc);
        //    var dec = Decrypt(enc, _privateKey, rsa);
        //    return dec;
        //    //Console.WriteLine("RSA // Decrypted Text: " + dec);
        //    // Console.ReadLine();
        //}
        //public static string Decrypt(string data, string _privateKey, RSACryptoServiceProvider rsa)
        //{
        //    //var rsa = new RSACryptoServiceProvider();
        //    var dataArray = data.Split(new char[] { ',' });
        //    byte[] dataByte = new byte[dataArray.Length];
        //    for (int i = 0; i < dataArray.Length; i++)
        //    {
        //        dataByte[i] = Convert.ToByte(dataArray[i]);
        //    }

        //    rsa.FromXmlString(_privateKey);
        //    try
        //    {
        //        var decryptedByte = rsa.Decrypt(dataByte, false);
        //        return _encoder.GetString(decryptedByte);
        //    }
        //    catch (Exception ex)
        //    {
        //        return "ERROR";
        //    }
        //}
        public static string Encrypt(string data, string _publicKey, RSACryptoServiceProvider rsa)
        {
            // var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_publicKey);
            var dataToEncrypt = _encoder.GetBytes(data);
            var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            String encrypt = _encoder.GetString(encryptedByteArray);
            Console.WriteLine("ENCRYPT :" + encrypt);
            var length = encryptedByteArray.Count();
            var item = 0;
            var sb = new StringBuilder();
            foreach (var x in encryptedByteArray)
            {
                item++;
                sb.Append(x);
                if (item < length)
                    sb.Append(",");
            }
            return sb.ToString();
        }
        //public static Boolean Auth(String user, String recvstring, String _privateKey, String _publicKey, RSACryptoServiceProvider rsa)
        //{
        //    //Crypto.VerifyHashedPassword
        //    String table = "auth";
        //    using (NpgsqlDBConnection db = new NpgsqlDBConnection())
        //    {
        //        try
        //        {
        //            if (db.open_connection())
        //            {
        //                string sql_command = "SELECT password from " + table + " where user_name = " + "\'" + user + "\'";
        //                using (NpgsqlCommand cmd = db._conn.CreateCommand())
        //                {
        //                    cmd.CommandText = sql_command;
        //                    NpgsqlDataReader dr = cmd.ExecuteReader();
        //                    DataTable data = new DataTable();
        //                    // call load method of datatable to copy content of reader 
        //                    data.Load(dr); // Load 
        //                    String hashedpwd = data.Rows[0][0].ToString();
        //                    Boolean i = false;
        //                    //String privatekey = "9G&yH";
        //                    //var csp = new RSACryptoServiceProvider(2048);
        //                    //var privKey = csp.ExportParameters(true);
        //                    //csp.ImportParameters(privKey);
        //                    //byte[] ret = csp.Decrypt(recvstring, false);
        //                    //string decrypt = System.Text.Encoding.Unicode.GetString(ret);
        //                    String decrypt = Au(_privateKey, _publicKey, recvstring, rsa);
        //                    if (Crypto.VerifyHashedPassword(hashedpwd, decrypt))
        //                    {
        //                        i = true;
        //                    }
        //                    db.close_connection();
        //                    if (i)
        //                    {
        //                        Console.WriteLine("TRUE");
        //                        return true;
        //                    }
        //                    else Console.WriteLine("FALSE"); return false;
        //                }
        //            }
        //            else
        //            {
        //                db.close_connection();
        //                return false;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            string[] Error = new String[1] { "error" };
        //            Console.WriteLine("\nMessage ---\n{0}", ex.Message);
        //            Console.WriteLine("\nMessage ---\n{0}", ex.StackTrace);
        //            return false;
        //        }
        //    }
        //}
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
        public static Boolean Timeout(TcpClient client)
        {
            if (client.ReceiveTimeout <= 60000) { return true; }
            else return false;

        }
        public static void DATA(byte[] buffer, String data, int j, NetworkStream nwStream, TcpClient client)
        {

            if (_encoder.GetString(buffer.SubArray(j + 19, 1)).Equals("M"))
            {
                byte[] _STX = new byte[1];
                (new byte[] { 0x02 }).CopyTo(_STX, 0);
                string table = "data_5minute_values";
                string col = "created";
                String measuretime = getMeasureTime(table, col);
                byte[] _measuretime = new byte[14];
                _encoder.GetBytes(measuretime).CopyTo(_measuretime, 0);
                byte[] _Command = new byte[4];
                _encoder.GetBytes("DATA").CopyTo(_Command, 0);
                byte[] _Param = new byte[1];
                String _param = "M";
                _encoder.GetBytes(_param).CopyTo(_Param, 0);
                byte[] _dcd = new byte[1];
                _encoder.GetBytes("1").CopyTo(_dcd, 0);
                byte[] _streamCode = new byte[11];
                _encoder.GetBytes(data.Substring(1, j)).CopyTo(_streamCode, 0);
                byte[] _header = _STX.Concat(_Command).Concat(_Param).Concat(_streamCode).Concat(_dcd).Concat(_measuretime).ToArray();
                byte[] _item = DataSQL("data_5minute_values", "databinding");
                byte[] _EOT = new byte[1];
                (new byte[] { 0x04 }).CopyTo(_EOT, 0);
                byte[] _ETX = new byte[1];
                (new byte[] { 0x03 }).CopyTo(_ETX, 0);
                String checksum = CalculateChecksum(_encoder.GetString(_header) + _encoder.GetString(_item) + _encoder.GetString(_ETX));
                byte[] _checksum = new byte[2];
                _encoder.GetBytes(checksum).CopyTo(_checksum, 0);
                byte[] _CR = new byte[1];
                (new byte[] { 0x0D }).CopyTo(_CR, 0);
                byte[] _tailer = _ETX.Concat(_checksum).Concat(_CR).ToArray();
                byte[] _sender = _header.Concat(_item).Concat(_tailer).ToArray();
                sendByte(nwStream, _sender);
                buffer = new byte[BUFFER_SIZE];
                if (nwStream.Read(buffer, 0, buffer.Length) != 0 && Timeout(client))
                {
                    if (buffer[0] == 15 && Timeout(client))
                    {
                        sendByte(nwStream, _sender);
                    }
                    else
                        if (buffer[0] == 6 && Timeout(client))
                    {
                        sendByte(nwStream, _EOT);
                    }
                    else
                    {
                        sendByte(nwStream, _encoder.GetBytes("ERROR FORMAT OR TIMEOUT >60s"));
                    }
                }
                else
                {
                    sendByte(nwStream, _encoder.GetBytes("ERROR FORMAT OR TIMEOUT >60s"));
                }
            }
            else
                if (_encoder.GetString(buffer.SubArray(j + 19, 1)).Equals("H"))
            {
                byte[] _STX = new byte[1];
                (new byte[] { 0x02 }).CopyTo(_STX, 0);
                string table = "data_60minute_values";
                string col = "created";
                String measuretime = getMeasureTime(table, col);
                byte[] _measuretime = new byte[14];
                _encoder.GetBytes(measuretime).CopyTo(_measuretime, 0);
                byte[] _Command = new byte[4];
                _encoder.GetBytes("DATA").CopyTo(_Command, 0);
                byte[] _Param = new byte[1];
                String _param = "H";
                _encoder.GetBytes(_param).CopyTo(_Param, 0);
                byte[] _dcd = new byte[1];
                _encoder.GetBytes("1").CopyTo(_dcd, 0);
                byte[] _streamCode = new byte[11];
                _encoder.GetBytes(data.Substring(1, j)).CopyTo(_streamCode, 0);
                byte[] _header = _STX.Concat(_Command).Concat(_Param).Concat(_streamCode).Concat(_dcd).Concat(_measuretime).ToArray();
                byte[] _item = DataSQL("data_60minute_values", "databinding");
                byte[] _EOT = new byte[1];
                (new byte[] { 0x04 }).CopyTo(_EOT, 0);
                byte[] _ETX = new byte[1];
                (new byte[] { 0x03 }).CopyTo(_ETX, 0);
                String checksum = CalculateChecksum(_encoder.GetString(_header) + _encoder.GetString(_item) + _encoder.GetString(_ETX));
                byte[] _checksum = new byte[2];
                _encoder.GetBytes(checksum).CopyTo(_checksum, 0);
                //String tailer = ETX + checksum + "\x0D";
                byte[] _CR = new byte[1];
                (new byte[] { 0x0D }).CopyTo(_CR, 0);
                byte[] _tailer = _ETX.Concat(_checksum).Concat(_CR).ToArray();
                byte[] _sender = _header.Concat(_item).Concat(_tailer).ToArray();
                sendByte(nwStream, _sender);
                buffer = new byte[BUFFER_SIZE];
                if (nwStream.Read(buffer, 0, buffer.Length) != 0 && Timeout(client))
                {
                    if (buffer[0] == 15 && Timeout(client))
                    {
                        //sendMsg(nwStream, sender);
                        sendByte(nwStream, _sender);
                    }
                    else
                        if (buffer[0] == 6 && Timeout(client))
                    {
                        sendByte(nwStream, _EOT);
                        //sendMsg(nwStream, EOT);
                        //Console.WriteLine("Sended: {0}", EOT);
                    }
                    else
                    {
                        //sendMsg(nwStream, "ERROR FORMAT OR TIMEOUT >60s");
                        //Console.WriteLine("Sended: {0}", "ER");
                        sendByte(nwStream, _encoder.GetBytes("ERROR FORMAT OR TIMEOUT >60s"));
                    }
                }
                else
                {
                    sendByte(nwStream, _encoder.GetBytes("ERROR FORMAT OR TIMEOUT >60s"));
                    //sendMsg(nwStream, "ERROR FORMAT OR TIMEOUT >60s");
                    //Console.WriteLine("Sended: {0}", "ER");
                }
            }
            else
            {
                sendByte(nwStream, _encoder.GetBytes("ERROR : " + "\" " + "DATA " + "\" " + "COMMAND"));
                //sendMsg(nwStream, "ERROR : " + "\" " + "DATA " + "\" " + "COMMAND");
                //Console.WriteLine("Sended: {0}", "DATA");
            }

        }
        public static void RDAT(byte[] buffer, String data, int j, NetworkStream nwStream, TcpClient client, measured_data objMeasuredDataGlobal)
        {
            byte[] _STX = new byte[1];
            (new byte[] { 0x02 }).CopyTo(_STX, 0);
            String date = DateFormat(DateTime.Now.ToString());
            byte[] _date = new byte[14];
            _encoder.GetBytes(date).CopyTo(_date, 0);
            byte[] _Command = new byte[4];
            _encoder.GetBytes("RDAT").CopyTo(_Command, 0);
            byte[] _streamCode = new byte[11];
            _encoder.GetBytes(data.Substring(1, j)).CopyTo(_streamCode, 0);
            byte[] _dcd = new byte[1];
            _encoder.GetBytes("1").CopyTo(_dcd, 0);
            byte[] _header = _STX.Concat(_Command).Concat(_streamCode).Concat(_dcd).Concat(_date).ToArray();
            // String item;
            byte[] _ETX = new byte[1];
            (new byte[] { 0x03 }).CopyTo(_ETX, 0);
            #region item
            String mps_ph_value = ConvertStr(objMeasuredDataGlobal.var1.ToString(), 10);
            String mps_ph_status = ConvertStr(objMeasuredDataGlobal.var1_status.ToString(), 2);
            byte[] _mps_ph_code = new byte[5];
            byte[] _mps_ph_value = new byte[10];
            byte[] _mps_ph_status = new byte[2];
            _encoder.GetBytes("mpsph").CopyTo(_mps_ph_code, 0);
            _encoder.GetBytes(mps_ph_value).CopyTo(_mps_ph_value, 0);
            _encoder.GetBytes(mps_ph_status).CopyTo(_mps_ph_status, 0);
            byte[] _mps_ph = _mps_ph_code.Concat(_mps_ph_value).Concat(_mps_ph_status).ToArray();

            String mps_ec_value = ConvertStr(objMeasuredDataGlobal.var2.ToString(), 10);
            String mps_ec_status = ConvertStr(objMeasuredDataGlobal.var2_status.ToString(), 2);
            byte[] _mps_ec_code = new byte[5];
            byte[] _mps_ec_value = new byte[10];
            byte[] _mps_ec_status = new byte[2];
            _encoder.GetBytes("mpsec").CopyTo(_mps_ec_code, 0);
            _encoder.GetBytes(mps_ec_value).CopyTo(_mps_ec_value, 0);
            _encoder.GetBytes(mps_ec_status).CopyTo(_mps_ec_status, 0);
            byte[] _mps_ec = _mps_ec_code.Concat(_mps_ec_value).Concat(_mps_ec_status).ToArray();

            String mps_do_value = ConvertStr(objMeasuredDataGlobal.var3.ToString(), 10);
            String mps_do_status = ConvertStr(objMeasuredDataGlobal.var3_status.ToString(), 2);
            byte[] _mps_do_code = new byte[5];
            byte[] _mps_do_value = new byte[10];
            byte[] _mps_do_status = new byte[2];
            _encoder.GetBytes("mpsdo").CopyTo(_mps_do_code, 0);
            _encoder.GetBytes(mps_do_value).CopyTo(_mps_do_value, 0);
            _encoder.GetBytes(mps_do_status).CopyTo(_mps_do_status, 0);
            byte[] _mps_do = _mps_do_code.Concat(_mps_do_value).Concat(_mps_do_status).ToArray();

            String mps_orp_value = ConvertStr(objMeasuredDataGlobal.var5.ToString(), 10);
            String mps_orp_status = ConvertStr(objMeasuredDataGlobal.var5_status.ToString(), 2);
            byte[] _mps_orp_code = new byte[5];
            byte[] _mps_orp_value = new byte[10];
            byte[] _mps_orp_status = new byte[2];
            _encoder.GetBytes("mporp").CopyTo(_mps_orp_code, 0);
            _encoder.GetBytes(mps_orp_value).CopyTo(_mps_orp_value, 0);
            _encoder.GetBytes(mps_orp_status).CopyTo(_mps_orp_status, 0);
            byte[] _mps_orp = _mps_orp_code.Concat(_mps_orp_value).Concat(_mps_orp_status).ToArray();

            String mps_temp_value = ConvertStr(objMeasuredDataGlobal.var6.ToString(), 10);
            String mps_temp_status = ConvertStr(objMeasuredDataGlobal.var6_status.ToString(), 2);
            byte[] _mps_temp_code = new byte[5];
            byte[] _mps_temp_value = new byte[10];
            byte[] _mps_temp_status = new byte[2];
            _encoder.GetBytes("mtemp").CopyTo(_mps_temp_code, 0);
            _encoder.GetBytes(mps_temp_value).CopyTo(_mps_temp_value, 0);
            _encoder.GetBytes(mps_temp_status).CopyTo(_mps_temp_status, 0);
            byte[] _mps_temp = _mps_temp_code.Concat(_mps_temp_value).Concat(_mps_temp_status).ToArray();

            String mps_turbi_value = ConvertStr(objMeasuredDataGlobal.var4.ToString(), 10);
            String mps_turbi_status = ConvertStr(objMeasuredDataGlobal.var4_status.ToString(), 10);
            byte[] _mps_turbi_code = new byte[5];
            byte[] _mps_turbi_value = new byte[10];
            byte[] _mps_turbi_status = new byte[2];
            _encoder.GetBytes("turbi").CopyTo(_mps_turbi_code, 0);
            _encoder.GetBytes(mps_turbi_value).CopyTo(_mps_turbi_value, 0);
            _encoder.GetBytes(mps_turbi_status).CopyTo(_mps_turbi_status, 0);
            byte[] _mps_turbi = _mps_turbi_code.Concat(_mps_turbi_value).Concat(_mps_turbi_status).ToArray();
        
            #endregion item
            //item =      "mpsph" + mps_ph + mps_ph_status +
            //            "mpsec" + mps_ec + mps_ec_status +
            //            "mpsdo" + mps_do + mps_do_status +
            //            "turbi" + mps_turbidity + mps_turbidity_status +
            //            "mporp" + mps_orp + mps_orp_status +
            //            "mtemp" + mps_temp + mps_temp_status +
            //            "clntn" + tn + tn_status +
            //            "clntp" + tp + tp_status +
            //            "cltoc" + toc + toc_status +
            //            "power" + module_Power + equiment_staus +
            //            "mdups" + module_UP + equiment_staus +
            //            "mdoor" + module_Door + equiment_staus +
            //            "mfire" + module_Fire + equiment_staus +
            //            "mflow" + module_Flow + equiment_staus +
            //            "pplam" + module_PumpLAM + equiment_staus +
            //            "pplrs" + module_PumpLRS + equiment_staus +
            //            "plflt" + module_PumpLFLT + equiment_staus +
            //            "ppram" + module_PumpRAM + equiment_staus +
            //            "pprrs" + module_PumpRRS + equiment_staus +
            //            "prflt" + module_PumpRFLT + equiment_staus +
            //            "mdtem" + module_Temperature + equiment_staus +
            //            "mdhum" + module_Humidity + equiment_staus;
            byte[] _item = _mps_ph.Concat(_mps_ec).Concat(_mps_do).Concat(_mps_turbi).ToArray();
            _item = _encoder.GetBytes("22").Concat(_item).ToArray();
            String checksum = CalculateChecksum(_encoder.GetString(_header) + _encoder.GetString(_item) + _encoder.GetString(_ETX));
            byte[] _checksum = new byte[2];
            _encoder.GetBytes(checksum).CopyTo(_checksum, 0);
            byte[] _CR = new byte[1];
            (new byte[] { 0x0D }).CopyTo(_CR, 0);
            byte[] _tailer = _ETX.Concat(_checksum).Concat(_CR).ToArray();
            byte[] _sender = _header.Concat(_item).Concat(_tailer).ToArray();
            sendByte(nwStream, _sender);
            byte[] _EOT = new byte[1];
            (new byte[] { 0x04 }).CopyTo(_EOT, 0);
            buffer = new byte[BUFFER_SIZE];
            if (nwStream.Read(buffer, 0, buffer.Length) != 0 && Timeout(client))
            {
                if (buffer[0] == 6 && Timeout(client))
                {
                    sendByte(nwStream, _EOT);
                    //sendMsg(nwStream, EOT1);
                    //Console.WriteLine("Sended: {0}", EOT1);
                }
                else
                {
                    sendByte(nwStream, _encoder.GetBytes("ERROR FORMAT OR TIMEOUT >60s"));
                    //sendMsg(nwStream, "ERROR FORMAT OR TIMEOUT >60s");
                    //Console.WriteLine("Sended: {0}", "ER");
                }
            }
            else
            {
                sendByte(nwStream, _encoder.GetBytes("ERROR FORMAT OR TIMEOUT >60s"));
                //sendMsg(nwStream, "ERROR FORMAT OR TIMEOUT >60s");
            }
        }
        public static void DUMP(byte[] buffer, String data, int j, NetworkStream nwStream, TcpClient client)
        {
            List<byte[]> _dataarray = new List<byte[]>();
            string _param = "";
            if (_encoder.GetString(buffer.SubArray(j + 19, 1)).Equals("M"))
            {
                String date1 = _DateDeFormat(data.Substring(j + 20, 14));
                String date2 = _DateDeFormat(data.Substring(j + 34, 14));
                // Lay danh sach du lieu gui di
                _dataarray = DataDUMP(date1, date2, "data_5minute_values", "databinding");
                _param = "M";
            }
            else
                if (_encoder.GetString(buffer.SubArray(j + 19, 1)).Equals("H"))
            {
                String date1 = _DateDeFormat(data.Substring(j + 20, 14));
                String date2 = _DateDeFormat(data.Substring(j + 34, 14));
                // Lay danh sach du lieu gui di
                _dataarray = DataDUMP(date1, date2, "data_60minute_values", "databinding");
                _param = "H";
            }
            else
            {
                sendByte(nwStream, _encoder.GetBytes("ERROR : " + "\" " + "DUMP" + "\" " + "COMMAND"));
            }
            Boolean hasData = true;
            if (_dataarray.Count > 0)
            {
                for (int i = 0; i < _dataarray.Count; i++)
                {
                    byte[] _STX = new byte[1];
                    (new byte[] { 0x02 }).CopyTo(_STX, 0);
                    byte[] _Command = new byte[4];
                    _encoder.GetBytes("DUMP").CopyTo(_Command, 0);
                    byte[] _Param = new byte[1];
                    _encoder.GetBytes(_param).CopyTo(_Param, 0);
                    byte[] _streamCode = new byte[11];
                    _encoder.GetBytes(data.Substring(1, j)).CopyTo(_streamCode, 0);
                    byte[] _dcd = new byte[1];
                    _encoder.GetBytes("1").CopyTo(_dcd, 0);
                    byte[] _header = _STX.Concat(_Command).Concat(_Param).Concat(_streamCode).Concat(_dcd).ToArray();
                    byte[] _ETX = new byte[1];
                    (new byte[] { 0x03 }).CopyTo(_ETX, 0);
                    String checksum = CalculateChecksum(_encoder.GetString(_header) + _encoder.GetString(_dataarray[i]) + _encoder.GetString(_ETX));
                    byte[] _checksum = new byte[2];
                    _encoder.GetBytes(checksum).CopyTo(_checksum, 0);
                    byte[] _CR = new byte[1];
                    (new byte[] { 0x0D }).CopyTo(_CR, 0);
                    byte[] _tailer = _ETX.Concat(_checksum).Concat(_CR).ToArray();
                    byte[] _sender = _header.Concat(_dataarray[i]).Concat(_tailer).ToArray();
                    //List cac lenh truyen du lieu
                    _dataarray[i] = _sender;
                }

            }
            else
            {
                hasData = false;
            }
            byte[] _EOT = new byte[1];
            new byte[] { 0x04 }.CopyTo(_EOT, 0);
            byte[] _NAK = new byte[1];
            new byte[] { 0x15 }.CopyTo(_NAK, 0);
            int a = 0;
            int b = 0;
            //sendMsg(nwStream, dataarray[a]);

            //Dem du lieu gui di
            int ct = 0;
            int ctend = _dataarray.Count - 1;
            //a++;
            if (hasData == true) //co data khi goi DUMP
            {
                do
                {
                    if (b == 3)
                    {
                        sendByte(nwStream, _EOT);
                        break;
                    }
                    if (ctend < 0)
                    {
                        sendByte(nwStream, _NAK);
                        break;
                    }
                    buffer = new byte[BUFFER_SIZE];
                    sendByte(nwStream, _dataarray[ct]);

                    if (nwStream.Read(buffer, 0, buffer.Length) != 0)
                    {
                        if (buffer[0] == 6 && ct == ctend)  //ACK - Va het du lieu truyen
                        {
                            b = 0;
                            sendByte(nwStream, _EOT);
                            a = 0;
                            break;
                        }
                        else if (buffer[0] == 6 && ct < ctend) //Neu gui thanh cong tang bien công
                        {
                            ct++;
                            b = 0;
                        }
                        else if (buffer[0] == 15)  //NAK
                        {
                            b++;
                            sendByte(nwStream, _dataarray[ct]);
                        }
                        else
                        {
                            sendByte(nwStream, _encoder.GetBytes("ERROR FORMAT "));
                            break;
                        }
                    }
                    else
                    {
                        //sendMsg(nwStream, "ERROR FORMAT OR TIMEOUT >60s");
                        sendByte(nwStream, _encoder.GetBytes("ERROR FORMAT"));
                        break;
                    }
                    //Bien dem so lenh truyen di
                } while (true);
            }
            else
            {
                sendByte(nwStream, _EOT);   //ko co data khi goi DUMP
                //break;
            }
        }
        public static void CCHK(NetworkStream nwStream)
        {

            byte[] _EOT = new byte[1];
            new byte[] { 0x04 }.CopyTo(_EOT, 0);
            sendByte(nwStream, _EOT);
        }
        public static void RCHK(SerialPort serialPortTN, SerialPort serialPortTP, SerialPort serialPortTOC, byte[] buffer, String data, int j, NetworkStream nwStream, TcpClient client)
        {

        }
        public static void requestInforSAMPLER(SerialPort com)
        {
            try
            {
                if (com.IsOpen)
                {
                    byte[] packet = new byte[11];
                    //Fill to packet
                    packet[0] = 0x02; // STX

                    packet[1] = 0x53; // 'S'
                    packet[2] = 0x41; // 'A'
                    packet[3] = 0x4D; // 'M'
                    packet[4] = 0x50; // 'P'
                    packet[5] = 0x31; // 1
                    packet[6] = 0x30; // 0
                    packet[7] = 0x03; // ETX
                    packet[8] = 0x39;
                    packet[9] = 0x37;
                    packet[10] = 0x0D;//
                    com.Write(packet, 0, 11);
                }
            }
            catch
            {

            }

        }
        public static void requestAutoSAMPLER(SerialPort com)
        {
            try
            {
                if (com.IsOpen)
                {
                    byte[] packet = new byte[11];
                    //Fill to packet
                    packet[0] = 0x02; // STX

                    packet[1] = 0x53; // 'S'
                    packet[2] = 0x41; // 'A'
                    packet[3] = 0x4D; // 'M'
                    packet[4] = 0x50; // 'P'
                    packet[5] = 0x30; // 0
                    packet[6] = 0x30; // 0
                    packet[7] = 0x03; // ETX
                    packet[8] = 0x39; // '9'
                    packet[9] = 0x36; // '6'
                    packet[10] = 0x0D;//
                    com.Write(packet, 0, 11);
                }
            }
            catch
            {

            }

        }
        public static void SAMP(SerialPort serialPortSAMP, byte[] buffer, String data, int j, NetworkStream nwStream, TcpClient client)
        {

            if (_encoder.GetString(buffer.SubArray(j + 26, 2)).Equals("00"))
            {

                requestAutoSAMPLER(serialPortSAMP);
                Thread.Sleep(300);
                requestInforSAMPLER(serialPortSAMP);
                byte[] _EOT = new byte[1];
                new byte[] { 0x04 }.CopyTo(_EOT, 0);
                sendByte(nwStream, _EOT);
            }
            else if (_encoder.GetString(buffer.SubArray(j + 26, 2)).Equals("10"))
            {
                byte[] _STX = new byte[1];
                (new byte[] { 0x02 }).CopyTo(_STX, 0);
                //String header = STX + "SAMP" + data.Substring(1, j) + new user_repository().DateFormat(DateTime.Now.ToString()) + data.Substring(j + 19, 7);
                byte[] _Command = new byte[4];
                _encoder.GetBytes("SAMP").CopyTo(_Command, 0);
                byte[] _StationCode = new byte[11];
                _encoder.GetBytes(data.Substring(1, j)).CopyTo(_StationCode, 0);
                byte[] _Datetime = new byte[14];
                _encoder.GetBytes(DateFormat(DateTime.Now.ToString())).CopyTo(_Datetime, 0);
                byte[] _Analyze = new byte[7];
                _encoder.GetBytes(data.Substring(j + 19, 7)).CopyTo(_Analyze, 0);
                byte[] _header = _STX.Concat(_Command).Concat(_StationCode).Concat(_Datetime).Concat(_Analyze).ToArray();
                byte[] _item = new byte[65];
                byte[] _ETX = new byte[1];
                (new byte[] { 0x03 }).CopyTo(_ETX, 0);
                string checksum = CalculateChecksum(_encoder.GetString(_header) + _encoder.GetString(_item) + _encoder.GetString(_ETX));
                byte[] _checksum = new byte[2];
                _encoder.GetBytes(checksum).CopyTo(_checksum, 0);
                byte[] _CR = new byte[1];
                (new byte[] { 0x0D }).CopyTo(_CR, 0);
                byte[] _tailer = _ETX.Concat(_checksum).Concat(_CR).ToArray();
                byte[] _EOT = new byte[1];
                (new byte[] { 0x04 }).CopyTo(_EOT, 0);
                byte[] _sender = _header.Concat(_item).Concat(_tailer).ToArray();
                sendByte(nwStream, _sender);
            }
            else
            {
                sendByte(nwStream, _encoder.GetBytes("ERROR : " + "\" " + "SAMP" + "\" " + "COMMAND"));
            }
        }
        public static void INFO(byte[] buffer, String data, int j, NetworkStream nwStream, TcpClient client)
        {
            byte[] _STX = new byte[1];
            (new byte[] { 0x02 }).CopyTo(_STX, 0);
            byte[] _Command = new byte[4];
            _encoder.GetBytes("INFO").CopyTo(_Command, 0); ;
            byte[] _StationCode = new byte[11];
            _encoder.GetBytes(data.Substring(1, j)).CopyTo(_StationCode, 0); ;
            byte[] _Datetime = new byte[14];
            _encoder.GetBytes(DateFormat(DateTime.Now.ToString())).CopyTo(_Datetime, 0); ;
            byte[] _Analyze = new byte[7];
            _encoder.GetBytes(data.Substring(j + 19, 7)).CopyTo(_Analyze, 0); ;
            byte[] _header = _STX.Concat(_Command).Concat(_StationCode).Concat(_Datetime).Concat(_Analyze).ToArray();
            byte[] _itemcode = new byte[5];
            byte[] _addinfo = new byte[50];
            byte[] _item = _itemcode.Concat(_addinfo).ToArray();
            byte[] _ETX = new byte[1];
            (new byte[] { 0x03 }).CopyTo(_ETX, 0);
            string checksum = CalculateChecksum(_encoder.GetString(_header) + _encoder.GetString(_item) + _encoder.GetString(_ETX));
            byte[] _checksum = new byte[1];
            _encoder.GetBytes(checksum).CopyTo(_checksum, 0);
            byte[] _CR = new byte[1];
            (new byte[] { 0x0D }).CopyTo(_CR, 0);
            byte[] _tailer = _ETX.Concat(_checksum).Concat(_CR).ToArray();
            byte[] _EOT = new byte[1];
            (new byte[] { 0x04 }).CopyTo(_EOT, 0);
            byte[] _sender = _header.Concat(_item).Concat(_tailer).ToArray();
            sendByte(nwStream, _sender);
            buffer = new byte[BUFFER_SIZE];
            if (nwStream.Read(buffer, 0, buffer.Length) != 0 && Timeout(client))
            {
                if (buffer[0] == 6 && Timeout(client))
                {
                    sendByte(nwStream, _EOT);
                }
                else
                {
                    sendByte(nwStream, _encoder.GetBytes("ERROR FORMAT OR TIMEOUT >60s"));
                }
            }
            else
            {
                sendByte(nwStream, _encoder.GetBytes("ERROR FORMAT OR TIMEOUT >60s"));
            }
        }
        public static void RSET(NetworkStream nwStream)
        {
            //Process[] GetPArry = Process.GetProcesses();
            //foreach (Process testProcess in GetPArry)
            //{
            //    string ProcessName = testProcess.ProcessName;
            //    //ProcessName = ProcessName.ToLower();
            //    if (ProcessName.CompareTo("DataLogger") == 0)
            //    {
            //        string fullPath = testProcess.MainModule.FileName;
            //        testProcess.Kill();
            //        Process.Start(fullPath);
            //    }
            //}
            //String EOT = "\x04";
            byte[] _EOT = new byte[1];
            (new byte[] { 0x04 }).CopyTo(_EOT, 0);
            sendByte(nwStream, _EOT);
            String applicationName = "DataLogger";
            int pid = 0;
            // Wait for the process to terminate
            Process process = null;
            try
            {
                process = Process.GetProcessById(pid);
                process.WaitForExit(1000);
            }
            catch (ArgumentException ex)
            {
                // ArgumentException to indicate that the 
                // process doesn't exist?   LAME!!
            }
            Process.Start(applicationName, "");
        }
        public static void SETP(String data, int j, NetworkStream nwStream, String username)
        {
            //String EOT = "\x04";
            byte[] _EOT = new byte[1];
            (new byte[] { 0x04 }).CopyTo(_EOT, 0);
            String newpass = data.Substring(j + 19, 10).TrimStart(new Char[] { '*' });
            UpdateData(username, newpass);
            //sendMsg(nwStream, EOT);
            sendByte(nwStream, _EOT);
        }
        public static void SETT(String data, int j, NetworkStream nwStream)
        {
            String date1 = data.Substring(j + 19, 14);
            String date = DateFormat(DateTime.Now.ToString());
            SYSTEMTIME st = new SYSTEMTIME
            {
                wYear = Convert.ToInt16(date1.Substring(4, 4)),
                wMonth = Convert.ToInt16(date1.Substring(0, 2)),
                wDay = Convert.ToInt16(date1.Substring(2, 2)),
                wHour = Convert.ToInt16(date1.Substring(8, 2)),
                wMinute = Convert.ToInt16(date1.Substring(10, 2)),
                wSecond = Convert.ToInt16(date1.Substring(12, 2))
            };
            if (SetSystemTime(ref st))
            {
                byte[] _EOT = new byte[1];
                (new byte[] { 0x04 }).CopyTo(_EOT, 0);
                sendByte(nwStream, _EOT);
            }
            else
            {
                byte[] _NAK = new byte[1];
                (new byte[] { 0x15 }).CopyTo(_NAK, 0);
                sendByte(nwStream, _NAK);
            }
        }
        public static void init(SerialPort serialPortTN, SerialPort serialPortTP, SerialPort serialPortTOC, SerialPort serialPortSAMP, NetworkStream nwStream, byte[] buffer, String data, TcpClient client, String username, measured_data objMeasuredDataGlobal)
        {
            int i;
            try
            {
                //buffer = new byte[BUFFER_SIZE];
                while
                ((i = nwStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    Console.WriteLine(" Listening COMMAND ...");
                    try
                    {
                        int j = 10;
                        data = System.Text.Encoding.ASCII.GetString(buffer, 0, i);
                        Console.WriteLine(" Received: {0}", data);
                        int count = 1;
                        int choice = 0;

                        if (_encoder.GetString(buffer.SubArray(j + 15, 4)).Equals("DATA"))
                        {
                            DATA(buffer, data, j, nwStream, client);
                            break;
                        }
                        else
                            if (_encoder.GetString(buffer.SubArray(j + 15, 4)).Equals("RDAT"))
                        {
                            RDAT(buffer, data, j, nwStream, client, objMeasuredDataGlobal);
                            break;
                        }
                        else
                                if (_encoder.GetString(buffer.SubArray(j + 15, 4)).Equals("DUMP"))
                        {
                            int countNAK = 0;
                            DUMP(buffer, data, j, nwStream, client);
                            break;
                        }
                        else
                                    if (_encoder.GetString(buffer.SubArray(j + 15, 4)).Equals("CCHK"))
                        {
                            //DataLogger.frmNewMain.requestAutoSAMPLER(serialPortSAMP);
                            String EOT = "\x04";
                            CCHK(nwStream);
                            //sendMsg(nwStream, EOT);
                            break;
                        }
                        else
                                        if (_encoder.GetString(buffer.SubArray(j + 15, 4)).Equals("RCHK"))
                        {
                            RCHK(serialPortTN, serialPortTP, serialPortTOC, buffer, data, j, nwStream, client);
                            break;
                        }
                        else
                                            if (_encoder.GetString(buffer.SubArray(j + 15, 4)).Equals("SAMP"))
                        {
                            SAMP(serialPortSAMP, buffer, data, j, nwStream, client);
                            /*
                            if (data.Substring(j + 26, 2).Equals("00"))
                            {
                                DataLogger.frmNewMain.doSAMP();
                                //String bottle_position = DataLogger.frmNewMain.objWaterSamplerGLobal.bottle_position.ToString();
                                //String status_info = DataLogger.frmNewMain.objWaterSamplerGLobal.door_status.ToString();
                                //String temp = DataLogger.frmNewMain.objWaterSamplerGLobal.refrigeration_Temperature.ToString();
                                //String type = "1";
                                String EOT = "\x04";
                                sendMsg(nwStream, EOT);
                                //Console.WriteLine("Sended: {0}", EOT);
                                break;
                            }
                            else if (data.Substring(j + 26, 2).Equals("10"))
                            {
                                String STX = "\x02";
                                String header = STX + "SAMP" + data.Substring(1, j) + DateFormat(DateTime.Now.ToString()) + data.Substring(j + 19, 7);
                                String bottle_position = DataLogger.frmNewMain.objWaterSamplerGLobal.bottle_position.ToString();
                                String status_info = DataLogger.frmNewMain.objWaterSamplerGLobal.door_status.ToString();
                                String temp = DataLogger.frmNewMain.objWaterSamplerGLobal.refrigeration_Temperature.ToString();
                                String type = "1";
                                String pad = new String('*', 50);
                                String item = temp + type + status_info + bottle_position + pad;
                                String ETX = "\x03";
                                string checksum = CalculateChecksum(header + item + ETX);
                                String tailer = ETX + checksum + "\x0D";

                                String EOT = "\x04";
                                String sender = header + item + tailer;
                                sendMsg(nwStream, sender);
                                //Console.WriteLine("Sended: {0}", sender);

                            }
                            else
                            {

                                sendMsg(nwStream, "ERROR : " + "\" " + "SAMPLE " + "\" " + "COMMAND");
                                //Console.WriteLine("Sended: {0}", "ERROR : " + "\" " + "SAMPLE " + "\" " + "COMMAND");
                                break;
                            }
                            */
                            break;
                        }
                        else
                                                if (_encoder.GetString(buffer.SubArray(j + 15, 4)).Equals("INFO"))
                        {
                            INFO(buffer, data, j, nwStream, client);
                            break;
                        }
                        else
                                                    if (_encoder.GetString(buffer.SubArray(j + 15, 4)).Equals("RSET"))
                        {
                            RSET(nwStream);
                            //sendMsg(nwStream, EOT);
                            //Console.WriteLine("Sended: {0}", EOT);
                            break;
                        }
                        else
                                                        if (_encoder.GetString(buffer.SubArray(j + 15, 4)).Equals("SETP"))
                        {
                            SETP(data, j, nwStream, username);
                            break;
                        }
                        else
                                                            if (_encoder.GetString(buffer.SubArray(j + 15, 4)).Equals("SETT"))
                        {
                            SETT(data, j, nwStream);
                            break;
                        }
                    }
                    catch
                    {
                        sendByte(nwStream, _encoder.GetBytes("ERROR : FORMAT MESSAGE"));
                        //sendMsg(nwStream, "ERROR : FORMAT MESSAGE");
                        break;
                        //Console.WriteLine("Sended: {0}", "ERROR : FORMAT MESSAGE");
                    }
                    //}
                    //}
                }
            }
            catch (Exception e)
            {
                sendByte(nwStream, _encoder.GetBytes("ERROR : RECV MESSAGE"));
                //Console.WriteLine("Exception: {0}", e);
                //sendMsg(nwStream, "ERROR : RECV MESSAGE");
                //Console.WriteLine("Sended: {0}", "ERROR : RECV MESSAGE");
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME st);
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
        public static void Main()
        {
            ////TcpListener tcpListener = null;
            //Int32 port = 14111;
            //IPAddress localAddr = IPAddress.Parse(GetLocalIPAddress());
            ////IPAddress localAddr = IPAddress.Parse("10.239.164.254");
            ////IPAddress localAddr = IPAddress.Parse("192.168.1.62");
            //// TcpListener server = new TcpListener(port);
            //TcpListener tcpListener = new TcpListener(localAddr, port);
            //tcpListener.Start();
            ////TcpListener tcpListener = new TcpListener(port);
            ////tcpListener.Server.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName)27, false);
            //tcpListener.Start();

            ////Protocol(serialPortTN, serialPortTP, serialPortTOC, serialPortSAMP, tcpListener, objRelayIOControlGlobal, objStationStatusGlobal, objWaterSamplerGLobal, objMeasuredDataGlobal);
            string data = "BLVTRS0001DUMPM2015100100000020161122000000";
            int j = 11;
            List<byte[]> _dataarray = new List<byte[]>();
            string _param = "";
                String date1 = _DateDeFormat("20170226000000");
                String date2 = _DateDeFormat("20170228000000");
                // Lay danh sach du lieu gui di
                _dataarray = DataDUMP(date1, date2, "data_5minute_values", "databinding");
                _param = "M";
            Boolean hasData = true;
            if (_dataarray.Count > 0)
            {
                for (int i = 0; i < _dataarray.Count; i++)
                {
                    byte[] _STX = new byte[1];
                    (new byte[] { 0x02 }).CopyTo(_STX, 0);
                    byte[] _Command = new byte[4];
                    _encoder.GetBytes("DUMP").CopyTo(_Command, 0);
                    byte[] _Param = new byte[1];
                    _encoder.GetBytes(_param).CopyTo(_Param, 0);
                    byte[] _streamCode = new byte[11];
                    _encoder.GetBytes(data.Substring(1, j)).CopyTo(_streamCode, 0);
                    byte[] _dcd = new byte[1];
                    _encoder.GetBytes("1").CopyTo(_dcd, 0);
                    byte[] _header = _STX.Concat(_Command).Concat(_Param).Concat(_streamCode).Concat(_dcd).ToArray();
                    byte[] _ETX = new byte[1];
                    (new byte[] { 0x03 }).CopyTo(_ETX, 0);
                    String checksum = CalculateChecksum(_encoder.GetString(_header) + _encoder.GetString(_dataarray[i]) + _encoder.GetString(_ETX));
                    byte[] _checksum = new byte[2];
                    _encoder.GetBytes(checksum).CopyTo(_checksum, 0);
                    byte[] _CR = new byte[1];
                    (new byte[] { 0x0D }).CopyTo(_CR, 0);
                    byte[] _tailer = _ETX.Concat(_checksum).Concat(_CR).ToArray();
                    byte[] _sender = _header.Concat(_dataarray[i]).Concat(_tailer).ToArray();
                    //List cac lenh truyen du lieu
                    _dataarray[i] = _sender;
                }

            }
            else
            {
                hasData = false;
            }
            byte[] _EOT = new byte[1];
            new byte[] { 0x04 }.CopyTo(_EOT, 0);
            byte[] _NAK = new byte[1];
            new byte[] { 0x15 }.CopyTo(_NAK, 0);
            int a = 0;
            int b = 0;
            //sendMsg(nwStream, dataarray[a]);

            //Dem du lieu gui di
            int ct = 0;
            int ctend = _dataarray.Count - 1;
            //a++;
        }
        //public static void Protocol()
        public static void Protocol(SerialPort serialPortTN, SerialPort serialPortTP, SerialPort serialPortTOC, SerialPort serialPortSAMP, TcpListener tcpListener, measured_data objMeasuredDataGlobal)
        //public static void Protocol()
        {
            //TcpListener server = null;
            try
            {
                String data = null;
                TcpClient client = new TcpClient(AddressFamily.InterNetworkV6);
                client.Client.DualMode = true;
                Console.WriteLine(" >> " + "Server Started");
                counter = 0;
                //server.Start();
                while (true)
                {
                    counter += 1;
                    //Console.WriteLine(" >> " + "Waiting connect client ...");
                    //---incoming client connected---
                    Console.WriteLine(" >> " + "11111");
                    client = tcpListener.AcceptTcpClient();
                    Console.WriteLine(" >> " + client.Connected);
                    Console.WriteLine(" >> " + "Client IP:" + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString() + " started!");
                    //Console.WriteLine(" >> " + "Client IP:" + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
                    //handleClinet handleClinet = new handleClinet();
                    //NetworkStream nwStream = client.GetStream();
                    //startClient(client, Convert.ToString(counter), data, _privateKey, _publicKey, _encoder, serialPortTN, serialPortTP, serialPortTOC, serialPortSAMP, objRelayIOControlGlobal, objStationStatusGlobal, objWaterSamplerGLobal, objMeasuredDataGlobal);
                    newThread = new Thread(() => doChat(client, data, _encoder, serialPortTN, serialPortTP, serialPortTOC, serialPortSAMP, objMeasuredDataGlobal));
                    //Console.WriteLine(" >> " + "Listening ...");
                    newThread.Start();
                    //client.Close();
                    //newThread.Abort();
                    Thread.Sleep(1000);
                }
                client.Close();
                tcpListener.Stop();
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                tcpListener.Stop();
            }
            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
        public static void doChat(TcpClient client, String data, ASCIIEncoding _encoder, SerialPort serialPortTN, SerialPort serialPortTP, SerialPort serialPortTOC, SerialPort serialPortSAMP,measured_data objMeasuredDataGlobal)
        {
            int requestCount = 0;
            //byte[] buffer = new byte[BUFFER_SIZE];
            string _privateKey;
            string _publicKey;
            requestCount = 0;
            
            while (true)
            {
                try
                {
                    Console.WriteLine(" >> " + "Listening ...");
                    requestCount = requestCount + 1;
                    //---get the incoming data through a network stream--- 
                    if (!client.Connected) break;
                    NetworkStream nwStream = client.GetStream();
                    //client.ReceiveTimeout = 60000;
                    byte[] buffer = new byte[BUFFER_SIZE];
                    byte[] sender = null;
                    //Translate data bytes to a ASCII string.
                    //buffer = new byte[BUFFER_SIZE];
                    //Console.WriteLine(nwStream.);
                    //int i = 0;
                    Console.WriteLine(client.Connected);
                    int i = nwStream.Read(buffer, 0, buffer.Length);   
                    // bug o dong 1224, nguyen nhan co the do client close truoc khi lay dc network strem tu client              
                    if (i != 0)
                    {
                        data = null;
                        data = Encoding.ASCII.GetString(buffer, 0, i);
                        int flag = 0;
                        String stationid = "BLVTRS0001";
                        string[] separators = { "\\" };
                        try
                        {
                            String[] datas = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                            String username = datas[1];
                            var rsa = new RSACryptoServiceProvider();
                            _privateKey = rsa.ToXmlString(true);
                            _publicKey = rsa.ToXmlString(false);
                            //Console.WriteLine(randomnumber.ToString());   
                            //foreach (var item in buffer)
                            //{ Console.Write("{0}", item + " "); }
                            Console.WriteLine("\n Received: {0}", data);
                            //Console.WriteLine("Length: {0}", data.Length);
                            //buffer.SubArray();
                            if ((buffer[0] == 5 && datas[2].Remove(datas[2].Length - 1, 1).Equals(stationid)) || (buffer[0] == 5 && datas[2].Remove(datas[2].Length - 2, 2).Equals(stationid)) || (buffer[0] == 5 && datas[2].Equals(stationid)))
                            {
                                //String msg = "\x06";
                                byte[] msg = new byte[] { 0x06 };
                                //msg = msg +"12345";
                                byte[] _publicKey_ = _encoder.GetBytes(_publicKey);
                                sender = msg.Concat(_publicKey_).ToArray();
                                MyTcpListener.sendByte(nwStream, sender);
                                //Console.WriteLine("PUBLIC KEY :" + _publicKey);
                                //Console.WriteLine("ENCRYPT BYTE :" + MyTcpListener.Encrypt("123456", _publicKey, rsa));
                                //Console.WriteLine("\n\n\n" + Encrypt("123456", _publicKey, rsa));
                                buffer = new byte[BUFFER_SIZE];
                                try
                                {
                                    //buffer = new byte[BUFFER_SIZE];
                                    i = nwStream.Read(buffer, 0, buffer.Length);
                                    if (i != 0 && MyTcpListener.Timeout(client))
                                    {
                                        data = System.Text.Encoding.ASCII.GetString(buffer, 0, i);
                                        //Console.WriteLine("Received ENCRYPT : {0}", data);
                                        //sendMsg(nwStream, _privateKey);
                                        byte[] encryptbyte = _encoder.GetBytes(data);
                                        var encryptedByteArray = Convert.FromBase64String(data);
                                        var length = encryptedByteArray.Count();
                                        var item = 0;
                                        var sb = new StringBuilder();
                                        foreach (var x in encryptedByteArray)
                                        {
                                            item++;
                                            sb.Append(x);
                                            if (item < length)
                                                sb.Append(",");
                                        }

                                        String encryptstring = sb.ToString();
                                        //Console.WriteLine("Received ENCRYPT BYTE : {0}", encryptstring);
                                        //if (new user_repository().Auth(username, encryptstring, _privateKey, _publicKey, rsa))
                                        if (true)
                                        {
                                            byte[] msg1 = new byte[] { 0x06 };
                                            MyTcpListener.sendByte(nwStream, msg1);
                                            MyTcpListener.init(serialPortTN, serialPortTP, serialPortTOC, serialPortSAMP, nwStream, buffer, data, client, username, objMeasuredDataGlobal);
                                        }
                                        else
                                        {
                                            MyTcpListener.sendByte(nwStream, new byte[] { 0x15 });
                                        }
                                    }
                                    else
                                    {
                                        MyTcpListener.sendByte(nwStream, _encoder.GetBytes("TIME OUT"));
                                    }
                                }
                                catch
                                {
                                    MyTcpListener.sendByte(nwStream, _encoder.GetBytes("ERROR : CONNECTION ERROR"));
                                }
                            }
                            else
                            {
                                MyTcpListener.sendByte(nwStream, _encoder.GetBytes("ERROR : FORMAT MESSAGE"));
                            }
                        }
                        catch
                        {
                            MyTcpListener.sendByte(nwStream, _encoder.GetBytes("ERROR : FORMAT MESSAGE"));
                        }
                    }
                    else
                    {
                        break;
                        //MyTcpListener.sendByte(nwStream, _encoder.GetBytes("ERROR : NO INPUT MESSAGE"));
                    }
                    nwStream.Close();
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("ERROR : CAN NOT LISTEN FROM IP :" + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString() + " CHECK CONNECT IN CENTER.");
                    
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                    Console.WriteLine(ex.HelpLink);
                    Console.WriteLine(ex.HResult);
                    Console.WriteLine(ex.InnerException);
                    Console.WriteLine(ex.TargetSite);
                    Console.WriteLine(ex.GetType());
                    Console.WriteLine(ex.Data);
                    Console.WriteLine(ex.GetBaseException());
                    //nwStream.Close();
                    if (client == null)
                    { break; }
                    break;
                }
            }           
            //newThread.Interrupt();
            if (!newThread.Join(999999999))
            { // or an agreed resonable time
                newThread.Abort();
            }
            newThread = null;
            //client.Close();
        }
        public static void UpdateData(String username, String newpass)
        {
            try
            {
                String connstring = "Server = localhost;Port = 5432; User Id = postgres;Password = 123;Database = DataLoggerDB";
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                newpass = Crypto.HashPassword(newpass);
                string sql_command = "UPDATE auth SET password = " + "\'" + newpass + "\'" + " WHERE user_name = " + "\'" + username + "\'";
                //NpgsqlCommand cmd = new NpgsqlCommand("UPDATE auth SET password = " + "\'" + newpass + "\'" + " WHERE user_name = " + "\'" + username + "\'", conn);
                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql_command;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                //else
                //{
                //    db.close_connection();
                //}
            }
            catch (Exception ex)
            {
                string[] Error = new String[1] { "error" };
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                Console.WriteLine("\nMessage ---\n{0}", ex.StackTrace);
            }
        }
        public static string[] SplitIntoChunks(this string toSplit, int chunkSize)
        {
            int stringLength = toSplit.Length;

            int chunksRequired = (int)Math.Ceiling((decimal)stringLength / (decimal)chunkSize);
            var stringArray = new string[chunksRequired];

            int lengthRemaining = stringLength;

            for (int i = 0; i < chunksRequired; i++)
            {
                int lengthToUse = Math.Min(lengthRemaining, chunkSize);
                int startIndex = chunkSize * i;
                stringArray[i] = toSplit.Substring(startIndex, lengthToUse);

                lengthRemaining = lengthRemaining - lengthToUse;
            }

            return stringArray;
        }
        public static String DateDeFormat(string date)
        {
            date = date.Substring(0, 2) + "/" + date.Substring(2, 2) + "/" + date.Substring(4, 4) + " " + date.Substring(8, 2) + ":" + date.Substring(10, 2) + ":" + date.Substring(12, 2);
            //Console.WriteLine(date);
            DateTime dateValue = DateTime.ParseExact(date, "MM/dd/yyyy HH:mm:ss", new CultureInfo("en-US"), DateTimeStyles.None);

            return date;
        }
        public static String _DateDeFormat(string date)
        {
            //date = date.Substring(0, 2) + "/" + date.Substring(2, 2) + "/" + date.Substring(4, 4) + " " + date.Substring(8, 2) + ":" + date.Substring(10, 2) + ":" + date.Substring(12, 2);
            //Console.WriteLine(date);
            //date = date.Substring(4, 4) + "-" + date.Substring(0, 2) + "-" + date.Substring(2, 2) + " " + date.Substring(8, 2) + ":" + date.Substring(10, 2) + ":" + date.Substring(12, 2);
            date = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2) + " " + date.Substring(8, 2) + ":" + date.Substring(10, 2) + ":" + date.Substring(12, 2);
            //DateTime dateValue = DateTime.ParseExact(date, "MM/dd/yyyy HH:mm:ss", new CultureInfo("en-US"), DateTimeStyles.None);
            //date = date.Substring(0, 2) + date.Substring(3, 2) + date.Substring(6, 4) + date.Substring(11, 2) + date.Substring(14, 2) + date.Substring(17, 2);
            return date;
        }
        public static string ConvertStr(String str, int i)
        {
            int j;
            //if (str.Length < i)
            //{
            //    j = i - str.Length;
            //    String pad = new String('*', j);
            //    str = pad + str;
            //}
            if (str.Length > i)
            {
                if (str.Contains("."))
                {
                    j = str.Length - i;
                    str = str.Substring(0, 9);
                }
            }
            return str;
        }
        //public static void Main() {
        public static String getMeasureTime(String table, String time)
        {
            String measuretime;
            //String time = "created";
            //String table = "data_5minute_values";
            try
            {
                String connstring = "Server = localhost;Port = 5432; User Id = postgres;Password = 123;Database = DataLoggerDB_ND";
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql_command = "SELECT " + time + " from " + table + " order by ID desc limit 1";
                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql_command;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    DataTable data = new DataTable();
                    // call load method of datatable to copy content of reader 
                    data.Load(dr); // Load 
                    string strvalue = "";
                    foreach (DataRow row in data.Rows)
                    {
                        strvalue = Convert.ToString(row["created"]);
                    }
                    strvalue = DateFormat(strvalue);
                    //Console.WriteLine(strvalue);
                    //Console.Read();
                    conn.Close();
                    return strvalue;
                }
                //else
                //{
                //    db.close_connection();
                //    return "ERROR";
                //}
            }
            catch (Exception ex)
            {

                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                Console.WriteLine("\nMessage ---\n{0}", ex.StackTrace);
                return "ERROR";
            }
        }
        public static String DateFormat(String date)
        {
            //yyyymmddHHmmss
            DateTime dateTime = DateTime.Parse(date);
            date = dateTime.ToString("MM/dd/yyyy HH:mm:ss");
            date = date.Substring(6, 4) + date.Substring(0, 2) + date.Substring(3, 2) + date.Substring(11, 2) + date.Substring(14, 2) + date.Substring(17, 2);

            return date;

        }
        public static byte[] mergeArrayByte(byte[] output, byte[] subarray)
        {

            int i;
            for (i = 0; i < subarray.Length; i++)
            {
                output[i] = subarray[i];
            }
            return output;
        }
        //public static void Main(){

        //public static byte[] DataDUMP(String date1, String date2, String table, String tablebinding)
        //{
        //    DateTime dateValue;
        //    //String date1 = "08052016121000";
        //    //String date2 = "08052016130000";
        //    //date1 = DateDeFormat(date1);
        //    //date2 = DateDeFormat(date2);
        //    //String table = "data_5minute_values";
        //    //String tablebinding = "databinding";
        //    //Console.WriteLine(date);
        //    //Console.Read();
        //    using (NpgsqlDBConnection db = new NpgsqlDBConnection())
        //    {
        //        try
        //        {
        //            if (db.open_connection())
        //            {
        //                //String connstring = "Server = localhost;Port = 5432; User Id = postgres;Password = 123;Database = DataLoggerDB";
        //                //NpgsqlConnection conn = new NpgsqlConnection(connstring);
        //                //conn.Open();
        //                string sql_command = "SELECT * from " + table + " WHERE created < " + "\'" + date2 + "\'" + " AND created > " + "\'" + date1 + "\'" + "ORDER BY created ASC";
        //                //NpgsqlCommand cmd = new NpgsqlCommand("SELECT * from " + table + " WHERE created < " + "\'" + date2 + "\'" + " AND created > " + "\'" + date1 + "\'" + "ORDER BY created ASC", conn);
        //                using (NpgsqlCommand cmd = db._conn.CreateCommand())
        //                {
        //                    cmd.CommandText = sql_command;
        //                    NpgsqlDataReader dr = cmd.ExecuteReader();
        //                    DataTable data = new DataTable();
        //                    // call load method of datatable to copy content of reader 
        //                    data.Load(dr); // Load data phu hop trong command DUMP

        //                    string sql_command1 = "SELECT * from " + tablebinding;
        //                    //cmd = new NpgsqlCommand("SELECT * from " + tablebinding, conn);
        //                    cmd.CommandText = sql_command1;
        //                    dr = cmd.ExecuteReader();
        //                    DataTable tbcode = new DataTable();
        //                    byte[] databyte = new byte[BUFFER_SIZE];
        //                    tbcode.Load(dr); // Load bang chua mapping cac truong

        //                    string strvalue = "";

        //                    byte[] sql = null;

        //                    byte[] clnnamevalue = new byte[10];
        //                    byte[] clnnamestatus = new byte[2];
        //                    byte[] code = new byte[5];

        //                    byte[] countitem = new byte[2];
        //                    byte[] measuretime = new byte[14];
        //                    string[] strvalues = new string[data.Rows.Count];


        //                    countitem = _encoder.GetBytes(ConvertStr(data.Rows.Count.ToString(), 2));
        //                    //sql = sql.Concat(_encoder.GetBytes(DateFormat(Convert.ToString(row1["created"])))).Concat(_encoder.GetBytes(ConvertStr(data.Rows.Count.ToString(), 2))).ToArray();
        //                    foreach (DataRow row1 in data.Rows)  // lay moi row trong data phu hop voi DUMP command
        //                    {
        //                        //Console.WriteLine("\n ID \n" + Convert.ToString(row1["id"]));
        //                        measuretime = _encoder.GetBytes(DateFormat(Convert.ToString(row1["created"])));
        //                        sql = sql.Concat(measuretime).Concat(countitem).ToArray();  //Measure time + count item
        //                        //strvalue = strvalue + DateFormat(Convert.ToString(row1["created"])) + tbcode.Rows.Count.ToString() + "\\";
        //                        foreach (DataRow row2 in tbcode.Rows)
        //                        {

        //                            //strvalue = strvalue + Convert.ToString(row2["code"]);
        //                            code = _encoder.GetBytes(Convert.ToString(row2["code"]));
        //                            //strvalue = strvalue + ConvertStr(Convert.ToString(row1[Convert.ToString(row2["clnnamevalue"])]), 10);
        //                            clnnamevalue = _encoder.GetBytes(ConvertStr(Convert.ToString(row1[Convert.ToString(row2["clnnamevalue"])]), 10));
        //                            //strvalue = strvalue + ConvertStr(Convert.ToString(row1[Convert.ToString(row2["clnnamestatus"])]), 2);
        //                            clnnamestatus = _encoder.GetBytes(ConvertStr(Convert.ToString(row1[Convert.ToString(row2["clnnamestatus"])]), 2));

        //                            sql = sql.Concat(code).Concat(clnnamevalue).Concat(clnnamestatus).ToArray();
        //                            //Console.WriteLine(strvalue);
        //                            //strvalues[count] = strvalue;
        //                            //strvalue = "";
        //                        }
        //                    }
        //                    //strvalues = SplitIntoChunks(strvalue, 17 * (tbcode.Rows.Count) + 2 + 14);  //chia thanh moi string de nhet vao array ?
        //                    //foreach (string item in strvalues) { 

        //                    //}
        //                    //Console.WriteLine(strvalues[1]);
        //                    //Console.Read();
        //                    //strvalue = ConvertStr(tbcode.Rows.Count.ToString(), 2) + strvalue;
        //                    db.close_connection();
        //                    //return strvalues;
        //                    return sql;
        //                }
        //            }
        //            else
        //            {
        //                db.close_connection();
        //                string[] Error = new String[1] { "error" };
        //                //return Error;
        //                byte[] rt = _encoder.GetBytes("ERROR");
        //                return rt;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            string[] Error = new String[1] { "error" };
        //            Console.WriteLine("\nMessage ---\n{0}", ex.Message);
        //            Console.WriteLine("\nMessage ---\n{0}", ex.StackTrace);
        //            //return Error;
        //            byte[] rt = _encoder.GetBytes("ERROR");
        //            return rt;
        //        }
        //    }
        //}

        public static List<byte[]> DataDUMP(String date1, String date2, String table, String tablebinding)
        {
            DateTime dateValue;
            List<byte[]> lstData = new List<byte[]>();
            try
            {
                String connstring = "Server = localhost;Port = 5432; User Id = postgres;Password = 123;Database = DataLoggerDB_ND";
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                //String connstring = "Server = localhost;Port = 5432; User Id = postgres;Password = 123;Database = DataLoggerDB";
                //NpgsqlConnection conn = new NpgsqlConnection(connstring);
                //conn.Open();
                string sql_command = "SELECT * from " + table + " WHERE created < " + "\'" + date2 + "\'" + " AND created > " + "\'" + date1 + "\'" + "ORDER BY created ASC";
                //NpgsqlCommand cmd = new NpgsqlCommand("SELECT * from " + table + " WHERE created < " + "\'" + date2 + "\'" + " AND created > " + "\'" + date1 + "\'" + "ORDER BY created ASC", conn);
                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql_command;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    DataTable data = new DataTable();
                    // call load method of datatable to copy content of reader 
                    data.Load(dr); // Load data phu hop trong command DUMP

                    string sql_command1 = "SELECT * from " + tablebinding;
                    //cmd = new NpgsqlCommand("SELECT * from " + tablebinding, conn);
                    cmd.CommandText = sql_command1;
                    dr = cmd.ExecuteReader();
                    DataTable tbcode = new DataTable();
                    byte[] databyte = new byte[BUFFER_SIZE];
                    tbcode.Load(dr); // Load bang chua mapping cac truong

                    string strvalue = "";
                    byte[] clnnamevalue;
                    byte[] clnnamestatus;
                    byte[] code;

                    byte[] measuretime;
                    string[] strvalues = new string[data.Rows.Count];

                    //byte[] countitem = new byte[2];
                    //_encoder.GetBytes(ConvertStr(data.Rows.Count.ToString(), 2)).CopyTo(countitem, 0);

                    //byte[] countitem1 = new byte[2];
                    //_encoder.GetBytes(ConvertStr(tbcode.Rows.Count.ToString(), 2)).CopyTo(countitem1, 0);
                    byte[] countitem1 = new byte[2];
                    byte[] countitem0 = _encoder.GetBytes(ConvertStr(tbcode.Rows.Count.ToString(), 2));
                    //byte[] sql = new byte[(5 + 10 + 2) * tbcode.Rows.Count + 2];
                    countitem1 = new byte[2];
                    countitem0.CopyTo(countitem1, 2 - countitem0.Length);
                    byte[] sql = null;

                    foreach (DataRow row1 in data.Rows)  // lay moi row trong data phu hop voi DUMP command
                    {
                        sql = null;
                        clnnamevalue = new byte[10];
                        clnnamestatus = new byte[2];
                        code = new byte[5];

                        //Console.WriteLine("\n ID \n" + Convert.ToString(row1["id"]));
                        byte[] _measuretime = _encoder.GetBytes(DateFormat(Convert.ToString(row1["created"])));
                        measuretime = new byte[14];
                        _measuretime.CopyTo(measuretime, 0);


                        if (sql == null)
                        {
                            sql = measuretime.Concat(countitem1).ToArray();  //Measure time + count item
                        }
                        else
                        {
                            sql = sql.Concat(measuretime).Concat(countitem1).ToArray();
                        }
                        //strvalue = strvalue + DateFormat(Convert.ToString(row1["created"])) + tbcode.Rows.Count.ToString() + "\\";
                        foreach (DataRow row2 in tbcode.Rows)
                        {

                            byte[] _code = _encoder.GetBytes(Convert.ToString(row2["code"]));
                            byte[] _clnnamevalue = _encoder.GetBytes(ConvertStr(Convert.ToString(row1[Convert.ToString(row2["clnnamevalue"])]), 10));
                            byte[] _clnnamestatus = _encoder.GetBytes(ConvertStr(Convert.ToString(row1[Convert.ToString(row2["clnnamestatus"])]), 2));
                            //strvalue = strvalue + Convert.ToString(row2["code"]);
                            code = new byte[5];
                            _code.CopyTo(code, 0);
                            //strvalue = strvalue + ConvertStr(Convert.ToString(row1[Convert.ToString(row2["clnnamevalue"])]), 10);
                            clnnamevalue = new byte[10];
                            _clnnamevalue.CopyTo(clnnamevalue, 10 - _clnnamevalue.Length);
                            //strvalue = strvalue + ConvertStr(Convert.ToString(row1[Convert.ToString(row2["clnnamestatus"])]), 2);
                            clnnamestatus = new byte[2];
                            _clnnamestatus.CopyTo(clnnamestatus, 2 - _clnnamestatus.Length);

                            sql = sql.Concat(code).Concat(clnnamevalue).Concat(clnnamestatus).ToArray();

                        }
                        lstData.Add(sql);
                    }

                    conn.Close();

                    return lstData;
                }
                //else
                //{
                //    db.close_connection();
                //    string[] Error = new String[1] { "error" };
                //    //return Error;
                //    byte[] rt = _encoder.GetBytes("ERROR");
                //    lstData.Add(rt);
                //    return lstData;
                //}
            }
            catch (Exception ex)
            {
                string[] Error = new String[1] { "error" };
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                Console.WriteLine("\nMessage ---\n{0}", ex.StackTrace);
                //return Error;
                byte[] rt = _encoder.GetBytes("ERROR");
                lstData.Add(rt);
                return lstData;
            }
        }
        public static byte[] DataSQL(String table, String tablebinding)
        {
            try
            {
                String connstring = "Server = localhost;Port = 5432; User Id = postgres;Password = 123;Database = DataLoggerDB_ND";
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql_command = "SELECT * from " + table + " order by ID desc limit 1";
                //String connstring = "Server = localhost;Port = 5432; User Id = postgres;Password = 123;Database = DataLoggerDB";
                //NpgsqlConnection conn = new NpgsqlConnection(connstring);
                //conn.Open();
                //NpgsqlCommand cmd = new NpgsqlCommand("SELECT * from " + table + " order by ID desc limit 1", conn);
                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql_command;
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    DataTable data = new DataTable();
                    byte[] databyte = new byte[BUFFER_SIZE];
                    // call load method of datatable to copy content of reader 
                    data.Load(dr); // Load 

                    string sql_command1 = "SELECT * from " + tablebinding;
                    cmd.CommandText = sql_command1;
                    //cmd = new NpgsqlCommand("SELECT * from " + tablebinding, conn);
                    dr = cmd.ExecuteReader();
                    DataTable tbcode = new DataTable();
                    tbcode.Load(dr);
                    string strvalue = "";
                    byte[] countitem = new byte[2];
                    byte[] _countitem = _encoder.GetBytes(ConvertStr(tbcode.Rows.Count.ToString(), 2));
                    //byte[] sql = new byte[(5 + 10 + 2) * tbcode.Rows.Count + 2];
                    countitem = new byte[2];
                    _countitem.CopyTo(countitem,2- _countitem.Length);
                    byte[] sql = countitem;
                    //sql = sql.Concat(countitem).ToArray();
                    byte[] clnnamevalue;
                    byte[] clnnamestatus;
                    byte[] code;
                    foreach (DataRow row in tbcode.Rows)
                    {
                        clnnamevalue = new byte[10];
                        clnnamestatus = new byte[2];
                        code = new byte[5];
                        byte[] _clnnamevalue = _encoder.GetBytes(ConvertStr(Convert.ToString(data.Rows[0][Convert.ToString(row["clnnamevalue"])]), 10));
                        byte[] _clnnamestatus = _encoder.GetBytes(ConvertStr(Convert.ToString(data.Rows[0][Convert.ToString(row["clnnamestatus"])]), 2));
                        byte[] _code = _encoder.GetBytes(Convert.ToString(row["code"]));
                        //strvalue = strvalue + Convert.ToString(row["code"]);
                        code = new byte[5];
                        _code.CopyTo(code, 0);
                        clnnamevalue = new byte[10];
                        _clnnamevalue.CopyTo(clnnamevalue, 10 - _clnnamevalue.Length);
                        clnnamestatus = new byte[2];
                        _clnnamestatus.CopyTo(clnnamestatus, 2 - _clnnamestatus.Length);
                        sql = sql.Concat(code).Concat(clnnamevalue).Concat(clnnamestatus).ToArray();
                    }
                    //Console.WriteLine(strvalue);
                    //Console.Read();
                    //sql = ConvertStr(tbcode.Rows.Count.ToString(), 2) + "\\" + strvalue + "\\";
                    conn.Close();
                    return sql;
                }
                //else
                //{
                //    db.close_connection();
                //    byte[] rt = _encoder.GetBytes("ERROR");
                //    return rt;
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                Console.WriteLine("\nMessage ---\n{0}", ex.StackTrace);
                //return "ERROR";
                byte[] rt = _encoder.GetBytes("ERROR");
                return rt;
            }
        }
        //}
        //public static class handleClinet
        //{
        //    static TcpClient client;
        //    static string clNo;
        //    private const int BUFFER_SIZE = 1024;
        public static void startClient(TcpClient client, string clineNo, String data, String _privateKey, String _publicKey, ASCIIEncoding _encoder, SerialPort serialPortTN, SerialPort serialPortTP, SerialPort serialPortTOC, SerialPort serialPortSAMP, measured_data objMeasuredDataGlobal)
        {
            try
            {
                //client = inClient;
                //clNo = clineNo;
                Thread newThread = new Thread(() => doChat(client, data, _encoder, serialPortTN, serialPortTP, serialPortTOC, serialPortSAMP, objMeasuredDataGlobal));
                //Console.WriteLine(" >> " + "Listening ...");
                newThread.Start();
                //Thread t = new Thread(doChat);
                //t.Start(client, data, _privateKey, _publicKey, _encoder, serialPortTN, serialPortTP, serialPortTOC, serialPortSAMP, objRelayIOControlGlobal, objStationStatusGlobal, objWaterSamplerGLobal, objMeasuredDataGlobal);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
        }
    }
}
