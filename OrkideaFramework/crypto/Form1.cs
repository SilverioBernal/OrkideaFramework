using Orkidea.Framework.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crypto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncryp_Click(object sender, EventArgs e)
        {
            if (chkEncrip.Checked && chkHex.Checked)
            {
                string res = Cryptography.Encrypt(textBox1.Text);
                crypText1.Text = HexSerialization.StringToHex(res);
            }

            if (chkEncrip.Checked && !chkHex.Checked)
            {
                string res = Cryptography.Encrypt(textBox1.Text);
                crypText1.Text = res;
            }

            if (!chkEncrip.Checked && chkHex.Checked)
            {
                string res = textBox1.Text;
                if (chkHex.Checked)
                    crypText1.Text = HexSerialization.StringToHex(res);
            }            
        }

        private void btnDecryp_Click(object sender, EventArgs e)
        {
            if (chkDecryp.Checked && chkString.Checked)
            {
                string res = HexSerialization.HexToString(crypText2.Text);
                textBox2.Text = Cryptography.Decrypt(res);
            }

            if (chkDecryp.Checked && !chkString.Checked)
            {
                string res = crypText2.Text;
                textBox2.Text = Cryptography.Decrypt(res);
            }


            if (!chkDecryp.Checked && chkString.Checked)
            {
                string res = HexSerialization.HexToString(crypText2.Text);
                textBox2.Text = res;
            }            
        }
    }
}
