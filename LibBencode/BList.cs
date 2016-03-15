using System;
using System.Text;
using System.Collections.Generic;

namespace LibBencode
{
    public class BList : IBType
    {
        private List<IBType> m_value = null;
        public List<IBType> Value
        {
            get
            {
                return m_value;
            }

            set
            {
                m_value = value;

                List<byte> lst_ret = new List<byte>();
                lst_ret.Add((byte)'l');
                foreach (IBType bobj in value)
                {
                    lst_ret.AddRange(bobj.IntervalValue);
                }
                lst_ret.Add((byte)'e');

                m_internal_value = lst_ret.ToArray();
            }
        }

        public BList()
        {
            m_type = BType.BLIST;
            m_value = new List<IBType>();
            m_internal_value = Encoding.UTF8.GetBytes("le");
        }

        public BList(List<IBType> val)
        {
            m_type = BType.BLIST;
            Value = val;
        }

        public BList(byte[] blist)
        {
            m_type = BType.BLIST;
            m_internal_value = blist;
            m_value = DecodeString();
        }

        private List<IBType> DecodeString()
        {
            List<IBType> val = null;
            int istart = 0;
            BencodeUtil.GetBListFromBEncodedString(m_internal_value, ref istart, out val);
            return val;
        }
    }
}
