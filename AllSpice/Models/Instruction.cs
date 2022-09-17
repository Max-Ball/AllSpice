namespace AllSpice.Models
{
  public class Instruction
  {
    public int Id { get; set; }
    public int? Step { get; set; }
    public string Body { get; set; }
    public int RecipeId { get; set; }
  }
}