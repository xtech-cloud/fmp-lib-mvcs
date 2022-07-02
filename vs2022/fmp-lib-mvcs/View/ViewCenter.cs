/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System.Collections.Generic;

namespace XTC.FMP.Lib.MVCS
{
    internal class ViewCenter
    {
        public ViewCenter(Board _board)
        {
            board_ = _board;
        }

        public void HandleAction(string _action, Model.Status? _status, object _obj)
        {
            foreach (View.Inner inner in units_.Values)
            {
                inner.Handle(_action, _status, _obj);
            }
        }

        public Error Register(string _uuid, View.Inner _inner)
        {
            board_.getLogger()?.Info("register view {0}", _uuid);
            if (units_.ContainsKey(_uuid))
                return Error.NewAccessErr("view {0} exists", _uuid);
            units_[_uuid] = _inner;
            return Error.OK;
        }

        public Error Cancel(string _uuid)
        {
            board_.getLogger()?.Info("cancel view {0}", _uuid);
            if (!units_.ContainsKey(_uuid))
                return Error.NewAccessErr("view {0} not found", _uuid);
            units_.Remove(_uuid);
            return Error.OK;
        }

        public View.Inner? FindUnit(string _uuid)
        {
            View.Inner? inner;
            if (!units_.TryGetValue(_uuid, out inner))
                return null;
            return inner;
        }
        public Error PushFacade(string _uuid, View.Facade _facade)
        {
            board_.getLogger()?.Info("push facade {0}", _uuid);

            if (facades_.ContainsKey(_uuid))
                return Error.NewAccessErr("facade {0} exists", _uuid);
            facades_[_uuid] = _facade;
            return Error.OK;
        }

        public Error PopFacade(string _uuid)
        {
            board_.getLogger()?.Info("pop facade {0}", _uuid);

            if (!facades_.ContainsKey(_uuid))
                return Error.NewAccessErr("facade {0} not found", _uuid);
            facades_.Remove(_uuid);
            return Error.OK;
        }

        public View.Facade? FindFacade(string _uuid)
        {
            View.Facade? facade;
            if (!facades_.TryGetValue(_uuid, out facade))
                return null;
            return facade;
        }

        public void PreSetup()
        {
            board_.getLogger()?.Info("preSetup views");
            foreach (View.Inner inner in units_.Values)
            {
                inner.PreSetup();
            }
        }

        public void Setup()
        {
            board_.getLogger()?.Info("setup views");
            foreach (View.Inner inner in units_.Values)
            {
                inner.Setup();
            }
        }

        public void PostSetup()
        {
            board_.getLogger()?.Info("postSetup views");
            foreach (View.Inner inner in units_.Values)
            {
                inner.PostSetup();
            }
        }

        public void PreDismantle()
        {
            board_.getLogger()?.Info("preDismantle views");
            foreach (View.Inner inner in units_.Values)
            {
                inner.PreDismantle();
            }
        }

        public void Dismantle()
        {
            board_.getLogger()?.Info("dismantle views");
            foreach (View.Inner inner in units_.Values)
            {
                inner.Dismantle();
            }
        }

        public void PostDismantle()
        {
            board_.getLogger()?.Info("postDismantle views");
            foreach (View.Inner inner in units_.Values)
            {
                inner.PostDismantle();
            }
        }

        private Dictionary<string, View.Inner> units_ = new Dictionary<string, View.Inner>();
        private Dictionary<string, View.Facade> facades_ = new Dictionary<string, View.Facade>();

        private Board board_;
    }
}//namespace
