namespace RESTApi.DataBase.Models;

public class CarViewModel
{
    public string Title { get; set; } = null!;
    public string? Information { get; set; }
    public string? Description { get; set; }
    public int? TeamId { get; set; }
    public IFormFile Image { get; set; }   
}