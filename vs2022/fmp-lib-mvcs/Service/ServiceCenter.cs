/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System.Collections.Generic;

namespace XTC.FMP.Lib.MVCS
{
    internal class ServiceCenter
    {
        public ServiceCenter(Board _board)
        {
            board_ = _board;
        }

        public Error Register(string _uuid, Service.Inner _inner)
        {
            board_.getLogger()?.Info("register service {0}", _uuid);
            if (units_.ContainsKey(_uuid))
                return Error.NewAccessErr("service {0} exists", _uuid);
            units_[_uuid] = _inner;
            return Error.OK;
        }

        public Error Cancel(string _uuid)
        {
            board_.getLogger()?.Info("cancel service {0}", _uuid);
            if (!units_.ContainsKey(_uuid))
                return Error.NewAccessErr("service {0} not found", _uuid);
            units_.Remove(_uuid);
            return Error.OK;
        }

        public Service.Inner? FindUnit(string _uuid)
        {
            Service.Inner? inner;
            if (!units_.TryGetValue(_uuid, out inner))
                return null;
            return inner;
        }

        public void PreSetup()
        {
            board_.getLogger()?.Info("perSetup services");
            foreach (Service.Inner inner in units_.Values)
            {
                inner.PreSetup();
            }
        }

        public void Setup()
        {
            board_.getLogger()?.Info("setup services");
            foreach (Service.Inner inner in units_.Values)
            {
                inner.Setup();
            }
        }

        public void PostSetup()
        {
            board_.getLogger()?.Info("postSetup services");
            foreach (Service.Inner inner in units_.Values)
            {
                inner.PostSetup();
            }
        }

        public void PreDismantle()
        {
            board_.getLogger()?.Info("preDismantle services");
            foreach (Service.Inner inner in units_.Values)
            {
                inner.PreDismantle();
            }
        }

        public void Dismantle()
        {
            board_.getLogger()?.Info("dismantle services");
            foreach (Service.Inner inner in units_.Values)
            {
                inner.Dismantle();
            }
        }

        public void PostDismantle()
        {
            board_.getLogger()?.Info("postDismantle services");
            foreach (Service.Inner inner in units_.Values)
            {
                inner.PostDismantle();
            }
        }

        private Dictionary<string, Service.Inner> units_ = new Dictionary<string, Service.Inner>();
        private Board board_;
    }
}//namespace
