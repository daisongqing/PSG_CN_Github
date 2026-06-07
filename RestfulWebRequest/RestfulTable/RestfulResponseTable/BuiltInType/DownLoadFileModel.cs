using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType
{
    public class DownLoadFileModel
    {
        private Stream _stream;
        private string _fileName;
        private string _fileExtension;

        public Stream Stream => _stream;
        public string FileName => _fileName;
        public string FileExtension => _fileExtension;

        public DownLoadFileModel(Stream stream,
                                string fileName)
        {
            _stream = stream;
            _fileName = fileName.Replace("\"", "");
            _fileExtension = _fileName.Split('.').Last();
        }
    }
}
