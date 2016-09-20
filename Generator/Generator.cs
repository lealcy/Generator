using System;

namespace Generator
{
    class Generator
    {
        public int[] Target;
        public Random RandomSource;

        public Generator(int[] target, Random randomSource)
        {
            Target = target;
            RandomSource = randomSource;
        }

        public Generator(string target, Random randomSource): 
            this(Generator.TargetFromString(target), randomSource)
        { }

        public static int[] TargetFromString(string text)
        {
            int[] target = new int[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                target[i] = (int)text[i];
            }

            return target;
        }
    }
}
