using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Windows.Forms;
using LibBencode;

namespace TestTorrent
{
    public partial class frmMain : Form
    {
        private string CalcInfoHash(BDict bobj)
        {
            string str_ret = "";
            foreach (KeyValuePair<IBType, IBType> kv in bobj.Value)
            {
                if (BType.BSTRING == kv.Key.BType &&
                    "info" == ((BString)kv.Key).StringValue)
                {
                    byte[] bytes_ret = new SHA1CryptoServiceProvider().ComputeHash(kv.Value.IntervalValue);
                    str_ret = BitConverter.ToString(bytes_ret).Replace("-", "");
                    break;
                }
            }

            return str_ret;
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cmbEncoding.BeginUpdate();
            cmbEncoding.Items.Clear();

            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                cmbEncoding.Items.Add(ei.Name);
            }

            cmbEncoding.EndUpdate();
            if (cmbEncoding.Items.Contains("utf-8"))
            {
                cmbEncoding.Text = "utf-8";
            }
        }

        private void ParseBObj(TreeNode tn, IBType bobj)
        {
            switch (bobj.BType)
            {
                case BType.BINT:
                    {
                        BInt bint = (BInt)bobj;
                        tn.Nodes.Add("int = " + bint.Value);
                    }
                    break;

                case BType.BSTRING:
                    {
                        BString bstring = (BString)bobj;
                        tn.Nodes.Add("string(" + bstring.Value.Length + ") = " + bstring.StringValue);
                    }
                    break;

                case BType.BLIST:
                    {
                        BList blist = (BList)bobj;
                        TreeNode tn_child = new TreeNode("list(" + blist.Value.Count + ")");
                        foreach (IBType ibobj in blist.Value)
                        {
                            ParseBObj(tn_child, ibobj);
                        }

                        tn.Nodes.Add(tn_child);
                    }
                    break;

                case BType.BDICT:
                    {
                        BDict bdict = (BDict)bobj;
                        TreeNode tn_child = new TreeNode("dict(" + bdict.Value.Count + ")");
                        foreach (KeyValuePair<IBType, IBType> kv in bdict.Value)
                        {
                            TreeNode tn_key = new TreeNode("key(" + kv.Key.BType.ToString() + ")");
                            ParseBObj(tn_key, kv.Key);

                            TreeNode tn_val = new TreeNode("val(" + kv.Value.BType.ToString() + ")");
                            ParseBObj(tn_val, kv.Value);

                            tn_child.Nodes.Add(tn_key);
                            tn_child.Nodes.Add(tn_val);
                        }

                        tn.Nodes.Add(tn_child);
                    }
                    break;
            }
        }

        private void lstTorrents_DragEnter(object sender, DragEventArgs e)
        {
            foreach (string str_fmt in e.Data.GetFormats())
            {
                if (str_fmt == DataFormats.FileDrop)
                {
                    e.Effect = DragDropEffects.Link;
                    return;
                }
            }

            e.Effect = DragDropEffects.None;
        }

        private void lstTorrents_DragDrop(object sender, DragEventArgs e)
        {
            lstTorrents.BeginUpdate();
            lstTorrents.Items.Clear();

            string[] arr_files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string str_file in arr_files)
            {
                FileInfo fi = new FileInfo(str_file);
                if (".torrent" != fi.Extension)
                {
                    continue;
                }

                lstTorrents.Items.Add(str_file);
            }

            lstTorrents.EndUpdate();
        }

        private void lstTorrents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTorrents.SelectedIndex <= -1 ||
                "" == lstTorrents.Text)
            {
                return;
            }

            FileInfo fi = new FileInfo(lstTorrents.Text);
            FileStream fs = fi.OpenRead();
            byte[] bytes_file = new byte[fi.Length];
            fs.Read(bytes_file, 0, (int)fi.Length);
            fs.Close();

            IBType bobj = BencodeUtil.Parse(bytes_file, cmbEncoding.Text);
            TreeNode tn = new TreeNode(lstTorrents.Text);
            ParseBObj(tn, bobj);

            if (BType.BDICT == bobj.BType)
            {
                lblInfoHash.Text = "info_hash = " + CalcInfoHash((BDict)bobj);
            }

            tvwOutput.BeginUpdate();
            tvwOutput.Nodes.Clear();
            tvwOutput.Nodes.Add(tn);
            tvwOutput.ExpandAll();
            tvwOutput.EndUpdate();
        }
    }
}
