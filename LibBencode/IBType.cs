using System;
using System.Text;

namespace LibBencode
{
    public enum BType
    {
        BNONE = 0,
        BINT = 1,
        BSTRING = 2,
        BLIST = 3,
        BDICT = 4
    }

    public abstract class IBType
    {
        protected byte[] m_internal_value = null;
        public byte[] IntervalValue
        {
            get
            {
                return m_internal_value;
            }
        }

        protected BType m_type = BType.BNONE;
        public BType BType
        {
            get
            {
                return m_type;
            }
        }

        public override string ToString()
        {
            return ToString("utf-8");
        }

        public string ToString(string encoding_name = "utf-8")
        {
            return Encoding.GetEncoding(encoding_name).GetString(m_internal_value);
        }
    }
}
