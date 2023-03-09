using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
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

        public void Save(){
           using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        UPDATE orders
        SET size = $size
        WHERE phone = $phone
    ";
                // commandUpdate.Parameters.AddWithValue("$size", Size);
                // commandUpdate.Parameters.AddWithValue("$phone", Phone);
                int nRows = commandUpdate.ExecuteNonQuery();
                if(nRows == 0){
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(size, phone)
            VALUES($size, $phone)
        ";
                    // commandInsert.Parameters.AddWithValue("$size", Size);
                    // commandInsert.Parameters.AddWithValue("$phone", Phone);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}
