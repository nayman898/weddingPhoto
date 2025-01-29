using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingPhotoUpload.Global.DataObjects
{
    public class FileUpload
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Stream Content { get; set; }
        public string ContentType { get; set; }
    }
}
