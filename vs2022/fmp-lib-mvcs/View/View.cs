/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System;
using System.Collections.Generic;

namespace XTC.FMP.LIB.MVCS
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

            public void RouteMessage(string _message, Model.Status? _status, object _data)
            {
                if (!unit_.subscribers_.ContainsKey(_message))
                    return;
                unit_.subscribers_[_message](_status, _data);
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

            public Facade(string _uid)
            {
                uid_ = _uid;
            }

            public string getUID()
            {
                return uid_;
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
            private string uid_;
        }
        #endregion

        public View(string _uid)
        {
            uid_ = _uid;
        }

        public string getUID()
        {
            return uid_;
        }

        /// <summary>
        /// 设置用户数据
        /// </summary>
        /// <param name="_key">键</param>
        /// <param name="_value">值，为空时删除对应的键</param>
        public void setUserData(string _key, UserData? _value)
        {
            if (null == _value)
            {
                if (userDatas_.ContainsKey(_key))
                    userDatas_.Remove(_key);
                return;
            }
            userDatas_[_key] = _value;
        }

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <param name="_key">键</param>
        /// <returns>不存在时返回空</returns>
        public UserData? getUserData(string _key)
        {
            if (!userDatas_.ContainsKey(_key))
                return null;
            return userDatas_[_key];
        }


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
        /// 添加订阅者
        /// </summary>
        /// <param name="_message">订阅的消息</param>
        /// <param name="_handler">处理函数</param>
        protected void addSubscriber(string _message, Action<Model.Status?, object> _handler)
        {
            subscribers_[_message] = _handler;
        }

        /// <summary>
        /// 移除订阅者
        /// </summary>
        /// <param name="_message">订阅的消息</param>
        protected void removeSubscriber(string _message)
        {
            if (!subscribers_.ContainsKey(_message))
                return;
            subscribers_.Remove(_message);
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
        private string uid_;
        private Dictionary<string, UserData> userDatas_ = new Dictionary<string, UserData>();
        private Dictionary<string, Action<Model.Status?, object>> subscribers_ = new Dictionary<string, Action<Model.Status?, object>>();
    }
}//namespace
