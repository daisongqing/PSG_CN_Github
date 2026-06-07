using System;

namespace RestfulWebRequest.RestfulAttribute
{
    /// <summary>
    /// Restful表特性,
    /// </summary>
    /// <remarks>
    /// MultipartFormDataIgnore属性可选, 默认为false
    ///                         此特性专用于文件传输, ContentType为multipart/form-data时,
    ///                         在转换成formdata 将自动忽略该属性
    ///                         
    /// IsFileName属性必选, 指示哪个字段为上传的文件名
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class FileUploadAttribute : Attribute
    {
        /// <summary>
        /// 转换为multipart/form-data时, 是否忽略该字段
        /// </summary>
        private bool _multipartFormDataIgnore = false;

        /// <summary>
        /// 此字段是否为待上传的文件名字段
        /// </summary>
        private bool _isFileName = false;

        public FileUploadAttribute(bool isFileName)
        {
            _isFileName = isFileName;
        }
        public bool IsFileName => _isFileName;

        public bool MultipartFormDataIgnore
        {
            get => _multipartFormDataIgnore;
            set => _multipartFormDataIgnore = value;
        }
    }
}
