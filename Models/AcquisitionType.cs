namespace Models
{
    public record AcquisitionType : ModelBase
    {
        public string? Name { get; set; }

        public int Sequence { get; set; }
    }
}
