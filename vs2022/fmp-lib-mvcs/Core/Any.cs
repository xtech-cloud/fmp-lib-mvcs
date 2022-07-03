/********************************************************************
     Copyright (c) XTechCloud
     All rights reserved.
*********************************************************************/

using System.Collections.Generic;

namespace XTC.FMP.LIB.MVCS
{
    /// <summary>
    /// 使用字符串作为底层数据的任意类型
    /// 支持不同类型之间直接转换
    /// </summary>
    public class Any
    {
        public enum Tag
        {
            NULL = 0,
            StringValue = 1,
            Int32Value = 2,
            Int64Value = 3,
            Float32Value = 4,
            Float64Value = 5,
            BoolValue = 6,
            BytesValue = 7,
            StringAryValue = 11,
            Int32AryValue = 12,
            Int64AryValue = 13,
            Float32AryValue = 14,
            Float64AryValue = 15,
            BoolAryValue = 16,
            StringMapValue = 21,
            Int32MapValue = 22,
            Int64MapValue = 23,
            Float32MapValue = 24,
            Float64MapValue = 25,
            BoolMapValue = 26,
            ObjectValue = 99
        }

        private string value_ = "";
        private object? obj_ = null;
        private Tag tag_ = Tag.NULL;

        public Any()
        {
            value_ = "";
            obj_ = null;
        }

        public static Any FromObject(object _value)
        {
            Any any = new Any();
            any.tag_ = Tag.ObjectValue;
            any.value_ = "";
            any.obj_ = _value;
            return any;
        }

        public static Any FromString(string _value)
        {
            Any any = new Any();
            any.tag_ = Tag.StringValue;
            any.value_ = _value;
            any.obj_ = null;
            return any;
        }

        public static Any FromFloat32(float _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Float32Value;
            any.value_ = _value.ToString();
            any.obj_ = null;
            return any;
        }

        public static Any FromFloat64(double _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Float64Value;
            any.value_ = _value.ToString();
            any.obj_ = null;
            return any;
        }

        public static Any FromBool(bool _value)
        {
            Any any = new Any();
            any.tag_ = Tag.BoolValue;
            any.value_ = _value ? "true" : "false";
            any.obj_ = null;
            return any;
        }

        public static Any FromInt32(int _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Int32Value;
            any.value_ = _value.ToString();
            any.obj_ = null;
            return any;
        }

        public static Any FromInt64(long _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Int64Value;
            any.value_ = _value.ToString();
            any.obj_ = null;
            return any;
        }

