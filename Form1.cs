using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private SpeechSynthesizer Synth;

        public Form1()
        {
            InitializeComponent();
            Synth = new SpeechSynthesizer();                
        }

        private void SayText(string text) 
        {
            try
            {
                Synth.Speak(text);
            }
            catch { }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                //SayText(textBox1.Text);                
                ClearTextAsync();
            }
        }

        delegate void ClearTextDelegate();
        private void ClearText() 
        {
            if(!String.IsNullOrEmpty(textBox1.Text))
            {
                listBox1.Items.Add(textBox1.Text);
            textBox1.Text = ""; 
            }            
        }
        private void ClearTextAsync() 
        {
            if (InvokeRequired)
            {
                ClearTextDelegate ctd = new ClearTextDelegate(ClearText);
                Invoke(ctd);
            }
            else
                ClearText();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try 
            {
                SayText(listBox1.SelectedItem.ToString());
            }
            catch { }            
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Keys.Delete == e.KeyCode)
                {
                    listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                }
                else if (Keys.Enter == e.KeyCode)
                {
                    SayText(listBox1.SelectedItem.ToString()); 
                }
            }
            catch { }
        }

    }
}
