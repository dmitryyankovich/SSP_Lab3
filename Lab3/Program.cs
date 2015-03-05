using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            var exit = true;
            const string connectingString = @"provider=Microsoft.Jet.OLEDB.4.0;data source=..\mydb.mdb";
            var myConn = new OleDbConnection(connectingString);
            while (exit)
            {
                Console.WriteLine("1. Select all\n2. Insert\n3. Update\n4. Delete\n5. Exit");
                switch (Console.ReadLine())
                {
                    case "1":
                        SelectFromDatabase(myConn);
                        break;
                    case "2":
                        InsertToDatabase(myConn);
                        break;
                    case "3":
                        UpdateDatabase(myConn);
                        break;
                    case "4":
                        DeleteFromDatabase(myConn);
                        break;
                    case "5":
                        exit = false;
                        continue;
                    default:
                        Console.Clear();
                        Console.WriteLine("Try again");
                        break;
                }
            }
            myConn.Close();
        }

        private static void SelectFromDatabase(OleDbConnection myConn)
        {
            const string selectString = "Select * from stud";
            OleDbCommand myCommand = myConn.CreateCommand();
            myCommand.CommandText = selectString;
            var oda = new OleDbDataAdapter {SelectCommand = myCommand};
            var myDataset = new DataSet();
            if (myConn.State != ConnectionState.Open)
            {
                myConn.Open();
            }
            const string dataTableName = "studA";
            oda.Fill(myDataset, dataTableName);
            var myDataTable = myDataset.Tables[dataTableName];
            foreach (DataRow dr in myDataTable.Rows)
            {
                Console.Write("ID = " + dr["id"] + "   ");
                Console.Write("Name = " + dr["name"] + "   ");
                Console.Write("Group = " + dr["group"] + "   ");
                Console.WriteLine();
            }
            Console.ReadLine();
            Console.Clear();
        }

        private static void InsertToDatabase(OleDbConnection myConn)
        {
            Console.Clear();
            Console.WriteLine("Enter name");
            var name = Console.ReadLine();
            Console.WriteLine("Enter group");
            int group;
            var isInt = Int32.TryParse(Console.ReadLine(), out @group);
            if (!isInt)
            {
                Console.Clear();
                Console.WriteLine("Wrong group number");
                return;
            }
            string insertString = "INSERT INTO stud ([Name], [Group])  VALUES (\"" + name + "\", " + @group + ");";
            OleDbCommand myCommand = myConn.CreateCommand();
            myCommand.CommandText = insertString;
            if (myConn.State != ConnectionState.Open)
            {
                myConn.Open();
            }
            myCommand.ExecuteNonQuery();
            Console.Clear();
            Console.WriteLine("Success");
        }

        private static void UpdateDatabase(OleDbConnection myConn)
        {
            Console.Clear();
            Console.WriteLine("Enter id");
            int id;
            var isNormal = Int32.TryParse(Console.ReadLine(), out id);
            if (!isNormal)
            {
                Console.Clear();
                Console.WriteLine("Wrong input!");
                return;
            }
            Console.WriteLine("Enter name");
            var name = Console.ReadLine();
            Console.WriteLine("Enter group");
            int group;
            var isInt = Int32.TryParse(Console.ReadLine(), out @group);
            if (!isInt)
            {
                Console.Clear();
                Console.WriteLine("Wrong group number");
                return;
            }
            string updateString = "UPDATE stud SET [Name] = \"" + name + "\", [Group] =" + group + " WHERE Id= " + id + ";";
            OleDbCommand myCommand = myConn.CreateCommand();
            myCommand.CommandText = updateString;
            if (myConn.State != ConnectionState.Open)
            {
                myConn.Open();
            }
            var rows = myCommand.ExecuteNonQuery();
            Console.Clear();
            Console.WriteLine(rows > 0 ? "Success" : "Wrong id!");
        }

        private static void DeleteFromDatabase(OleDbConnection myConn)
        {
            Console.Clear();
            Console.WriteLine("Enter id");
            int id;
            var isNormal = Int32.TryParse(Console.ReadLine(), out id);
            if (!isNormal)
            {
                Console.Clear();
                Console.WriteLine("Wrong input!");
                return;
            }
            string deleteString = "DELETE FROM stud WHERE Id = " + id + ";";
            OleDbCommand myCommand = myConn.CreateCommand();
            myCommand.CommandText = deleteString;
            if (myConn.State != ConnectionState.Open)
            {
                myConn.Open();
            }
            var rows = myCommand.ExecuteNonQuery();
            Console.Clear();
            Console.WriteLine(rows > 0 ? "Success" : "Wrong id!");
        }
    }
}