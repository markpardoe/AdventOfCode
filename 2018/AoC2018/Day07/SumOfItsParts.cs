using System;
using AoC.Common;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Aoc2018.Day07
{
    public class SumOfItsParts : AoCSolution<string>
    {
        public override int Year => 2018;
        public override int Day => 7;
        public override string Name => "Day 7: The Sum of Its Parts";
        public override string InputFileName => "Day07.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            var graph = ParseInput(input);
            yield return FindShortestPath(graph);
            yield return FindQuickestParallel(graph, 5).ToString();
        }

        private string FindShortestPath(IReadOnlyDictionary<string, Node> graph)
        {
            var closedList = new List<Node>();  // we can't use hashset as we want to maintain the order.           
            var openList = graph.Values.Where(n => n.Parents.Count == 0).ToHashSet();   // select all nodes with no parents

            while (openList.Count > 0)
            {
                Node current = openList.OrderBy(c => c.NodeId).First();
                closedList.Add(current);
                openList.Remove(current);

                foreach (Node child in current.Children)
                {
                    if (!closedList.Contains(child) && !openList.Contains(child) && child.Parents.All(n => closedList.Contains(n)))
                    {
                        openList.Add(child);
                    }
                }
            }
            return string.Join("", closedList.Select(n => n.NodeId));
        }

        private int FindQuickestParallel(IReadOnlyDictionary<string, Node> graph, int numWorkers)
        {
            List<Worker> workers = new List<Worker>(numWorkers);
            for (int i=0; i < numWorkers;i++)
            {
                workers.Add(new Worker());
            }

            int currentTime = 0;

            // Keep a list of all nodes to be processed - once its empty we know we've finished
            var allNodes = graph.Values.ToHashSet();        
            var closedList = new HashSet<Node>();       // nodes that have been checked
            var openList = graph.Values.Where(n => n.Parents.Count == 0).ToList();   // select all nodes with no parents

            while (true)
            {
                // Get the workers with no current job.
                // Have to include previously finished as otherwise they'll never be used again.
                // Sort descending - this fixes an where if the last worker added multiple jobs to the openList - they wouldn't get picked up until the next loop
                // - causing the final result to be out by one.
                // By selecting jobs that have just finished first - we guarantee they're added to the queue and picked up by empty workers
                var finishedWorkers = workers.Where(w => w.EndTime <= currentTime).OrderByDescending(w => w.EndTime);

                foreach (var worker in finishedWorkers)
                {

                    // Process their last job if its just finished
                    var job = worker.LastJob;
                    if (job != null && job.EndTime == currentTime)
                    {
                        closedList.Add(job.Node);
                        allNodes.Remove(job.Node);
                        foreach (Node child in job.Node.Children)
                        {
                            // Add the job to the queue if:
                            // Not already finished (in closed list)
                            // Not already queued for processing (in open list)
                            // All its pre-requisites are processed (parents all in closedList)
                            // Not being processed by another jon.
                            // There's probably a better way of doing this - but it works!
                            if (!closedList.Contains(child) && !openList.Contains(child) && child.Parents.All(n => closedList.Contains(n)) && !workers.Any(x => x.LastJob?.Node == child))
                            {
                                openList.Add(child);
                            }
                        }
                    }

                    // Add a new job to the worker - if there are jobs free (in openList)
                    openList = openList.OrderBy(x => x.Time).ToList();
                    if (openList.Count > 0)
                    {
                        var nextNode = openList[0];
                        openList.Remove(nextNode);
                        // Console.WriteLine($"({currentTime} Adding job: {nextNode} to worker");
                        worker.AddJob(nextNode, currentTime);
                    }
                }

                // Check if we've processed all the nodes - and quit
                if (allNodes.Count == 0)
                {
                    return workers.Max(x => x.EndTime);
                }

                currentTime++;
            }
        }

       

        private Dictionary<string, Node> ParseInput(IEnumerable<string> input)
        {
            Dictionary<string, Node> graph = new Dictionary<string, Node>();

            foreach (string s in input)
            {
                string parent = s[5].ToString();
                string child = s[36].ToString();

                // Add new nodes if not already existing
                if (!graph.ContainsKey(parent)) { graph.Add(parent, new Node(parent)); }
                if (!graph.ContainsKey(child)) { graph.Add(child, new Node(child)); }

                graph[parent].Children.Add(graph[child]);
                graph[child].Parents.Add(graph[parent]);
            }

            return graph;
        }

        private readonly IEnumerable<string> _example = new List<string>()
        {
            "Step C must be finished before step A can begin.",
            "Step C must be finished before step F can begin.",
            "Step A must be finished before step B can begin.",
            "Step A must be finished before step D can begin.",
            "Step B must be finished before step E can begin.",
            "Step D must be finished before step E can begin.",
            "Step F must be finished before step E can begin."
        };
    }
}
