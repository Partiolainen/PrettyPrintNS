# PrettyPrintNS
[![NuGet](https://img.shields.io/nuget/v/PrettyPrintNS.svg)](https://www.nuget.org/packages/PrettyPrintNS/)

.Net Standard port of [PrettyPrintNet](https://github.com/angularsen/PrettyPrintNet)

==============

Human friendly, textual representations of TimeSpan and file size.

Install
=======
`dotnet add package PrettyPrintNS`

Features
========
## TimeSpan.ToPrettyString()
```csharp
var t = new TimeSpan(hours: 3, minutes: 4, seconds: 0);

// Default is 1 unit, long representation, use units from days to seconds, round smallest unit down
t.ToPrettyString();                                             // "3 hours"

// 3 units requested, but seconds is zero and skipped
t.ToPrettyString(3);                                            // "3 hours and 4 minutes"

// Four different unit representations
t.ToPrettyString(2, UnitStringRepresentation.Long);             // "3 hours and 4 minutes"
t.ToPrettyString(2, UnitStringRepresentation.Short);            // "3 hrs 4 mins"
t.ToPrettyString(2, UnitStringRepresentation.CompactWithSpace); // "3h 4m"
t.ToPrettyString(2, UnitStringRepresentation.Compact);          // "3h4m"

// Three types of rounding of the smallest unit, defaulting to 'ToNearestOrUp'
// As an example, ToTimeRemainingString() uses IntegerRounding.Up to not
// show "0 seconds" remaining when there is 0.9 seconds remaining.
var t2 = new TimeSpan(hours: 3, minutes: 30, seconds: 0);
t2.ToPrettyString(1, lowestUnitRounding: IntegerRounding.Down);          // "3 hours"
t2.ToPrettyString(1, lowestUnitRounding: IntegerRounding.Up);            // "4 hours"
t2.ToPrettyString(1, lowestUnitRounding: IntegerRounding.ToNearestOrUp); // "4 hours"
```

## TimeSpan.ToTimeRemainingString()
This is helpful to avoid showing strings like "0 seconds remaining" or "9 seconds remaining" when it really is 9.999 seconds remaining. It basically just calls ```ToPrettyString()``` with ```IntegerRounding.Up```.
```csharp
TimeSpan.FromSeconds(  60.1).TotimeRemainingString(); // "1 minute and 1 second"
TimeSpan.FromSeconds(  60  ).TotimeRemainingString(); // "1 minute"
TimeSpan.FromSeconds(  59.9).TotimeRemainingString(); // "1 minute"
TimeSpan.FromSeconds(   1.1).TotimeRemainingString(); // "2 seconds"
TimeSpan.FromSeconds(   1  ).TotimeRemainingString(); // "1 second"
TimeSpan.FromSeconds(   0.1).TotimeRemainingString(); // "1 second"
TimeSpan.FromSeconds(   0  ).TotimeRemainingString(); // "0 seconds" 
```
