/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System.Collections.Generic;
using System.Threading.Tasks;

namespace XTC.FMP.LIB.MVCS
{
    /// <summary>
    /// 服务层
    /// </summary>
    public class Service
    {
        #region
        // 内部类，用于接口隔离,隐藏Service无需暴露给外部的公有方法
        internal class Inner
        {
            public Inner(Service _unit, Board _board)
            {
                unit_ = _unit;
                unit_.board_ = _board;
            }

            public Service getUnit()
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

            private Service unit_;
        }
        #endregion

        /// <summary> 选项 </summary>
        public class Options
        {
            /// 头
            public Dictionary<string, string> header = new Dictionary<string, string>();
            public string method = "GET";
            // 是否使用mock
            public bool useMock = false;
        }

        public Service(string _uid)
        {
            uid_ = _uid;
        }

        public string getUID()
        {
            return uid_;
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
        /// 查找一个服务层
        /// </summary>
        /// <param name="_uuid"> 服务层唯一识别码</param>
        /// <returns>找到的服务层</returns>
        protected Service? findService(string _uuid)
        {
            return board_!.getServiceCenter().FindUnit(_uuid)?.getUnit();
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

        /// 别名列表
        private Board? board_;
        private string uid_;
    }
}//namespace
