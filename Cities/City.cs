namespace Cities;

public class City
{
    public string _name;
    public Dictionary<City, int> _distances;

    public City(string name, int i = 0, string oldName = "") // MARCHE PAS
    {
        _name = name.ToLower();
        _distances = new Dictionary<City, int>(0);
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
        Dictionary<City, int> distances = new Dictionary<City, int>();
        foreach (City city in _cities)
        {
            distances[city] = int.MaxValue;
        }

        distances[start] = 0;

        Dictionary<City, City> previous = new Dictionary<City, City>();
        foreach (City city in _cities)
        {
            previous[city] = null;
        }

        List<City> unvisited = new List<City>(_cities);

        while (unvisited.Count > 0)
        {
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

            unvisited.Remove(current);

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