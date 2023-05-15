using System;
using Newtonsoft.Json;

namespace Model
{
    public class Pokemon : IComparable
    {
        [JsonProperty(PropertyName = "base_experience")]
        public int BaseExperience { get; set; }
        public string Name { get; set; }

        public int CompareTo(object other)
        {
            Pokemon otherTemp = other as Pokemon;
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var baseExperienceComparison = BaseExperience.CompareTo(otherTemp!.BaseExperience);
            return baseExperienceComparison;
        }

        public override string ToString()
        {
            return Name + " " + BaseExperience;
        }
    }
}