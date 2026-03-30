namespace AspKnP231.Models.Home
{
    public class DkViewModel
    {
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public bool AutoGenerateSalt { get; set; }
        public string? ResultDk { get; set; }
    }
}