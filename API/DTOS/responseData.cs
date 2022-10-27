namespace API.DTOS
{
    public class responseData<T>
    {
        public responseData()
        {

        }
        public responseData(IReadOnlyList<T> data, int count = 0)
        {
            Items = data;
            TotalCount = count;
        }
        public IReadOnlyList<T> Items { get; set; }
        public int TotalCount { get; set; }


        //كسب عمل
        public decimal TotalTaxNormal { get; set; }
        //دمغه عاديه
        public decimal TotalStamp { get; set; }
        //تسويه ضريبيه
        public decimal TotalTaxsettlement { get; set; }
        // ضريبه اضافيه
        public decimal TotalTax2 { get; set; }
        //اتساع او متنوعه
        public decimal TotalOther { get; set; }

        public decimal TotalSumTax { get; set; }
        // تنميه
        public decimal TotalTaxDevelopment { get; set; }
    }
}