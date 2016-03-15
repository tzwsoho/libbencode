using System;
using System.Text;

namespace LibBencode
{
    public class BString : IBType
    {
        private byte[] m_value = null;
        public string Value
        {
            get
            {
                return m_encoding.GetString(m_value);
            }

            set
            {
                m_value = m_encoding.GetBytes(value);
                m_internal_value = m_encoding.GetBytes(m_value.Length + ":" + value);
            }
        }

        private Encoding m_encoding = Encoding.UTF8;
        public string EncodingName
        {
            get
            {
                return m_encoding.BodyName;
            }

            set
            {
                Encoding encoding = Encoding.GetEncoding(value);
                if (null == encoding)
                {
                    m_encoding = Encoding.UTF8;
                }
                else
                {
                    m_encoding = encoding;
                }
            }
        }

        public BString()
        {
            m_type = BType.BSTRING;
            m_value = null;
            m_internal_value = m_encoding.GetBytes("0:");
        }

        public BString(string val, string encoding_name = "utf-8")
        {
            m_type = BType.BSTRING;
            EncodingName = encoding_name;
            m_value = m_encoding.GetBytes(val);
            m_internal_value = m_encoding.GetBytes(val.Length + ":" + val);
        }

        public BString(byte[] val, string encoding_name = "utf-8")
        {
            m_type = BType.BSTRING;
            EncodingName = encoding_name;
            Value = m_encoding.GetString(val);
        }

        public BString(byte[] bstring)
        {
            m_type = BType.BSTRING;
            m_internal_value = bstring;
            m_value = DecodeString();
        }

        private byte[] DecodeString()
        {
            byte[] val = null;
            int istart = 0;
            BencodeUtil.GetBStringFromBEncodedString(m_internal_value, ref istart, out val);
            return val;
        }
    }
}
