using System.Text.RegularExpressions;

namespace InvalidRecordFinder.Data
{
    public class Record
    {
        public string? RecordLine { get; set; }

        public List<InvalidRecord> ValidateRecord( List<Field> fields, List<InvalidRecord> invalidRecords, int lineNumber )
        {
            string recordNumber = "";

            foreach ( Field field in fields )
            {
                Regex regexPattern = new( "" ); // Reset the regex

                // Check if field is in range
                if ( field.StartIndex + field.FieldLength > RecordLine!.Length )
                {
                    if ( field.StartIndex > RecordLine.Length - 1 )
                    {
                        var invalidRecord = new InvalidRecord(
                            string.IsNullOrWhiteSpace( recordNumber ) ? $"Line {lineNumber}" : recordNumber, // if the recordNumber is invalid, use line number
                            field.Name,
                            "Field error: StartIndex past end of line"
                        );
                        invalidRecords.Add( invalidRecord );
                        continue;
                    }
                    else
                    {
                        var invalidRecord = new InvalidRecord(
                            string.IsNullOrWhiteSpace( recordNumber ) ? $"Line {lineNumber}" : recordNumber,
                            field.Name,
                            RecordLine[( int ) field.StartIndex..]
                        );
                        invalidRecords.Add( invalidRecord );
                        continue;
                    }
                }

                var currentValue = RecordLine.Substring( ( int ) field.StartIndex, ( int ) field.FieldLength ).Trim();

                // Skip if not required
                if ( string.IsNullOrWhiteSpace( currentValue ) && !field.IsRequired )
                    continue;

                // Check if there is a value
                if ( string.IsNullOrWhiteSpace( currentValue ) )
                {
                    // Skip if not required
                    if ( field.IsRequired != true )
                    {
                        continue;
                    }

                    var invalidRecord = new InvalidRecord(
                                string.IsNullOrWhiteSpace( recordNumber ) ? $"Line {lineNumber}" : recordNumber,
                                field.Name,
                                "Field error: Empty value" );
                    invalidRecords.Add( invalidRecord );
                    continue;
                }

                // Set the regex pattern

                if ( !string.IsNullOrWhiteSpace( field.Pattern ) )
                {
                    regexPattern = new Regex( field.Pattern );
                }
                else if ( field.Type == Field.FieldType.Number )
                {
                    regexPattern = new Regex( @"^\d*$" );
                }

                // Check the value
                if ( !string.IsNullOrWhiteSpace( regexPattern.ToString() ) )
                {
                    if ( !regexPattern.IsMatch( currentValue ) )
                    {
                        var invalidRecord = new InvalidRecord(
                                string.IsNullOrWhiteSpace( recordNumber ) ? $"Line {lineNumber}" : recordNumber,
                                field.Name,
                                currentValue );
                        invalidRecords.Add( invalidRecord );
                        continue;
                    }

                    // Set the record number if possible
                    if ( field.Name == "RecordNumber" )
                    {
                        recordNumber = currentValue;
                    }
                }
            }

            return invalidRecords;
        }

    }
}
