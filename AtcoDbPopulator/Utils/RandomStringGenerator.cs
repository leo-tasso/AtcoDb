// <copyright file="RandomStringGenerator.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator.Utils;

/// <summary>
/// Class to generate a random string.
/// </summary>
public static class RandomStringGenerator
{
    /// <summary>
    /// Method to generate a random string.
    /// </summary>
    /// <param name="length">The number of chars.</param>
    /// <returns>The random string.</returns>
    public static string GenerateRandomString(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}