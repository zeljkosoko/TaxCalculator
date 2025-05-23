using TaxCalculatorMVC.Application.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TaxCalculatorMVC.Application.Enums;

namespace TaxCalculatorMVC.Infrastructure.InMemory
{
    public class InMemoryCustomRateRepository: ICustomRateRepository
    {
        private readonly ConcurrentDictionary<Commodity, List<(DateTime TimestampUtc, double Rate)>> _store = new();

        public void SaveRate(Commodity commodity, double rate, DateTime timestampUtc)
        {
            var list = _store.GetOrAdd(commodity, _ => new List<(DateTime, double)>());

            lock (list)
            {
                var index = list.FindIndex(r => Math.Abs((r.TimestampUtc - timestampUtc).TotalSeconds) < 1);
                if (index >= 0)
                    list.RemoveAt(index);

                list.Add((timestampUtc, rate));
            }

            Console.WriteLine($"SAVE: {commodity} @ {timestampUtc} = {rate}");
        }

        public List<(DateTime TimestampUtc, double Rate)> GetRates(Commodity commodity)
        {
            return _store.TryGetValue(commodity, out var list) ? list.ToList() : new List<(DateTime, double)>();
        }
    }
}
