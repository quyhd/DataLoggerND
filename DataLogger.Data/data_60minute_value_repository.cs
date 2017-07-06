using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Npgsql;
using DataLogger.Entities;

namespace DataLogger.Data
{
    public class data_60minute_value_repository : NpgsqlDBConnection
    {
        #region Public procedure


        public int add(ref data_value obj)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    int ID = -1;

                    if (db.open_connection())
                    {
                        string sql_command = "INSERT INTO data_60minute_values (var1, var1_status, var2, var2_status, " +
                                            " var3, var3_status, var4, var4_status, " +
                                            " var5, var5_status, var6, var6_status, " +
                                            //" TN, TN_status, TP, TP_status, TOC, TOC_status, " +
                                            //" module_Power, module_UPS, module_Door, module_Fire, module_Flow, " +
                                            //" module_PumpLAM, module_PumpLRS, module_PumpLFLT, module_PumpRAM, module_PumpRRS, " +
                                            //" module_PumpRFLT, module_Temperature, module_Humidity, " +
                                            " stored_date, stored_hour, stored_minute, MPS_status, " +
                                            //"pumping_system_status, station_status, " +
                                            //" refrigeration_temperature, bottle_position, door_status, equipment_status, " +
                                            " created)" +
                                            " VALUES (:var1, :var1_status, :var2, :var2_status, " +
                                            " :var3,:var3_status, :var4, :var4_status, " +
                                            " :var5, :var5_status, :var6, :var6_status, " +
                                            //" :TN, :TN_status, :TP, :TP_status, :TOC, :TOC_status, " +
                                            //" :module_Power, :module_UPS, :module_Door, :module_Fire, :module_Flow, " +
                                            //" :module_PumpLAM,:module_PumpLRS,:module_PumpLFLT,:module_PumpRAM,:module_PumpRRS," +
                                            //" :module_PumpRFLT, :module_Temperature, :module_Humidity," +
                                            " :stored_date, :stored_hour, :stored_minute, :MPS_status, " +
                                            //":pumping_system_status, :station_status, " +
                                            //" :refrigeration_temperature, :bottle_position, :door_status, :equipment_status, " +
                                            " :created)";
                        sql_command += " RETURNING id;";

                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            cmd.Parameters.Add(":var1", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.var1;
                            cmd.Parameters.Add(":var1_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.var1_status;
                            cmd.Parameters.Add(":var2", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.var2;
                            cmd.Parameters.Add(":var2_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.var2_status;
                            cmd.Parameters.Add(":var3", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.var3;
                            cmd.Parameters.Add(":var3_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.var3_status;
                            cmd.Parameters.Add(":var4", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.var4;
                            cmd.Parameters.Add(":var4_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.var4_status;
                            cmd.Parameters.Add(":var5", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.var5;
                            cmd.Parameters.Add(":var5_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.var5_status;
                            cmd.Parameters.Add(":var6", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.var6;
                            cmd.Parameters.Add(":var6_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.var6_status;
                            //cmd.Parameters.Add(":TN", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.TN;
                            //cmd.Parameters.Add(":TN_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.TN_status;
                            //cmd.Parameters.Add(":TP", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.TP;
                            //cmd.Parameters.Add(":TP_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.TP_status;
                            //cmd.Parameters.Add(":TOC", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.TOC;
                            //cmd.Parameters.Add(":TOC_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.TOC_status;

                            //cmd.Parameters.Add(":module_Power", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_Power;
                            //cmd.Parameters.Add(":module_UPS", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_UPS;
                            //cmd.Parameters.Add(":module_Door", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_Door;
                            //cmd.Parameters.Add(":module_Fire", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_Fire;
                            //cmd.Parameters.Add(":module_Flow", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_Flow;
                            //cmd.Parameters.Add(":module_PumpLAM", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpLAM;

                            //cmd.Parameters.Add(":module_PumpLRS", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpLRS;
                            //cmd.Parameters.Add(":module_PumpLFLT", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpLFLT;
                            //cmd.Parameters.Add(":module_PumpRAM", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpRAM;
                            //cmd.Parameters.Add(":module_PumpRRS", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpRRS;
                            //cmd.Parameters.Add(":module_PumpRFLT", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpRFLT;

                            //cmd.Parameters.Add(":module_Temperature", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.module_Temperature;
                            //cmd.Parameters.Add(":module_Humidity", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.module_Humidity;

                            cmd.Parameters.Add(":stored_date", NpgsqlTypes.NpgsqlDbType.Date).Value = obj.stored_date;
                            cmd.Parameters.Add(":stored_hour", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.stored_hour;
                            cmd.Parameters.Add(":stored_minute", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.stored_minute;
                            cmd.Parameters.Add(":MPS_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.MPS_status;
                            //cmd.Parameters.Add(":pumping_system_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.pumping_system_status;
                            //cmd.Parameters.Add(":station_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.station_status;

                            //cmd.Parameters.Add(":refrigeration_temperature", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.refrigeration_temperature;
                            //cmd.Parameters.Add(":bottle_position", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.bottle_position;
                            //cmd.Parameters.Add(":door_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.door_status;
                            //cmd.Parameters.Add(":equipment_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.equipment_status;
                            cmd.Parameters.Add(":created", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = obj.created;
                            //cmd.ExecuteNonQuery();
                            ID = Convert.ToInt32(cmd.ExecuteScalar());
                            obj.id = ID;

                            db.close_connection();
                            return ID;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return -1;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return -1;
                }
                finally
                {
                    db.close_connection();
                }
            }
        }
        public int update(ref data_value obj)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {

                    if (db.open_connection())
                    {
                        string sql_command = "UPDATE data_60minute_values set  " +
                                            " var1 = :var1, var1_status =:var1_status, " +
                                            " var2 =:var2, var2_status =:var2_status,  " +
                                            " var3 =:var3, var3_status =:var3_status,  " +
                                            " var4 =:var4, var4_status =:var4_status,  " +
                                            " var5 =:var5, var5_status =:var5_status,  " +
                                            " var6 =:var6, var6_status =:var6_status,  " +
                                            //" TN =:TN, TN_status =:TN_status,  " +
                                            //" TP =:TP, TP_status =:TP_status,  " +
                                            //" TOC =:TOC, TOC_status =:TOC_status,  " +
                                            //" module_Power =:module_Power, module_UPS =:module_UPS,  " +
                                            //" module_Door =:module_Door, module_Fire =:module_Fire,  " +
                                            //" module_Flow =:module_Flow, module_PumpLAM =:module_PumpLAM,  " +
                                            //" module_PumpLRS =:module_PumpLRS, module_PumpLFLT =:module_PumpLFLT,  " +
                                            //" module_PumpRAM =:module_PumpRAM, module_PumpRRS =:module_PumpRRS,  " +

                                            //" module_PumpRFLT =:module_PumpRFLT, module_Temperature =:module_Temperature,  " +
                                            //" module_Humidity =:module_Humidity, " +
                                            "stored_date =:stored_date,  " +
                                            " stored_hour =:stored_hour, stored_minute =:stored_minute,  " +

                                            " MPS_status =:MPS_status, " + 
                                            //"pumping_system_status =:pumping_system_status, station_status=:station_status, "+
                                            //" refrigeration_temperature =:refrigeration_temperature,  " +
                                            //" bottle_position =:bottle_position, door_status =:door_status,  " +
                                            //" equipment_status =:equipment_status, " + 
                                            "created =:created,  " +

                                            " where id = :id";

                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            cmd.Parameters.Add(":var1", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.var1;
                            cmd.Parameters.Add(":var1_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.var1_status;
                            cmd.Parameters.Add(":var2", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.var2;
                            cmd.Parameters.Add(":var2_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.var2_status;
                            cmd.Parameters.Add(":var3", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.var3;
                            cmd.Parameters.Add(":var3_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.var3_status;
                            cmd.Parameters.Add(":var4", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.var4;
                            cmd.Parameters.Add(":var4_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.var4_status;
                            cmd.Parameters.Add(":var5", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.var5;
                            cmd.Parameters.Add(":var5_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.var5_status;
                            cmd.Parameters.Add(":var6", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.var6;
                            cmd.Parameters.Add(":var6_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.var6_status;
                            //cmd.Parameters.Add(":TN", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.TN;
                            //cmd.Parameters.Add(":TN_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.TN_status;
                            //cmd.Parameters.Add(":TP", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.TP;
                            //cmd.Parameters.Add(":TP_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.TP_status;
                            //cmd.Parameters.Add(":TOC", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.TOC;
                            //cmd.Parameters.Add(":TOC_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.TOC_status;

                            //cmd.Parameters.Add(":module_Power", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_Power;
                            //cmd.Parameters.Add(":module_UPS", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_UPS;
                            //cmd.Parameters.Add(":module_Door", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_Door;
                            //cmd.Parameters.Add(":module_Fire", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_Fire;
                            //cmd.Parameters.Add(":module_Flow", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_Flow;
                            //cmd.Parameters.Add(":module_PumpLAM", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpLAM;

                            //cmd.Parameters.Add(":module_PumpLRS", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpLRS;
                            //cmd.Parameters.Add(":module_PumpLFLT", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpLFLT;
                            //cmd.Parameters.Add(":module_PumpRAM", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpRAM;
                            //cmd.Parameters.Add(":module_PumpRRS", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpRRS;
                            //cmd.Parameters.Add(":module_PumpRFLT", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.module_PumpRFLT;

                            //cmd.Parameters.Add(":module_Temperature", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.module_Temperature;
                            //cmd.Parameters.Add(":module_Humidity", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.module_Humidity;

                            cmd.Parameters.Add(":stored_date", NpgsqlTypes.NpgsqlDbType.Date).Value = obj.stored_date;
                            cmd.Parameters.Add(":stored_hour", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.stored_hour;
                            cmd.Parameters.Add(":stored_minute", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.stored_minute;
                            cmd.Parameters.Add(":MPS_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.MPS_status;
                            //cmd.Parameters.Add(":pumping_system_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.pumping_system_status;
                            //cmd.Parameters.Add(":station_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.station_status;

                            //cmd.Parameters.Add(":refrigeration_temperature", NpgsqlTypes.NpgsqlDbType.Double).Value = obj.refrigeration_temperature;
                            //cmd.Parameters.Add(":bottle_position", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.bottle_position;
                            //cmd.Parameters.Add(":door_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.door_status;
                            //cmd.Parameters.Add(":equipment_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.equipment_status;
                            cmd.Parameters.Add(":created", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = obj.created;
                            cmd.Parameters.Add(new NpgsqlParameter(":id", obj.id));

                            cmd.ExecuteNonQuery();

                            db.close_connection();
                            return obj.id;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return -1;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return -1;
                }
            }
        }
        public IEnumerable<data_value> get_all()
        {
            List<data_value> listUser = new List<data_value>();
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    if (db.open_connection())
                    {
                        string sql_command = @"SELECT * FROM data_60minute_values
	                                               ORDER BY created ASC
	                                                ";
                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;
                            NpgsqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                data_value obj = new data_value();
                                obj = (data_value)_get_info(reader);
                                listUser.Add(obj);
                            }
                            reader.Close();
                            db.close_connection();
                            return listUser;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return null;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return null;
                }
                finally
                { db.close_connection(); }
            }
        }
        public DataTable get_all_mps(DateTime datetime_from, DateTime datetime_to)
        {
            DataTable dt = new DataTable();
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    if (db.open_connection())
                    {
                        string sql_command = @"SELECT created ,stored_date, stored_hour, stored_minute, var1,
                                                      var2, var3, var4,var5, var6, mps_status
                                               FROM data_60minute_values
                                               WHERE (:d_from < stored_date AND stored_date < :d_to)
                                                     OR
                                                     (stored_date = :d_from AND stored_date < :d_to   AND (stored_hour  >= :h_from))
                                                     OR
                                                     (stored_date = :d_to   AND stored_date > :d_from AND (stored_hour  <= :h_to))
                                                     OR
                                                     (stored_date = :d_to   AND stored_date = :d_from AND ((stored_hour  >= :h_from AND stored_hour  <= :h_to)  ))
                                                ORDER BY created ASC                                                     
                                                ";

                        DateTime d_from = new DateTime(datetime_from.Year, datetime_from.Month, datetime_from.Day); // datetime_from.ToString("yyyy-MM-dd");
                        DateTime d_to = new DateTime(datetime_to.Year, datetime_to.Month, datetime_to.Day); // datetime_to.ToString("yyyy-MM-dd");

                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            cmd.Parameters.Add(":d_from", NpgsqlTypes.NpgsqlDbType.Date).Value = d_from;
                            cmd.Parameters.Add(":d_to", NpgsqlTypes.NpgsqlDbType.Date).Value = d_to;

                            cmd.Parameters.Add(":h_from", NpgsqlTypes.NpgsqlDbType.Integer).Value = datetime_from.Hour;
                            cmd.Parameters.Add(":h_to", NpgsqlTypes.NpgsqlDbType.Integer).Value = datetime_to.Hour;

                            NpgsqlDataReader reader = cmd.ExecuteReader();

                            dt.Load(reader);

                            reader.Close();
                            db.close_connection();
                            return dt;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return null;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return null;
                }
                finally
                { db.close_connection(); }
            }
        }
        public DataTable get_all_station(DateTime datetime_from, DateTime datetime_to)
        {
            DataTable dt = new DataTable();
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    if (db.open_connection())
                    {
                        string sql_command = @"SELECT created, stored_date, stored_hour, stored_minute,
                                                        module_power,
                                                        module_ups,
                                                        module_door,
                                                        module_fire,
                                                        module_flow,
                                                        module_pumplam,
                                                        module_pumplrs,
                                                        module_pumplflt,
                                                        module_pumpram,
                                                        module_pumprrs,
                                                        module_pumprflt,
                                                        module_temperature,
                                                        module_humidity,
                                                        mps_status,
                                                        station_status,
                                                        pumping_system_status
                                               FROM data_60minute_values
                                               WHERE (:d_from < stored_date AND stored_date < :d_to)
                                                     OR
                                                     (stored_date = :d_from AND stored_date < :d_to   AND (stored_hour  >= :h_from))
                                                     OR
                                                     (stored_date = :d_to   AND stored_date > :d_from AND (stored_hour  <= :h_to))
                                                     OR
                                                     (stored_date = :d_to   AND stored_date = :d_from AND ((stored_hour  >= :h_from AND stored_hour  <= :h_to)  ))
                                                ORDER BY created ASC                                                     
                                                ";

                        DateTime d_from = new DateTime(datetime_from.Year, datetime_from.Month, datetime_from.Day); // datetime_from.ToString("yyyy-MM-dd");
                        DateTime d_to = new DateTime(datetime_to.Year, datetime_to.Month, datetime_to.Day); // datetime_to.ToString("yyyy-MM-dd");

                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            cmd.Parameters.Add(":d_from", NpgsqlTypes.NpgsqlDbType.Date).Value = d_from;
                            cmd.Parameters.Add(":d_to", NpgsqlTypes.NpgsqlDbType.Date).Value = d_to;

                            cmd.Parameters.Add(":h_from", NpgsqlTypes.NpgsqlDbType.Integer).Value = datetime_from.Hour;
                            cmd.Parameters.Add(":h_to", NpgsqlTypes.NpgsqlDbType.Integer).Value = datetime_to.Hour;

                            NpgsqlDataReader reader = cmd.ExecuteReader();

                            dt.Load(reader);

                            reader.Close();
                            db.close_connection();
                            return dt;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return null;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return null;
                }
                finally
                { db.close_connection(); }
            }
        }
        public DataTable get_all_custom(DateTime datetime_from, DateTime datetime_to, List<string> custom_param_list)
        {
            DataTable dt = new DataTable();
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    if (db.open_connection())
                    {

                        string sql_command = @"SELECT created, id, stored_date, stored_hour, stored_minute
                                                        {custom_param}
                                               FROM data_60minute_values
                                               WHERE created BETWEEN :date_from AND :date_to
                                               ORDER BY created ASC
                                                ";

                        DateTime d_from = new DateTime(datetime_from.Year, datetime_from.Month, datetime_from.Day); // datetime_from.ToString("yyyy-MM-dd");
                        DateTime d_to = new DateTime(datetime_to.Year, datetime_to.Month, datetime_to.Day); // datetime_to.ToString("yyyy-MM-dd");

                        DateTime date_from = datetime_from;
                        DateTime date_to = datetime_to;

                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            cmd.Parameters.Add(":date_from", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = date_from;
                            cmd.Parameters.Add(":date_to", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = date_to;

                            NpgsqlDataReader reader = cmd.ExecuteReader();

                            dt.Load(reader);

                            reader.Close();
                            db.close_connection();
                            return dt;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return null;
                    }
                }
                catch (Exception e)
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return null;
                }
                finally
                { db.close_connection(); }
            }
        }
        public DataTable get_all_sampler(DateTime datetime_from, DateTime datetime_to)
        {
            DataTable dt = new DataTable();
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    if (db.open_connection())
                    {
                        string sql_command = @"SELECT stored_date, stored_hour, stored_minute, refrigeration_temperature, bottle_position, door_status, equipment_status
                                               FROM data_60minute_values
                                               WHERE (:d_from < stored_date AND stored_date < :d_to)
                                                     OR
                                                     (stored_date = :d_from AND stored_date < :d_to   AND (stored_hour  >= :h_from))
                                                     OR
                                                     (stored_date = :d_to   AND stored_date > :d_from AND (stored_hour  <= :h_to))
                                                     OR
                                                     (stored_date = :d_to   AND stored_date = :d_from AND ((stored_hour  >= :h_from AND stored_hour  <= :h_to)  ))
                                                     ";

                        DateTime d_from = new DateTime(datetime_from.Year, datetime_from.Month, datetime_from.Day); // datetime_from.ToString("yyyy-MM-dd");
                        DateTime d_to = new DateTime(datetime_to.Year, datetime_to.Month, datetime_to.Day); // datetime_to.ToString("yyyy-MM-dd");

                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            cmd.Parameters.Add(":d_from", NpgsqlTypes.NpgsqlDbType.Date).Value = d_from;
                            cmd.Parameters.Add(":d_to", NpgsqlTypes.NpgsqlDbType.Date).Value = d_to;

                            cmd.Parameters.Add(":h_from", NpgsqlTypes.NpgsqlDbType.Integer).Value = datetime_from.Hour;
                            cmd.Parameters.Add(":h_to", NpgsqlTypes.NpgsqlDbType.Integer).Value = datetime_to.Hour;

                            NpgsqlDataReader reader = cmd.ExecuteReader();

                            dt.Load(reader);

                            reader.Close();
                            db.close_connection();
                            return dt;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return null;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return null;
                }
                finally
                { db.close_connection(); }
            }
        }
        public DataTable get_all_history(DateTime datetime_from, DateTime datetime_to)
        {
            DataTable dt = new DataTable();
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    if (db.open_connection())
                    {
                        string sql_command = @"SELECT *
                                               FROM data_60minute_values
                                               WHERE (:d_from < stored_date AND stored_date < :d_to)
                                                     OR
                                                     (stored_date = :d_from AND stored_date < :d_to   AND (stored_hour  >= :h_from))
                                                     OR
                                                     (stored_date = :d_to   AND stored_date > :d_from AND (stored_hour  <= :h_to))
                                                     OR
                                                     (stored_date = :d_to   AND stored_date = :d_from AND ((stored_hour  >= :h_from AND stored_hour  <= :h_to)  ))
                                                     ";

                        DateTime d_from = new DateTime(datetime_from.Year, datetime_from.Month, datetime_from.Day); // datetime_from.ToString("yyyy-MM-dd");
                        DateTime d_to = new DateTime(datetime_to.Year, datetime_to.Month, datetime_to.Day); // datetime_to.ToString("yyyy-MM-dd");

                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            cmd.Parameters.Add(":d_from", NpgsqlTypes.NpgsqlDbType.Date).Value = d_from;
                            cmd.Parameters.Add(":d_to", NpgsqlTypes.NpgsqlDbType.Date).Value = d_to;

                            cmd.Parameters.Add(":h_from", NpgsqlTypes.NpgsqlDbType.Integer).Value = datetime_from.Hour;
                            cmd.Parameters.Add(":h_to", NpgsqlTypes.NpgsqlDbType.Integer).Value = datetime_to.Hour;

                            NpgsqlDataReader reader = cmd.ExecuteReader();

                            dt.Load(reader);

                            reader.Close();
                            db.close_connection();
                            return dt;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return null;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return null;
                }
                finally
                { db.close_connection(); }
            }
        }
        public DataTable get_all_analyzer(DateTime datetime_from, DateTime datetime_to)
        {
            DataTable dt = new DataTable();
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    if (db.open_connection())
                    {
                        string sql_command = @"SELECT stored_date, stored_hour, stored_minute, tn, tn_status, tp_tp_status, toc, toc_status
                                               FROM data_60minute_values
                                               WHERE (:d_from < stored_date AND stored_date < :d_to)
                                                     OR
                                                     (stored_date = :d_from AND stored_date < :d_to   AND (stored_hour  >= :h_from))
                                                     OR
                                                     (stored_date = :d_to   AND stored_date > :d_from AND (stored_hour  <= :h_to))
                                                     OR
                                                     (stored_date = :d_to   AND stored_date = :d_from AND ((stored_hour  >= :h_from AND stored_hour  <= :h_to)  ))
                                                     ";

                        DateTime d_from = new DateTime(datetime_from.Year, datetime_from.Month, datetime_from.Day); // datetime_from.ToString("yyyy-MM-dd");
                        DateTime d_to = new DateTime(datetime_to.Year, datetime_to.Month, datetime_to.Day); // datetime_to.ToString("yyyy-MM-dd");

                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            cmd.Parameters.Add(":d_from", NpgsqlTypes.NpgsqlDbType.Date).Value = d_from;
                            cmd.Parameters.Add(":d_to", NpgsqlTypes.NpgsqlDbType.Date).Value = d_to;

                            cmd.Parameters.Add(":h_from", NpgsqlTypes.NpgsqlDbType.Integer).Value = datetime_from.Hour;
                            cmd.Parameters.Add(":h_to", NpgsqlTypes.NpgsqlDbType.Integer).Value = datetime_to.Hour;

                            NpgsqlDataReader reader = cmd.ExecuteReader();

                            dt.Load(reader);

                            reader.Close();
                            db.close_connection();
                            return dt;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return null;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return null;
                }
                finally
                { db.close_connection(); }
            }
        }
        public IEnumerable<data_value> get_all_for_monthly_report(int year)
        {
            List<data_value> listUser = new List<data_value>();
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    if (db.open_connection())
                    {
                        string sql_command = @" SELECT *
                                                FROM data_60minute_values
                                                WHERE EXTRACT(YEAR FROM stored_date) = :year
                                                ORDER BY created ASC
                                              ";
                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;
                            cmd.Parameters.Add(":year", NpgsqlTypes.NpgsqlDbType.Integer).Value = year;
                            NpgsqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                data_value obj = new data_value();
                                obj = (data_value)_get_info(reader);
                                listUser.Add(obj);
                            }
                            reader.Close();
                            db.close_connection();
                            return listUser;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return null;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return null;
                }
                finally
                { db.close_connection(); }
            }
        }
        public data_value get_info_by_id(int id)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {

                    data_value obj = null;
                    if (db.open_connection())
                    {
                        string sql_command = "SELECT * FROM data_60minute_values WHERE id = " + id;
                        sql_command += " LIMIT 1";
                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            NpgsqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                obj = new data_value();
                                obj = (data_value)_get_info(reader);
                                break;
                            }
                            reader.Close();
                            db.close_connection();
                            return obj;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return null;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return null;
                }
                finally
                { db.close_connection(); }
            }
        }
        public data_value get_latest_info()
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {

                    data_value obj = null;
                    if (db.open_connection())
                    {
                        string sql_command = "SELECT * FROM data_60minute_values ";
                        sql_command += " ORDER BY created DESC ";
                        sql_command += " LIMIT 1";
                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            NpgsqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                obj = new data_value();
                                obj = (data_value)_get_info(reader);
                                break;
                            }
                            reader.Close();
                            db.close_connection();
                            return obj;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return null;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return null;
                }
                finally
                { db.close_connection(); }
            }
        }
        #endregion Public procedure
        #region private procedure

        private data_value _get_info(NpgsqlDataReader dataReader)
        {
            data_value obj = new data_value();
            try
            {
                if (!DBNull.Value.Equals(dataReader["id"]))
                    obj.id = Convert.ToInt32(dataReader["id"].ToString().Trim());
                else
                    obj.id = 0;
                if (!DBNull.Value.Equals(dataReader["var1"]))
                    obj.var1 = Convert.ToDouble(dataReader["var1"].ToString().Trim());
                else
                    obj.var1 = 0;
                if (!DBNull.Value.Equals(dataReader["var1_status"]))
                    obj.var1_status = Convert.ToInt32(dataReader["var1_status"].ToString().Trim());
                else
                    obj.var1_status = 0;

                if (!DBNull.Value.Equals(dataReader["var2"]))
                    obj.var2 = Convert.ToDouble(dataReader["var2"].ToString().Trim());
                else
                    obj.var2 = 0;
                if (!DBNull.Value.Equals(dataReader["var2_status"]))
                    obj.var2_status = Convert.ToInt32(dataReader["var2_status"].ToString().Trim());
                else
                    obj.var2_status = 0;

                if (!DBNull.Value.Equals(dataReader["var3"]))
                    obj.var3 = Convert.ToDouble(dataReader["var3"].ToString().Trim());
                else
                    obj.var3 = 0;
                if (!DBNull.Value.Equals(dataReader["var3_status"]))
                    obj.var3_status = Convert.ToInt32(dataReader["var3_status"].ToString().Trim());
                else
                    obj.var3_status = 0;

                if (!DBNull.Value.Equals(dataReader["var4"]))
                    obj.var4 = Convert.ToDouble(dataReader["var4"].ToString().Trim());
                else
                    obj.var4 = 0;
                if (!DBNull.Value.Equals(dataReader["var4_status"]))
                    obj.var4_status = Convert.ToInt32(dataReader["var4_status"].ToString().Trim());
                else
                    obj.var4_status = 0;

                if (!DBNull.Value.Equals(dataReader["var5"]))
                    obj.var5 = Convert.ToDouble(dataReader["var5"].ToString().Trim());
                else
                    obj.var5 = 0;
                if (!DBNull.Value.Equals(dataReader["var5_status"]))
                    obj.var5_status = Convert.ToInt32(dataReader["var5_status"].ToString().Trim());
                else
                    obj.var5_status = 0;

                if (!DBNull.Value.Equals(dataReader["var6"]))
                    obj.var6 = Convert.ToDouble(dataReader["var6"].ToString().Trim());
                else
                    obj.var6 = 0;
                if (!DBNull.Value.Equals(dataReader["var6_status"]))
                    obj.var6_status = Convert.ToInt32(dataReader["var6_status"].ToString().Trim());
                else
                    obj.var6_status = 0;

                //if (!DBNull.Value.Equals(dataReader["TN"]))
                //    obj.TN = Convert.ToDouble(dataReader["TN"].ToString().Trim());
                //else
                //    obj.TN = 0;
                //if (!DBNull.Value.Equals(dataReader["TN_status"]))
                //    obj.TN_status = Convert.ToInt32(dataReader["TN_status"].ToString().Trim());
                //else
                //    obj.TN_status = 0;

                //if (!DBNull.Value.Equals(dataReader["TP"]))
                //    obj.TP = Convert.ToDouble(dataReader["TP"].ToString().Trim());
                //else
                //    obj.TP = 0;
                //if (!DBNull.Value.Equals(dataReader["TP_status"]))
                //    obj.TP_status = Convert.ToInt32(dataReader["TP_status"].ToString().Trim());
                //else
                //    obj.TP_status = 0;

                //if (!DBNull.Value.Equals(dataReader["TOC"]))
                //    obj.TOC = Convert.ToDouble(dataReader["TOC"].ToString().Trim());
                //else
                //    obj.TOC = 0;
                //if (!DBNull.Value.Equals(dataReader["TOC_status"]))
                //    obj.TOC_status = Convert.ToInt32(dataReader["TOC_status"].ToString().Trim());
                //else
                //    obj.TOC_status = 0;

                //if (!DBNull.Value.Equals(dataReader["module_Power"]))
                //    obj.module_Power = Convert.ToInt32(dataReader["module_Power"].ToString().Trim());
                //else
                //    obj.module_Power = 0;

                //if (!DBNull.Value.Equals(dataReader["module_UPS"]))
                //    obj.module_UPS = Convert.ToInt32(dataReader["module_UPS"].ToString().Trim());
                //else
                //    obj.module_UPS = 0;

                //if (!DBNull.Value.Equals(dataReader["module_Door"]))
                //    obj.module_Door = Convert.ToInt32(dataReader["module_Door"].ToString().Trim());
                //else
                //    obj.module_Door = 0;

                //if (!DBNull.Value.Equals(dataReader["module_Fire"]))
                //    obj.module_Fire = Convert.ToInt32(dataReader["module_Fire"].ToString().Trim());
                //else
                //    obj.module_Fire = 0;

                //if (!DBNull.Value.Equals(dataReader["module_Flow"]))
                //    obj.module_Flow = Convert.ToInt32(dataReader["module_Flow"].ToString().Trim());
                //else
                //    obj.module_Flow = 0;

                //if (!DBNull.Value.Equals(dataReader["module_PumpLAM"]))
                //    obj.module_PumpLAM = Convert.ToInt32(dataReader["module_PumpLAM"].ToString().Trim());
                //else
                //    obj.module_PumpLAM = 0;

                //if (!DBNull.Value.Equals(dataReader["module_PumpLRS"]))
                //    obj.module_PumpLRS = Convert.ToInt32(dataReader["module_PumpLRS"].ToString().Trim());
                //else
                //    obj.module_PumpLRS = 0;

                //if (!DBNull.Value.Equals(dataReader["module_PumpLFLT"]))
                //    obj.module_PumpLFLT = Convert.ToInt32(dataReader["module_PumpLFLT"].ToString().Trim());
                //else
                //    obj.module_PumpLFLT = 0;

                //if (!DBNull.Value.Equals(dataReader["module_PumpRAM"]))
                //    obj.module_PumpRAM = Convert.ToInt32(dataReader["module_PumpRAM"].ToString().Trim());
                //else
                //    obj.module_PumpRAM = 0;

                //if (!DBNull.Value.Equals(dataReader["module_PumpRRS"]))
                //    obj.module_PumpRRS = Convert.ToInt32(dataReader["module_PumpRRS"].ToString().Trim());
                //else
                //    obj.module_PumpRRS = 0;

                //if (!DBNull.Value.Equals(dataReader["module_PumpRFLT"]))
                //    obj.module_PumpRFLT = Convert.ToInt32(dataReader["module_PumpRFLT"].ToString().Trim());
                //else
                //    obj.module_PumpRFLT = 0;

                //if (!DBNull.Value.Equals(dataReader["module_Temperature"]))
                //    obj.module_Temperature = Convert.ToDouble(dataReader["module_Temperature"].ToString().Trim());
                //else
                //    obj.module_Temperature = 0;

                //if (!DBNull.Value.Equals(dataReader["module_Humidity"]))
                //    obj.module_Humidity = Convert.ToDouble(dataReader["module_Humidity"].ToString().Trim());
                //else
                //    obj.module_Humidity = 0;

                //if (!DBNull.Value.Equals(dataReader["pumping_system_status"]))
                //    obj.pumping_system_status = Convert.ToInt32(dataReader["pumping_system_status"].ToString().Trim());
                //else
                //    obj.pumping_system_status = 0;
                //if (!DBNull.Value.Equals(dataReader["station_status"]))
                //    obj.station_status = Convert.ToInt32(dataReader["station_status"].ToString().Trim());
                //else
                //    obj.station_status = 0;

                if (!DBNull.Value.Equals(dataReader["stored_date"]))
                    obj.stored_date = Convert.ToDateTime(dataReader["stored_date"].ToString().Trim());
                else
                    obj.stored_date = DateTime.Now;
                if (!DBNull.Value.Equals(dataReader["stored_hour"]))
                    obj.stored_hour = Convert.ToInt32(dataReader["stored_hour"].ToString().Trim());
                else
                    obj.stored_hour = 0;
                if (!DBNull.Value.Equals(dataReader["stored_minute"]))
                    obj.stored_minute = Convert.ToInt32(dataReader["stored_minute"].ToString().Trim());
                else
                    obj.stored_minute = 0;
                if (!DBNull.Value.Equals(dataReader["MPS_status"]))
                    obj.MPS_status = Convert.ToInt32(dataReader["MPS_status"].ToString().Trim());
                else
                    obj.MPS_status = 0;
                //if (!DBNull.Value.Equals(dataReader["refrigeration_temperature"]))
                //    obj.refrigeration_temperature = Convert.ToDouble(dataReader["refrigeration_temperature"].ToString().Trim());
                //else
                //    obj.refrigeration_temperature = 0;

                //if (!DBNull.Value.Equals(dataReader["bottle_position"]))
                //    obj.bottle_position = Convert.ToInt32(dataReader["bottle_position"].ToString().Trim());
                //else
                //    obj.bottle_position = 0;
                //if (!DBNull.Value.Equals(dataReader["equipment_status"]))
                //    obj.equipment_status = Convert.ToInt32(dataReader["equipment_status"].ToString().Trim());
                //else
                //    obj.equipment_status = 0;
                //if (!DBNull.Value.Equals(dataReader["door_status"]))
                //    obj.door_status = Convert.ToInt32(dataReader["door_status"].ToString().Trim());
                //else
                //    obj.door_status = 0;
                if (!DBNull.Value.Equals(dataReader["created"]))
                    obj.created = Convert.ToDateTime(dataReader["created"].ToString().Trim());
                else
                    obj.created = DateTime.Now;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        #endregion private procedure
    }
}