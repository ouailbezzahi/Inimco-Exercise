namespace backend.Models
{
    public class PersonAnalysis
    {
        public int VowelsCount { get; set; }
        public int ConsonantsCount { get; set; }
        public string FullName { get; set; }
        public string ReversedFullName { get; set; }
        public Person Person { get; set; }
    }
}