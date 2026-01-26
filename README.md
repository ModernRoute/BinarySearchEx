# ModernRoute.BinarySearchEx

[![Nuget](https://img.shields.io/nuget/vpre/ModernRoute.BinarySearchEx.svg?label=ModernRoute.BinarySearchEx&style=flat-square&color=d8b541)](https://www.nuget.org/packages/ModernRoute.BinarySearchEx)

The package adds `IReadOnlyList` extensions: `BinarySearchFirst` and `BinarySearchLast`. Both method sets are similar to the original [BinarySearch](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.binarysearch) except the following.

* The BinarySearchFirst finds the first occurrence of the provided element. 
* The BinarySearchLast finds the last occurrence of the provided element.

Example:

```csharp
using ModernRoute.BinarySearchEx;

List<int> sortedList = [1, 4, 4, 7, 9, 9, 9, 15, 20];
Console.WriteLine(sortedList.BinarySearchFirst(9)); // Prints: 4
Console.WriteLine(sortedList.BinarySearchLast(9));  // Prints: 6
Console.WriteLine(sortedList.BinarySearchFirst(3)); // Prints: -2
Console.WriteLine(sortedList.BinarySearchLast(3));  // Prints: -2
```

Of course, it works with more complex [comparable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.icomparer?view=net-10.0) types.

Enjoy!