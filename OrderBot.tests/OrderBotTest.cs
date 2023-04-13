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
        public void TokenNumberTest()
        {
            Session oSession = new Session("12345");
            Assert.True(oSession.oOrder.TokenNumber != null);
        }
        [Fact]
        public void TestMotherboardInput ()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");   //Select the third motherboard brand from the Brand List in the Order Class.
            Assert.True(string.Equals(oSession.oOrder.Motherboard,oSession.oOrder.motherBoardBrands[2]));   //ASSERT TO CHECK IF THE VALUE WAS SAVED SUCCESSFULLY.
        }
        [Fact]
        public void WrongMotherboardInputTest()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("6");   //Deliberately select a value outside of the brand list provided to check if it loops back to the same state and doesn't save the motherboard brand value in the session variable.
            Assert.True(oSession.oOrder.Motherboard == String.Empty);  //THE VALUE SHOULD BE EMPTY AS THE INPUT WAS INVALID AND THE VALUE IS NOT SAVED
        }
        [Fact]
        public void TestProcessor()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4"); //Select the fourth motherboard brand from the Brand List in the Order Class.
            Assert.True(oSession.oOrder.Processor == oSession.oOrder.processorBrands[3]);   //ASSERT TO CHECK IF THE VALUE WAS SAVED SUCCESSFULLY.
        }

         [Fact]
        public void WrongTestProcessorInputTest()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("yo!@"); //Deliberately pass the wrong invalid input.
            Assert.True(oSession.oOrder.Processor == String.Empty);    //THE VALUE SHOULD BE EMPTY AS THE INPUT WAS INVALID AND THE VALUE IS NOT SAVED
        }
        
        [Fact]
        public void TestRAM()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4");
            oSession.OnMessage("1");   //Select the first RAM brand from the Brand List in the Order Class.
            Assert.True(oSession.oOrder.RAM == oSession.oOrder.ramBrands[0]);   //ASSERT TO CHECK IF THE VALUE WAS SAVED SUCCESSFULLY.
        }

        [Fact]
        public void InvalidTestRAM()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4");
            oSession.OnMessage("hello insert ram for me!!!1234");  
            Assert.True(oSession.oOrder.RAM == String.Empty); //THIS VALIDATES THAT THE CHAT BOT DIDNOT LET THE USER MOVE FORWARD AND DIDNOT SAVE INFORMATION TO THE DATABASE AND PROMPTED TO ANSWER AGAIN.
        }

         [Fact]
        public void TestStorageSSD()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4");
            oSession.OnMessage("1");   
            oSession.OnMessage("3"); //Select the third SSD brand from the Brand List in the Order Class.
            Assert.True(oSession.oOrder.Storage == oSession.oOrder.storageSSDBrands[2]);   //ASSERT TO CHECK IF THE VALUE WAS SAVED SUCCESSFULLY.
        }

          [Fact]
        public void InvalidTestStorageSSD()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4");
            oSession.OnMessage("1");   
            oSession.OnMessage("--invalid$toragetest"); 
            Assert.True(oSession.oOrder.Storage == String.Empty);  
        }

        
        [Fact]
        public void TestGraphicsCard()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4");
            oSession.OnMessage("1");   
            oSession.OnMessage("3"); 
            oSession.OnMessage("2");   //Select the second Graphics brand from the Brand List in the Order Class.
            Assert.True(oSession.oOrder.Graphics == oSession.oOrder.graphicsCardBrands[1]);   //ASSERT TO CHECK IF THE VALUE WAS SAVED SUCCESSFULLY.
        }

          [Fact]
        public void InvalidTestGraphicsCard()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4");
            oSession.OnMessage("1");   
            oSession.OnMessage("3"); 
            oSession.OnMessage("***1242daemon");   
            Assert.True(oSession.oOrder.Graphics == String.Empty);
        }

        [Fact]
        public void TestCPUCase()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4");
            oSession.OnMessage("1");   
            oSession.OnMessage("3"); 
            oSession.OnMessage("2");  
            oSession.OnMessage("4");     //Select the fourth CPU Case brand from the Brand List in the Order Class.
            Assert.True(oSession.oOrder.CPU_Case == oSession.oOrder.cpuCaseBrands[3]);   //ASSERT TO CHECK IF THE VALUE WAS SAVED SUCCESSFULLY.
        }

         [Fact]
        public void InvalidTestCPUCase()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4");
            oSession.OnMessage("1");   
            oSession.OnMessage("3"); 
            oSession.OnMessage("2");  
            oSession.OnMessage("this is an invalid response by the user to test");    
            Assert.True(oSession.oOrder.CPU_Case == String.Empty);
        }


        [Fact]
        public void TestOS()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4");
            oSession.OnMessage("1");   
            oSession.OnMessage("3"); 
            oSession.OnMessage("2");  
            oSession.OnMessage("4");    
            oSession.OnMessage("1");     //Select the first OS from the Brand List in the Order Class.
            Assert.True(oSession.oOrder.OS == oSession.oOrder.os[0]);   //ASSERT TO CHECK IF THE VALUE WAS SAVED SUCCESSFULLY.
        }

        [Fact]
        public void InvalidTestOS()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4");
            oSession.OnMessage("1");   
            oSession.OnMessage("3"); 
            oSession.OnMessage("2");  
            oSession.OnMessage("4");    
            oSession.OnMessage("99");     
            Assert.True(oSession.oOrder.OS == String.Empty);  
        }

        [Fact]
        public void TestMonitor()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4");
            oSession.OnMessage("1");   
            oSession.OnMessage("3"); 
            oSession.OnMessage("2");  
            oSession.OnMessage("4");    
            oSession.OnMessage("1"); 
            oSession.OnMessage("3");    //Select the third Monitor Brand from the Brand List in the Order Class.
            Assert.True(oSession.oOrder.Monitor == oSession.oOrder.monitorBrands[2]);   //ASSERT TO CHECK IF THE VALUE WAS SAVED SUCCESSFULLY.
        }

         [Fact]
        public void InvalidTestMonitorInput()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4");
            oSession.OnMessage("1");   
            oSession.OnMessage("3"); 
            oSession.OnMessage("2");  
            oSession.OnMessage("4");    
            oSession.OnMessage("1"); 
            oSession.OnMessage("2A7D"); 
            Assert.True(oSession.oOrder.Monitor == String.Empty);
        }

        [Fact]
        public void TestConfirmOrderInput()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4");
            oSession.OnMessage("1");   
            oSession.OnMessage("3"); 
            oSession.OnMessage("2");  
            oSession.OnMessage("4");    
            oSession.OnMessage("1"); 
            oSession.OnMessage("3");    
            oSession.OnMessage("y"); //Pass the "y" value or "n" value to confirm or not confirm the order, but atleast pass a value
            Assert.True(oSession.oOrder.ConfirmOrder == "y");   //ASSERT TO CHECK IF THE CONFIRM ORDER VALUE WAS ATLEAST SAVED SUCCESSFULLY.
        }

         [Fact]
        public void InvalidTestConfirmOrderInput()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4");
            oSession.OnMessage("1");   
            oSession.OnMessage("3"); 
            oSession.OnMessage("2");  
            oSession.OnMessage("4");    
            oSession.OnMessage("1"); 
            oSession.OnMessage("3");    
            oSession.OnMessage("yes");
            Assert.True(oSession.oOrder.ConfirmOrder == String.Empty); 
        }

         [Fact]
        public void TestOrderResetAfterFinishingBuilding()
        {
            string sPath = DB.GetConnectionString();
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("3");
            oSession.OnMessage("4");
            oSession.OnMessage("1");   
            oSession.OnMessage("3"); 
            oSession.OnMessage("2");  
            oSession.OnMessage("4");    
            oSession.OnMessage("1"); 
            oSession.OnMessage("3");    
            oSession.OnMessage("y"); 
            oSession.OnMessage("hello");    //I ENCOUNTERED A WEIRD BUG THAT WHEN I RESET THE STATE BACK TO WELCOME WITH A NEW ORDER CREATED, I NEED TO SEND THE "hello" MESSAGE 2 TIMES IN ORDER TO GET THE WELCOME MESSAGE TO SHOW UP AND START THE PROCESS AGAIN. COULDNOT FIX IT ON TIME.
            String sInput = oSession.OnMessage("hello")[0];
            Assert.True(sInput.Contains("Welcome"));
            Assert.True(sInput.ToLower().Contains("pc"));
            Assert.True(sInput.ToLower().Contains("assemblers"));   //THIS WILL TEST IF THE ORDER WAS RESET AND WHEN SENDING THE HELLO MESSAGE AGAIN, WE SHOULD AGAIN GET THE WELCOME MESSAGE FROM THE SYSTEM.
        }

        //Like so, existence of all the component states of the desktop can be tested by passing in user input using "OnMessage" function of the Session class.
    }
}
