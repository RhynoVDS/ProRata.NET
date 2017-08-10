using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace ProRata.NET.Tests
{
    [TestClass]
    public class ProRate_Weighted
    {
        [TestMethod]
        public void Weight_ProRate100WithDifferentSizes_WeightedProrata()
        {
            var Item1 = new TestObj() { Size = 2 };
            var Item2 = new TestObj() { Size = 5 };
            var Item3 = new TestObj() { Size = 4 };
            var Item4 = new TestObj() { Size = 8 };
            var Item5 = new TestObj() { Size = 8 };

            List<TestObj> testCollection = new List<TestObj>() {
                Item1,Item2,Item3,Item4,Item5
            };

            var testCollectionSum = testCollection.Sum(i => i.Size);

            var result = testCollection.ProRate(100)
                                       .Weight(testObj => testObj.Size / (decimal)testCollectionSum)
                                       .Calculate();

            result.Result.Sum(r => r.Value).Should().Be(100);
            result.Result.Should().ContainKey(Item1).WhichValue.ShouldBeEquivalentTo(7.41);
            result.Result.Should().ContainKey(Item2).WhichValue.ShouldBeEquivalentTo(18.52);
            result.Result.Should().ContainKey(Item3).WhichValue.ShouldBeEquivalentTo(14.81);
            result.Result.Should().ContainKey(Item4).WhichValue.ShouldBeEquivalentTo(29.63);
            result.Result.Should().ContainKey(Item5).WhichValue.ShouldBeEquivalentTo(29.63);
        }

        [TestMethod]
        public void WeightToEvenRounding_ProRate100WithDifferentSizes_WeightedProrataWithEvenRounding()
        {
            var Item1 = new TestObj() { Size = 2 };
            var Item2 = new TestObj() { Size = 5 };
            var Item3 = new TestObj() { Size = 4 };
            var Item4 = new TestObj() { Size = 8 };
            var Item5 = new TestObj() { Size = 8 };

            List<TestObj> testCollection = new List<TestObj>() {
                Item1,Item2,Item3,Item4,Item5
            };

            var testCollectionSum = testCollection.Sum(i => i.Size);

            var result = testCollection.ProRate(100)
                                       .Weight(testObj => testObj.Size / (decimal)testCollectionSum)
                                       .RoundMethod(MidpointRounding.ToEven)
                                       .RoundTo(3)
                                       .Calculate();

            result.Result.Sum(r => r.Value).Should().Be(100);
            result.Result.Should().ContainKey(Item1).WhichValue.ShouldBeEquivalentTo(7.407);
            result.Result.Should().ContainKey(Item2).WhichValue.ShouldBeEquivalentTo(18.519);
            result.Result.Should().ContainKey(Item3).WhichValue.ShouldBeEquivalentTo(14.815);
            result.Result.Should().ContainKey(Item4).WhichValue.ShouldBeEquivalentTo(29.630);
            result.Result.Should().ContainKey(Item5).WhichValue.ShouldBeEquivalentTo(29.629);
        }

        [TestMethod]
        public void WeightTo3DecimalPlaces_ProRate341dot891WithDifferentSizes_EachSizeProrated()
        {
            var Item1 = new TestObj() { Size = 7 };
            var Item2 = new TestObj() { Size = 2 };
            var Item3 = new TestObj() { Size = 4 };
            var Item4 = new TestObj() { Size = 9 };
            var Item5 = new TestObj() { Size = 3 };

            List<TestObj> testCollection = new List<TestObj>() {
                Item1,Item2,Item3,Item4,Item5
            };

            var testCollectionSum = testCollection.Sum(i => i.Size);

            var result = testCollection.ProRate(341.891M)
                                       .Weight(testObj => testObj.Size / (decimal)testCollectionSum)
                                       .RoundTo(3)
                                       .Calculate();

            result.Result.Sum(r => r.Value).Should().Be(341.891M);
            result.Result.Should().ContainKey(Item1).WhichValue.ShouldBeEquivalentTo(95.729M);
            result.Result.Should().ContainKey(Item2).WhichValue.ShouldBeEquivalentTo(27.351M);
            result.Result.Should().ContainKey(Item3).WhichValue.ShouldBeEquivalentTo(54.703M);
            result.Result.Should().ContainKey(Item4).WhichValue.ShouldBeEquivalentTo(123.081M);
            result.Result.Should().ContainKey(Item5).WhichValue.ShouldBeEquivalentTo(41.027M);
        }


    }
    public class TestObj
    {
        public int Size;
    }
}