        public static Any FromStringAry(string[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.StringAryValue;
            any.obj_ = null;
            any.value_ = stringAryToString(_value);
            return any;
        }

        public static Any FromFloat32Ary(float[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Float32AryValue;
            any.obj_ = null;
            any.value_ = float32AryToString(_value);
            return any;
        }

        public static Any FromFloat64Ary(double[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Float64AryValue;
            any.obj_ = null;
            any.value_ = float64AryToString(_value);
            return any;
        }

        public static Any FromBoolAry(bool[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.BoolAryValue;
            any.obj_ = null;
            any.value_ = boolAryToString(_value);
            return any;
        }

        public static Any FromInt32Ary(int[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Int32AryValue;
            any.obj_ = null;
            any.value_ = int32AryToString(_value);
            return any;
        }

        public static Any FromInt64Ary(long[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Int64AryValue;
            any.obj_ = null;
            any.value_ = int64AryToString(_value);
            return any;
        }

        public static Any FromBytes(byte[] _value)
        {
            Any any = new Any();
            any.tag_ = Tag.BytesValue;
            any.obj_ = null;
            any.value_ = System.Convert.ToBase64String(_value);
            return any;
        }

        public static Any FromStringMap(Dictionary<string, string> _value)
        {
            Any any = new Any();
            any.tag_ = Tag.StringMapValue;
            any.value_ = stringMapToString(_value);
            any.obj_ = null;
            return any;
        }

        public static Any FromFloat32Map(Dictionary<string, float> _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Float32MapValue;
            any.value_ = float32MapToString(_value);
            any.obj_ = null;
            return any;
        }

        public static Any FromFloat64Map(Dictionary<string, double> _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Float64MapValue;
            any.value_ = float64MapToString(_value);
            any.obj_ = null;
            return any;
        }

        public static Any FromBoolMap(Dictionary<string, bool> _value)
        {
            Any any = new Any();
            any.tag_ = Tag.BoolMapValue;
            any.value_ = boolMapToString(_value);
            any.obj_ = null;
            return any;
        }

        public static Any FromInt32Map(Dictionary<string, int> _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Int32MapValue;
            any.value_ = int32MapToString(_value);
            any.obj_ = null;
            return any;
        }

        public static Any FromInt64Map(Dictionary<string, long> _value)
        {
            Any any = new Any();
            any.tag_ = Tag.Int64MapValue;
            any.value_ = int64MapToString(_value);
            any.obj_ = null;
            return any;
        }

        public bool IsNull()
        {
            return tag_ == Tag.NULL;
        }

        public bool IsObject()
        {
            return tag_ == Tag.ObjectValue;
        }

        public bool IsString()
        {
            return tag_ == Tag.StringValue;
        }

        public bool IsInt32()
        {
            return tag_ == Tag.Int32Value;
        }

        public bool IsInt64()
        {
            return tag_ == Tag.Int64Value;
        }

        public bool IsFloat32()
        {
            return tag_ == Tag.Float32Value;
        }

        public bool IsFloat64()
        {
            return tag_ == Tag.Float64Value;
        }

        public bool IsBool()
        {
            return tag_ == Tag.BoolValue;
        }
        public bool IsStringAry()
        {
            return tag_ == Tag.StringAryValue;
        }

        public bool IsInt32Ary()
        {
            return tag_ == Tag.Int32AryValue;
        }

        public bool IsInt64Ary()
        {
            return tag_ == Tag.Int64AryValue;
        }

        public bool IsFloat32Ary()
        {
            return tag_ == Tag.Float32AryValue;
        }

        public bool IsFloat64Ary()
        {
            return tag_ == Tag.Float64AryValue;
        }

        public bool IsBoolAry()
        {
            return tag_ == Tag.BoolAryValue;
        }

        public bool IsBytes()
        {
            return tag_ == Tag.BytesValue;
        }

        public bool IsStringMap()
        {
            return tag_ == Tag.StringMapValue;
        }

        public bool IsInt32Map()
        {
            return tag_ == Tag.Int32MapValue;
        }

        public bool IsInt64Map()
        {
            return tag_ == Tag.Int64MapValue;
        }

        public bool IsFloat32Map()
        {
            return tag_ == Tag.Float32MapValue;
        }

        public bool IsFloat64Map()
        {
            return tag_ == Tag.Float64MapValue;
        }

        public bool IsBoolMap()
        {
            return tag_ == Tag.BoolMapValue;
        }

        /// <summary>
        /// 转换为字符串
        /// 支持源类型：全部
        /// </summary>
        /// <returns>失败返回空字符串</returns>
        public string AsString()
        {
            return value_;
        }

        /// <summary>
        /// 转换为32位整型
        /// 支持源类型：int, long, float, double, string, bool, byte[]
        /// </summary>
        /// <returns>失败返回0</returns>
        public int AsInt32()
        {
            int value = 0;
            if (IsInt32() || IsInt64() || IsString())
            {
                if (!int.TryParse(value_, out value))
                    value = 0;
            }
            else if (IsFloat32() || IsFloat64())
            {
                double dv;
                if (!double.TryParse(value_, out dv))
                    dv = 0;
                if (dv <= int.MaxValue)
                    value = (int)dv;
            }
            else if (IsBool())
            {
                value = AsBool() ? 1 : 0;
            }
            else if (IsBytes())
            {
                value = bytesToInt32(AsBytes());
            }
            return value;
        }

        /// <summary>
        /// 转换为64位整型
        /// 支持源类型：int, long, float, double, string, bool, byte[]
        /// </summary>
        /// <returns>失败返回0</returns>
        public long AsInt64()
        {
            long value = 0;
            if (IsInt32() || IsInt64() || IsString())
            {
                if (!long.TryParse(value_, out value))
                    value = 0;
            }
            else if (IsFloat32() || IsFloat64())
            {
                double dv;
                if (!double.TryParse(value_, out dv))
                    dv = 0;
                if (dv <= long.MaxValue)
                    value = (long)dv;
            }
            else if (IsBool())
            {
                value = AsBool() ? 1 : 0;
            }
            else if (IsBytes())
            {
                value = bytesToInt64(AsBytes());
            }
            return value;
        }

        /// <summary>
        /// 转换为32位浮点型
        /// 支持源类型：int, long, float, double, bool, string
        /// </summary>
        /// <returns>失败返回0</returns>
        public float AsFloat32()
        {
            float value = 0;
            if (IsFloat32() || IsFloat64() || IsString())
            {
                if (!float.TryParse(value_, out value))
                    value = 0f;
            }
            else if (IsInt32() || IsInt64())
            {
                long lv;
                if (!long.TryParse(value_, out lv))
                    lv = 0;
                if (lv <= float.MaxValue)
                    value = (float)lv;
            }
            else if (IsBool())
            {
                value = AsBool() ? 1 : 0;
            }
            return value;
        }

        /// <summary>
        /// 转换为64位浮点型
        /// 支持源类型：float,double, int, long,string
        /// </summary>
        /// <returns>失败返回0</returns>
        public double AsFloat64()
        {
            double value = 0;
            if (IsFloat32() || IsFloat64() || IsString())
            {
                if (!double.TryParse(value_, out value))
                    value = 0;
            }
            else if (IsInt32() || IsInt64())
            {
                long lv;
                if (!long.TryParse(value_, out lv))
                    lv = 0;
                if (lv <= double.MaxValue)
                    value = (float)lv;
            }
            else if (IsBool())
            {
                value = AsBool() ? 1 : 0;
            }
            return value;
        }

        /// <summary>
        /// 转换为布尔型
        /// 支持源类型：bool,string,int,long, byte[]
        /// </summary>
        /// <returns>失败返回false</returns>
        public bool AsBool()
        {
            bool value = false;
            if (IsBool() || IsString())
            {
                value = value_.Equals("true");
            }
            else if(IsInt32() || IsInt64())
            {
                value = !value_.Equals("0");
            }
            else if (IsBytes())
            {
                value = bytesToBool(AsBytes());
            }
            return value;
        }

        /// <summary>
        /// 转换为字符型数组
        /// 支持源类型：string[],string
        /// </summary>
        /// <returns>失败返回0长度数组</returns>
        public string[] AsStringAry()
        {
            string[] value = new string[0];
            if (IsStringAry() || IsString())
                value = stringToStringAry(value_);
            return value;
        }

        /// <summary>
        /// 转换为32位整型数组
        /// 支持源类型：int[],string
        /// </summary>
        /// <returns>失败返回0长度数组</returns>
        public int[] AsInt32Ary()
        {
            int[] value = new int[0];
            if (IsInt32Ary() || IsString())
                value = stringToInt32Ary(value_);
            return value;
        }

        /// <summary>
        /// 转换为64位整型数组
        /// 支持源类型：long[],string
        /// </summary>
        /// <returns>失败返回0长度数组</returns>
        public long[] AsInt64Ary()
        {
            long[] value = new long[0];
            if (IsInt64Ary() || IsString())
                value = stringToInt64Ary(value_);
            return value;
        }

        /// <summary>
        /// 转换为32位浮点型数组
        /// 支持源类型：float[],string
        /// </summary>
        /// <returns>失败返回0长度数组</returns>
        public float[] AsFloat32Ary()
        {
            float[] value = new float[0];
            if (IsFloat32Ary() || IsString())
                value = stringToFloat32Ary(value_);
            return value;
        }

        /// <summary>
        /// 转换为64位浮点型数组
        /// 支持源类型：double[],string
        /// </summary>
        /// <returns>失败返回0长度数组</returns>
        public double[] AsFloat64Ary()
        {
            double[] value = new double[0];
            if (IsFloat64Ary() || IsString())
                value = stringToFloat64Ary(value_);
            return value;
        }

        /// <summary>
        /// 转换为布尔型数组
        /// 支持源类型：bool[],string
        /// </summary>
        /// <returns>失败返回0长度数组</returns>
        public bool[] AsBoolAry()
        {
            bool[] value = new bool[0];
            if (IsBoolAry() || IsString())
                value = stringToBoolAry(value_);
            return value;
        }

        /// <summary>
        /// 转换为字节数组
        /// 支持源类型：bytes,string,int32,int64,bool
        /// </summary>
        /// <returns>失败返回0长度数组</returns>
        public byte[] AsBytes()
        {
            byte[] value = new byte[0];
            if (IsBytes())
            {
                value = System.Convert.FromBase64String(value_);
            }
            else if (IsString())
            {
                value = stringToBytes(AsString());
            }
            else if (IsInt32())
            {
                value = int32ToBytes(AsInt32());
            }
            else if (IsInt64())
            {
                value = int64ToBytes(AsInt64());
            }
            else if (IsBool())
            {
                value = boolToBytes(AsBool());
            }
            return value;
        }

        /// <summary>
        /// 转换为字符串字典
        /// 支持源类型：map<string,string>, string
        /// </summary>
        /// <returns>失败返回0长度字典</returns>
        public Dictionary<string, string> AsStringMap()
        {
            Dictionary<string, string> value = new Dictionary<string, string>();
            if (IsStringMap() || IsString())
                value = stringToStringMap(value_);
            return value;
        }


        /// <summary>
        /// 转换为32位整型字典
        /// 支持源类型：map<string,int>, string
        /// </summary>
        /// <returns>失败返回0长度字典</returns>
        public Dictionary<string, int> AsInt32Map()
        {
            Dictionary<string, int> value = new Dictionary<string, int>();
            if (IsInt32Map() || IsString())
                value = stringToInt32Map(value_);
            return value;
        }

        /// <summary>
        /// 转换为64位整型字典
        /// 支持源类型：map<string,long>, string
        /// </summary>
        /// <returns>失败返回0长度字典</returns>
        public Dictionary<string, long> AsInt64Map()
        {
            Dictionary<string, long> value = new Dictionary<string, long>();
            if (IsInt64Map() || IsString())
                value = stringToInt64Map(value_);
            return value;
        }

        /// <summary>
        /// 转换为32位浮点型字典
        /// 支持源类型：map<string,float>, string
        /// </summary>
        /// <returns>失败返回0长度字典</returns>
        public Dictionary<string, float> AsFloat32Map()
        {
            Dictionary<string, float> value = new Dictionary<string, float>();
            if (IsFloat32Map() || IsString())
                value = stringToFloat32Map(value_);
            return value;
        }

        /// <summary>
        /// 转换为64位浮点型字典
        /// 支持源类型：map<string,double>, string
        /// </summary>
        /// <returns>失败返回0长度字典</returns>
        public Dictionary<string, double> AsFloat64Map()
        {
            Dictionary<string, double> value = new Dictionary<string, double>();
            if (IsFloat64Map() || IsString())
                value = stringToFloat64Map(value_);
            return value;
        }

        /// <summary>
        /// 转换为布尔型字典
        /// 支持源类型：map<string,bool>, string
        /// </summary>
        /// <returns>失败返回0长度字典</returns>
        public Dictionary<string, bool> AsBoolMap()
        {
            Dictionary<string, bool> value = new Dictionary<string, bool>();
            if (IsBoolMap() || IsString())
                value = stringToBoolMap(value_);
            return value;
        }

        public object? AsObject()
        {
            return obj_;
        }

        private byte[] int32ToBytes(int _value)
        {
            byte[] buffer = new byte[4];
            buffer[0] = (byte)_value;
            buffer[1] = (byte)(_value >> 8);
            buffer[2] = (byte)(_value >> 16);
            buffer[3] = (byte)(_value >> 24);
            return buffer;
        }

        private byte[] int64ToBytes(long _value)
        {
            byte[] buffer = new byte[8];
            buffer[0] = (byte)_value;
            buffer[1] = (byte)(_value >> 8);
            buffer[2] = (byte)(_value >> 16);
            buffer[3] = (byte)(_value >> 24);
            buffer[4] = (byte)(_value >> 32);
            buffer[5] = (byte)(_value >> 40);
            buffer[6] = (byte)(_value >> 48);
            buffer[7] = (byte)(_value >> 56);
            return buffer;
        }

        private byte[] boolToBytes(bool _value)
        {
            byte[] buffer = new byte[1];
            buffer[0] = _value ? (byte)1 : (byte)0;
            return buffer;
        }

        private byte[] stringToBytes(string _value)
        {
            return System.Text.Encoding.UTF8.GetBytes(_value);
        }

        private int bytesToInt32(byte[] _value)
        {
            int value = 0;
            value |= (int)_value[0];
            value |= ((int)_value[1]) << 8;
            value |= ((int)_value[2]) << 16;
            value |= ((int)_value[3]) << 24;
            return value;
        }

        private long bytesToInt64(byte[] _value)
        {
            long value = 0;
            value |= (long)_value[0];
            value |= ((long)_value[1]) << 8;
            value |= ((long)_value[2]) << 16;
            value |= ((long)_value[3]) << 24;
            value |= ((long)_value[4]) << 32;
            value |= ((long)_value[5]) << 40;
            value |= ((long)_value[6]) << 48;
            value |= ((long)_value[7]) << 56;
            return value;
        }

        private bool bytesToBool(byte[] _value)
        {
            return 0 != _value[0];
        }

        private string bytesToString(byte[] _value)
        {
            return System.Text.Encoding.UTF8.GetString(_value);
        }

        private static string stringAryToString(string[] _value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[");
            for (int i = 0; i < _value.Length; i++)
            {
                sb.Append(string.Format("\"{0}\"", _value[i]));
                if (i != _value.Length - 1)
                    sb.Append(",");
            }
            sb.Append("]");
            return sb.ToString();
        }

        private static string int32AryToString(int[] _value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[");
            for (int i = 0; i < _value.Length; i++)
            {
                sb.Append(string.Format("{0}", _value[i]));
                if (i != _value.Length - 1)
                    sb.Append(",");
            }
            sb.Append("]");
            return sb.ToString();
        }

        private static string int64AryToString(long[] _value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[");
            for (int i = 0; i < _value.Length; i++)
            {
                sb.Append(string.Format("{0}", _value[i]));
                if (i != _value.Length - 1)
                    sb.Append(",");
            }
            sb.Append("]");
            return sb.ToString();
        }

        private static string float32AryToString(float[] _value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[");
            for (int i = 0; i < _value.Length; i++)
            {
                sb.Append(string.Format("{0}", _value[i]));
                if (i != _value.Length - 1)
                    sb.Append(",");
            }
            sb.Append("]");
            return sb.ToString();
        }

        private static string float64AryToString(double[] _value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[");
            for (int i = 0; i < _value.Length; i++)
            {
                sb.Append(string.Format("{0}", _value[i]));
                if (i != _value.Length - 1)
                    sb.Append(",");
            }
            sb.Append("]");
            return sb.ToString();
        }

        private static string boolAryToString(bool[] _value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[");
            for (int i = 0; i < _value.Length; i++)
            {
                sb.Append(string.Format("{0}", _value[i] ? "true" : "false"));
                if (i != _value.Length - 1)
                    sb.Append(",");
            }
            sb.Append("]");
            return sb.ToString();
        }


        private static string stringMapToString(Dictionary<string, string> _value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            int index = 0;
            foreach (var pair in _value)
            {
                sb.Append(string.Format("\"{0}\":", pair.Key));
                sb.Append(string.Format("\"{0}\"", pair.Value));
                if (index != _value.Count - 1)
                    sb.Append(",");
                index += 1;
            }
            sb.Append("}");
            return sb.ToString();
        }

        private static string int32MapToString(Dictionary<string, int> _value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            int index = 0;
            foreach (var pair in _value)
            {
                sb.Append(string.Format("\"{0}\":", pair.Key));
                sb.Append(string.Format("{0}", pair.Value));
                if (index != _value.Count - 1)
                    sb.Append(",");
                index += 1;
            }
            sb.Append("}");
            return sb.ToString();
        }

        private static string int64MapToString(Dictionary<string, long> _value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            int index = 0;
            foreach (var pair in _value)
            {
                sb.Append(string.Format("\"{0}\":", pair.Key));
                sb.Append(string.Format("{0}", pair.Value));
                if (index != _value.Count - 1)
                    sb.Append(",");
                index += 1;
            }
            sb.Append("}");
            return sb.ToString();
        }

        private static string float32MapToString(Dictionary<string, float> _value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            int index = 0;
            foreach (var pair in _value)
            {
                sb.Append(string.Format("\"{0}\":", pair.Key));
                sb.Append(string.Format("{0}", pair.Value));
                if (index != _value.Count - 1)
                    sb.Append(",");
                index += 1;
            }
            sb.Append("}");
            return sb.ToString();
        }

        private static string float64MapToString(Dictionary<string, double> _value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            int index = 0;
            foreach (var pair in _value)
            {
                sb.Append(string.Format("\"{0}\":", pair.Key));
                sb.Append(string.Format("{0}", pair.Value));
                if (index != _value.Count - 1)
                    sb.Append(",");
                index += 1;
            }
            sb.Append("}");
            return sb.ToString();
        }

        private static string boolMapToString(Dictionary<string, bool> _value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            int index = 0;
            foreach (var pair in _value)
            {
                sb.Append(string.Format("\"{0}\":", pair.Key));
                sb.Append(string.Format("{0}", pair.Value ? "true" : "false"));
                if (index != _value.Count - 1)
                    sb.Append(",");
                index += 1;
            }
            sb.Append("}");
            return sb.ToString();
        }

        public string[] stringToStringAry(string _value)
        {
            List<string> ary = new List<string>();
            if (value_.StartsWith("[") && value_.EndsWith("]"))
            {
                string aryStr = value_.Remove(0, 1);
                aryStr = aryStr.Remove(aryStr.Length - 1, 1);
                
                foreach (string e in aryStr.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries))
                {
                    string value = e.Trim();
                    if (value.StartsWith("\"") && value.EndsWith("\""))
                    {
                        value = value.Remove(0, 1);
                        value = value.Remove(value.Length - 1, 1);
                    }
                    ary.Add(value);
                }
            }
            return ary.ToArray();
        }

        public int[] stringToInt32Ary(string _value)
        {
            List<int> ary = new List<int>();
            if (value_.StartsWith("[") && value_.EndsWith("]"))
            {
                string aryStr = value_.Remove(0, 1);
                aryStr = aryStr.Remove(aryStr.Length - 1, 1);
                foreach (string e in aryStr.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries))
                {
                    string value = e.Trim();
                    if (value.StartsWith("\"") && value.EndsWith("\""))
                    {
                        value = value.Remove(0, 1);
                        value = value.Remove(value.Length - 1, 1);
                    }
                    int v;
                    if (!int.TryParse(value, out v))
                        v = 0;
                    ary.Add(v);
                }
            }
            return ary.ToArray();
        }

        public long[] stringToInt64Ary(string _value)
        {
            List<long> ary = new List<long>();
            if (value_.StartsWith("[") && value_.EndsWith("]"))
            {
                string aryStr = value_.Remove(0, 1);
                aryStr = aryStr.Remove(aryStr.Length - 1, 1);
                foreach (string e in aryStr.Split(','))
                {
                    string value = e.Trim();
                    if (value.StartsWith("\"") && value.EndsWith("\""))
                    {
                        value = value.Remove(0, 1);
                        value = value.Remove(value.Length - 1, 1);
                    }
                    long v;
                    if (!long.TryParse(value, out v))
                        v = 0;
                    ary.Add(v);
                }
            }
            return ary.ToArray();
        }

        public float[] stringToFloat32Ary(string _value)
        {
            List<float> ary = new List<float>();
            if (value_.StartsWith("[") && value_.EndsWith("]"))
            {
                string aryStr = value_.Remove(0, 1);
                aryStr = aryStr.Remove(aryStr.Length - 1, 1);
                foreach (string e in aryStr.Split(','))
                {
                    string value = e.Trim();
                    if (value.StartsWith("\"") && value.EndsWith("\""))
                    {
                        value = value.Remove(0, 1);
                        value = value.Remove(value.Length - 1, 1);
                    }
                    float v;
                    if (!float.TryParse(value, out v))
                        v = 0;
                    ary.Add(v);
                }
            }
            return ary.ToArray();
        }

        public double[] stringToFloat64Ary(string _value)
        {
            List<double> ary = new List<double>();
            if (value_.StartsWith("[") && value_.EndsWith("]"))
            {
                string aryStr = value_.Remove(0, 1);
                aryStr = aryStr.Remove(aryStr.Length - 1, 1);
                foreach (string e in aryStr.Split(','))
                {
                    string value = e.Trim();
                    if (value.StartsWith("\"") && value.EndsWith("\""))
                    {
                        value = value.Remove(0, 1);
                        value = value.Remove(value.Length - 1, 1);
                    }
                    double v;
                    if (!double.TryParse(value, out v))
                        v = 0;
                    ary.Add(v);
                }
            }
            return ary.ToArray();
        }

        public bool[] stringToBoolAry(string _value)
        {
            List<bool> ary = new List<bool>();
            if (value_.StartsWith("[") && value_.EndsWith("]"))
            {
                string aryStr = value_.Remove(0, 1);
                aryStr = aryStr.Remove(aryStr.Length - 1, 1);
                foreach (string e in aryStr.Split(','))
                {
                    string value = e.Trim();
                    if (value.StartsWith("\"") && value.EndsWith("\""))
                    {
                        value = value.Remove(0, 1);
                        value = value.Remove(value.Length - 1, 1);
                    }
                    bool v;
                    if (!bool.TryParse(value, out v))
                        v = false;
                    ary.Add(v);
                }
            }
            return ary.ToArray();
        }

        public Dictionary<string, string> stringToStringMap(string _value)
        {
            Dictionary<string, string> v = new Dictionary<string, string>();
            if (value_.StartsWith("{") && value_.EndsWith("}"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(','))
                {
                    string[] pair = e.Split(':');
                    if (2 != pair.Length)
                        continue;
                    string key = pair[0].Trim();
                    if (key.StartsWith("\"") && key.EndsWith("\""))
                    {
                        key = key.Remove(0, 1);
                        key = key.Remove(key.Length - 1, 1);
                    }
                    string value = pair[1].Trim();
                    if (value.StartsWith("\"") && value.EndsWith("\""))
                    {
                        value = value.Remove(0, 1);
                        value = value.Remove(value.Length - 1, 1);
                    }
                    v[key] = value;
                }
            }
            return v;
        }

        public Dictionary<string, int> stringToInt32Map(string _value)
        {
            Dictionary<string, int> v = new Dictionary<string, int>();
            if (value_.StartsWith("{") && value_.EndsWith("}"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(','))
                {
                    string[] pair = e.Split(':');
                    if (2 != pair.Length)
                        continue;
                    string key = pair[0].Trim();
                    if (key.StartsWith("\"") && key.EndsWith("\""))
                    {
                        key = key.Remove(0, 1);
                        key = key.Remove(key.Length - 1, 1);
                    }
                    int value;
                    if (!int.TryParse(pair[1].Trim(), out value))
                        value = 0;
                    v[key] = value;
                }
            }
            return v;
        }

        public Dictionary<string, long> stringToInt64Map(string _value)
        {
            Dictionary<string, long> v = new Dictionary<string, long>();
            if (value_.StartsWith("{") && value_.EndsWith("}"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(','))
                {
                    string[] pair = e.Split(':');
                    if (2 != pair.Length)
                        continue;
                    string key = pair[0].Trim();
                    if (key.StartsWith("\"") && key.EndsWith("\""))
                    {
                        key = key.Remove(0, 1);
                        key = key.Remove(key.Length - 1, 1);
                    }
                    long value;
                    if (!long.TryParse(pair[1].Trim(), out value))
                        value = 0;
                    v[key] = value;
                }
            }
            return v;
        }

        public Dictionary<string, float> stringToFloat32Map(string _value)
        {
            Dictionary<string, float> v = new Dictionary<string, float>();
            if (value_.StartsWith("{") && value_.EndsWith("}"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(','))
                {
                    string[] pair = e.Split(':');
                    if (2 != pair.Length)
                        continue;
                    string key = pair[0].Trim();
                    if (key.StartsWith("\"") && key.EndsWith("\""))
                    {
                        key = key.Remove(0, 1);
                        key = key.Remove(key.Length - 1, 1);
                    }
                    float value;
                    if (!float.TryParse(pair[1].Trim(), out value))
                        value = 0;
                    v[key] = value;
                }
            }
            return v;
        }

        public Dictionary<string, double> stringToFloat64Map(string _value)
        {
            Dictionary<string, double> v = new Dictionary<string, double>();
            if (value_.StartsWith("{") && value_.EndsWith("}"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(','))
                {
                    string[] pair = e.Split(':');
                    if (2 != pair.Length)
                        continue;
                    string key = pair[0].Trim();
                    if (key.StartsWith("\"") && key.EndsWith("\""))
                    {
                        key = key.Remove(0, 1);
                        key = key.Remove(key.Length - 1, 1);
                    }
                    double value;
                    if (!double.TryParse(pair[1].Trim(), out value))
                        value = 0;
                    v[key] = value;
                }
            }
            return v;
        }

        public Dictionary<string, bool> stringToBoolMap(string _value)
        {
            Dictionary<string, bool> v = new Dictionary<string, bool>();
            if (value_.StartsWith("{") && value_.EndsWith("}"))
            {
                string ary = value_.Remove(0, 1);
                ary = ary.Remove(ary.Length - 1, 1);
                foreach (string e in ary.Split(','))
                {
                    string[] pair = e.Split(':');
                    if (2 != pair.Length)
                        continue;
                    string key = pair[0].Trim();
                    if (key.StartsWith("\"") && key.EndsWith("\""))
                    {
                        key = key.Remove(0, 1);
                        key = key.Remove(key.Length - 1, 1);
                    }
                    bool value;
                    if (!bool.TryParse(pair[1].Trim(), out value))
                        value = false;
                    v[key] = value;
                }
            }
            return v;
        }

    }//class
}//namespace
