namespace Stutooroo.Models
{
    public class ListingFilterModel
    {
        public string? SearchString { get; set; }
        public DateTime? DateFilterStart { get; set; }
        public DateTime? DateFilterEnd { get; set; }
        public int? ExperienceLvlIdFilter { get; set; }
        public float? HourlyRateFilter { get; set; }
        public float? RatingFilter { get; set; }
        public int? SubjectGroupIdFilter { get; set; }

        public ListingFilterModel()
        {
            SearchString ??= string.Empty;
            DateFilterStart ??= DateTime.MinValue;
            DateFilterEnd ??= DateTime.MaxValue;
            HourlyRateFilter ??= float.MaxValue;
            RatingFilter ??= 0;
        }

        public static ListingFilterModel FillNullValues(ListingFilterModel l)
        {
            l.SearchString ??= string.Empty;
            l.DateFilterStart ??= DateTime.MinValue;
            l.DateFilterEnd ??= DateTime.MaxValue;
            l.HourlyRateFilter ??= float.MaxValue;
            l.RatingFilter ??= 0;

            return l;
        }

    }
}
