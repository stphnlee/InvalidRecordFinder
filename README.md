# InvalidRecordFinder
This is a Blazor tool to identify records with invalid values. 


## Defaults
The default fields are configured as specified in the project instructions (based on the field definitions in the SampleFiles/SampleFileLayout.xlsx file). To make the program more maintainable and flexible, the fields can be modified or deleted, and additional fields can be created.

## Usage
To use the tool, make sure that the field settings are correct, and then upload a file to process. Any invalid values will be listed in the Results section of the page. Each result will include the record number (or the line number if the record number is invalid), the name of the field with the invalid value, and the value of that field for that record.
