namespace NewBirdApp.Models
{
    public class Bird
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Gender Gender { get; set; }
        public int GenderId { get; set; }
    }
}
