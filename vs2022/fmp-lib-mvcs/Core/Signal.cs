using System.Collections.Generic;

namespace XTC.FMP.LIB.MVCS
{
    public class Signal
    {
        private Model model_;
        private List<System.Action<Model.Status?, object>> slots_;

        public Signal(Model _model)
        {
            model_ = _model;
            slots_ = new List<System.Action<Model.Status?, object>>();
        }

        public void Connect(System.Action<Model.Status?, object> _slot)
        {
            if (slots_.Contains(_slot))
                return;
            slots_.Add(_slot);
        }

        public void Disconnect(Model _model, System.Action<Model.Status?, object> _slot)
        {
            if (!slots_.Contains(_slot))
                return;
            slots_.Remove(_slot);
        }

        public void Emit(object _data)
        {
            foreach(var slot in slots_)
            {
                model_.emit(slot, _data);
            }
        }
    }
}