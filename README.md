# WordFinder

A .NET library for searching words in 2D matrices.

## Features

- Search for words in a 2D matrix (horizontally, vertically).
- Parallel processing support for better performance.

## Installation

Since the library is not published on NuGet, clone the repository and include the project as a reference in your solution.

```bash
git clone https://github.com/gargamuza/WordFinder.git
```

## Usage

Here’s an example of how to use the library:

```csharp
using WordFinder.Core;

var matrix = new[]
{
    "abcd",
    "efgh",
    "ijkl",
    "mnop"
};

var wordFinder = new WordFinder(matrix);

var wordsToSearch = new[] { "abc", "mno", "xyz" };
var results = wordFinder.Find(wordsToSearch);

foreach (var word in results)
{
    Console.WriteLine(word);
}

```

## Testing the Library

The repository includes a console application to test the library. You can use it to generate matrices, search for words, and validate results interactively.


## Requirements

.NET Core 8
