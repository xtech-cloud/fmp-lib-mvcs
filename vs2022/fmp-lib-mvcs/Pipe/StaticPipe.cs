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
        /// <param name="_model">数据层实例</param>
        /// <returns>错误</returns>
        public Error RegisterModel(Model _model)
        {
            Model.Inner inner = new Model.Inner(_model, board_);
            return board_.getModelCenter().Register(_model.getUID(), inner);
        }

        /// <summary>注销数据层</summary>
        /// <param name="_model">数据层实例</param>
        /// <returns>错误</returns>
        public Error CancelModel(Model _model)
        {
            return board_.getModelCenter().Cancel(_model.getUID());
        }

        /// <summary>注册视图层</summary>
        /// <param name="_view">视图层实例</param>
        /// <returns>错误</returns>
        public Error RegisterView(View _view)
        {
            View.Inner inner = new View.Inner(_view, board_);
            return board_.getViewCenter().Register(_view.getUID(), inner);
        }

        /// <summary>注销视图层</summary>
        /// <param name="_view">视图层实例</param>
        /// <returns>错误</returns>
        public Error CancelView(View _view)
        {
            return board_.getViewCenter().Cancel(_view.getUID());
        }

        /// <summary>注册控制层</summary>
        /// <param name="_controller">控制层实例</param>
        /// <returns>错误</returns>
        public Error RegisterController(Controller _controller)
        {
            Controller.Inner inner = new Controller.Inner(_controller, board_);
            return board_.getControllerCenter().Register(_controller.getUID(), inner);
        }

        /// <summary>注销控制层</summary>
        /// <param name="_controller">控制层实例</param>
        /// <returns>错误</returns>
        public Error CancelController(Controller _controller)
        {
            return board_.getControllerCenter().Cancel(_controller.getUID());
        }

        /// <summary>注册服务层</summary>
        /// <param name="_service">服务层实例</param>
        /// <returns>错误</returns>
        public Error RegisterService(Service _service)
        {
            Service.Inner inner = new Service.Inner(_service, board_);
            return board_.getServiceCenter().Register(_service.getUID(), inner);
        }

        /// <summary>注销服务层</summary>
        /// <param name="_service">服务层实例</param>
        /// <returns>错误</returns>
        public Error CancelService(Service _service)
        {
            return board_.getServiceCenter().Cancel(_service.getUID());
        }

        /// <summary>注册UI装饰</summary>
        /// <param name="_facade">UI装饰实例</param>
        /// <returns>错误</returns>
        public Error RegisterFacade(View.Facade _facade)
        {
            return board_.getViewCenter().PushFacade(_facade);
        }

        /// <summary>注销UI装饰</summary>
        /// <param name="_facade">UI装饰实例</param>
        /// <returns>错误</returns>
        public Error CancelFacade(View.Facade _facade)
        {
            return board_.getViewCenter().PopFacade(_facade);
        }

        private Board board_;
    }
}
