using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtcoDbPopulator
{
    public class NormalDistribution
    {
        private Random random;

        public NormalDistribution()
        {
            random = new Random();
        }

        public double GenerateNormalRandomNumber()
        {
           return GenerateNormalRandomNumber(0, 1);
        }

        public double GenerateNormalRandomNumber(double mean, double standardDeviation)
        {
            double u1 = 1.0 - random.NextDouble(); // Uniform random number 1
            double u2 = 1.0 - random.NextDouble(); // Uniform random number 2

            // Box-Muller transform to convert uniform distribution to normal distribution
            double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

            // Scale the generated random number to the desired mean and standard deviation
            double x = mean + z * standardDeviation;

            return x;
        }
    }
}
