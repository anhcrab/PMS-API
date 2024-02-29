namespace api.Interfaces.Core
{
  public interface ISoftDelete
  {
    public bool IsDeleted { get; set; }
    public void Undo()
    {
      IsDeleted = false;
    }
  }
}