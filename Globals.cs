global using FluentAssertions;
global using NUnit;
global using Reqnroll;

using NUnit.Framework;
[assembly: LevelOfParallelism(80)]
[assembly: Parallelizable(ParallelScope.Children)]
