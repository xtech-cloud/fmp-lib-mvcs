/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

namespace XTC.FMP.LIB.MVCS
{
    public enum LogLevel
    {
        NONE,
        EXCEPTION,
        ERROR,
        WARNING,
        INFO,
        DEBUG,
        TRACE,
        ALL,
    }

    public class Logger
    {
        private System.Diagnostics.StackTrace stackTrace_ = new System.Diagnostics.StackTrace();

        public void setLevel(LogLevel _level)
        {
            level_ = _level;
        }

        public void Trace(string _message, params object[] _args)
        {
            log(LogLevel.TRACE, this.trace, _message, _args);
        }

        public void Debug(string _message, params object[] _args)
        {
            log(LogLevel.DEBUG, this.debug, _message, _args);
        }

        public void Info(string _message, params object[] _args)
        {
            log(LogLevel.INFO, this.info, _message, _args);
        }

        public void Warning(string _message, params object[] _args)
        {
            log(LogLevel.WARNING, this.warning, _message, _args);
        }

        public void Error(string _message, params object[] _args)
        {
            log(LogLevel.ERROR, this.error, _message, _args);
        }

        public void Exception(System.Exception _ex)
        {
            if (level_ < LogLevel.EXCEPTION)
                return;
            this.exception(_ex);
        }

        protected virtual void trace(string _categoray, string _message)
        {
        }

        protected virtual void debug(string _category, string _message)
        {
        }

        protected virtual void info(string _category, string _message)
        {
        }

        protected virtual void warning(string _category, string _message)
        {
        }

        protected virtual void error(string _category, string _message)
        {
        }

        protected virtual void exception(System.Exception _exception)
        {
        }

        private void log(LogLevel _level, System.Action<string, string> _log, string _message, params object[] _args)
        {
            if (level_ < _level)
                return;

            string message = "";
            if (null != _args && _args.Length > 0)
            {
                message = string.Format(_message, _args);
            }
            else
            {
                message = _message;
            }
            string method;
            string file;
            getMethod(out file, out method);
            string categoray = string.Format("{0}::{1}",file, method);
            _log(categoray, message);
        }

        private void getMethod(out string _file, out string _method)
        {
            _method = "";
            _file = "";
            try
            {
                System.Diagnostics.StackFrame sf = new System.Diagnostics.StackTrace().GetFrame(3);
                _method = sf.GetMethod().Name;
                _file = sf.GetMethod().DeclaringType.FullName;
            }
            catch (System.Exception)
            {
            }
        }

        private LogLevel level_ = LogLevel.ALL;
    }
}
