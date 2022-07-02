/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System.Collections.Generic;

namespace XTC.FMP.Lib.MVCS
{
    public class Config
    {
        protected Dictionary<string, Any> fields_ = new Dictionary<string, Any>();

        public virtual void Merge(string _content)
        {
            throw new System.NotImplementedException();
        }

        public bool Has(string _field)
        {
            return fields_.ContainsKey(_field);
        }

        public Any getField(string _aKey)
        {
            if (fields_.TryGetValue(_aKey, out Any field))
                return field;
            return new Any();
        }
    }
}//namespace
