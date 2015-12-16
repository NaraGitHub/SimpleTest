using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Form1
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// button1_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string result = (Convert.ToDouble(textBox1.Text) + Convert.ToDouble(textBox2.Text)).ToString();
    
            string jsonValue = JsonConvert.SerializeObject(new { value1 = 1, Value2 = 2 });
            MessageBox.Show(result + " : " + jsonValue);
        }

        /// <summary>
        /// Added comment button2_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
           string result = (Convert.ToDouble(textBox1.Text) - Convert.ToDouble(textBox2.Text)).ToString();
            string jsonValue = JsonConvert.SerializeObject(new { value1 = 1 });
            MessageBox.Show(result + " : " + jsonValue);
        }
    }
}
