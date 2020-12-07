﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC.AoC2020.Problems.Day07
{
    public class Bag :IEquatable<Bag>
    {
        public string Name { get; }

        private readonly HashSet<Bag> _children = new HashSet<Bag>(); // Bags that can be placed in this bag
        private readonly HashSet<Bag> _parents = new HashSet<Bag>();  // Bags this bag fits into


        public IReadOnlyCollection<Bag> Parents => _parents;

        public Bag(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            this.Name = name.Trim();
        }

        public bool Equals([AllowNull] Bag other)
        {
            if (other == null)
            {
                return false;
            }
            return Name.Equals(other.Name);
        }

        public void AddChild(Bag bag)
        {
            _children.Add(bag);
        }

        public void AddParent(Bag bag)
        {
            _parents.Add(bag);
        }

        public bool ContainsChild(Bag bag)
        {
            return _children.Contains(bag);
        }

        public bool ContainsParent(Bag bag)
        {
            return _parents.Contains(bag);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
