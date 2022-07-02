/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

namespace XTC.FMP.Lib.MVCS
{
    /// <summary>动态管线</summary>
    public class DynamicPipe
    {
        public DynamicPipe(Board _board)
        {
            board_ = _board;
        }


        /// <summary>添加数据层</summary>
        /// <param name="_uuid">数据层唯一识别码</param>
        /// <param name="_model">数据层实列</param>
        /// <returns>错误</returns>
        public Error PushModel(string _uuid, Model _model)
        {
            Model.Inner inner = new Model.Inner(_model, board_);
            Error err = board_.getModelCenter().Register(_uuid, inner);
            if (!Error.IsOK(err))
                return err;
            inner.Setup();
            return Error.OK;
        }

        /// <summary>删除数据层</summary>
        /// <param name="_uuid">数据层唯一识别码</param>
        /// <returns>错误</returns>
        public Error PopModel(string _uuid)
        {
            Model.Inner? inner = board_.getModelCenter().FindUnit(_uuid);
            if (null == inner)
                return Error.NewAccessErr("model {0} not found", _uuid);
            inner.Dismantle();
            return board_.getModelCenter().Cancel(_uuid);
        }

        /// <summary>添加视图层</summary>
        /// <param name="_uuid">视图层唯一识别码</param>
        /// <param name="_view">视图层实列</param>
        /// <returns>错误</returns>
        public Error PushView(string _uuid, View _view)
        {
            View.Inner inner = new View.Inner(_view, board_);
            Error err = board_.getViewCenter().Register(_uuid, inner);
            if (!Error.IsOK(err))
                return err;
            inner.Setup();
            return Error.OK;
        }

        /// <summary>删除视图层</summary>
        /// <param name="_uuid">视图层唯一识别码</param>
        /// <returns>错误</returns>
        public Error PopView(string _uuid)
        {
            View.Inner? inner = board_.getViewCenter().FindUnit(_uuid);
            if (null == inner)
                return Error.NewAccessErr("view {0} not found", _uuid);
            inner.Dismantle();
            return board_.getViewCenter().Cancel(_uuid);
        }

        /// <summary>添加控制层</summary>
        /// <param name="_uuid">控制层唯一识别码</param>
        /// <param name="_view">控制层实列</param>
        /// <returns>错误</returns>
        public Error PushController(string _uuid, Controller _controller)
        {
            Controller.Inner inner = new Controller.Inner(_controller, board_);
            Error err = board_.getControllerCenter().Register(_uuid, inner);
            if (!Error.IsOK(err))
                return err;
            inner.Setup();
            return Error.OK;
        }

        /// <summary>删除控制层</summary>
        /// <param name="_uuid">控制层唯一识别码</param>
        /// <returns>错误</returns>
        public Error PopController(string _uuid)
        {
            Controller.Inner? inner = board_.getControllerCenter().FindUnit(_uuid);
            if (null == inner)
                return Error.NewAccessErr("controller {0} not found", _uuid);
            inner.Dismantle();
            return board_.getControllerCenter().Cancel(_uuid);
        }

        /// <summary>添加服务层</summary>
        /// <param name="_uuid">服务层唯一识别码</param>
        /// <param name="_view">服务层实列</param>
        /// <returns>错误</returns>
        public Error PushService(string _uuid, Service _service)
        {
            Service.Inner inner = new Service.Inner(_service, board_);
            Error err = board_.getServiceCenter().Register(_uuid, inner);
            if (!Error.IsOK(err))
                return err;
            inner.Setup();
            return Error.OK;
        }

        /// <summary>删除服务层</summary>
        /// <param name="_uuid">服务层唯一识别码</param>
        /// <returns>错误</returns>
        public Error PopService(string _uuid)
        {
            Service.Inner? inner = board_.getServiceCenter().FindUnit(_uuid);
            if (null == inner)
                return Error.NewAccessErr("controller {0} not found", _uuid);
            inner.Dismantle();
            return board_.getServiceCenter().Cancel(_uuid);
        }

        /// <summary>添加UI装饰</summary>
        /// <param name="_uuid">UI装饰唯一识别码</param>
        /// <param name="_model">UI装饰实例</param>
        /// <returns>错误</returns>
        public Error PushFacade(string _uuid, View.Facade _facade)
        {
            return board_.getViewCenter().PushFacade(_uuid, _facade);
        }

        /// <summary>删除UI装饰</summary>
        /// <param name="_uuid">UI装饰层唯一识别码</param>
        /// <returns>错误</returns>
        public Error PopFacade(string _uuid)
        {
            return board_.getViewCenter().PopFacade(_uuid);
        }

        private Board board_;

    }
}
