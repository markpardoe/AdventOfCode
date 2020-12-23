using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.Aoc2018.Day07
{
    public class Worker
    {
        private readonly List<Job> _jobs = new List<Job>();
        public int EndTime => _jobs.LastOrDefault()?.EndTime ?? 0;
        public Job LastJob => _jobs.LastOrDefault();

        public void AddJob(Node node, int startTime)
        {
            _jobs.Add(new Job(node, startTime));
        }
    }

    public class Job
    {
        public int StartTime { get; }
        public int EndTime => StartTime + Node.Time;
        public Node Node { get; }

        public Job(Node node, int startTime)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            StartTime = startTime;
        }

        public override string ToString()
        {
            return $"[{Node}] Start: {StartTime}, EndTime: {EndTime}";
        }
    }
}
