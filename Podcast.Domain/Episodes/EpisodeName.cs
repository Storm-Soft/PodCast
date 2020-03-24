using System.Collections.Generic;
using Value;

namespace Podcast.Domain.Episodes
{
    public class EpisodeName : ValueType<EpisodeName>
    {
        private readonly string _value;

        public EpisodeName(string value)
        {
            _value = value;
        }

        public static implicit operator string(EpisodeName vo) => vo._value;


        public override string ToString() => _value;

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() => new[] { _value };
    }
}
