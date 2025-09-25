namespace fpt_backend.Data.Models;

public class BaseModel
{
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string? CreatedBy { get; set; }
}