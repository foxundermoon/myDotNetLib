using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///aspDBManager 的摘要说明
/// </summary>
/// change @ submodule
/// 

namespace FoxundermoonLib.Database.Sql
{
    public class AspDBManager
    {

        public System.Data.SqlClient.SqlConnection cnn = null;
        public string DbConnectionString { get; set; }

        public bool bSuccess;
        public string strError;
        public AspDBManager()
        {
            bSuccess = true;
            strError = "";
        }
        public void Clear()
        {
            bSuccess = true;
            strError = "";
        }

        public bool Open()
        {
            var strConn = "";
            if (string.IsNullOrEmpty(DbConnectionString))
                strConn = "server=10.80.5.107;" + " integrated security=False; database=CDDL;User ID=sa;Password=small;";
            else
                strConn = DbConnectionString;

            //string strConn = "server=10.80.4.9\\hydgtdt;" + " Persist Security Info=True; Initial Catalog=CDDL;User ID=sa;Password=abc@123;";
            //strConn = @"Data Source=FOXUNDERMOON\SQLEXPRESS;Initial Catalog=垃圾回收;Integrated Security=True";
            //string strConn = ""; //@"Data Source=FOXUNDERMOON\SQLEXPRESS;Initial Catalog=垃圾回收;Integrated Security=True";
            //string strConn = "server=user-pc\\sqlexpress;" + " integrated security=False; database=CityRubDispose;User ID=sa;Password=abc@123;";
            //  string strConn = "server=mapdev;" + " integrated security=False; database=workflowtest;User ID=sa;Password=781129;";
            // string strConn = "server=192.168.1.110;" + " integrated security=False; database=workflowtest;User ID=sa;Password=small;";
            try
            {
                cnn = new System.Data.SqlClient.SqlConnection(strConn);
                cnn.Open();
                bSuccess = true;
            }
            catch (System.Exception ex)
            {
                bSuccess = false;
                strError = ex.Message;
                return false;
            }
            return true;
        }
        public bool Open(string strConnectionString)        //打开数据库的连接
        {
            try
            {
                cnn = new System.Data.SqlClient.SqlConnection(strConnectionString);
                cnn.Open();
                bSuccess = true;
            }
            catch (System.Exception ex)
            {
                bSuccess = false;
                strError = ex.Message;
                return false;
            }
            return true;
        }
        public bool Close()       //关闭数据库连接 
        {
            try
            {
                if (cnn != null)
                {
                    if (cnn.State == System.Data.ConnectionState.Open)
                    {
                        cnn.Close();
                        bSuccess = true;
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                bSuccess = false;
                strError = ex.Message;
                return false;
            }
        }
        public System.Data.DataTable GetDataTable(string cmdstring, string tablename)           //返回oledbdatareader 
        {
            if (cnn == null)
            {
                bSuccess = false;
                strError = "数据库没有打开";
                return null;
            }
            try
            {

                System.Data.SqlClient.SqlDataAdapter myda = new System.Data.SqlClient.SqlDataAdapter();
                myda.SelectCommand = new System.Data.SqlClient.SqlCommand(cmdstring, cnn);
                System.Data.DataSet myds = new System.Data.DataSet();
                myda.Fill(myds, tablename);
                bSuccess = true;
                return myds.Tables[tablename];
            }
            catch (System.Exception ex)
            {
                bSuccess = false;
                strError = ex.Message;
                return null;
            }
            return null;
        }
        public System.Data.Common.DbDataReader GetDataReader(string cmdstring)          //返回影响数据库的行数
        {
            try
            {
                System.Data.SqlClient.SqlCommand mycmd = new System.Data.SqlClient.SqlCommand(cmdstring, cnn);
                System.Data.SqlClient.SqlDataReader mydr = mycmd.ExecuteReader();
                return mydr;
            }
            catch (System.Exception ex)
            {
                bSuccess = false;
                strError = ex.Message;
                return null;
            }
            return null;
        }
        public bool ExecuteSql(string cmdstring)           //执行sql语句的接口
        {
            try
            {
                System.Data.SqlClient.SqlCommand mycmd = new System.Data.SqlClient.SqlCommand(cmdstring, cnn);
                int cmd = mycmd.ExecuteNonQuery();
                bSuccess = true;
                return true;
            }
            catch (System.Exception ex)
            {
                bSuccess = false;
                strError = ex.Message;
                return false;
            }
        }
        public string GetFldValue(string strSql, string strFldName)
        {
            System.Data.Common.DbDataReader reader = null;
            try
            {
                System.Data.SqlClient.SqlCommand mycmd = cnn.CreateCommand();
                mycmd.CommandText = strSql;
                reader = mycmd.ExecuteReader();
                if (reader == null)
                {
                    bSuccess = false;
                    strError = "内容不存在";
                    return "";
                }
                if (!reader.HasRows)
                {
                    bSuccess = true;
                    strError = "内容为空";
                    reader.Close();
                    return "";
                }
                string strReturnValue = "";
                if (reader.Read())
                {
                    if (!Convert.IsDBNull(reader[strFldName]))
                    {
                        strReturnValue = reader[strFldName].ToString().Trim();
                    }
                }
                reader.Close();
                bSuccess = true;
                return strReturnValue;
            }
            catch (System.Exception ex)
            {
                bSuccess = false;
                strError = ex.Message;
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                return "";
            }
            return "";

        }
        public List<string> GetFldsValues(string strSql, List<string> lstFlds)
        {
            if (lstFlds == null)
                return null;
            if (lstFlds.Count < 1)
                return null;
            System.Data.Common.DbDataReader reader = null;
            List<string> lstReturn = null;
            try
            {
                System.Data.SqlClient.SqlCommand mycmd = cnn.CreateCommand();
                mycmd.CommandText = strSql;
                reader = mycmd.ExecuteReader();
                if (reader == null)
                {
                    bSuccess = false;
                    strError = "内容不存在";

                    return null;
                }
                if (!reader.HasRows)
                {
                    bSuccess = true;
                    strError = "内容为空";
                    reader.Close();
                    return null;
                }

                lstReturn = new List<string>();
                if (reader.Read())
                {
                    for (int i = 0; i < lstFlds.Count; i++)
                    {
                        if (!Convert.IsDBNull(reader[lstFlds[i]]))
                        {
                            string strRT = reader[lstFlds[i]].ToString().TrimEnd();
                            lstReturn.Add(strRT);
                        }
                        else
                        {
                            lstReturn.Add("");
                        }
                    }

                }
                reader.Close();
                bSuccess = true;

            }
            catch (System.Exception ex)
            {
                bSuccess = false;
                strError = ex.Message;
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                return null;
            }


            return lstReturn;
        }
        public List<string> GetFldValues(string strSql, string strFldName)
        {
            List<string> lstReturn = null;
            System.Data.Common.DbDataReader reader = null;
            try
            {
                System.Data.SqlClient.SqlCommand mycmd = cnn.CreateCommand();
                mycmd.CommandText = strSql;
                reader = mycmd.ExecuteReader();
                if (reader == null)
                {
                    bSuccess = false;
                    strError = "内容不存在";
                    return null;
                }
                if (!reader.HasRows)
                {
                    bSuccess = true;
                    strError = "内容为空";
                    reader.Close();
                    return null;
                }

                lstReturn = new List<string>();
                while (reader.Read())
                {
                    if (!Convert.IsDBNull(reader[strFldName]))
                    {
                        string strRT = reader[strFldName].ToString().TrimEnd();
                        lstReturn.Add(strRT);
                    }
                }
                reader.Close();
                bSuccess = true;

            }
            catch (System.Exception ex)
            {
                bSuccess = false;
                strError = ex.Message;
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                return null;
            }


            return lstReturn;
        }

        public string GetFldMaxValue(string strSql, string strFldName)
        {
            System.Data.Common.DbDataReader reader = null;
            try
            {
                System.Data.SqlClient.SqlCommand mycmd = cnn.CreateCommand();
                mycmd.CommandText = strSql;
                reader = mycmd.ExecuteReader();
                if (reader == null)
                {
                    bSuccess = false;
                    strError = "内容不存在";
                    return "";
                }
                if (!reader.HasRows)
                {
                    bSuccess = true;
                    strError = "内容为空";
                    reader.Close();
                    return "";
                }
                string strReturnMaxValue = "";
                if (reader.Read())
                {
                    if (!Convert.IsDBNull(reader[strFldName]))
                    {
                        strReturnMaxValue = reader[strFldName].ToString();
                    }
                }
                reader.Close();
                bSuccess = true;
                return strReturnMaxValue;
            }
            catch (System.Exception ex)
            {
                bSuccess = false;
                strError = ex.Message;
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                return "0";
            }

            return "0";

        }
        public List<string> GetFieldInfoFromMeta(String strOwner, String strTableName)
        {

            try
            {
                string[] res = new string[4] { null, null, strTableName, null };
                System.Data.DataTable tb = cnn.GetSchema("Columns", res);
                System.Data.DataRow[] rows = tb.Select("", "ORDINAL_POSITION");
                List<string> lstFields = new List<string>();
                for (int i = 0; i < rows.Length; i++)
                {

                    string strFldName = rows[i]["Column_Name"].ToString();
                    String strDataType = rows[i]["Data_Type"].ToString();
                    String strLength = rows[i]["CHARACTER_MAXIMUM_LENGTH"].ToString();
                    String strPreci = rows[i]["NUMERIC_PRECISION"].ToString();
                    String strDot = rows[i]["NUMERIC_SCALE"].ToString();
                    if (strDataType == "7" || strDataType == "135")
                    {
                        continue;
                    }

                    lstFields.Add(strFldName);
                }
                return lstFields;
            }
            catch (System.Exception ex)
            {

            }
            return null;
        }
        public string GetFieldType(String strTableName, string strFieldName)
        {
            string strFldT = "字符";
            try
            {
                string[] res = new string[4] { null, null, strTableName, null };
                System.Data.DataTable tb = cnn.GetSchema("Columns", res);
                System.Data.DataRow[] rows = tb.Select("", "ORDINAL_POSITION");
                List<string> lstFields = new List<string>();
                for (int i = 0; i < rows.Length; i++)
                {

                    string strFldName = rows[i]["Column_Name"].ToString();
                    String strDataType = rows[i]["Data_Type"].ToString();

                    if (strFldName.ToUpper() == strFieldName.ToUpper())
                    {
                        if (strDataType == "129" || strDataType == "201" || strDataType == "203" || strDataType == "200" || strDataType == "202" || strDataType == "130")
                        {
                            strFldT = "字符";

                        }

                        else if (strDataType == "20" || strDataType == "14" || strDataType == "5" || strDataType == "3" || strDataType == "131" || strDataType == "4" || strDataType == "2" || strDataType == "17")
                        {
                            strFldT = "数值";


                        }
                        else if (strDataType == "7" || strDataType == "135")
                        {
                            strFldT = "日期";
                        }
                        else if (strDataType == "204" || strDataType == "128")
                        {
                            strFldT = "二进制";

                        }
                        else
                        {
                            strFldT = "字符";


                        }
                        return strFldT;
                    }



                }
                return strFldT;
            }
            catch (System.Exception ex)
            {

            }
            return "";
        }
        public System.Data.Common.DbCommand CreateCommand(string cmdstring)
        {
            System.Data.SqlClient.SqlCommand mycmd = new System.Data.SqlClient.SqlCommand(cmdstring, cnn);
            return mycmd;
        }
        public System.Data.Common.DbParameter CreateCommanDbParameter()
        {
            try
            {
                System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter();
                return param;
            }
            catch (System.Exception ex)
            {
                return null;
            }
            return null;

        }


    }
}
