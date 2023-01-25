namespace Cities;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<City> Destinations { get; set; }

    public City()
    {
        var citiesArr = File.ReadAllLines("../../../../Cities/distances.csv");
        foreach (var line in citiesArr)
        {
            string[] values = line.Split(';');
            City city1 = new City(values[0], values[1]);
            City city2 = new City(values[2], values[3]);
            int distance = int.Parse(values[4]);
            TimeSpan time = TimeSpan.Parse(values[5]);
            city1.AddDestination(city2, distance, time);
            city2.AddDestination(city1, distance, time);
        }
    }

    public City(int id, string name)
    {
        this.Id = id;
        this.Name = name;
        this.Destinations = new List<City>();
    }

    public void AddDestination(City destination)
    {
        Destinations.Add(destination);
    }
}

public abstract class Map
{
    public List<City> Cities { get; set; }
    public Dictionary<City, Dictionary<City, int>> Distances { get; set; }

    protected Map(List<City> cities)
    {
        this.Cities = cities;
        this.Distances = new Dictionary<City, Dictionary<City, int>>();

        // Create dictionaries of distances to each other city in the graph
        foreach (City city in cities)
        {
            Distances.Add(city, new Dictionary<City, int>());
            foreach (City otherCity in cities)
            {
                if (city != otherCity)
                {
                    Distances[city].Add(otherCity, 0);
                }
            }
        }
    }

    public void AddDistance(City fromCity, City toCity, int distance)
    {
        Distances[fromCity][toCity] = distance;
    }
}

public static class Graph
{
    public static List<City> ShortestPath(Map map, City fromCity, City toCity)
    {
        // Create a dictionary to store visited cities and the distances from the start city
        Dictionary<City, int> visited = new Dictionary<City, int>();
        foreach (City city in map.Cities)
        {
            visited.Add(city, int.MaxValue);
        }
        visited[fromCity] = 0;

        // Create a priority queue to store the cities to visit
        PriorityQueue<City, int> queue = new PriorityQueue<City, int>();
        queue.Enqueue(fromCity, 0);

        while (queue.Count > 0)
        {
            // Dequeue the city with the lowest distance from the start city
            City currentCity = queue.Dequeue();

            if (currentCity == toCity)
            {
                // If the current city is the destination city, return the shortest path
                return GetShortestPath(visited, toCity, map);
            }

            // Iterate through each of the destinations of the current city
            foreach (City destination in currentCity.Destinations)
            {
                // Calculate the total distance of the path from the start city to the destination
                int totalDistance = visited[currentCity] + map.Distances[currentCity][destination];

                // If the total distance is less than the current distance stored, update the distance
                if (totalDistance < visited[destination])
                {
                    visited[destination] = totalDistance;
                    queue.Enqueue(destination, totalDistance);
                }
            }
        }

        // If the destination city was not reached, return an empty list
        return new List<City>();
    }

    // Recursive function to get the shortest path from the start city to the destination city
    private static List<City> GetShortestPath(Dictionary<City, int> visited, City toCity, Map map)
    {
        List<City> path = new List<City>();

        path.Add(toCity);

        City currentCity = toCity;
        int currentDistance = visited[toCity];

        while (currentDistance > 0)
        {
            foreach (City city in currentCity.Destinations)
            {
                int totalDistance = visited[city] + map.Distances[city][currentCity];
                if (totalDistance == currentDistance)
                {
                    path.Add(city);
                    currentCity = city;
                    currentDistance = visited[city];
                    break;
                }
            }
        }

        path.Reverse();
        return path;
    }
}