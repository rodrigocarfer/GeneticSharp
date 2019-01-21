﻿using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Extensions.Tsp;

namespace GeneticSharp.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Method, MethodOrderPolicy.Declared)]
    [MinIterationCount(5)]
    [MaxIterationCount(10)]
    public class SelectionsBenchmark
    {
        private static readonly int _numberOfCities = 10;
        private readonly Generation _generation = new Generation(1, new List<IChromosome>
        {
            new TspChromosome(_numberOfCities),
            new TspChromosome(_numberOfCities),
            new TspChromosome(_numberOfCities),
            new TspChromosome(_numberOfCities),
            new TspChromosome(_numberOfCities)
        });

        [Params(2)]
        public int ChromosomesNumber { get; set; }

        [Benchmark]
        public ISelection Elite()
        {
            var target = new EliteSelection();
            target.SelectChromosomes(ChromosomesNumber, _generation);
            return target;
        }

        [Benchmark]
        public ISelection RouletteWheel()
        {
            var target = new RouletteWheelSelection();
            target.SelectChromosomes(ChromosomesNumber, _generation);
            return target;
        }

        [Benchmark]
        public ISelection StochasticUniversalSampling()
        {
            var target = new StochasticUniversalSamplingSelection();
            target.SelectChromosomes(ChromosomesNumber, _generation);
            return target;
        }

        [Benchmark]
        public ISelection Tournament()
        {
            var target = new TournamentSelection();
            target.SelectChromosomes(ChromosomesNumber, _generation);
            return target;
        }
    }
}