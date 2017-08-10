using System;
using System.Collections.Generic;
using System.Linq;

namespace ProRata.NET
{
    public class ProRateResult<T>
    {
        public Dictionary<T, decimal> Result = new Dictionary<T, decimal>();
    }
    public class ProRate<T>
    {
        private IEnumerable<T> collectionToProrate;
        private Func<T, decimal> weightFunction;
        private decimal amountToProrate;
        private MidpointRounding roundingMethod = MidpointRounding.AwayFromZero;
        private int roundingDecimalPlaces = 2;

        public ProRate(IEnumerable<T> collection,decimal amount)
        {
            this.collectionToProrate = collection;
            this.amountToProrate = amount;
            this.weightFunction = item => 1M / collectionToProrate.Count();
        }


        public ProRate<T> Weight(Func<T,decimal> WeightFunction)
        {
            this.weightFunction = WeightFunction;
            return this;
        }

        public ProRate<T> RoundTo(int decimalPlaces)
        {
            roundingDecimalPlaces = decimalPlaces;
            return this;
        }

        public ProRate<T> RoundMethod(MidpointRounding roundingMethod)
        {
            this.roundingMethod = roundingMethod;
            return this;
        }

        public ProRateResult<T> Calculate()
        {
            ProRateResult<T> result = new ProRateResult<T>();
            decimal totalRemaining = this.amountToProrate;

            foreach(var item in this.collectionToProrate)
            {
                decimal itemResult = amountToProrate * weightFunction.Invoke(item);
                decimal itemResultRounded = Math.Round(itemResult, roundingDecimalPlaces, roundingMethod);
                totalRemaining -= itemResultRounded;
                result.Result[item] = itemResultRounded;
            }

            if(totalRemaining !=0)
            {
                var lastItem = result.Result.Last();
                result.Result[lastItem.Key] += totalRemaining;
            }
            return result;
        }
    }
    public static class ProRata
    {
        public static ProRate<T> ProRate<T>(this IEnumerable<T> collection,decimal amount)
        {
            ProRate<T> prorate = new ProRate<T>(collection,amount);
            return prorate;
        }
    }
}