using System;
using ProRata.NET;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FluentAssertions;

namespace ProRata.NET.Tests
{
    [TestClass]
    public class ProRate_Simple
    {
        [TestMethod]
        public void ListProRate_init()
        {
            List<string> test = new List<string>();
            test.Add("hi");
            test.ProRate(10);
        }

        [TestMethod]
        public void ProRate_4Items100amount_25AllocatedToEach()
        {
            List<string> test = new List<string>();
            test.Add("Item 1");
            test.Add("Item 2");
            test.Add("Item 3");
            test.Add("Item 4");

            var result = test.ProRate(100).Calculate();

            result.Result.Should().HaveCount(4);
            result.Result.Should().ContainKey("Item 1").WhichValue.ShouldBeEquivalentTo(25);
            result.Result.Should().ContainKey("Item 2").WhichValue.ShouldBeEquivalentTo(25);
            result.Result.Should().ContainKey("Item 3").WhichValue.ShouldBeEquivalentTo(25);
            result.Result.Should().ContainKey("Item 4").WhichValue.ShouldBeEquivalentTo(25);
        }

        [TestMethod]
        public void ProRateRoundTo2_6Items100amount_1667AllocatedToEach()
        {
            List<string> test = new List<string>();
            test.Add("Item 1");
            test.Add("Item 2");
            test.Add("Item 3");
            test.Add("Item 4");
            test.Add("Item 5");
            test.Add("Item 6");

            var result = test.ProRate(100)
                             .RoundTo(2)
                             .Calculate();

            result.Result.Sum(r => r.Value).Should().Be(100);
            result.Result.Should().ContainKey("Item 1").WhichValue.ShouldBeEquivalentTo(16.67);
            result.Result.Should().ContainKey("Item 2").WhichValue.ShouldBeEquivalentTo(16.67);
            result.Result.Should().ContainKey("Item 3").WhichValue.ShouldBeEquivalentTo(16.67);
            result.Result.Should().ContainKey("Item 4").WhichValue.ShouldBeEquivalentTo(16.67);
            result.Result.Should().ContainKey("Item 5").WhichValue.ShouldBeEquivalentTo(16.67);
            result.Result.Should().ContainKey("Item 6").WhichValue.ShouldBeEquivalentTo(16.65);
        }

        [TestMethod]
        public void ProRateRoundTo5_6Items100amount_16667AllocatedToEach()
        {
            List<string> test = new List<string>();
            test.Add("Item 1");
            test.Add("Item 2");
            test.Add("Item 3");
            test.Add("Item 4");
            test.Add("Item 5");
            test.Add("Item 6");

            var result = test.ProRate(100)
                             .RoundTo(5)
                             .Calculate();

            result.Result.Sum(r => r.Value).Should().Be(100);
            result.Result.Should().ContainKey("Item 1").WhichValue.ShouldBeEquivalentTo(16.66667);
            result.Result.Should().ContainKey("Item 2").WhichValue.ShouldBeEquivalentTo(16.66667);
            result.Result.Should().ContainKey("Item 3").WhichValue.ShouldBeEquivalentTo(16.66667);
            result.Result.Should().ContainKey("Item 4").WhichValue.ShouldBeEquivalentTo(16.66667);
            result.Result.Should().ContainKey("Item 5").WhichValue.ShouldBeEquivalentTo(16.66667);
            result.Result.Should().ContainKey("Item 6").WhichValue.ShouldBeEquivalentTo(16.66665);
        }

        [TestMethod]
        public void ProRate_7Items1amount_014AllocatedToeach()
        {
            List<string> test = new List<string>();
            test.Add("Item 1");
            test.Add("Item 2");
            test.Add("Item 3");
            test.Add("Item 4");
            test.Add("Item 5");
            test.Add("Item 6");
            test.Add("Item 7");

            var result = test.ProRate(1)
                             .Calculate();

            result.Result.Sum(r => r.Value).Should().Be(1);
            result.Result.Should().ContainKey("Item 1").WhichValue.ShouldBeEquivalentTo(0.14);
            result.Result.Should().ContainKey("Item 2").WhichValue.ShouldBeEquivalentTo(0.14);
            result.Result.Should().ContainKey("Item 3").WhichValue.ShouldBeEquivalentTo(0.14);
            result.Result.Should().ContainKey("Item 4").WhichValue.ShouldBeEquivalentTo(0.14);
            result.Result.Should().ContainKey("Item 5").WhichValue.ShouldBeEquivalentTo(0.14);
            result.Result.Should().ContainKey("Item 6").WhichValue.ShouldBeEquivalentTo(0.14);
            result.Result.Should().ContainKey("Item 7").WhichValue.ShouldBeEquivalentTo(0.16);
        }

        [TestMethod]
        public void ProRate_6Items4923dot48amount_820dot58AllocatedToeach()
        {
            List<string> test = new List<string>();
            test.Add("Item 1");
            test.Add("Item 2");
            test.Add("Item 3");
            test.Add("Item 4");
            test.Add("Item 5");
            test.Add("Item 6");

            var result = test.ProRate(4923.48M)
                             .Calculate();

            result.Result.Sum(r => r.Value).Should().Be(4923.48M);
            result.Result.Should().ContainKey("Item 1").WhichValue.ShouldBeEquivalentTo(820.58);
            result.Result.Should().ContainKey("Item 2").WhichValue.ShouldBeEquivalentTo(820.58);
            result.Result.Should().ContainKey("Item 3").WhichValue.ShouldBeEquivalentTo(820.58);
            result.Result.Should().ContainKey("Item 4").WhichValue.ShouldBeEquivalentTo(820.58);
            result.Result.Should().ContainKey("Item 5").WhichValue.ShouldBeEquivalentTo(820.58);
            result.Result.Should().ContainKey("Item 6").WhichValue.ShouldBeEquivalentTo(820.58);
        }
    }
}
