namespace Cities;

public class City
{
    public string _name;
    public List<Dictionary<City, int>> _distances;

    public City(string name, int i = 0, string oldName = "") // MARCHE PAS
    {
        _name = name.ToLower();
        _distances = new List<Dictionary<City, int>>(0);
        string[] csv = File.ReadAllLines("../../../../Cities/distances.csv");
        foreach (var line in csv)
        {
            var cityInfo = line.Split(";");
            var cityName = cityInfo[0];
            var otherCityName = cityInfo[1];
            if (cityName == name)
            {
                
            }
        }
    }

    public City(Dictionary<City, int> distances, string name)
    {
        this._distances = distances;
        this._name = name;
    }
}


//Graph of cities
public class Map
{
    public List<City> _cities { get; set; }

    public Map()
    {
        string[] csv = File.ReadAllLines("../../../../Cities/distances.csv");
        _cities = new List<City>(0);
        foreach (var line in csv)
        {
            if (_cities.FindIndex(delegate(City city) { return city._name == line.Split(';')[0]; })>-1)
            {
                
            }
        }
    }

    public List<City> FindShortestPath(City start, City end)
    {
        // Initialize the data structures to hold the distances from the start city
        // to each other city in the map.
        Dictionary<City, int> distances = new Dictionary<City, int>();
        foreach (City city in _cities)
        {
            // Initialize the distances to start as infinity
            distances[city] = int.MaxValue;
        }

        // Start with the start city at 0 distance
        distances[start] = 0;

        // Initialize the data structure to hold the previous city in the path
        // from the start to each other city in the map.
        Dictionary<City, City> previous = new Dictionary<City, City>();
        foreach (City city in _cities)
        {
            previous[city] = null;
        }

        // Initialize the data structure to hold the cities that have been
        // visited and the ones that are still unvisited.
        List<City> unvisited = new List<City>(_cities);

        // Until all cities have been visited
        while (unvisited.Count > 0)
        {
            // Select the city with the smallest distance as the current city.
            City current = null;
            int minDistance = int.MaxValue;
            foreach (City city in unvisited)
            {
                if (distances[city] < minDistance)
                {
                    current = city;
                    minDistance = distances[city];
                }
            }

            // If the current city is the end city, then we have finished
            // the search and can build the shortest path.
            if (current == end)
            {
                List<City> path = new List<City>();
                while (previous[current] != null)
                {
                    path.Add(current);
                    current = previous[current];
                }

                path.Reverse();
                return path;
            }

            // Mark the current city as visited and remove it from the
            // unvisited list.
            unvisited.Remove(current);

            // Update the distances of the adjacent cities.
            foreach (City city in current._distances.Keys)
            {
                if (unvisited.Contains(city))
                {
                    int distance = distances[current] + current._distances[city];
                    if (distance < distances[city])
                    {
                        distances[city] = distance;
                        previous[city] = current;
                    }
                }
            }
        }
        return null;
    }
}