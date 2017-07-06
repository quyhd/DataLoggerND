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
    public class data_value_repository : NpgsqlDBConnection
    {
        #region Public procedure

        /// <summary>
        /// add new
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int add(ref data_value obj)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    Int32 ID = -1;

                    if (db.open_connection())
                    {
                        string sql_command = "INSERT INTO data_values (var1, var1_status, var2, var2_status, " +
                                            " var3, var3_status, var4, var4_status, " +
                                            " var5, var5_status, var6, var6_status, " +
                                            " stored_date, stored_hour, stored_minute, MPS_status," +
                                            " push, push_time, " +
                                            " created)" +
                                            " VALUES (:var1, :var1_status, :var2, :var2_status, " +
                                            " :var3,:var3_status, :var4, :var4_status, " +
                                            " :var5, :var5_status, :var6, :var6_status, " +
                                            " :stored_date, :stored_hour, :stored_minute, :MPS_status, " +
                                            " :push, :push_time, " +
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

                            cmd.Parameters.Add(":stored_date", NpgsqlTypes.NpgsqlDbType.Date).Value = obj.stored_date;
                            cmd.Parameters.Add(":stored_hour", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.stored_hour;
                            cmd.Parameters.Add(":stored_minute", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.stored_minute;
                            cmd.Parameters.Add(":MPS_status", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.MPS_status;

                            cmd.Parameters.Add(":created", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = obj.created;
                            cmd.Parameters.Add(":push", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.push;
                            cmd.Parameters.Add(":push_time", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = obj.push_time;
                            //cmd.ExecuteNonQuery();
                            ID = (Int32)cmd.ExecuteScalar();
                            obj.id = ID;

                            db.close_connection();
                            return ID;
                        }
                        Console.Write("ADD data_values TRUE");
                    }
                    else
                    {
                        db.close_connection();
                        return -1;
                        Console.Write("ADD data_values FALSE");
                    }
                }
                catch(Exception e)
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return -1;
                    Console.Write(e.StackTrace);
                }
                finally
                {
                    db.close_connection();
                }
            }
        }
        ///// <summary>
        ///// update
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public int update(ref data_value obj)
        //{
        //    using (NpgsqlDBConnection db = new NpgsqlDBConnection())
        //    {
        //        try
        //        {

        //            if (db.open_connection())
        //            {
        //                string sql_command = "UPDATE data_values set  " +
        //                                    " data_value_key = :data_value_key, data_value_value =:data_value_value, " +
        //                                    " data_value_type =:data_value_type, " +
        //                                    " note = :note " +
        //                                    " where id = :id";

        //                using (NpgsqlCommand cmd = db._conn.CreateCommand())
        //                {                            
        //                    cmd.CommandText = sql_command;

        //                    cmd.Parameters.Add(new NpgsqlParameter(":data_value_key", obj.data_value_key));
        //                    cmd.Parameters.Add(new NpgsqlParameter(":data_value_value", obj.data_value_value));
        //                    cmd.Parameters.Add(new NpgsqlParameter(":data_value_type", obj.data_value_type));
        //                    cmd.Parameters.Add(new NpgsqlParameter(":note", obj.note));
        //                    cmd.Parameters.Add(new NpgsqlParameter(":id", obj.id));

        //                    cmd.ExecuteNonQuery();

        //                    db.close_connection();
        //                    return obj.id;
        //                }
        //            }
        //            else
        //            {
        //                db.close_connection();
        //                return -1;
        //            }
        //        }
        //        catch
        //        {
        //            if (db != null)
        //            {
        //                db.close_connection();
        //            }
        //            return -1;
        //        }
        //    }
        //}


        ///// <summary>
        ///// delete
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public bool delete(int id)
        //{
        //    using (NpgsqlDBConnection db = new NpgsqlDBConnection())
        //    {
        //        try
        //        {
        //            bool result = false;

        //            if (db.open_connection())
        //            {
        //                string sql_command = "DELETE from data_values where id = " + id;

        //                using (NpgsqlCommand cmd = db._conn.CreateCommand())
        //                {                            
        //                    cmd.CommandText = sql_command;
        //                    result = cmd.ExecuteNonQuery() > 0;
        //                    db.close_connection();
        //                    return true;
        //                }
        //            }
        //            else
        //            {
        //                db.close_connection();
        //                return result;
        //            }
        //        }
        //        catch
        //        {
        //            if (db != null)
        //            {
        //                db.close_connection();
        //            }
        //            return false;
        //        }
        //        finally
        //        { db.close_connection(); }
        //    }
        //}

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<data_value> get_all()
        {
            List<data_value> listUser = new List<data_value>();
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    if (db.open_connection())
                    {
                        string sql_command = "SELECT * FROM data_values";
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

        /// <summary>
        /// get info by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public data_value get_info_by_id(int id)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {

                    data_value obj = null;
                    if (db.open_connection())
                    {
                        string sql_command = "SELECT * FROM data_values WHERE id = " + id;
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

                if (!DBNull.Value.Equals(dataReader["push"]))
                    obj.push = Convert.ToInt32(dataReader["push"].ToString().Trim());
                else
                    obj.push = -1;
                if (!DBNull.Value.Equals(dataReader["push_time"]))
                    obj.push_time = Convert.ToDateTime(dataReader["push_time"].ToString().Trim());
                else
                    obj.push_time = new DateTime();

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