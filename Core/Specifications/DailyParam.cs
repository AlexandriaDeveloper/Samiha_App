namespace Core.Specifications
{
    public class DailyParam : Param
    {
        public string Name { get; set; } = "";

        public decimal? Total { get; set; }
        public decimal? TotalTaxDevelopment { get; set; }

        public DateTime? DailyDate { get; set; }
        public DateTime? InMonth { get; set; }
    }


    public class DailyBoxParam : Param
    {
        public int? Id { get; set; }
        public string Name { get; set; } = "";
        public int? DailyId { get; set; }
        public int? BoxId { get; set; }
        public int? CollageId { get; set; }
    }

    public class BoxParam : Param
    {
        public string Name { get; set; }
        public int? CollageId { get; set; }
    }
}