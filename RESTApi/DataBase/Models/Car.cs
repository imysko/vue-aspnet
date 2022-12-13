using System.ComponentModel.DataAnnotations.Schema;

namespace RESTApi.DataBase.Models
{
    public partial class Car
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Information { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }

        [ForeignKey("Team")]
        public int? TeamId { get; set; }
        public virtual Team? Team { get; set; }
    }
}
