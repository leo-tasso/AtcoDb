// <copyright file="AirportFetcher.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator;

/// <summary>
/// Class to load airport list from file.
/// </summary>
public class AirportFetcher
{
    /// <summary>
    /// Method to load airport list from file.
    /// </summary>
    /// <param name="filePath">The path to the file containing the list.</param>
    /// <returns>The list of tuples of strings containing (apt name, iata code, icao code).</returns>
    public List<(string, string, string)> FetchAirportInfo(string filePath)
    {
        List<(string, string, string)> airportInfoList = new List<(string, string, string)>();

        try
        {
            using StreamReader reader = new StreamReader(filePath);
            while (reader.ReadLine() is { } line)
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 3)
                {
                    string airportName = parts[0].Trim();
                    string iataCode = parts[1].Trim();
                    string icaoCode = parts[2].Trim();

                    airportInfoList.Add((airportName, iataCode, icaoCode));
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(@"Error: unable to fetch the list" + e.Message);
        }

        return airportInfoList;
    }
}