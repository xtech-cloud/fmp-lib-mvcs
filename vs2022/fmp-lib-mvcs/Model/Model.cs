/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System.Collections.Generic;

namespace XTC.FMP.Lib.MVCS
{
    /// <summary>
    /// 数据单元
    /// </summary>
    public class Model
    {
        #region
        // 内部类，用于接口隔离,隐藏Model无需暴露给外部的公有方法
        internal class Inner
        {
            internal Inner(Model _unit, Board _board)
            {
                unit_ = _unit;
                unit_.board_ = _board;
            }

            public Model getUnit()
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

            public void AddObserver(string _action, System.Action<Status?, object> _handler)
            {
                unit_.addObserver(_action, _handler);
            }

            public void RemoveObserver(string _action, System.Action<Status?, object> _handler)
            {
                unit_.removeObserver(_action, _handler);
            }

            private Model unit_;
        }
        #endregion

        #region
        /// <summary> 状态 </summary>
        public class Status
        {
            // 内部工厂，用于赋值私有成员
            internal class Factory
            {
                public T New<T>(Board _board, string _uuid) where T : Status, new()
                {
                    T status = new T();
                    status.board_ = _board;
                    status.uuid_ = _uuid;
                    return status;
                }
            }

            /// <summary>获取状态码</summary>
            public int getCode()
            {
                return code_;
            }

            /// <summary>获取状态信息</summary>
            public string getMessage()
            {
                return message_;
            }

            /// <summary>获取唯一识别码</summary>
            public string getUuid()
            {
                return uuid_;
            }

            public Status()
            {
            }

            public static T New<T>(int _code, string _message) where T : Status, new()
            {
                T status = new T();
                status.code_ = _code;
                status.message_ = _message;
                return status;
            }

            /// <summary> 访问数据中心中的已注册状态 </summary>
            /// <param name="_uuid"> 状态的唯一识别码 </param>
            public Status? Access(string _uuid)
            {
                if (string.IsNullOrEmpty(_uuid))
                    return null;
                return board_!.getModelCenter().FindStatus(_uuid);
            }

            protected int code_ = 0;
            protected string message_ = "";
            private string uuid_ = "";
            private Board? board_;
        }
        #endregion




        /// <summary>向视图层广播消息</summary>
        /// 适用于状态发生变化后，通知外部
        /// 在视图层使用addRouter和removeRouter
        /// <param name="_action">行为</param>
        /// <param name="_data">数据</param>
        public void Broadcast(string _action, object _data)
        {
            board_!.getModelCenter()!.Broadcast(_action, status_, _data);
        }

        /// <summary>向视图层冒泡</summary>
        /// 适用于外部获取未发生变化的状态
        /// 在视图层使用addObserver和removeObserver
        /// <param name="_action">行为</param>
        /// <param name="_data">数据</param>
        public void Bubble(string _action, object _data)
        {
            if (null == observers_)
                return;

            List<System.Action<Status?, object>>? handlers;
            if (!observers_.TryGetValue(_action, out handlers))
                return;

            foreach(var handler in handlers)
            {
                handler(status_, _data);
            }
        }


        public void SetProperty(string _key, Any _value)
        {
            if (null == property_)
                return;
            if (!isAllowSetProperty_)
                throw new System.MethodAccessException("Not allowed to set a property, the isAllowSetProperty_ is false");

            property_[_key] = _value;
        }


        public Any GetProperty(string _key)
        {
            if (null == property_)
                return new Any();
            Any value;
            if(!property_.TryGetValue(_key, out value))
            {
                value = new Any();
            }
            return value;
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
        /// 查找一个控制层
        /// </summary>
        /// <param name="_uuid"> 控制层唯一识别码</param>
        /// <returns>找到的控制层</returns>
        protected Controller? findController(string _uuid)
        {
            return board_!.getControllerCenter().FindUnit(_uuid)?.getUnit();

        }

        /// <summary>
        /// 从数据层中创建一个已经注册的状态
        /// </summary>
        /// <param name="_uuid">状态唯一识别码</param>
        /// <param name="_err">错误</param>
        /// <returns>状态<returns>
        protected Model.Status? spawnStatus<T>(string _uuid, out Error _err) where T : Model.Status, new()
        {
            Model.Status.Factory factory = new Model.Status.Factory();
            Status status = factory.New<T>(board_!, _uuid);
            _err = board_!.getModelCenter().PushStatus(_uuid, status);
            if (Error.IsOK(_err))
                return status;
            return null;
        }

        /// <summary>
        /// 从数据中心中销毁一个已经注册的状态
        /// </summary>
        /// <param name="_uuid">状态唯一识别码</param>
        /// <param name="_err">错误</param>
        protected void killStatus(string _uuid, out Error _err)
        {
            _err = board_!.getModelCenter().PopStatus(_uuid);
        }

        protected Status? status_;

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

        private void addObserver(string _action, System.Action<Status?, object> _handler)
        {
            if (null == _handler)
                throw new System.ArgumentNullException("_handler is null");

            if(null == observers_)
                observers_ = new Dictionary<string, List<System.Action<Status?, object>>>();
            if (!observers_.ContainsKey(_action))
                observers_[_action] = new List<System.Action<Status?, object>>();
            observers_[_action].Add(_handler);
        }

        private void removeObserver(string _action, System.Action<Status?, object> _handler)
        {
            if (null == _handler)
                throw new System.ArgumentNullException("_handler is null");

            if (null == observers_)
                return;
            List<System.Action<Status?, object>> handlers;
            if (!observers_.TryGetValue(_action, out handlers))
                return;
            handlers.Remove(_handler);
        }

        protected Dictionary<string, Any> property_ = new Dictionary<string, Any>();
        protected bool isAllowSetProperty_ = true;
        private Dictionary<string, List<System.Action<Status?, object>>>? observers_;
        private Board? board_;
    }
}//namespace
