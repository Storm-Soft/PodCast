using System;
using System.Collections.Generic;
using Value;

namespace Podcast.Domain.Episodes
{
    public class PublicationDate : ValueType<PublicationDate>
    {
        private readonly DateTime _value;

        public PublicationDate(DateTime value)
        {
            _value = value;
        }

        public static implicit operator DateTime(PublicationDate vo) => vo._value;


        public override string ToString() => _value.ToString("yyyy/MM/dd");

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() => new object[] { _value };
    }
}
