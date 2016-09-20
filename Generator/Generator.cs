using System;

namespace Generator
{
    class Generator
    {
        public readonly int[] GenePool;
        public readonly int[] TargetGenome;
        public readonly int BestGroupLength;
        public readonly int PopulationSize;
        public readonly double MutationChange;
        public readonly Random RandomSource;
        public int[][] Population;

        public bool MutationOccurred = false;

        public Generator(int[] targetGenome, int[] genePool, int populationSize, int bestGroupLength, double mutationChange, Random randomSource)
        {
            TargetGenome = targetGenome;
            GenePool = genePool;
            RandomSource = randomSource;
            PopulationSize = populationSize;
            BestGroupLength = bestGroupLength;
            MutationChange = mutationChange;
            Population = new int[PopulationSize][];
            Populate();
        }

        public bool HasBestFit()
        {
            for (int i = 0; i < TargetGenome.Length; i++)
            {
                if (TargetGenome[i] != Population[0][i])
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            return String.Format(
                "Target: '{0}', Best Fit: '{1}', Mutation Occured: {2}",
                GenomeToString(TargetGenome),
                GenomeToString(Population[0]),
                MutationOccurred
            );
        }


        public void Populate()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                Population[i] = RandomIndividual();
            }
            Array.Sort(Population, Weight);
        }

        public int Weight(int[] ind1, int[] ind2)
        {
            return FitnessScore(ind1) - FitnessScore(ind2);
        }

        public int[] RandomIndividual()
        {
            int[] individual = new int[TargetGenome.Length];
            for (int i = 0; i < TargetGenome.Length; i++)
            {
                individual[i] = GenePool[RandomSource.Next(0, GenePool.Length)];
            }
            return individual;
        }


        public int FitnessScore(int[] ind)
        {
            int score = 0;
            for (int i = 0; i < TargetGenome.Length; i++)
            {
                score += Math.Abs(ind[i] - TargetGenome[i]);
            }
            return score;
        }

        public int[] Breed(int[] ind1, int[] ind2)
        {
            int[] newInd = new int[TargetGenome.Length];
            for (int i = 0; i < TargetGenome.Length; i++)
            {

                if (RandomSource.NextDouble() < MutationChange)
                {
                    MutationOccurred = true;
                    newInd[i] = GenePool[RandomSource.Next(0, GenePool.Length)];
                }
                else if (ind1[i] == ind2[i])
                {
                    newInd[i] = ind1[i];
                }
                else
                {
                    newInd[i] = RandomSource.Next(0, 1) == 0 ? ind1[1] : ind1[2];
                }
            }

            return newInd;
        }

        public void Evolve()
        {
            int[][] newPop = new int[PopulationSize][];
            MutationOccurred = false;
            for (int i = 0; i < PopulationSize; i++)
            {
                newPop[i] = Breed(
                    Population[RandomSource.Next(0, BestGroupLength)],
                    Population[RandomSource.Next(0, BestGroupLength)]
                );
            }
            Population = newPop;
            Array.Sort(Population, Weight);
        }

        public static int[] GenomeFromString(string text)
        {
            int[] genome = new int[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                genome[i] = (int)text[i];
            }

            return genome;
        }

        public static string GenomeToString(int[] genome)
        {
            string text = "";
            foreach(int i in genome)
            {
                text += (char)i;
            }
            return text;
        }

        public static int[] GenePoolFromRange(int begin, int end)
        {
            int[] genePool = new int[end - begin + 1];
            for (int i = begin; i <= end; i++)
            {
                genePool[i - begin] = i;
            }
            return genePool;
        }
    }
}
