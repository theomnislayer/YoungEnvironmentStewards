using YES.Dto;
using Shouldly;

namespace YES.Tests;

[TestClass]
public class DocumentTests : BaseTest
{
    private IYESClient _YESClient;
    protected override void Setup()
    {
        _YESClient = GetService<IYESClient>();
    }

    [TestMethod]
    public async Task DocumentTest1()
    {
        DocumentMyCodeDto request = new();
        request.Messages.Add(
            new()
            {
                Role = "user",
                Content = "public int AddTwoNumbers(int a, int b) { return a+b; }"
            });

        _YESClient.Authenticate();
        var result = await _YESClient.Document(request);
        Console.WriteLine(result.ToString());
        result.ToString().ShouldContain("Add");
    }
}
