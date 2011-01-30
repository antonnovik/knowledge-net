using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.KIF.Converter.model.kif
{
    public class KifRTException : SystemException
    {

        private String _msgString;

        /**
           Create an instance of KifRTException with default message.
        */
        public KifRTException()
        {
            _msgString = "KifRTException !".ToString();
        }

        /** 
	   Create an instance of KifRTException with given message.
           @param msg the given message to be associated with the exception.
        */
        public KifRTException(String msg): base(msg)
        {
            _msgString = msg;
        }

        /**
	 * Get the message associated with the exception.
	 * @return the message associated with the exception.
         */
        public virtual String getMessage()
        {
            return _msgString;

        }
    }

}
