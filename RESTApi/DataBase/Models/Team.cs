namespace RESTApi.DataBase.Models
{
    public partial class Team
    {
        public Team()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; }
    }
}
