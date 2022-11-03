using System.Globalization;

namespace PrettyPrintNS.Tests;

[TestFixture]
// ReSharper disable once InconsistentNaming
public class TimeSpanExtensions_ToPrettyString
{
    [Test]
    public void ShowsAtLeastOneUnitValue()
    {
        var t = new TimeSpan(2, 3, 4, 5, 6);
        Assert.Multiple(() =>
        {
            Assert.That(t.ToPrettyString(0), Is.EqualTo("2 days"));
            Assert.That(t.ToPrettyString(-1), Is.EqualTo("2 days"));
        });
    }

    [Test]
    public void RepresentsTimeSpanZeroInLowestConfiguredUnit()
    {
        Assert.Multiple(() =>
        {
            Assert.That(TimeSpan.Zero.ToPrettyString(3), Is.EqualTo("0 seconds"));
            Assert.That(TimeSpan.Zero.ToPrettyString(3, lowestUnit: TimeSpanUnit.Hours), Is.EqualTo("0 hours"));
        });
    }

    [Test]
    public void ClampsToHighestAndLowestUnit()
    {
        var t = new TimeSpan(2, 3, 4, 5, 6);
        Assert.That(t.ToPrettyString(4, highestUnit: TimeSpanUnit.Hours, lowestUnit: TimeSpanUnit.Minutes), Is.EqualTo("3 hours and 4 minutes"));
    }

    [TestCase(UnitStringRepresentation.Long, ExpectedResult = "3 hours and 4 minutes")]
    [TestCase(UnitStringRepresentation.Short, ExpectedResult = "3 hrs 4 mins")]
    [TestCase(UnitStringRepresentation.CompactWithSpace, ExpectedResult = "3h 4m")]
    [TestCase(UnitStringRepresentation.Compact, ExpectedResult = "3h4m")]
    public string ShowsUnitValuesInConfiguredUnitRepresentation(UnitStringRepresentation rep)
    {
        var t = new TimeSpan(3, 4, 0);
        return t.ToPrettyString(2, rep);
    }

    [Test]
    public void UsesSpecifiedCulture()
    {
        var t = new TimeSpan(3, 4, 0);
        Assert.Multiple(() =>
        {
            Assert.That(t.ToPrettyString(4, formatProvider: CultureInfo.GetCultureInfo("en-US")), Is.EqualTo("3 hours and 4 minutes"));
            Assert.That(t.ToPrettyString(4, formatProvider: CultureInfo.GetCultureInfo("zh-CN")), Is.EqualTo("3 hours and 4 minutes"));
        });
    }

    [Test]
    [TestCase(1, ExpectedResult = "2 days")]
    [TestCase(2, ExpectedResult = "2 days and 3 hours")]
    [TestCase(3, ExpectedResult = "2 days, 3 hours and 5 seconds")]
    [TestCase(4, ExpectedResult = "2 days, 3 hours, 5 seconds and 6 milliseconds")]
    public string ReturnsNoMoreThanMaxUnitValues(int maxUnitsGroups)
    {
        var t = new TimeSpan(2, 3, 0, 5, 6);
        return t.ToPrettyString(maxUnitsGroups, lowestUnit: TimeSpanUnit.Milliseconds);
    }

    [TestCase(3.5, IntegerRounding.Down, ExpectedResult = "3 hours")]
    [TestCase(2.9, IntegerRounding.Down, ExpectedResult = "2 hours")]
    [TestCase(3.5, IntegerRounding.Up, ExpectedResult = "4 hours")]
    [TestCase(3.4, IntegerRounding.Up, ExpectedResult = "4 hours")]
    [TestCase(3.5, IntegerRounding.ToNearestOrUp, ExpectedResult = "4 hours")]
    [TestCase(3.4, IntegerRounding.ToNearestOrUp, ExpectedResult = "3 hours")]
    public string RoundsLowestUnitAsSpecified(double hours, IntegerRounding rounding)
    {
        return TimeSpan.FromHours(hours).ToPrettyString(lowestUnitRounding: rounding);
    }

    [TestCase(24*3600 - 1, ExpectedResult = "1 day")]
    [TestCase(3600 - 1, ExpectedResult = "1 hour")]
    [TestCase(60 - 0.1, ExpectedResult = "1 minute")]
    public string RoundsUnitUp(double seconds)
    {
        return TimeSpan.FromSeconds(seconds).ToPrettyString(lowestUnitRounding: IntegerRounding.Up);
    }

    [TestCase(3600 - 1, ExpectedResult = "59 minutes")]
    [TestCase(59.9, ExpectedResult = "59 seconds")]
    [TestCase(0.9, ExpectedResult = "0 seconds")]
    public string RoundsUnitDown(double seconds)
    {
        return TimeSpan.FromSeconds(seconds).ToPrettyString(lowestUnitRounding: IntegerRounding.Down);
    }
}