using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Models.NQeensProblem.Parameters
{
    public class GeneticAlgorithmParameters : IParams
    {
        public int sizeOfASingleGeneration;
        public int percentOfElitism;
        public int crossoverProbability;
        public int mutationProbability;
        public int numberOfGenerations;
    }
}
