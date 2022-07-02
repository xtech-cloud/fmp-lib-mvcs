/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

namespace XTC.FMP.Lib.MVCS
{
    public class Error
    {
        public const int NULL = -1;
        public const int PARAM = -2;
        public const int ACCESS = -3;
        public const int EXCEPTION = -99;

        public int getCode()
        {
            return code_;
        }

        public string getMessage()
        {
            return message_;
        }

        public static readonly Error OK = new Error(0, "");

        public Error(int _code, string _message)
        {
            code_ = _code;
            message_ = _message;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", code_, message_);
        }

        public static bool IsOK(Error _err)
        {
            return _err.getCode() == 0;
        }

        public static Error NewNullErr(string _message, params object[] _args)
        {
            string message = string.Format(_message, _args);
            return new Error(NULL, message);
        }

        public static Error NewParamErr(string _message, params object[] _args)
        {
            string message = string.Format(_message, _args);
            return new Error(PARAM, message);
        }

        public static Error NewAccessErr(string _message, params object[] _args)
        {
            string message = string.Format(_message, _args);
            return new Error(ACCESS, message);
        }

        public static Error NewException(System.Exception _ex)
        {
            return new Error(EXCEPTION, _ex.Message);
        }

        protected int code_ = 0;
        protected string message_ = "";
    }//class
}//namespace
