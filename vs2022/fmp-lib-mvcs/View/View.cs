/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System;
using System.Collections.Generic;

namespace XTC.FMP.Lib.MVCS
{
    /// <summary>
    /// 视图层
    /// </summary>
    public class View
    {
        #region
        // 内部类，用于接口隔离,隐藏View无需暴露给外部的公有方法
        internal class Inner
        {
            public Inner(View _unit, Board _board)
            {
                unit_ = _unit;
                unit_.board_ = _board;
            }

            public View getUnit()
            {
                return unit_;
            }

            public void PreSetup()
            {
                unit_.preSetup();
            }

            public void Setup()
            {
                unit_.setup();
            }

            public void PostSetup()
            {
                unit_.postSetup();
            }

            public void PreDismantle()
            {
                unit_.preDismantle();
            }

            public void Dismantle()
            {
                unit_.dismantle();
            }

            public void PostDismantle()
            {
                unit_.postDismantle();
            }

            public Error Handle(string _action, Model.Status? _status, object _data)
            {
                if (!unit_.handlers_.ContainsKey(_action))
                    return Error.NewParamErr("handler {0} not found", _action);
                unit_.handlers_[_action](_status, _data);
                return Error.OK;
            }

            private View unit_;
        }
        #endregion

        #region
        /// <summary> UI装饰 </summary>
        public class Facade
        {
            public interface Bridge
            {
            }

            public void setViewBridge(Bridge _value)
            {
                viewBridge_ = _value;
            }

            public void setUiBridge(Bridge _value)
            {
                uiBridge_ = _value;
            }

            public Bridge? getViewBridge()
            {
                return viewBridge_;
            }

            public Bridge? getUiBridge()
            {
                return uiBridge_;
            }

            private Bridge? viewBridge_;
            private Bridge? uiBridge_;
        }
        #endregion

        private Dictionary<string, Action<Model.Status?, object>> handlers_ = new Dictionary<string, Action<Model.Status?, object>>();


        /// <summary>
        /// 查找一个数据层
        /// </summary>
        /// <param name="_uuid"> 数据层唯一识别码</param>
        /// <returns>找到的数据层</returns>
        protected Model? findModel(string _uuid)
        {
            return board_!.getModelCenter().FindUnit(_uuid)?.getUnit();
        }

        /// <summary>
        /// 查找一个视图层
        /// </summary>
        /// <param name="_uuid"> 视图层唯一识别码</param>
        /// <returns>找到的视图层</returns>
        protected View? findView(string _uuid)
        {
            return board_!.getViewCenter().FindUnit(_uuid)?.getUnit();

        }

        /// <summary>
        /// 查找一个服务层
        /// </summary>
        /// <param name="_uuid"> 服务层唯一识别码</param>
        /// <returns>找到的服务层</returns>
        protected Service? findService(string _uuid)
        {
            return board_!.getServiceCenter().FindUnit(_uuid)?.getUnit();
        }

        /// <summary>
        /// 查找一个UI装饰
        /// </summary>
        /// <param name="_uuid"> UI装饰唯一识别码</param>
        /// <returns>找到的UI装饰</returns>
        protected View.Facade? findFacade(string _uuid)
        {
            return board_!.getViewCenter().FindFacade(_uuid);
        }


        /// <summary>
        /// 添加行为路由，使用指定函数处理指定行为
        /// 设置了路由的行为，在数据层进行广播时，会自动调用相应的处理函数
        /// </summary>
        /// <param name="_action">需要处理的行为</param>
        /// <param name="_handler">行为对应的处理函数</param>
        protected void addRouter(string _action, Action<Model.Status?, object> _handler)
        {
            handlers_[_action] = _handler;
        }

        /// <summary>
        /// 移除行为路由
        /// </summary>
        /// <param name="_action">需要处理的行为</param>
        protected void removeRouter(string _action)
        {
            if (!handlers_.ContainsKey(_action))
                return;
            handlers_.Remove(_action);
        }


        /// <summary>
        /// 观察一个数据层
        /// 当数据层冒泡时，会自动调用相应的处理函数
        /// </summary>
        /// <param name="_uuid">model的uuid</param>
        /// <param name="_action">行为</param>
        protected void addObserver(string _uuid, string _action, Action<Model.Status?, object> _handler)
        {
            Model.Inner? inner = board_!.getModelCenter().FindUnit(_uuid);
            if (null == inner)
                return;
            inner.AddObserver(_action, _handler);
        }

        /// <summary>
        /// 取消观察一个数据层
        /// 当数据层冒泡时，会自动调用相应的处理函数
        /// </summary>
        /// <param name="_uuid">model的uuid</param>
        /// <param name="_action">行为</param>
        protected void removeObserver(string _uuid, string _action, Action<Model.Status?, object> _handler)
        {
            Model.Inner? inner = board_!.getModelCenter().FindUnit(_uuid);
            if (null == inner)
                return;
            inner.RemoveObserver(_action, _handler);
        }

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <returns>
        /// 日志实列
        /// </returns>
        protected Logger? getLogger()
        {
            return board_!.getLogger();
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns>
        /// 配置实列
        /// </returns>
        protected Config? getConfig()
        {
            return board_!.getConfig();
        }

        /// <summary>
        /// 单元的安装前处理
        /// </summary>
        protected virtual void preSetup()
        {

        }

        /// <summary>
        /// 单元的安装
        /// </summary>
        protected virtual void setup()
        {

        }

        /// <summary>
        /// 单元的安装后处理
        /// </summary>
        protected virtual void postSetup()
        {

        }

        /// <summary>
        /// 单元的拆卸前处理
        /// </summary>
        protected virtual void preDismantle()
        {

        }

        /// <summary>
        /// 单元的拆卸
        /// </summary>
        protected virtual void dismantle()
        {

        }

        /// <summary>
        /// 单元的拆卸后处理
        /// </summary>
        protected virtual void postDismantle()
        {

        }

        private Board? board_;
    }
}//namespace
