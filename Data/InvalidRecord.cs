namespace InvalidRecordFinder.Data
{
    public class InvalidRecord
    {
        public string? RecordNumber { get; }
        public string? FieldName { get; }
        public string? Value { get; }
        public InvalidRecord( string recordNumber, string fieldName, string value )
        {
            RecordNumber = recordNumber;
            FieldName = fieldName;
            Value = value;
        }
    }
}
