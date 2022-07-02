
namespace XTC.FMP.Lib.MVCS
{
    // 内部通讯的主板
    // get和set方法为多语言规范，不直接使用C#的属性存取
    public class Board
    {
        internal void setServiceCenter(ServiceCenter? _value)
        {
            serviceCenter_ = _value;
        }

        internal void setControllerCenter(ControllerCenter? _value)
        {
            controllerCenter_ = _value;
        }

        internal void setViewCenter(ViewCenter? _value)
        {
            viewCenter_ = _value;
        }

        internal void setModelCenter(ModelCenter? _value)
        {
            modelCenter_ = _value;
        }

        public void setConfig(Config? _value)
        {
            config_ = _value;
        }

        public void setLogger(Logger? _logger)
        {
            logger_ = _logger;
        }

        public Config? getConfig()
        {
            return config_;
        }

        public Logger? getLogger()
        {
            return logger_;
        }

        internal ServiceCenter getServiceCenter()
        {
            return serviceCenter_!;
        }

        internal ControllerCenter getControllerCenter()
        {
            return controllerCenter_!;
        }

        internal ViewCenter getViewCenter()
        {
            return viewCenter_!;
        }

        internal ModelCenter getModelCenter()
        {
            return modelCenter_!;
        }

        private ServiceCenter? serviceCenter_;

        private ControllerCenter? controllerCenter_;

        private ViewCenter? viewCenter_;

        private ModelCenter? modelCenter_;

        private Config? config_;

        private Logger? logger_;
    }
}
