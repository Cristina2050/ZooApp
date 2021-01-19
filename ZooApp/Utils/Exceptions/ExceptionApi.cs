using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Exceptions
{
    public class ExceptionApi:Exception
    {
        /// <summary>
        /// The identifier
        /// </summary>
        private Guid _id;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set
            {
                if (_id != null)
                {
                    _id = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the user message.
        /// </summary>

        public string ErrorMessage { get; set; }

        /// <summary>
        /// Determines whether [is null identifier].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is null identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNullId()
        {
            return _id == null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionDto"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="messagae">The messagae.</param>
        public ExceptionApi(Guid id, string messagae) : base(messagae)
        {
            _id = id;
            ErrorMessage = messagae;
        }
    }
}
