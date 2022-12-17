using ADO.NET_Lesson11.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADO.NET_Lesson11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string conn =
                ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;

            using (var connection = new SqlConnection(conn))
            {
                connection.Open();

                var sql = @"SELECT Capitals.CapitalId,Capitals.Name,Capitals.CountryId,
                            Countries.CountryId, Countries.Name
                            FROM Capitals
                            INNER JOIN Countries
                            ON Capitals.CountryId = Countries.CountryId";

                var capitals = connection.Query<Capital, Country, Capital>(sql,
                    (capital, country) =>
                    {
                        capital.Country = country;
                        return capital;
                    }, splitOn: "CountryId").ToList();


                MyDataGrid.ItemsSource = capitals;


            }



        }
    }
}
