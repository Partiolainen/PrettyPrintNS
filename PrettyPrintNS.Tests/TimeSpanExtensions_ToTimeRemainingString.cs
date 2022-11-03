namespace PrettyPrintNS.Tests;

[TestFixture]
// ReSharper disable once InconsistentNaming
public class TimeSpanExtensions_ToTimeRemainingString
{
    [TestCase(3661, ExpectedResult = "1 hour and 2 minutes")]
    [TestCase(3659, ExpectedResult = "1 hour and 59 seconds")]
    [TestCase(3601, ExpectedResult = "1 hour and 1 second")]
    [TestCase(3600.5, ExpectedResult = "1 hour and 1 second")]
    [TestCase(3600, ExpectedResult = "1 hour")]
    [TestCase(3599.9, ExpectedResult = "1 hour")]
    [TestCase(60.1, ExpectedResult = "1 minute and 1 second")]
    [TestCase(60, ExpectedResult = "1 minute")]
    [TestCase(59.9, ExpectedResult = "1 minute")]
    [TestCase(2, ExpectedResult = "2 seconds")]
    [TestCase(1.1, ExpectedResult = "2 seconds")]
    [TestCase(1, ExpectedResult = "1 second")]
    [TestCase(0.1, ExpectedResult = "1 second")]
    [TestCase(0, ExpectedResult = "0 seconds")]
    [Test]
    public string RoundsSmallestUnitUp(double seconds) => TimeSpan.FromSeconds(seconds).ToTimeRemainingString(2);
}