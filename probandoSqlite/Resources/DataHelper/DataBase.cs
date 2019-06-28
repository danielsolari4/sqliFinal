﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using probandoSqlite.Resources.Model;
using SQLite;

namespace probandoSqlite.Resources.DataHelper
{
    public class DataBase
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool createDataBase() {

            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db"))) {

                    connection.CreateTable<Model.Person>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {

                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool InsertIntoTablePerson(Person person)
        {

            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {

                    connection.Insert(person);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {

                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public List<Person> selectTablePerson()
        {

            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {

                    return connection.Table<Person>().ToList();
                    
                }
            }
            catch (SQLiteException ex)
            {

                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public bool updateTablePerson(Person person)
        {

            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {

                    connection.Query<Person>("UPDATE Person set Name=?,Age=?,Email=? Where Id=?",person.Name,person.Age,person.Email,person.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {

                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool deleteTablePerson(Person person)
        {

            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    //var pr = connection.Table<Person>().First(x => x.Id == person.Id);
                    connection.Delete(person);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {

                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool selectQueryTablePerson(int Id)
        {

            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {

                    connection.Query<Person>("SELECT * FROM Person Where Id=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {

                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

    }
}