using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace FoxundermoonLib.Diagnostics
{
    class MySqlTraceListener : TraceListener
    {
        MySqlConnection mycon;
        string server = "localhost";
        string name = "root";
        string pawd = "232381204";
        int port = 3306;
        string database = "application";
        MySqlCommand cmd;

        public MySqlTraceListener()
        {
            mycon = new MySqlConnection();
            mycon.ConnectionString = string.Format("server={0}; user={1}; password={2}; database={3};",
                    server, name, pawd, database);
            mycon.Open();
            cmd = mycon.CreateCommand();

        }

        private  void  insertRecord(Record record)
        {
            string sql = "INSERT INTO `trace`(`message`, `category`) VALUES ('{0}','{1}')";
            cmd.CommandText = string.Format(sql, record.Message, record.Category);
            cmd.ExecuteNonQuery();

        }
         
        public override void Write(string message)
        {
            Record r = new Record(message);
            insertRecord(r);
        }
        public override void WriteLine(string message)
        {
            Record r = new Record(message);
            insertRecord(r);
        }

        public override void Write(string message, string category)
        {
            Record r = new Record(message, category);
            insertRecord(r);
        }
        public override void Write(object o, string category)
        {
            Record r = new Record(o.ToString(), category);
            insertRecord(r);
        }

        struct Record
        {
            public Record(string message,string category) :this()
            {
                this.Message = message;
                this.Category = category;
            }
            public Record(string message) :this()
            {
                this.Message = message;
            }
            public string Message { get; set; }
            public string Category { get; set; }

        }
    }
}
