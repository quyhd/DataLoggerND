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
    public class station_repository : NpgsqlDBConnection
    {
        #region Public procedure

        /// <summary>
        /// add new
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int add(ref station obj)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    int ID = -1;

                    if (db.open_connection())
                    {
                        string sql_command = "INSERT INTO stations (station_name, " +
                                            " station_id,socket_port," +
                                            " ftpserver, ftpusername, " +
                                            " ftppassword, ftpfolder, " +
                                            " ftpflag, " +
                                            " modified)" +
                                            " VALUES (:station_name, " +
                                            " :station_id, :socket_port," +
                                            " :ftpserver, :ftpusername, " +
                                            " :ftppassword, :ftpfolder, " +
                                            " :ftpflag, " +
                                            " :modified)";
                        sql_command += " RETURNING id;";

                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            cmd.Parameters.Add(":socket_port", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.socket_port;
                            cmd.Parameters.Add(":station_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.station_name;
                            cmd.Parameters.Add(":station_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.station_id;
                            cmd.Parameters.Add(":modified", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = obj.modified;

                            cmd.Parameters.Add(":ftpserver", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.ftpserver;
                            cmd.Parameters.Add(":ftpusername", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.ftpusername;
                            cmd.Parameters.Add(":ftppassword", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.ftppassword;
                            cmd.Parameters.Add(":ftpfolder", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.ftpfolder;
                            cmd.Parameters.Add(":ftpflag", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.ftpflag;

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
                { db.close_connection(); }
            }
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int update(ref station obj)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {

                    if (db.open_connection())
                    {
                        string sql_command = "UPDATE stations set  " +
                                            " socket_port = :socket_port, station_name =:station_name, " +
                                            " station_id =:station_id," +
                                            " ftpserver =:ftpserver, ftpusername=:ftpusername," +
                                            " ftppassword =:ftppassword, ftpfolder=:ftpfolder," +
                                            " ftpflag =:ftpflag," +
                                            " modified = :modified " +
                                            " where id = :id";

                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            cmd.Parameters.Add(":socket_port", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.socket_port;
                            cmd.Parameters.Add(":station_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.station_name;
                            cmd.Parameters.Add(":station_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.station_id;
                            cmd.Parameters.Add(":modified", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = obj.modified;

                            cmd.Parameters.Add(":ftpserver", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.ftpserver;
                            cmd.Parameters.Add(":ftpusername", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.ftpusername;
                            cmd.Parameters.Add(":ftppassword", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.ftppassword;
                            cmd.Parameters.Add(":ftpfolder", NpgsqlTypes.NpgsqlDbType.Varchar).Value = obj.ftpfolder;
                            cmd.Parameters.Add(":ftpflag", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.ftpflag;

                            cmd.Parameters.Add(":id", NpgsqlTypes.NpgsqlDbType.Integer).Value = obj.id;

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


        /// <summary>
        /// delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool delete(int id)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    bool result = false;

                    if (db.open_connection())
                    {
                        string sql_command = "DELETE from stations where id = " + id;

                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;
                            result = cmd.ExecuteNonQuery() > 0;
                            db.close_connection();
                            return true;
                        }
                    }
                    else
                    {
                        db.close_connection();
                        return result;
                    }
                }
                catch
                {
                    if (db != null)
                    {
                        db.close_connection();
                    }
                    return false;
                }
                finally
                { db.close_connection(); }
            }
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<station> get_all()
        {
            List<station> listUser = new List<station>();
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {
                    if (db.open_connection())
                    {
                        string sql_command = "SELECT * FROM stations";
                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;
                            NpgsqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                station obj = new station();
                                obj = (station)_get_info(reader);
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
        public station get_info_by_id(int id)
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {

                    station obj = null;
                    if (db.open_connection())
                    {
                        string sql_command = "SELECT * FROM stations WHERE id = " + id;
                        sql_command += " LIMIT 1";
                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            NpgsqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                obj = new station();
                                obj = (station)_get_info(reader);
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

        /// <summary>
        /// get info by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public station get_info()
        {
            using (NpgsqlDBConnection db = new NpgsqlDBConnection())
            {
                try
                {

                    station obj = null;
                    if (db.open_connection())
                    {
                        string sql_command = "SELECT * FROM stations LIMIT 1";
                        using (NpgsqlCommand cmd = db._conn.CreateCommand())
                        {
                            cmd.CommandText = sql_command;

                            NpgsqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                obj = new station();
                                obj = (station)_get_info(reader);
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

        private station _get_info(NpgsqlDataReader dataReader)
        {
            station obj = new station();
            try
            {
                if (!DBNull.Value.Equals(dataReader["id"]))
                    obj.id = Convert.ToInt32(dataReader["id"].ToString().Trim());
                else
                    obj.id = 0;

                if (!DBNull.Value.Equals(dataReader["socket_port"]))
                    obj.socket_port = Convert.ToInt32(dataReader["socket_port"].ToString().Trim());
                else
                    obj.socket_port = 0;

                if (!DBNull.Value.Equals(dataReader["station_name"]))
                    obj.station_name = dataReader["station_name"].ToString().Trim();
                else
                    obj.station_name = "";
                if (!DBNull.Value.Equals(dataReader["station_id"]))
                    obj.station_id = dataReader["station_id"].ToString().Trim();
                else
                    obj.station_id = "";

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                try
                {
                    if (!DBNull.Value.Equals(dataReader["modified"]))
                        obj.modified = Convert.ToDateTime(dataReader["modified"].ToString().Trim());
                    else
                        obj.modified = DateTime.Now;
                }
                catch (Exception ex) { }

                if (!DBNull.Value.Equals(dataReader["ftpserver"]))
                    obj.ftpserver = dataReader["ftpserver"].ToString().Trim();
                else
                    obj.ftpserver = "";

                if (!DBNull.Value.Equals(dataReader["ftpusername"]))
                    obj.ftpusername = dataReader["ftpusername"].ToString().Trim();
                else
                    obj.ftpusername = "";

                if (!DBNull.Value.Equals(dataReader["ftppassword"]))
                    obj.ftppassword = dataReader["ftppassword"].ToString().Trim();
                else
                    obj.ftppassword = "";

                if (!DBNull.Value.Equals(dataReader["ftpfolder"]))
                    obj.ftpfolder = dataReader["ftpfolder"].ToString().Trim();
                else
                    obj.ftpfolder = "";

                if (!DBNull.Value.Equals(dataReader["ftpflag"]))
                    obj.ftpflag = Convert.ToInt32(dataReader["ftpflag"].ToString().Trim());
                else
                    obj.ftpflag = -1;
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