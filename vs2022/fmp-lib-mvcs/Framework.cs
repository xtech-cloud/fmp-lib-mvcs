/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System.Collections.Generic;

namespace XTC.FMP.LIB.MVCS
{
    public class Framework
    {
        private  const string nullException = "board is null";
        private StaticPipe? staticPipe_;
        private DynamicPipe? dynamicPipe_;
        private Board? board_;

        private Dictionary<string, UserData> userDatas_ = new Dictionary<string, UserData>();

        #region Get Set
        public StaticPipe getStaticPipe()
        {
            if (null == staticPipe_)
                throw new System.NullReferenceException(nullException);
            return staticPipe_;
        }

        public DynamicPipe getDynamicPipe()
        {
            if (null == dynamicPipe_)
                throw new System.NullReferenceException(nullException);
            return dynamicPipe_;
        }

        public void setConfig(Config _value)
        {
            if (null == board_)
                throw new System.NullReferenceException(nullException);
            board_.setConfig(_value);
        }

        public void setLogger(Logger _value)
        {
            if (null == board_)
                throw new System.NullReferenceException(nullException);
            board_.setLogger(_value);
        }
        #endregion

        public Framework()
        {
            board_ = new Board();
            staticPipe_ = new StaticPipe(board_);
            dynamicPipe_ = new DynamicPipe(board_);
        }

        ~Framework()
        {
            board_ = null;
            staticPipe_ = null;
            dynamicPipe_ = null;
        }

        /// <summary>
        /// 设置用户数据
        /// </summary>
        /// <param name="_key">键</param>
        /// <param name="_value">值，为空时删除对应的键</param>
        public void setUserData(string _key, UserData? _value)
        {
            if(null == _value)
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
        /// 框架初始化，完成各层中心的实例化
        /// </summary>
        public void Initialize()
        {
            if (null == board_)
                throw new System.NullReferenceException(nullException);

            board_.getLogger()?.Info("initialize framework");
            board_.setModelCenter(new ModelCenter(board_));
            board_.setViewCenter(new ViewCenter(board_));
            board_.setControllerCenter(new ControllerCenter(board_));
            board_.setServiceCenter(new ServiceCenter(board_));
        }

        /// <summary>
        /// 框架安装，完成各层中心已注册组件的安装
        /// 此过程将调用各派生组件的setup方法
        /// </summary>
        public void Setup()
        {
            if (null == board_)
                throw new System.NullReferenceException(nullException);

            board_.getLogger()?.Info("setup framework");
            board_.getModelCenter().PreSetup();
            board_.getViewCenter().PreSetup();
            board_.getControllerCenter().PreSetup();
            board_.getServiceCenter().PreSetup();
            board_.getModelCenter().Setup();
            board_.getViewCenter().Setup();
            board_.getControllerCenter().Setup();
            board_.getServiceCenter().Setup();
            board_.getModelCenter().PostSetup();
            board_.getViewCenter().PostSetup();
            board_.getControllerCenter().PostSetup();
            board_.getServiceCenter().PostSetup();
        }

        /// <summary>
        /// 框架拆卸，完成各层中心已注册组件的拆卸
        /// 此过程将调用各派生组件的dismantle方法
        /// </summary>
        public void Dismantle()
        {
            if (null == board_)
                return;

            board_.getLogger()?.Info("dismantle framework");
            board_.getServiceCenter().PreDismantle();
            board_.getControllerCenter().PreDismantle();
            board_.getViewCenter().PreDismantle();
            board_.getModelCenter().PreDismantle();
            board_.getServiceCenter().Dismantle();
            board_.getControllerCenter().Dismantle();
            board_.getViewCenter().Dismantle();
            board_.getModelCenter().Dismantle();
            board_.getServiceCenter().PostDismantle();
            board_.getControllerCenter().PostDismantle();
            board_.getViewCenter().PostDismantle();
            board_.getModelCenter().PostDismantle();
        }


        /// <summary>
        /// 框架销毁，完成各层中心的释放
        /// </summary>
        public void Release()
        {
            if (null == board_)
                return;

            board_.getLogger()?.Info("release framework");
            board_.setViewCenter(null);
            board_.setServiceCenter(null);
            board_.setModelCenter(null);
            board_.setControllerCenter(null);

        }

    }

}//namespace
