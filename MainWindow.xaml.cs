using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace EuroGUI
{
    public partial class MainWindow : Window
    {
        private MySqlConnection connection;
        private const string connectionString = "Server=localhost;Database=eurovizio;Uid=username;Pwd=password;";

        public MainWindow()
        {
            InitializeComponent();
            dataGrid.SelectedItem = dataGrid.Items[0];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT ev, eloado, cim, helyezes, pontszam FROM dal", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGrid.ItemsSource = dataTable.AsDataView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt az adatok betöltésekor: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void OnButton4Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();

                
                MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM dal WHERE orszag = 'Magyarország'", connection);
                int magyarVersenyzoSzam = Convert.ToInt32(command.ExecuteScalar());

                
                command = new MySqlCommand("SELECT MAX(helyezes) FROM dal WHERE orszag = 'Magyarország'", connection);
                int legjobbHelyezes = Convert.ToInt32(command.ExecuteScalar());

                MessageBox.Show($"Magyarországi versenyzők száma: {magyarVersenyzoSzam}\nLegjobb helyezés: {legjobbHelyezes}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a lekérdezés során: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void OnButton5Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();

                
                MySqlCommand command = new MySqlCommand("SELECT ROUND(AVG(pontszam), 2) FROM dal WHERE orszag = 'Németország'", connection);
                double atlagPontszam = Convert.ToDouble(command.ExecuteScalar());

                MessageBox.Show($"Németország átlagos pontszáma: {atlagPontszam}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a lekérdezés során: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void OnButton6Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();

                
                MySqlCommand command = new MySqlCommand("SELECT CONCAT(eloado, ' - ', cim) FROM dal WHERE cim LIKE '%Luck%'", connection);
                MySqlDataReader reader = command.ExecuteReader();

                string luckSongs = "";
                while (reader.Read())
                {
                    luckSongs += reader.GetString(0) + ", ";
                }

                reader.Close();

                MessageBox.Show("Számok, amelyekben szerepel a 'Luck' szó: " + luckSongs.TrimEnd(',', ' '));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a lekérdezés során: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void OnButton7Click(object sender, RoutedEventArgs e)
        {
            string searchKeyword = textBox.Text;

            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT cim FROM dal WHERE eloado LIKE @keyword ORDER BY eloado ASC, cim ASC", connection);
                command.Parameters.AddWithValue("@keyword", $"%{searchKeyword}%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                listBox.ItemsSource = dataTable.AsDataView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a lekérdezés során: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void OnButton8Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();

                DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;
                int ev = Convert.ToInt32(selectedRow["ev"]);

                MySqlCommand command = new MySqlCommand("SELECT datum FROM verseny WHERE ev = @ev", connection);
                command.Parameters.AddWithValue("@ev", ev);

                object result = command.ExecuteScalar();
                string versenyDatum = result != null ? result.ToString() : "Nincs adat";

                label.Content = $"Verseny dátuma: {versenyDatum}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a lekérdezés során: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}

