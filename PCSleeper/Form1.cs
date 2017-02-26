using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCSleeper
{
    public partial class Form1 : Form
    {
        int currLang = 0;
        string[][] optionsLoc = { new string[] { "Turn off", "Wyłącz"   },
                                  new string[] { "Restart", "Restartuj" }, 
                                  new string[] { "Log off", "Wyloguj"   }};

        public Form1()
        {
            InitializeComponent();
            SetLanguage();
        }

        void SetLanguage()
        {
            if (System.Globalization.CultureInfo.CurrentUICulture.EnglishName.Contains("Polish"))
                currLang = 1;
            else
                currLang = 0;

            ApplyLanguage();
        }

        void ApplyLanguage()
        {
            optionList.Items.Clear();

            foreach (string[] option in optionsLoc)
                optionList.Items.Add(option[currLang]);

            optionList.SelectedIndex = 0;

            if (currLang == 0)
            {
                label1.Text = "in";
                label2.Text = "minutes";
            }
            else
            {
                label1.Text = "za";
                label2.Text = "minut";
            }
        }

        private void StartTimer(object sender, EventArgs e)
        {
            minAmount.Enabled = startBtn.Enabled = false;
            stopBtn.Enabled = true;
            timer1.Start();
        }

        void StopTimer(object sender, EventArgs e)
        {
            minAmount.Enabled = startBtn.Enabled = true;
            stopBtn.Enabled = false;
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (minAmount.Value > 0)
                --minAmount.Value;

            if (minAmount.Value < 1)
                ExecuteCommand((int)optionList.SelectedIndex);
        }

        void ExecuteCommand(int option)
        {
            timer1.Stop();
            minAmount.Enabled = startBtn.Enabled = true;
            string command = "/c shutdown ";

            switch (option)
            {
                case 1:
                    command += "/r";
                    break;

                case 2:
                    command += "/l";
                    break;

                default:
                    command += "/s /f /t 0";
                    break;
            }

            Process.Start(@"cmd", command);
        }
    }
}
