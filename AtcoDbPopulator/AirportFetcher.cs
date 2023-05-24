namespace AtcoDbPopulator;

public class AirportFetcher
{
    public AirportFetcher()
    {
    }

    public List<(string, string, string)> FetchAirportInfo(string filePath)
    {
        List<(string, string, string)> airportInfoList = new List<(string, string, string)>();

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
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
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }

        return airportInfoList;
    }
}