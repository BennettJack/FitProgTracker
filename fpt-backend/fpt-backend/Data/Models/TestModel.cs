using fpt_backend.Data.Models.UserModels;
namespace fpt_backend.Data.Models;

public class TestModel
{
    public int Id { get; set; }
    public string TestName { get; set; }

    public void TestMethod()
    {
        var user = new User();
    }
}