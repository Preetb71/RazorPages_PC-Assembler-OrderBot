using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string _tokenNumber = String.Empty;
        private string _size = String.Empty;
        private string _phone = String.Empty;
        private string _motherboard = String.Empty;
        private string _processor = String.Empty;
        private string _ram = String.Empty;
        private string _storage = String.Empty;
        private string _graphics = String.Empty;
        private string _cpuCase = String.Empty;
        private string _os = String.Empty;
        private string _monitor = String.Empty;

        private string _confirmOrder = String.Empty;

        public String TokenNumber{
            get => _tokenNumber;
            set => _tokenNumber = value;
        }

        public string Motherboard{
            get => _motherboard;
            set => _motherboard = value;
        }

        public string Processor{
            get => _processor;
            set => _processor = value;
        }

        public string RAM{
            get => _ram;
            set => _ram = value;
        }

        public string Storage{
            get => _storage;
            set => _storage = value;
        }
        public string Graphics{
            get => _graphics;
            set => _graphics = value;
        }

        public string CPU_Case{
            get => _cpuCase;
            set => _cpuCase = value;
        }

        public string OS{
            get => _os;
            set => _os = value;
        }

        public string Monitor{
            get => _monitor;
            set => _monitor = value;
        }

        public string ConfirmOrder{
            get => _confirmOrder;
            set => _confirmOrder = value;
        }

        public void Save()
        {
            //PREET - I HAVE CREATED A NEW TABLE INSIDE THE ORDERS.DB DATABASE CONTAINING ALL OF THE PC INFORMATION WHICH WILL GET INSERTED AND UPDATED AS
            //THE USER WILL GIVE THEIR RESPONSES. IT WAS EASIER FOR ME TO JUST CREATE A NEW DATABASE THAN TO REMOVE THE EXISTING DATA FROM THE ORDERS TABLE.
            //HERE I HAVE USED TOKEN NUMBER AS THE PRIMARY KEY AND IDENTIFIER FOR THE ORDER.
           using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
                UPDATE pcOrders
                SET motherboard = $motherboard, processor = $processor, ram = $ram, storageSSD = $storageSSD,
                graphicsCard = $graphicsCard, cpuCase = $cpuCase, os = $os, monitor = $monitor, isOrderConfirmed = $isOrderConfirmed
                WHERE tokenId = $_tokenNumber";
                
                commandUpdate.Parameters.AddWithValue("$_tokenNumber", TokenNumber);
                commandUpdate.Parameters.AddWithValue("$motherboard", Motherboard);
                commandUpdate.Parameters.AddWithValue("$processor", Processor);
                commandUpdate.Parameters.AddWithValue("$ram", RAM);
                commandUpdate.Parameters.AddWithValue("$storageSSD", Storage);
                commandUpdate.Parameters.AddWithValue("$graphicsCard", Graphics);
                commandUpdate.Parameters.AddWithValue("$cpuCase", CPU_Case);
                commandUpdate.Parameters.AddWithValue("$os", OS);
                commandUpdate.Parameters.AddWithValue("$monitor", Monitor);
                commandUpdate.Parameters.AddWithValue("$isOrderConfirmed", ConfirmOrder);

                int nRows = commandUpdate.ExecuteNonQuery();
                if(nRows == 0)
                {
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
                    INSERT INTO pcOrders(tokenId, motherboard, processor, ram, storageSSD, graphicsCard, cpuCase, os, monitor, isOrderConfirmed)
                    VALUES($_tokenNumber, $motherboard, $processor, $ram, $storageSSD, $graphicsCard, $cpuCase, $os, $monitor, $isOrderConfirmed)";
                    commandInsert.Parameters.AddWithValue("$motherboard", Motherboard);
                    commandInsert.Parameters.AddWithValue("$processor", Processor);
                    commandInsert.Parameters.AddWithValue("$ram", RAM);
                    commandInsert.Parameters.AddWithValue("$storageSSD", Storage);
                    commandInsert.Parameters.AddWithValue("$graphicsCard", Graphics);
                    commandInsert.Parameters.AddWithValue("$cpuCase", CPU_Case);
                    commandInsert.Parameters.AddWithValue("$os", OS);
                    commandInsert.Parameters.AddWithValue("$monitor", Monitor);
                    commandInsert.Parameters.AddWithValue("$isOrderConfirmed", ConfirmOrder);
                    commandInsert.Parameters.AddWithValue("$_tokenNumber", TokenNumber);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();
                }
            }

        }
    }
}
