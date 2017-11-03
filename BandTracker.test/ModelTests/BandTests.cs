using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BandTracker.Models;

namespace BandTracker.Tests
{
    
    [TestClass]

    public class BandTests : IDisposable {

        public BandTests () {
            DCConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=bandtracker_test;";
        }

        public void Dispose () {
            Band.DeleteAll ();
            Venue.DeleteAll ();
        }

        
    }
}