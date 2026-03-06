using Xunit;

public class PersonTests
{
    [Fact]
    public void FullName_ReturnsExpectedFormat()
    {
        var p = new Person("Ishan", "Soni", 20);
        Assert.Equal("Soni, Ishan", p.FullName());
    }

    [Theory]
    [InlineData(18, true)]
    [InlineData(21, true)]
    [InlineData(17, false)]
    public void IsAdult_ReturnsTrue_WhenAge18OrMore(int age, bool expected)
    {
        var p = new Person("Test", "User", age);
        Assert.Equal(expected, p.IsAdult());
    }
}