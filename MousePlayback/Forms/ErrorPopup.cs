using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MousePlayback
{
    public partial class ErrorPopup : MaterialForm
    {
        public ErrorPopup(Exception ex)
        {
            InitializeComponent();
            materialMultiLineTextBox1.Text = ex.ToString();
        }
    }
}
