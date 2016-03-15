using System;
using System.Text;
using System.Collections.Generic;

namespace LibBencode
{
    public class BDict : IBType
    {
        private Dictionary<IBType, IBType> m_value = null;
        public Dictionary<IBType, IBType> Value
        {
            get
            {
                return m_value;
            }

            set
            {
                m_value = value;

                List<byte> lst_ret = new List<byte>();
                lst_ret.Add((byte)'d');
                foreach (KeyValuePair<IBType, IBType> kv in value)
                {
                    lst_ret.AddRange(kv.Key.IntervalValue);
                    lst_ret.AddRange(kv.Value.IntervalValue);
                }
                lst_ret.Add((byte)'e');

                m_internal_value = lst_ret.ToArray();
            }
        }

        public BDict()
        {
            m_type = BType.BDICT;
            m_value = new Dictionary<IBType, IBType>();
            m_internal_value = Encoding.UTF8.GetBytes("de");
        }

        public BDict(Dictionary<IBType, IBType> val)
        {
            m_type = BType.BDICT;
            Value = val;
        }

        public BDict(byte[] bdict)
        {
            m_type = BType.BDICT;
            m_internal_value = bdict;
            m_value = DecodeString();
        }

        private Dictionary<IBType, IBType> DecodeString()
        {
            Dictionary<IBType, IBType> val = null;
            int istart = 0;
            BencodeUtil.GetBDictFromBEncodedString(m_internal_value, ref istart, out val);
            return val;
        }
    }
}
