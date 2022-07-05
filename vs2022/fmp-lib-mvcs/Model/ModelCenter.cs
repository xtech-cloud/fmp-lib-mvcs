/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System.Collections.Generic;

namespace XTC.FMP.LIB.MVCS
{
    internal class ModelCenter
    {
        public ModelCenter(Board _board)
        {
            board_ = _board;
        }

        public Error Register(string _uuid, Model.Inner _inner)
        {
            board_.getLogger()?.Info("register model: {0}", _uuid);
            if (units_.ContainsKey(_uuid))
                return Error.NewAccessErr("model {0} exists", _uuid);
            units_[_uuid] = _inner;
            return Error.OK;
        }

        public Error Cancel(string _uuid)
        {
            board_.getLogger()?.Info("cancel model: {0}", _uuid);
            if (!units_.ContainsKey(_uuid))
                return Error.NewAccessErr("model {0} not found", _uuid);
            units_.Remove(_uuid);
            return Error.OK;
        }

        public Model.Inner? FindUnit(string _uuid)
        {
            Model.Inner? inner;
            if (!units_.TryGetValue(_uuid, out inner))
                return null;
            return inner;
        }

        public void PreSetup()
        {
            board_.getLogger()?.Info("perSetup models");
            foreach (Model.Inner inner in units_.Values)
            {
                board_.getLogger()?.Debug("perSetup model: {0}", inner.getUnit().getUID());
                inner.PreSetup();
            }
        }

        public void Setup()
        {
            board_.getLogger()?.Info("setup models");
            foreach (Model.Inner inner in units_.Values)
            {
                board_.getLogger()?.Debug("setup model: {0}", inner.getUnit().getUID());
                inner.Setup();
            }
        }

        public void PostSetup()
        {
            board_.getLogger()?.Info("postSetup models");
            foreach (Model.Inner inner in units_.Values)
            {
                board_.getLogger()?.Debug("postSetup model: {0}", inner.getUnit().getUID());
                inner.PostSetup();
            }
        }

        public void PreDismantle()
        {
            board_.getLogger()?.Info("perDismantle models");
            foreach (Model.Inner inner in units_.Values)
            {
                board_.getLogger()?.Debug("perDismantle model: {0}", inner.getUnit().getUID());
                inner.PreDismantle();
            }
        }

        public void Dismantle()
        {
            board_.getLogger()?.Info("dismantle models");
            foreach (Model.Inner inner in units_.Values)
            {
                board_.getLogger()?.Debug("dismantle model: {0}", inner.getUnit().getUID());
                inner.Dismantle();
            }
        }

        public void PostDismantle()
        {
            board_.getLogger()?.Info("postDismantle models");
            foreach (Model.Inner inner in units_.Values)
            {
                board_.getLogger()?.Debug("postDismantle model: {0}", inner.getUnit().getUID());
                inner.PostDismantle();
            }
        }

        public Error PushStatus(string _uuid, Model.Status _status)
        {
            board_.getLogger()?.Info("push status: {0}", _uuid);

            if (status_.ContainsKey(_uuid))
                return Error.NewAccessErr("status {0} exists", _uuid);
            status_[_uuid] = _status;
            return Error.OK;
        }

        public Error PopStatus(string _uuid)
        {
            board_.getLogger()?.Info("pop status: {0}", _uuid);

            if (!status_.ContainsKey(_uuid))
                return Error.NewAccessErr("status {0} not found", _uuid);
            status_.Remove(_uuid);
            return Error.OK;
        }

        public Model.Status? FindStatus(string _uuid)
        {
            Model.Status? status;
            if (!status_.TryGetValue(_uuid, out status))
                return null;
            return status;
        }

        public void Broadcast(string _action, Model.Status? _status, object _data)
        {
            board_.getViewCenter().HandleAction(_action, _status, _data);
        }

        private Dictionary<string, Model.Inner> units_ = new Dictionary<string, Model.Inner>();

        // 状态列表
        private Dictionary<string, Model.Status> status_ = new Dictionary<string, Model.Status>();

        private Board board_;
    }
}//namespace
