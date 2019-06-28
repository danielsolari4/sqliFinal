using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using probandoSqlite.Resources.Model;
using System.Collections.Generic;
using probandoSqlite.Resources.DataHelper;
using probandoSqlite.Resources;

namespace probandoSqlite
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        ListView lstData;
        List<Person> lstSource = new List<Person>();
        DataBase db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //Create DataBase
            db = new DataBase();
            db.createDataBase();

            lstData = FindViewById<ListView>(Resource.Id.listView);

            var edtName = FindViewById<EditText>(Resource.Id.edtName);
            var edtAge = FindViewById<EditText>(Resource.Id.edtAge);
            var edtEmail = FindViewById<EditText>(Resource.Id.edtEmail);

            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            var btnDelete = FindViewById<Button>(Resource.Id.btnDelete);
            
            //LoadData
            LoadData();

            //Event
            btnAdd.Click += delegate
            {
                Person person = new Person()
                {
                    Name = edtName.Text,
                    Age = int.Parse(edtAge.Text),
                    Email = edtEmail.Text
                };

                db.InsertIntoTablePerson(person);
                LoadData();
            };

            btnEdit.Click += delegate
            {
                Person person = new Person()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,
                    Age = int.Parse(edtAge.Text),
                    Email = edtEmail.Text
                };

                db.updateTablePerson(person);
                LoadData();
            };

            btnDelete.Click += delegate
            {
                Person person = new Person()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,
                    Age = int.Parse(edtAge.Text),
                    Email = edtEmail.Text
                };

                db.deleteTablePerson(person);
                LoadData();
            };


            lstData.ItemClick += (s,e) => {
                for (int i = 0; i < lstData.Count; i++)
                {
                    if (e.Position == i) {

                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.DarkGray);

                    }
                    else
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                }

                var txtName = e.View.FindViewById<TextView>(Resource.Id.textView1);
                var txtAge = e.View.FindViewById<TextView>(Resource.Id.textView2);
                var txtEmail = e.View.FindViewById<TextView>(Resource.Id.textView3);

                edtName.Text = txtName.Text;
                edtName.Tag = e.Id;
                edtAge.Text = txtName.Text;

                edtEmail.Text = txtEmail.Text;

            };
        }

        private void LoadData() {

            lstSource = db.selectTablePerson();
            var adapter = new ListViewAdapter(this, lstSource);
            lstData.Adapter = adapter;
        }
    }
}