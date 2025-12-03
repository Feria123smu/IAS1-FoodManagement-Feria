using IAS1_FoodManagement_Feria.Authentication;

using IAS1_FoodManagement_Feria.Food;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IAS1_FoodManagement_Feria
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadMenu();

            inactivityTimer.Interval = 1000;
            inactivityTimer.Tick += InactivityTimer_Tick;
            inactivityTimer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void ChangeForm(Form form)
        {
            this.Size = new Size(1246, 767);
            FormManagement.PlaceForm(form, panel1);
        }

        private void LoadMenu()
        {
            //MenuForm Menu = new MenuForm();
            //FormManagement.PlaceForm(Menu, panel1);

            CashierForm cashier = new CashierForm();
            ChangeForm(cashier);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private readonly System.Windows.Forms.Timer inactivityTimer = new System.Windows.Forms.Timer();
        private const int InactivityThresholdMs = 15000; // 15 seconds

        private void InactivityTimer_Tick(object sender, EventArgs e)
        {
            uint idleTime = GetIdleTime();

            if (idleTime < InactivityThresholdMs)
            {
                int remainingSeconds = (int)((InactivityThresholdMs - idleTime) / 1000);
                lblKnockOut.Text = $"System Lock in: {remainingSeconds}s";
            }
            if (idleTime >= InactivityThresholdMs)
            {
                inactivityTimer.Stop();
                LockApplication();
            }
        }

        private void LockApplication()
        {
            LoginForm loginForm = new LoginForm();
            this.Size = new Size(620, 470);

            FormManagement.PlaceForm(loginForm, panel1, true);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        private static uint GetTickCount()
        {
            return (uint)Environment.TickCount;
        }
        private uint GetIdleTime()
        {
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);

            // Get the time of the last user input
            if (GetLastInputInfo(ref lastInputInfo))
            {
                uint idleTime = GetTickCount() - lastInputInfo.dwTime;
                return idleTime;
            }
            return 0;
        }
    }
}