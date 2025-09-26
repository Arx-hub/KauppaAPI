using Microsoft.Data.Sqlite;

namespace KauppaAPI;

public record Asiakas (int Id, string Nimi);

class Kauppakanta
{
    //Tietokantayhteyttä _connectionString
    private static string _connectionString = "Data Source=kauppa.db";

    //rakentaja
    public Kauppakanta()
    {
        //Luodaan tietokantayhteys
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            //Luodaan Asiakkaat-taulu, jos sitä ei ole
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS Asiakkaat (id INTEGER PRIMARY KEY, nimi TEXT)";
            tableCmd.ExecuteNonQuery();
        }
    }

    //Metodi, jolla lisätään asiakas tietokantaan
    public void LisaaAsiakas(string nimi)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            //Lisätään asiakas tietokantaan
            var insertCmd = connection.CreateCommand();
            insertCmd.CommandText = @"INSERT INTO Asiakkaat (nimi) VALUES ($nimi)";
            insertCmd.Parameters.AddWithValue("$nimi", nimi);
            insertCmd.ExecuteNonQuery();
        }
    }

    public Dictionary<int, string> HaeAsiakkaat()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            //Haetaan kaikki asiakkaat tietokannasta
            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT * FROM Asiakkaat";

            using (var reader = selectCmd.ExecuteReader())
            {
                var asiakkaat = new Dictionary<int, string>();
                while (reader.Read())
                {
                    asiakkaat.Add(reader.GetInt32(0), reader.GetString(1));
                }
                return asiakkaat;
            }
        }
    }
}