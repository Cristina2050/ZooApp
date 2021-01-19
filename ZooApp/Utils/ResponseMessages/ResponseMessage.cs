using System;
using System.Collections.Generic;

namespace Utils.ResponseMessages
{
    public class ResponseMessage<T> where T : class
    {
        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public ICollection<T> CollectionData { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseMessage"/> class.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="id"></param>
        public ResponseMessage(string status, string message, ICollection<T> data = null, Guid? id = null)
        {
            Status = status;
            Message = id != null ? string.Concat(message) : message;
            CollectionData = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseMessage"/> class.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="id"></param>
        public ResponseMessage(string status, string message, T data = null, Guid? id = null)
        {
            Status = status;
            Message = id != null ? string.Concat(message) : message;
            Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseMessage"/> class.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="id"></param>
        public ResponseMessage(string status, string message, Guid? id = null)
        {
            Status = status;
            Message = id != null ? string.Concat(message) : message;
            Data = null;
            CollectionData = null;
        }
    }
}
