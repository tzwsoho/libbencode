using System;
using System.Text;

namespace LibBencode
{
    public class BInt : IBType
    {
        private int m_value = 0;
        public int Value
        {
            get
            {
                return m_value;
            }

            set
            {
                m_value = value;
                m_internal_value = Encoding.UTF8.GetBytes("i" + value + "e");
            }
        }

        public BInt()
        {
            m_type = BType.BINT;
            m_internal_value = Encoding.UTF8.GetBytes("i0e");
        }

        public BInt(int val)
        {
            m_type = BType.BINT;
            Value = val;
        }

        public BInt(byte[] bint)
        {
            m_type = BType.BINT;
            m_internal_value = bint;
            m_value = DecodeString();
        }

        private int DecodeString()
        {
            int val = 0;
            int istart = 0;
            BencodeUtil.GetBIntFromBEncodedString( m_internal_value, ref istart, out val);
            return val;
        }
    }
}
