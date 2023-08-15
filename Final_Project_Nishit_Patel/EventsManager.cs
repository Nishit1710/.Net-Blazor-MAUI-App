using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Final_Project_Nishit_Patel
{
    public class EventsManager
    {
        private string _connectionString;

        public EventsManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<ConcertEvent> SearchConcertEventsByTicketCost(decimal ticketCost)
        {
            List<ConcertEvent> events = new List<ConcertEvent>();

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            string selectQuery = "SELECT * FROM concerts_events WHERE concert_ticket_cost <= @TicketCost";
            using MySqlCommand command = new MySqlCommand(selectQuery, connection);
            command.Parameters.AddWithValue("@TicketCost", ticketCost);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ConcertEvent concert = new ConcertEvent
                {
                    TicketCost = reader.IsDBNull(reader.GetOrdinal("concert_ticket_cost")) ? 0 : reader.GetDecimal("concert_ticket_cost"),
                    Genre = reader.IsDBNull(reader.GetOrdinal("concert_genre")) ? string.Empty : reader.GetString("concert_genre"),
                    Artist = reader.IsDBNull(reader.GetOrdinal("concert_artist")) ? string.Empty : reader.GetString("concert_artist"),
                    Date = reader.IsDBNull(reader.GetOrdinal("concert_date")) ? DateTime.MinValue : reader.GetDateTime("concert_date"),
                    Time = reader.IsDBNull(reader.GetOrdinal("concert_time")) ? TimeSpan.Zero : reader.GetTimeSpan("concert_time"),
                    Venue = reader.IsDBNull(reader.GetOrdinal("concert_venue")) ? string.Empty : reader.GetString("concert_venue"),
                    City = reader.IsDBNull(reader.GetOrdinal("concert_city")) ? string.Empty : reader.GetString("concert_city"),
                    Description = reader.IsDBNull(reader.GetOrdinal("concert_description")) ? string.Empty : reader.GetString("concert_description")
                };
                events.Add(concert);
            }

            connection.Close();
            return events;
        }

        public List<ConcertEvent> GetConcertEvents()
        {
            List<ConcertEvent> events = new List<ConcertEvent>();

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            string selectQuery = "SELECT * FROM concerts_events";
            using MySqlCommand command = new MySqlCommand(selectQuery, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ConcertEvent concert = new ConcertEvent();

                if (!reader.IsDBNull(reader.GetOrdinal("concert_ticket_cost")))
                    concert.TicketCost = reader.GetDecimal("concert_ticket_cost");

                if (!reader.IsDBNull(reader.GetOrdinal("concert_genre")))
                    concert.Genre = reader.GetString("concert_genre");

                if (!reader.IsDBNull(reader.GetOrdinal("concert_artist")))
                    concert.Artist = reader.GetString("concert_artist");

                if (!reader.IsDBNull(reader.GetOrdinal("concert_date")))
                    concert.Date = reader.GetDateTime("concert_date");

                if (!reader.IsDBNull(reader.GetOrdinal("concert_time")))
                    concert.Time = reader.GetTimeSpan("concert_time");

                if (!reader.IsDBNull(reader.GetOrdinal("concert_venue")))
                    concert.Venue = reader.GetString("concert_venue");

                if (!reader.IsDBNull(reader.GetOrdinal("concert_city")))
                    concert.City = reader.GetString("concert_city");

                if (!reader.IsDBNull(reader.GetOrdinal("concert_description")))
                    concert.Description = reader.GetString("concert_description");

                events.Add(concert);
            }

            connection.Close();
            return events;
        }
        public void AddConcertEvent(ConcertEvent concert)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            string insertQuery = "INSERT INTO concerts_events (concert_ticket_cost, concert_genre, concert_artist, concert_date, concert_time, concert_venue, concert_city, concert_description) " +
                "VALUES (@TicketCost, @Genre, @Artist, @Date, @Time, @Venue, @City, @Description)";
            using MySqlCommand command = new MySqlCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@TicketCost", concert.TicketCost);
            command.Parameters.AddWithValue("@Genre", concert.Genre);
            command.Parameters.AddWithValue("@Artist", concert.Artist);
            command.Parameters.AddWithValue("@Date", concert.Date);
            command.Parameters.AddWithValue("@Time", concert.Time);
            command.Parameters.AddWithValue("@Venue", concert.Venue);
            command.Parameters.AddWithValue("@City", concert.City);
            command.Parameters.AddWithValue("@Description", concert.Description);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void UpdateConcertEvent(ConcertEvent concert)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            string updateQuery = "UPDATE concerts_events SET concert_ticket_cost = @TicketCost, concert_genre = @Genre, concert_date = @Date, " +
                "concert_time = @Time, concert_venue = @Venue, concert_city = @City, concert_description = @Description WHERE concert_artist = @Artist";
            using MySqlCommand command = new MySqlCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@TicketCost", concert.TicketCost);
            command.Parameters.AddWithValue("@Genre", concert.Genre);
            command.Parameters.AddWithValue("@Date", concert.Date);
            command.Parameters.AddWithValue("@Time", concert.Time);
            command.Parameters.AddWithValue("@Venue", concert.Venue);
            command.Parameters.AddWithValue("@City", concert.City);
            command.Parameters.AddWithValue("@Description", concert.Description);
            command.Parameters.AddWithValue("@Artist", concert.Artist);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}