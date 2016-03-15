using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LibBencode
{
    public static class BencodeUtil
    {
        //private const string BINT_PATTERN = @"(?<=i)-?\d+(?=e)";
        //private const string BSTRING_LEN_PATTERN = @"(\d+):.*?";
        //private const string BLIST_PATTERN = @"(?<=l)(?:i\d+e|\d:.+|l.+e|d.+e)(?=e(?:[^e]|$))";
        //private const string BDICT_PATTERN = @"(?<=d)(?:i\d+e|\d:.+|l.+e|d.+e)(?=e(?:[^e]|$))";

        static public string GetBIntFromBEncodedString(byte[] encoded_bytes, ref int istart, out int val)
        {
            val = 0;
            if (null == encoded_bytes ||
                0 == encoded_bytes.Length ||
                'i' != encoded_bytes[istart])
            {
                return "";
            }

            string str_int = "";
            do
            {
                str_int += (char)encoded_bytes[istart];
            } while ('e' != encoded_bytes[istart++]);

            if (!int.TryParse(str_int.Substring(1, str_int.Length - 2), out val))
            {
                return "";
            }

            return str_int;
        }

        static public string GetBStringFromBEncodedString(
            byte[] encoded_bytes,
            ref int istart,
            out byte[] val,
            string encoding_name = "utf-8")
        {
            val = null;
            if (null == encoded_bytes ||
                0 == encoded_bytes.Length ||
                ':' == encoded_bytes[istart])
            {
                return "";
            }

            string str_len = "";
            while (':' != encoded_bytes[istart])
            {
                str_len += (char)encoded_bytes[istart++];
            }

            int len = 0;
            if (!int.TryParse(str_len, out len))
            {
                return "";
            }

            istart++;
            val = new byte[len];
            Array.Copy(encoded_bytes, istart, val, 0, len);

            Encoding encoding = Encoding.GetEncoding(encoding_name);
            if (null == encoding)
            {
                encoding = Encoding.UTF8;
            }

            string str_str = str_len + ":" + encoding.GetString(val);
            istart += len;
            return str_str;
        }

        static public string GetBListFromBEncodedString(
            byte[] encoded_bytes,
            ref int istart,
            out List<IBType> blist,
            string encoding_name = "utf-8")
        {
            blist = new List<IBType>();
            if (null == encoded_bytes ||
                0 == encoded_bytes.Length ||
                'l' != encoded_bytes[istart])
            {
                return "";
            }

            string str_list = "";
            str_list += (char)encoded_bytes[istart++];

            do
            {
                IBType bobj = default(IBType);
                str_list += Parse(encoded_bytes, ref istart, out bobj, encoding_name);
                blist.Add(bobj);

                if ('e' == encoded_bytes[istart])
                {
                    str_list += (char)encoded_bytes[istart++];
                    break;
                }
            } while (istart < encoded_bytes.Length);

            return str_list;
        }

        static public string GetBDictFromBEncodedString(
            byte[] encoded_bytes,
            ref int istart,
            out Dictionary<IBType, IBType> bdict,
            string encoding_name = "utf-8")
        {
            bdict = new Dictionary<IBType, IBType>();
            if (null == encoded_bytes ||
                0 == encoded_bytes.Length ||
                'd' != encoded_bytes[istart])
            {
                return "";
            }

            string str_dict = "";
            str_dict += (char)encoded_bytes[istart++];

            do
            {
                IBType bkey = default(IBType);
                IBType bval = default(IBType);
                str_dict += Parse(encoded_bytes, ref istart, out bkey, encoding_name);
                str_dict += Parse(encoded_bytes, ref istart, out bval, encoding_name);
                bdict.Add(bkey, bval);

                if ('e' == encoded_bytes[istart])
                {
                    str_dict += (char)encoded_bytes[istart++];
                    break;
                }
            } while (istart < encoded_bytes.Length);

            return str_dict;
        }

        static private string Parse(byte[] encoded_bytes, ref int istart, out IBType bobj, string encoding_name = "utf-8")
        {
            while (istart < encoded_bytes.Length)
            {
                switch ((char)encoded_bytes[istart])
                {
                    case 'i': // BInt
                        {
                            int val = 0;
                            string str_int = GetBIntFromBEncodedString(encoded_bytes, ref istart, out val);
                            bobj = new BInt(val);
                            return str_int;
                        }

                    case '0': // BString
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        {
                            byte[] val = null;
                            string str_str = GetBStringFromBEncodedString(encoded_bytes, ref istart, out val, encoding_name);
                            bobj = new BString(val, encoding_name);
                            return str_str;
                        }

                    case 'l': // BList
                        {
                            List<IBType> val = null;
                            string str_list = GetBListFromBEncodedString(encoded_bytes, ref istart, out val, encoding_name);
                            bobj = new BList(val);
                            return str_list;
                        }

                    case 'd': // BDict
                        {
                            Dictionary<IBType, IBType> val = null;
                            string str_dict = GetBDictFromBEncodedString(encoded_bytes, ref istart, out val, encoding_name);
                            bobj = new BDict(val);
                            return str_dict;
                        }
                }
            }

            bobj = default(IBType);
            return "";
        }

        static public IBType Parse(byte[] encoded_bytes, string encoding_name = "utf-8")
        {
            int istart = 0;
            IBType bobj = default(IBType);
            Parse(encoded_bytes, ref istart, out bobj, encoding_name);
            return bobj;
        }
    }
}
