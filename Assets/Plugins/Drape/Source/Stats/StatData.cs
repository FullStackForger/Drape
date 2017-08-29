﻿using Drape.Slug;
using System.Collections.Generic;

namespace Drape
{
    public class StatData : BaseStatData
    {
        [System.Serializable]
        public class Dependency
        {
            public string code;
            public float value;

            public Dependency(string code, float value)
            {
                this.code = code;
                this.value = value;
            }
        }

        public Dependency[] dependencies = new Dependency[0];

        public StatData(string name, int value = 0, Dictionary<string, float> dependencies = null) : this(name.ToSlug(), name, value, dependencies) { }

        public StatData(string code, string name, int value, Dictionary<string, float> dependencies = null) : base(code, name, value)
        {
            List<Dependency> list = new List<Dependency>();
            if (dependencies != null) {
                foreach (KeyValuePair<string, float> pair in dependencies) {
                    list.Add(new Dependency(pair.Key, pair.Value));
                }
            }
            this.dependencies = list.ToArray();
        }
    }
}