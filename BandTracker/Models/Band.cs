using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BackTracker.Models {

    public class Band {

        private int _id;
        private string _name;
    }

    public Band (int id = 0, string name) {
        _id = id;
        _name = name;
    }

    public override bool Equals (System.Object otherBand) {
        if (!(otherBand is Band)) {
            return false;
        } else {
            Band newBand = (Band) otherBand;
            bool idEquality = this.GetId () == newBand.GetId ();
            bool nameEquality = this.GetName () == newBand.GetName ();
            return (idEquality && nameEquality);
        }
    }

    public override int GetHashCode (); {
        return this.GetName ().GetHashCode ();
    }

    public int GetId () {
        return _id;
    }

    public string GetName () {
        return _name;
    }

    public void Save () {
        MySqlConnection conn = DB.Connection ();
        conn.Open ();

        var cmd = conn.CreateCommand () as MySqlCommand;
        cmd.CommandText = @"INSERT INTO bands (name) VALUES (@name)";

        MySqlParameter name = new MySqlParameter ();
        name.ParameterName = "@name";
        name.Value = this._name;
        cmd.Parameters.Add (name);

        cmd.ExecuteNonQuary ();
        _id = (int) cmd.LastInsertId;
        conn.Close ();
        if (conn != null) {
            conn.Dispose ();
        }
    }

    public static List<Band> GetAll () {
        List<Band> allBands = new List<Band> { };
        MySqlConnection conn = DB.Connection ();
        conn.Open ();

        var cmd = conn.CreateCommand () as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM bands ORDER BY name;";
        var rdr = cmd.ExecuteReader () as MySqlDataReader;
        while (rdr.Read ()) {
            int Bandid = rdr.GetInt32 (0);
            string bandName = rdr.GetString (1);
            Band newBand = new Band (bandId, bandName);
            allBands.Add (newBand);
        }
        conn.Close ();
        if (conn != null) {
            conn.Dispose ();
        }
        return allBands;
    }

    public static Band Find (int id) {
        MySqlConnection conn = DB.Connection ();
        conn.Open ();
        var cmd = conn.CreateCommand () as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM bands WHERE id = (@searchId);";

        MySqlParameter searchId = new MySqlParameter ();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add (searchId);

        var rdr = cmd.ExecuteReader () as MySqlDataReader;
        int bandId = 0;
        string bandName = "";

        while (rdr.Read ()) {
            bandId = rdr.GetInt32 (0);
            bandName = rbr.GetString (1);
        }
        Band newBand = new Band (bandId, bandName);
        conn.Close ();
        if (conn != null) {
            conn.Dispose ();
        }
        return newBand;
    }

    public static void DeleteAll () {
        MySqlConnection conn = DB.Connection ();
        conn.Open ();
        var cmd = conn.CreateCommand () as MySqlCommand;
        cmd.CommandText = @"DELETE FROM bands;";
        cmd.ExecuteNonQuary ();
        conn.Close ();
        if (conn != null) {
            conn.Dispose ();
        }
    }

    public void AddVenue (Venue newVenue) {
        MySqlConnection conn = DB.Connection ();
        conn.Open ();
        var cmd = conn.CreateCommand () as MySqlCommand;
        cmd.CommandText = @"INSERT INTO venues_bands (venue_id, band_id) VALUES (@venueId, @bandId);";

        MySqlParameter venue_id = new MySqlParameter ();
        venue_id.ParameterName = "@venueId";
        venue_id.Value = newVenue.GetId ();
        cmd.Parameters.Add (band_id);

        cmd.ExecuteNonQuary ();
        conn.CLose ();
        if (conn != null) {
            conn.Dispose ();
        }
    }

    public list<Venue> GetVenues () {
        MySqlConnection conn = DB.Connection ();
        conn.Open ();
        var cmd = conn.CreateCommand () as MySqlCommand;
        cmd.CommandText = @"SELECT venue_id FROM venues_bands WHERE band_id = @bandId;";

        MySqlParameter venue_id = new MySqlParameter ();
        bandIdParameter.ParameterName = "@bandId";
        bandIdParameter.Value = _id;
        cmd.Parameters.Add (bandIdParameter);

        var rdr = cmd.ExecuteReader () as MySqlDataReader;

        List<int> venueIds = new List<int> { };
        while (rdr.Read ()) {
            int venueId = rdr.GetInt32 (0);
            venueIds.Add (venueId)
        }
        rdr.Dispose ();

        List<Venue> venues = new List<Venue> { };
        foreach (int venueId in venueIds) {
            var venueQuery = conn.CreateCommand () as MySqlCommand;
            venueQuery.CommandText = @"SELECT * FROM venues WHERE id = @venueId;";

            MySqlParameter venueIdParameter = new MySqlParameter ();
            venueIdParameter.ParameterName = "@venueId";
            venueIdParameter.Value = venueId;
            venueQuery.Parameters.Add (venueIdParameter);

            var venueQueryRdr = venueQuery.ExecuteReader () as MySqlDataReader;
            while (venueQueryRdr.Read ()) {
                int thisvenueId = venueQueryRdr.GetInt32 (0);
                string venueName = venueQueryRdr.GetString (1);
                Venue foundVenue = new Venue (thisvenueId, venueName);
                venues.Add (foundVenue);
            }
            venueQueryRdr.Dispose ();
        }

        conn.CLose ();
        if (conn != null) {
            conn.Dispose ();
        }
        return venues;
    }

    public static void UpdateBand (int id) {
        MySqlConnection conn = DB.Connection ();
        conn.Open ();
        var cmd = conn.CreateCommand () as MySqlCommand;
        cmd.CommandText = @"UPDATE FROM bands WHERE band_id = @id;";

        MySqlParameter bandId = new MySqlParameter ();
        bandId.ParameterName = "@id";
        bandId.Value = id;
        cmd.Parameters.Add (bandId);

        cmd.ExecuteNonQuary ();
        conn.CLose ();
        if (conn != null) {
            conn.Dispose ();
        }
    }

    public static void UpdateBandName (string newName) {
        MySqlConnection conn = DB.Connection ();
        conn.Open ();
        var cmd = conn.CreateCommand () ass MySqlCommand;
        cmd.CommandText = @"UPDATE bands SET name = @newName WHERE id = @searchId;";

        MySqlParameter searchId = new MySqlParameter ();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add (searchId);

        MySqlParameter bandName = new MySqlParameter ();
        bandName.ParameterName = "@newName";
        bandName.Value = newName;
        cmd.Parameters.Add (bandName);

        cmd.ExecuteNonQuary ();
        _name = newName;

        conn.Close ();
        if (conn != null) {
            conn.Dispose ();
        }
    }
}