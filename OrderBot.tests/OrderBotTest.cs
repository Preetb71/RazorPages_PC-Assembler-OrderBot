using System;
using System.IO;
using Xunit;
using OrderBot;
using Microsoft.Data.Sqlite;

namespace OrderBot.tests
{
    public class OrderBotTest
    {
        public OrderBotTest()
        {
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        DELETE FROM pcOrders
    ";
                commandUpdate.ExecuteNonQuery();

            }
        }
        [Fact]
        public void Test1()
        {

        }
        [Fact]
        public void TestWelcome()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            Assert.True(sInput.Contains("Welcome"));
        }
        [Fact]
        public void TestWelcomPerformance()
        {
            DateTime oStart = DateTime.Now;
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            DateTime oFinished = DateTime.Now;
            long nElapsed = (oFinished - oStart).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + nElapsed);
            Assert.True(nElapsed < 10000);
        }
        [Fact]
        public void TestPCAssemblerIntro()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            Assert.True(sInput.ToLower().Contains("pc"));
            Assert.True(sInput.ToLower().Contains("assemblers"));
        }
        [Fact]
        public void TestMotherboard()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[1];
            Assert.True(sInput.ToLower().Contains("motherboard"));
        }
        [Fact]
        public void TestProcessor()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            String sInput = oSession.OnMessage("Razor Motherboard")[0]; //OnMessage is passed as Razor Motherboard as an answer to the first question by the system, so that the next state is called and existence or processor can be tested.
            Assert.True(sInput.ToLower().Contains("processor"));
        }
        [Fact]
        public void TestRAM()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("Razor Motherboard");
            String sInput = oSession.OnMessage("Intel I7 11th Gen Processor")[0];
            Assert.True(sInput.ToLower().Contains("ram"));
        }

        //Like so, existence of all the component states of the desktop can be tested by passing in user input using "OnMessage" function of the Session class.
    }
}
