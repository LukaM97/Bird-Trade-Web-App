namespace NewBirdApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string NameOfCustomer { get; set; }
        public string SurnameOfCustomer { get; set; }
        public string AdressOfCustomer { get; set; }
        public string CustomerCity { get; set; }
        public long CustomerIdNumber { get; set; }
        public int Amount { get; set; }
        public Bird Bird { get; set; }
        public int BirdId { get; set; }
    }
}
