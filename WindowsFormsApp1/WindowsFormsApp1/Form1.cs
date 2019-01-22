using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Runtime.InteropServices;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        FontFamily ff;
        Font font;
        public Form1()
        {
            InitializeComponent();
        }
        private  void loadFont() 
        {
          
            Byte[] fontData = WindowsFormsApp1.Properties.Resources.oblivion;
            int dataLength = WindowsFormsApp1.Properties.Resources.oblivion.Length;

            IntPtr data = Marshal.AllocCoTaskMem(dataLength);

            Marshal.Copy(fontData, 0, data, dataLength);

            uint cFonts = 0;

            AddFontMemResourceEx(data, (uint)fontData.Length, IntPtr.Zero, ref cFonts);

            PrivateFontCollection pfc = new PrivateFontCollection();

            pfc.AddMemoryFont(data, dataLength);

            Marshal.FreeCoTaskMem(data);

            ff = pfc.Families[0];
            font = new Font(ff, 15f, FontStyle.Bold);
        }

        private void AllocFont(Font f, Control c, float size)
        {
            FontStyle fontStyle = FontStyle.Regular;
            c.Font = new Font(ff, size, fontStyle);
        }

        private void Form1_load(object sender, EventArgs e)
        {
            loadFont();
            AllocFont(font, this.textBox1, 20);

        }

       private void textBox1_TextChanged(object sender, EventArgs e)
        {
            loadFont();
            AllocFont(font, this.textBox1, 20);
        }
    }
}
