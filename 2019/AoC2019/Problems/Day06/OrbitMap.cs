using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2019.Problems.Day06
{
    internal class OrbitMap
    {
        private const string CenterOfMass = "COM";
        private readonly Dictionary<string, Planet> _allPlanets = new Dictionary<string, Planet>();

        internal OrbitMap(IEnumerable<string> mapData)
        {
            foreach (String orbit in mapData)
            {
                string[] split = orbit.Split(')');

                Planet innerPlanet = AddPlanet(split[0]);
                Planet outerPlanet = AddPlanet(split[1]);

                outerPlanet.Orbits = innerPlanet;
                innerPlanet.OrbitedBy.Add(outerPlanet);
            }
        }

        internal int CountOrbits()
        {
            Queue<Planet> planetsToCheck = new Queue<Planet>();
            Dictionary<string, int> orbitCounts = new Dictionary<string, int>();
            foreach (string p in _allPlanets.Keys)
            {
                orbitCounts.Add(p, 0);
            }

            planetsToCheck.Enqueue(_allPlanets[CenterOfMass]);

            while (planetsToCheck.Count > 0)
            {
                Planet planet = planetsToCheck.Dequeue();
                int currentValue = orbitCounts[planet.Name];

                foreach (Planet nextPlanet in planet.OrbitedBy)
                {
                    orbitCounts[nextPlanet.Name] += currentValue + 1;
                    planetsToCheck.Enqueue(nextPlanet);
                }
            }
            return orbitCounts.Values.Sum();
        }

        internal int FindPath(string startPlanet, string targetPlanet)
        {

            Planet start = _allPlanets[startPlanet];
            Planet target = _allPlanets[targetPlanet];

            HashSet<Planet> checkedPlanets = new HashSet<Planet>() { start };  // we don't want to search them again!
            Queue<SearchPlanet> searchQueue = new Queue<SearchPlanet>();

            searchQueue.Enqueue(new SearchPlanet(start, 0));

            while (searchQueue.Count > 0)
            {
                SearchPlanet search = searchQueue.Dequeue();

                if (search.Planet == target)
                {
                    return search.MoveCount - 2;  // ignore orbits betweeen start & target
                }
                else
                {
                    foreach (Planet p in search.Planet.AllConnections)
                    {
                        if (!checkedPlanets.Contains(p))
                        {
                            checkedPlanets.Add(p);
                            searchQueue.Enqueue(new SearchPlanet(p, search.MoveCount + 1));
                        }
                    }
                }
            }
            return -1;  // should never happen!
        }

        private Planet AddPlanet(string name)
        {
            if (!_allPlanets.ContainsKey(name))
            {
                Planet p = new Planet(name);
                _allPlanets.Add(name, p);
                return p;
            }
            else
            {
                return _allPlanets[name];
            }
        }

        private class SearchPlanet
        {
            public Planet Planet { get; }
            public int MoveCount { get; }

            public SearchPlanet(Planet planet, int moves)
            {
                Planet = planet ?? throw new ArgumentNullException(null);
                MoveCount = moves;
            }
        }

        private class Planet : IEquatable<Planet>
        {
            public string Name { get; }
            public Planet Orbits { get; set; }
            public List<Planet> OrbitedBy { get; } = new List<Planet>();

            public Planet(string name)
            {
                this.Name = name;
            }

            public bool Equals(Planet other)
            {
                if (other == null) return false;
                return this.Name == other.Name;
            }

            public override bool Equals(object obj)
            {
                if (obj is null) return false;
                if (obj.GetType() != GetType()) return false;
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                return Equals(obj as Planet);
            }

            public List<Planet> AllConnections
            {
                get
                {
                    var l = new List<Planet>(OrbitedBy);
                    if (Orbits != null)
                    {
                        l.Add(Orbits);
                    }
                    return l;
                }
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name);
            }
        }


    }
}
