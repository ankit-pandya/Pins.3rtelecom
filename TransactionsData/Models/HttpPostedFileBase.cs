using System.IO;

namespace TransactionsData.Models
{
    public class HttpPostedFileBase
    {
        public int ContentLength { get; internal set; }
        public object FileName { get; internal set; }
        public Stream InputStream { get; internal set; }
    }
}