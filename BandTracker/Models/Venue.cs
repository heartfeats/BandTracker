using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BackTracker.Models {

    public class Venue {

        private int _id;
        private string _name;
    }

    public Venue (int id = 0, string name) {
        _id = id;
        _name = name;
    }

    public override bool Equals (System.Object otherVenue) {
        if (!(otherVenue is Venue)) {
            return false;
        } else {
            Venue newVenue = (Venue) otherVenue;
            return this.GetId ().Equals (newVeneue.GetId ());
        }
    }

    public override int GetHashCode (); {
        return this.GetId ().GetHashCode ();
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
        cmd.CommandText = @"INSERT INTO venues (name) VALUES (@name)";

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

    public static List<Venue> GetAll () {
        List<Venue> allVenues = new List<Venue> { };
        MySqlConnection conn = DB.Connection ();
        conn.Open ();

        var cmd = conn.CreateCommand () as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM venues ORDER BY name;";
        var rdr = cmd.ExecuteReader () as MySqlDataReader;
        while (rdr.Read ()) {
            int venueId = rdr.GetInt32 (0);
            string venueName = rdr.GetString (1);
            Venue newVeneue = new Venue (venueId, venueName);
            allVenues.Add (newVenue);
        }
        conn.Close ();
        if (conn != null) {
            conn.Dispose ();
        }
        return allVenues;
    }

    public static Venue Find (int id) {
        MySqlConnection conn = DB.Connection ();
        conn.Open ();
        var cmd = conn.CreateCommand () as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM venues WHERE id = (@searchId);";

        MySqlParameter searchId = new MySqlParameter ();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add (searchId);

        var rdr = cmd.ExecuteReader () as MySqlDataReader;
        int venueId = 0;
        string venueName = "";

        while (rdr.Read ()) {
            venueId = rdr.GetInt32 (0);
            venueName = rbr.GetString (1);
        }
        Venue newVenue = new Venue (venueId, venueName);
        conn.Close ();
        if (conn != null) {
            conn.Dispose ();
        }
        return newVenue;
    }

    public static void DeleteAll () {
        MySqlConnection conn = DB.Connection ();
        conn.Open ();
        var cmd = conn.CreateCommand () as MySqlCommand;
        cmd.CommandText = @"DELETE FROM venues;";
        cmd.ExecuteNonQuary ();
        conn.Close ();
        if (conn != null) {
            conn.Dispose ();
        }
    }

    public void AddBand (Band newBand) {
        MySqlConnection conn = DB.Connection ();
        conn.Open ();
        var cmd = conn.CreateCommand () as MySqlCommand;
        cmd.CommandText = @"INSERT INTO venues_bands (venue_id, band_id) VALUES (@venueId, @bandId);";

        MySqlParameter venue_id = new MySqlParameter ();
        venue_id.ParameterName = "@venueId";
        venue_id.Value = _id;
        cmd.Parameters.Add (venue_id);

        MySqlParameter band_id = new MySqlParameter ();
        band_id.ParameterName = "@bandId";
        band_id.Value = newBand.GetId();
        cmd.Parameters.Add (band_id);

        cmd.ExecuteNonQuary ();
        conn.Close ();
        if (conn != null) {
            conn.Dispose ();
        }
    }

    public void DeleteVenue () {
        MySqlConnection conn = DB.Connection ();
        conn.Open ();

        MySqlCommand cmd = new MySqlCommand("DELETE FROM venues WHERE id = @venueId; DELETE FROM venues_bands WHERE venue_id = @venueId;");
        venueIdParameter.ParameterName = "@venueId";
        venueIdParameter.Value = this.GetId();
        cmd.Parameters.Add (venueIdParameter)

        cmd.ExecuteNonQuary ();

        conn.Close ();
        if (conn != null) {
            conn.Dispose ();
        }
    }

    public list<Band> GetBands () {
        MySqlConnection conn = DB.Connection ();
        conn.Open ();
        var cmd = conn.CreateCommand () as MySqlCommand;
        cmd.CommandText = @"SELECT bands.* FROM venues JOIN veunues_bands on (venues.id = venues_bands.venue_id) JOIN bands ON (venues_bands.band_id) WHERE venues.id = @venueId;";

        MySqlParameter venue_id = new MySqlParameter ();
        venueId.ParameterName = "@venueId";
        venueId.Value = _id;
        cmd.Parameters.Add (venueId);

        var rdr = cmd.ExecuteReader () as MySqlDataReader;

        List<Band> venueBands = new List<Band> {};
        while (rdr.Read ()) {
            int bandId = rdr.GetInt32 (0);
            string bandName = rdr.GetString(1);
            Band newBand (bandId, bandName);
            venueBands.Add (newBand);
        }

        conn.CLose ();
        if (conn != null) {
            conn.Dispose ();
        }
        return venueBands;
    }

    public static void UpdateVenue (int id) {
        MySqlConnection conn = DB.Connection ();
        conn.Open ();
        var cmd = conn.CreateCommand () as MySqlCommand;
        cmd.CommandText = @"UPDATE FROM venues WHERE venue_id = @id;";

        MySqlParameter venueId = new MySqlParameter ();
        venueId.ParameterName = "@id";
        venueId.Value = id;
        cmd.Parameters.Add (venueId);

        cmd.ExecuteNonQuary ();
        conn.Close ();
        if (conn != null) {
            conn.Dispose ();
        }
    }

    public static void UpdateVenueName (string newName) {
        MySqlConnection conn = DB.Connection ();
        conn.Open ();
        var cmd = conn.CreateCommand () ass MySqlCommand;
        cmd.CommandText = @"UPDATE venues SET name = @newName WHERE id = @searchId;";

        MySqlParameter searchId = new MySqlParameter ();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add (searchId);

        MySqlParameter venueName = new MySqlParameter ();
        venueName.ParameterName = "@newName";
        venueName.Value = newName;
        cmd.Parameters.Add (venueName);

        cmd.ExecuteNonQuary ();
        _name = newName;

        conn.Close ();
        if (conn != null) {
            conn.Dispose ();
        }
    }
}