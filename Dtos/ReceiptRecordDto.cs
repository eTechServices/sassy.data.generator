using sassy.bulk.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace sassy.bulk.Dtos
{
    public class ReceiptRecordDto
    {
        public int OrderNo { get; set; }
        public string RecordNo { get; set; }
        public RecordType RecordType { get; set; }
        [NotMapped]
        public string RecordTypeString { get { return Regex.Replace(RecordType.ToString(), "([a-z])([A-Z])", "$1 $2"); } }
        public string Data { get; set; }
        public string Format { get; set; } = FormatType.Image.ToString();
    }
}
