/*
 * @author:    Kaleb Eberhart
 * @date:      04/20/2018
 * @course:    CST-247
 * @professor: Mark Reha
 * @project:   Benchmark v.1
 * @file:      VerseDAO.cs
 * @revision:  1
 * @synapsis:  This class is used for the database calls involving verses. All database calls
 *             are located in this file. These include inserts, checks, and searches.
 */

using Benchmark.Models;
using Benchmark.Services.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Benchmark.Services.Data
{
    public class VerseDAO
    {
        //Variables for the model traits, conn string, and data.
        private String connString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Benchmark;" +
            "Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;" +
            "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private string testament, book, text, query;
        private int chapter, verse;
        SqlConnection conn;
        SqlDataReader dr;
        SqlCommand cmd;

        //Constructor sets class variables to the model traits and sets the connection.
        public VerseDAO(VerseModel model)
        {
            testament = model.Testament;
            book = model.Book;
            chapter = model.Chapter;
            verse = model.Verse;
            text = model.Text;

            conn = new SqlConnection(connString);
        }

        //This method checks to see if the verse the user is trying to add has already been added to the
        //database.
        public bool CheckExisting()
        {
            try
            {
                BenchLogger.getInstance().Info("Invoked CheckExisting in the verse DAO");

                //There's 3 wheres here because there are multiple duplicate verses in the bible,
                //some in the same book, some in the same verse#, but none in the same book and verse #
                query = "SELECT * FROM dbo.Verses WHERE BOOK=@book AND VERSE=@verse AND TEXT=@text";

                cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@book", SqlDbType.NVarChar, 50).Value = book;
                cmd.Parameters.Add("@verse", SqlDbType.Int).Value = verse;
                cmd.Parameters.Add("@text", SqlDbType.NVarChar).Value = text;

                conn.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    conn.Close();
                    BenchLogger.getInstance().Info("Leaving CheckExisting Method. Verse existed");
                    return false;
                }
                else
                {
                    conn.Close();
                    BenchLogger.getInstance().Info("Leaving CheckExisting Method. Verse did not exist in database");
                    return true;
                }
            }
            catch (SqlException e)
            {
                BenchLogger.getInstance().Error("Sql Exception occured in CheckExisting DAO method: " + e.ToString());
                throw e;
            }
        }

        //This method is called after the CheckExisting() method and inserts the verse into the database.
        public void InsertVerse()
        {
            try
            {
                BenchLogger.getInstance().Info("Invoked InsertVerse method in the verse DAO");

                query = "INSERT INTO dbo.Verses (TESTAMENT, BOOK, CHAPTER, VERSE, TEXT) VALUES (@test," +
                    " @book, @chp, @ver, @txt)";

                cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@test", SqlDbType.NVarChar, 50).Value = testament;
                cmd.Parameters.Add("@book", SqlDbType.NVarChar, 50).Value = book;
                cmd.Parameters.Add("@chp", SqlDbType.Int).Value = chapter;
                cmd.Parameters.Add("@ver", SqlDbType.Int).Value = verse;
                cmd.Parameters.Add("@txt", SqlDbType.NVarChar).Value = text;

                conn.Open();
                cmd.ExecuteReader();
                BenchLogger.getInstance().Info("Leaving InserVerse method in verse DAO");
                conn.Close();
            }
            catch(SqlException e)
            {
                BenchLogger.getInstance().Error("Sql Exception occured in InsertVerse DAO method: " + e.ToString());
                throw e;
            }

        }

        //This method searches the database for the verse the user is trying to find and then returns it as
        //a list of strings.
        public List<string> SearchVerse()
        {
            try
            {
                BenchLogger.getInstance().Info("Invoked SearchVerse method in the verse DAO");

                query = "SELECT * FROM dbo.Verses WHERE TESTAMENT=@test AND BOOK=@book AND CHAPTER=@chp" +
                    " AND VERSE=@ver";

                cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@test", SqlDbType.NVarChar, 50).Value = testament;
                cmd.Parameters.Add("@book", SqlDbType.NVarChar, 50).Value = book;
                cmd.Parameters.Add("@chp", SqlDbType.Int).Value = chapter;
                cmd.Parameters.Add("@ver", SqlDbType.Int).Value = verse;

                conn.Open();
                dr = cmd.ExecuteReader();

                List<string> list = new List<string>();

                if (!dr.HasRows)
                {
                    list.Add("No results found!");
                    BenchLogger.getInstance().Info("Leaving SearchVerse method with no results");
                    return list;
                }

                //Reads the results from the sql command and then adds the results to the list
                while (dr.Read())
                {
                    list.Add(dr["TESTAMENT"].ToString());
                    list.Add(dr["BOOK"].ToString());
                    list.Add(dr["CHAPTER"].ToString());
                    list.Add(dr["VERSE"].ToString());
                    list.Add(dr["TEXT"].ToString());
                }

                BenchLogger.getInstance().Info("Leaving SearchVerse method with results");
                conn.Close();
                return list;
            }
            catch(SqlException e)
            {
                BenchLogger.getInstance().Error("Sql Exception occured in SearchVerse method: " + e.ToString());
                throw e;
            }
        }
    }
}