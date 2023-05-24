// <copyright file="NormalDistribution.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator
{
    using System;

    /// <summary>
    /// Class to manage normal distribution.
    /// </summary>
    public class NormalDistribution
    {
        private readonly Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalDistribution"/> class.
        /// </summary>
        public NormalDistribution()
        {
            this.random = new Random();
        }

        /// <summary>
        /// Method to get a standard deviated number.
        /// </summary>
        /// <param name="mean">The mean value.</param>
        /// <param name="standardDeviation">The standard deviation.</param>
        /// <returns>A standard deviated number with the selected parameters.</returns>
        public double GenerateNormalRandomNumber(double mean = 0, double standardDeviation = 1)
        {
            var u1 = 1.0 - this.random.NextDouble(); // Uniform random number 1
            var u2 = 1.0 - this.random.NextDouble(); // Uniform random number 2

            // Box-Muller transform to convert uniform distribution to normal distribution
            var z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

            // Scale the generated random number to the desired mean and standard deviation
            var x = mean + (z * standardDeviation);

            return x;
        }
    }
}
