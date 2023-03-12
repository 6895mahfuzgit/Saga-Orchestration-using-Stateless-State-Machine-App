namespace TransportServiceApp.Core.Models
{
    public class Transpost
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string OrderRefNo { get; set; }
        public string Status { get; set; }

    }

}
