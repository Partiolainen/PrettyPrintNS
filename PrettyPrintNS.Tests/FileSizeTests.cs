using System.Globalization;

namespace PrettyPrintNS.Tests;

[TestFixture]
public class FileSizeTests
{
    private readonly CultureInfo _enUsCulture = new("en-US");

    [Test]
    public void ReturnsBytesForZeroSize()
    {
        Assert.Multiple(() =>
        {
            Assert.That(FileSize.ToLongString(0, _enUsCulture), Is.EqualTo("0 bytes"));
            Assert.That(FileSize.ToShortString(0, _enUsCulture), Is.EqualTo("0 B"));
        });
    }

    [Test]
    public void ReturnsBytesUnder1KB()
    {
        Assert.Multiple(() =>
        {
            Assert.That(FileSize.ToLongString(1, _enUsCulture), Is.EqualTo("1 byte"));
            Assert.That(FileSize.ToShortString(1, _enUsCulture), Is.EqualTo("1 B"));
            Assert.That(FileSize.ToLongString(500, _enUsCulture), Is.EqualTo("500 bytes"));
            Assert.That(FileSize.ToShortString(500, _enUsCulture), Is.EqualTo("500 B"));
        });
    }

    [Test]
    public void ReturnsKiloBytesUnder1MB()
    {
        const ulong KB = 1000;
        Assert.Multiple(() =>
        {
            Assert.That(FileSize.ToLongString(1 * KB, _enUsCulture), Is.EqualTo("1 kilobyte"));
            Assert.That(FileSize.ToShortString(1 * KB, _enUsCulture), Is.EqualTo("1 KB"));
            Assert.That(FileSize.ToLongString(500 * KB, _enUsCulture), Is.EqualTo("500 kilobytes"));
            Assert.That(FileSize.ToShortString(500 * KB, _enUsCulture), Is.EqualTo("500 KB"));
        });
    }

    [Test]
    public void ReturnsMegaBytesUnder1GB()
    {
        var MB = Convert.ToUInt64(Math.Pow(1000, 2));
        Assert.Multiple(() =>
        {
            Assert.That(FileSize.ToLongString(1 * MB, _enUsCulture), Is.EqualTo("1 megabyte"));
            Assert.That(FileSize.ToShortString(1 * MB, _enUsCulture), Is.EqualTo("1 MB"));
            Assert.That(FileSize.ToLongString(500 * MB, _enUsCulture), Is.EqualTo("500 megabytes"));
            Assert.That(FileSize.ToShortString(500 * MB, _enUsCulture), Is.EqualTo("500 MB"));
        });
    }

    [Test]
    public void ReturnsGigaBytesUnder1TB()
    {
        var GB = Convert.ToUInt64(Math.Pow(1000, 3));
        Assert.Multiple(() =>
        {
            Assert.That(FileSize.ToLongString(1 * GB, _enUsCulture), Is.EqualTo("1 gigabyte"));
            Assert.That(FileSize.ToShortString(1 * GB, _enUsCulture), Is.EqualTo("1 GB"));
            Assert.That(FileSize.ToLongString(500 * GB, _enUsCulture), Is.EqualTo("500 gigabytes"));
            Assert.That(FileSize.ToShortString(500 * GB, _enUsCulture), Is.EqualTo("500 GB"));
        });
    }

    [Test]
    public void ReturnsTeraBytesUnder1PB()
    {
        var TB = Convert.ToUInt64(Math.Pow(1000, 4));
        Assert.Multiple(() =>
        {
            Assert.That(FileSize.ToLongString(1 * TB, _enUsCulture), Is.EqualTo("1 terabyte"));
            Assert.That(FileSize.ToShortString(1 * TB, _enUsCulture), Is.EqualTo("1 TB"));
            Assert.That(FileSize.ToLongString(500 * TB, _enUsCulture), Is.EqualTo("500 terabytes"));
            Assert.That(FileSize.ToShortString(500 * TB, _enUsCulture), Is.EqualTo("500 TB"));
        });
    }

    [Test]
    public void ReturnsPetaBytesUnder1EB()
    {
        var PB = Convert.ToUInt64(Math.Pow(1000, 5));
        Assert.Multiple(() =>
        {
            Assert.That(FileSize.ToLongString(1 * PB, _enUsCulture), Is.EqualTo("1 petabyte"));
            Assert.That(FileSize.ToShortString(1 * PB, _enUsCulture), Is.EqualTo("1 PB"));
            Assert.That(FileSize.ToLongString(500 * PB, _enUsCulture), Is.EqualTo("500 petabytes"));
            Assert.That(FileSize.ToShortString(500 * PB, _enUsCulture), Is.EqualTo("500 PB"));
        });
    }

    [Test]
    public void ReturnsExaBytesUnder1000EB()
    {
        // 64-bit unsigned long can only hold 4.6 exabytes of bytes
        var EB = Convert.ToUInt64(Math.Pow(1000, 6));
        Assert.Multiple(() =>
        {
            Assert.That(FileSize.ToLongString(1 * EB, _enUsCulture), Is.EqualTo("1 exabyte"));
            Assert.That(FileSize.ToShortString(1 * EB, _enUsCulture), Is.EqualTo("1 EB"));
            Assert.That(FileSize.ToLongString(4 * EB, _enUsCulture), Is.EqualTo("4 exabytes"));
            Assert.That(FileSize.ToShortString(4 * EB, _enUsCulture), Is.EqualTo("4 EB"));
        });
    }

    [Test]
    public void DefaultStringFormatUseFewerDecimalsForLargerValues()
    {
        var MB = Convert.ToUInt64(Math.Pow(1000, 2));
        Assert.Multiple(() =>
        {
            Assert.That(FileSize.ToLongString(0, _enUsCulture), Is.EqualTo("0 bytes"));
            Assert.That(FileSize.ToLongString(Convert.ToUInt64(1.23456 * MB), _enUsCulture), Is.EqualTo("1.23 megabytes"));
            Assert.That(FileSize.ToLongString(Convert.ToUInt64(10.23456 * MB), _enUsCulture), Is.EqualTo("10.2 megabytes"));
            Assert.That(FileSize.ToLongString(Convert.ToUInt64(100.23456 * MB), _enUsCulture), Is.EqualTo("100 megabytes"));
        });
    }

    [Test]
    public void AppliesCustomStringFormatToValue()
    {
        var MB = Convert.ToUInt64(Math.Pow(1000, 2));
        const string stringFormat = "0.000";
        Assert.Multiple(() =>
        {
            Assert.That(FileSize.ToLongString(0, _enUsCulture, stringFormat), Is.EqualTo("0.000 bytes"));
            Assert.That(FileSize.ToLongString(Convert.ToUInt64(1.23456 * MB), _enUsCulture, stringFormat), Is.EqualTo("1.235 megabytes"));
            Assert.That(FileSize.ToLongString(Convert.ToUInt64(10.23456 * MB), _enUsCulture, stringFormat), Is.EqualTo("10.235 megabytes"));
            Assert.That(FileSize.ToLongString(Convert.ToUInt64(100.23456 * MB), _enUsCulture, stringFormat), Is.EqualTo("100.235 megabytes"));
        });
    }
}