namespace CC.Passwordless.API.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public double Budget { get; set; }
    }
}
