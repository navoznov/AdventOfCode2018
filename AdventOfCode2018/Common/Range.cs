using System;

namespace Common
{
    class Range<T> where T : IComparable<T>
    {
        public T From { get; set; }
        public T To { get; set; }

        public Range(T @from, T to)
        {
            From = @from;
            To = to;
        }

        public bool HasIntersectionWith(Range<T> second)
        {
            return  From.CompareTo(second.From) <= 0 && To.CompareTo(second.To) >=0 ||
                    From.CompareTo(second.From) <= 0 && To.CompareTo(second.From) > 0 ||
                    From.CompareTo(second.To) < 0 && To.CompareTo(second.To) >= 0;
        }

        public Range<T> GetUnionWith(Range<T> second)
        {
            // при условии что они пересекаются
            return new Range<T>(Min(From, second.From), Max(To, second.To));
        }

        public Range<T> GetIntersectionWith(Range<T> second)
        {
            // при условии что они пересекаются
            return new Range<T>(Max(From, second.From), Min(To, second.To));
        }

        static T Min(T first, T second)
        {
            return first.CompareTo(second) < 0 ? first : second;
        }
        static T Max(T first, T second)
        {
            return first.CompareTo(second) > 0 ? first : second;
        }
    }
}