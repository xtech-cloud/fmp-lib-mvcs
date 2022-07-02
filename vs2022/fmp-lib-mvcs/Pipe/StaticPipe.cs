/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

namespace XTC.FMP.Lib.MVCS
{
    /// <summary>静态管线</summary>
    public class StaticPipe
    {
        public StaticPipe(Board _board)
        {
            board_ = _board;
        }


        /// <summary>注册数据层</summary>
        /// <param name="_uuid">数据层唯一识别码</param>
        /// <param name="_model">数据层实例</param>
        /// <returns>错误</returns>
        public Error RegisterModel(string _uuid, Model _model)
        {
            Model.Inner inner = new Model.Inner(_model, board_);
            return board_.getModelCenter().Register(_uuid, inner);
        }

        /// <summary>注销数据层</summary>
        /// <param name="_uuid">数据层唯一识别码</param>
        /// <returns>错误</returns>
        public Error CancelModel(string _uuid)
        {
            return board_.getModelCenter().Cancel(_uuid);
        }

        /// <summary>注册视图层</summary>
        /// <param name="_uuid">视图层唯一识别码</param>
        /// <param name="_model">视图层实例</param>
        /// <returns>错误</returns>
        public Error RegisterView(string _uuid, View _view)
        {
            View.Inner inner = new View.Inner(_view, board_);
            return board_.getViewCenter().Register(_uuid, inner);
        }

        /// <summary>注销视图层</summary>
        /// <param name="_uuid">视图层唯一识别码</param>
        /// <returns>错误</returns>
        public Error CancelView(string _uuid)
        {
            return board_.getViewCenter().Cancel(_uuid);
        }

        /// <summary>注册控制层</summary>
        /// <param name="_uuid">控制层唯一识别码</param>
        /// <param name="_model">控制层实例</param>
        /// <returns>错误</returns>
        public Error RegisterController(string _uuid, Controller _controller)
        {
            Controller.Inner inner = new Controller.Inner(_controller, board_);
            return board_.getControllerCenter().Register(_uuid, inner);
        }

        /// <summary>注销控制层</summary>
        /// <param name="_uuid">控制层唯一识别码</param>
        /// <returns>错误</returns>
        public Error CancelController(string _uuid)
        {
            return board_.getControllerCenter().Cancel(_uuid);
        }

        /// <summary>注册服务层</summary>
        /// <param name="_uuid">服务层唯一识别码</param>
        /// <param name="_model">服务层实例</param>
        /// <returns>错误</returns>
        public Error RegisterService(string _uuid, Service _service)
        {
            Service.Inner inner = new Service.Inner(_service, board_);
            return board_.getServiceCenter().Register(_uuid, inner);
        }

        /// <summary>注销服务层</summary>
        /// <param name="_uuid">服务层唯一识别码</param>
        /// <returns>错误</returns>
        public Error CancelService(string _uuid)
        {
            return board_.getServiceCenter().Cancel(_uuid);
        }

        /// <summary>注册UI装饰</summary>
        /// <param name="_uuid">UI装饰唯一识别码</param>
        /// <param name="_model">UI装饰实例</param>
        /// <returns>错误</returns>
        public Error RegisterFacade(string _uuid, View.Facade _facade)
        {
            return board_.getViewCenter().PushFacade(_uuid, _facade);
        }

        /// <summary>注销UI装饰</summary>
        /// <param name="_uuid">UI装饰层唯一识别码</param>
        /// <returns>错误</returns>
        public Error CancelFacade(string _uuid)
        {
            return board_.getViewCenter().PopFacade(_uuid);
        }

        private Board board_;
    }
}
