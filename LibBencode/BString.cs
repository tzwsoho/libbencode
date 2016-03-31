using System;
using System.Text;

namespace LibBencode
{
    public class BString : IBType
    {
        private byte[] m_value = null;
        public byte[] Value
        {
            get
            {
                return m_value;
            }

            set
            {
                m_value = value;

                string str_len = value.Length.ToString();
                int value_len = m_encoding.GetByteCount(str_len) + 1;
                byte[] bytes_value = new byte[value_len + value.Length];
                Array.Copy(m_encoding.GetBytes(str_len + ":"), bytes_value, value_len);
                Array.Copy(value, 0, bytes_value, value_len, value.Length);
                m_internal_value = bytes_value;
            }
        }

        public string StringValue
        {
            get
            {
                return m_encoding.GetString(m_value);
            }

            set
            {
                m_value = m_encoding.GetBytes(value);
                m_internal_value = m_encoding.GetBytes(value.Length + ":" + value);
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
            StringValue = val;
        }

        public BString(byte[] val, string encoding_name = "utf-8")
        {
            m_type = BType.BSTRING;
            EncodingName = encoding_name;
            Value = val;
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
