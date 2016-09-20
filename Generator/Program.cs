using System;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator g = new Generator(
                Generator.GenomeFromString("Hello World"),
                Generator.GenePoolFromRange(' ', '~'),
                10000,
                1000,
                0.005,
                new Random()
            );

            while(!g.HasBestFit())
            {
                Console.WriteLine(g);
                g.Evolve();
            }

            Console.ReadKey(true);
        }
    }
}
