namespace InvalidRecordFinder.Data
{
    public class Field
    {
        public int FieldLength { get; set; }
        public bool IsRequired { get; set; }
        public string Name { get; set; }
        public string? Pattern { get; set; }
        public int StartIndex { get; set; }
        public FieldType Type { get; set; }

        public Field( string name, FieldType type, int startIndex, bool isRequired, int fieldLength, string? pattern )
        {
            Name = name;
            Type = type;
            StartIndex = startIndex;
            FieldLength = fieldLength;
            IsRequired = isRequired;
            Pattern = pattern;
        }

        public enum FieldType
        {
            Alpha,
            Number
        }

        public static readonly List<Field> DefaultFields = new()
        {
            new Field("RecordNumber",FieldType.Number,0,true,4,null),
            new Field("AccountNumber",FieldType.Alpha,4,true,10,@"^\d{2}-\d{7}$"),
            new Field("FirstName",FieldType.Alpha,14,true,20,null),
            new Field("LastName",FieldType.Alpha,34,true,20,null),
            new Field("AddressLine1",FieldType.Alpha,54,true,30,null),
            new Field("AddressLine2",FieldType.Alpha,84,false,30,null),
            new Field("City",FieldType.Alpha,114,true,20,null),
            new Field("State",FieldType.Alpha,134,true,2,@"^[A-Za-z]{2}$"),
            new Field("Amount",FieldType.Number,136,true,7,@"^\d{4}\.\d{2}$")
        };

        public static List<Field> SetFields()
        {
            List<Field> fields = new();
            foreach ( Field field in DefaultFields )
            {
                Field newfield = new(
                    field.Name,
                    field.Type,
                    field.StartIndex,
                    field.IsRequired,
                    field.FieldLength,
                    field.Pattern
                );
                fields.Add( newfield );
            }
            return fields;
        }

    }
}
