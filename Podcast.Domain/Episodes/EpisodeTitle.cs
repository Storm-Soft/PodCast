using System.Collections.Generic;
using Value;

namespace Podcast.Domain.Episodes
{
    public class EpisodeTitle : ValueType<EpisodeTitle>
    {
        private readonly string _value;

        public EpisodeTitle(string value)
        {
            _value = value;
        }

        public static implicit operator string(EpisodeTitle vo) => vo._value;


        public override string ToString() => _value;

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() => new[] { _value };
    }
}
