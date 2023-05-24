// <copyright file="FileToList.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator;

/// <summary>
/// Class to fetch a file and create a list from it.
/// </summary>
public class FileToList
{
    /// <summary>
    /// Method to fetch a file and create a list from it.
    /// </summary>
    /// <param name="filePath">The path of the file.</param>
    /// <returns>The generated list.</returns>
    public static List<string> ReadFileToList(string filePath)
    {
        List<string> strings = new List<string>();

        try
        {
            // Read all lines from the file and add them to the list
            strings.AddRange(File.ReadAllLines(filePath));
        }
        catch (IOException e)
        {
            Console.WriteLine(@"An error occurred while reading the file: " + e.Message);
        }

        return strings;
    }
}