using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;    //Input/Output
using System.Diagnostics;

namespace Clock
{
	public partial class MainForm : Form
	{
		ColorDialog backgroundColorDialog;
		ColorDialog foregroundColorDialog;
		FontDialog fontDialog;
		public MainForm()
		{
			InitializeComponent();
			AllocConsole();
			this.Location = new Point
			(
				Screen.PrimaryScreen.Bounds.Right - this.Width - 50,
				Screen.PrimaryScreen.Bounds.Bottom / 20
			);
			backgroundColorDialog = new ColorDialog();
			foregroundColorDialog = new ColorDialog();
			fontDialog = new FontDialog(this);

			LoadSettings();
			SetVisibility(false);
		}
		[DllImport("kernel32.dll")]
		public static extern bool AllocConsole();

		//			Methods:
		void SetVisibility(bool visible)
		{
			cbShowDate.Visible = visible;
			cbShowWeekday.Visible = visible;
			btnHideControls.Visible = visible;
			this.ShowInTaskbar = visible;
			this.FormBorderStyle = visible ? FormBorderStyle.FixedToolWindow : FormBorderStyle.None;
			this.TransparencyKey = visible ? Color.Empty : this.BackColor;
		}
		void SaveSettings()
		{
			string filename = "Settings.ini";
			StreamWriter writer = new StreamWriter(filename);
			writer.WriteLine(tsmiTopmost.Checked);
			writer.WriteLine(tsmiShowControls.Checked);
			writer.WriteLine(tsmiShowDate.Checked);
			writer.WriteLine(tsmiShowWeekday.Checked);
			writer.WriteLine(fontDialog.Filename);
			writer.WriteLine(labelTime.BackColor.ToArgb());
			writer.WriteLine(labelTime.ForeColor.ToArgb());
			writer.Close();
			Process.Start("notepad", filename);
		}
		void LoadSettings()
		{
			string filename = "Settings.ini";
			StreamReader reader = new StreamReader(filename);

			tsmiTopmost.Checked			= bool.Parse(reader.ReadLine());
			tsmiShowControls.Checked	= bool.Parse(reader.ReadLine());
			tsmiShowDate.Checked		= bool.Parse(reader.ReadLine());
			tsmiShowWeekday.Checked		= bool.Parse(reader.ReadLine());

			fontDialog.Filename = reader.ReadLine();
			PrivateFontCollection pfc = new PrivateFontCollection();
			pfc.AddFontFile($"{Application.ExecutablePath}\\..\\..\\..\\Fonts\\{fontDialog.Filename}");
			fontDialog.Font = labelTime.Font = new Font(pfc.Families[0], 32);

			backgroundColorDialog.Color = labelTime.BackColor = Color.FromArgb(Convert.ToInt32(reader.ReadLine()));
			foregroundColorDialog.Color = labelTime.ForeColor = Color.FromArgb(Convert.ToInt32(reader.ReadLine()));

			reader.Close();
			Process.Start("notepad", filename);
		}

		//			Event handlers (Обработчики событий)
		private void timer_Tick(object sender, EventArgs e)
		{
			labelTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
			if (cbShowDate.Checked) labelTime.Text += $"\n{DateTime.Now.ToString("yyyy.MM.dd")}";
			if (cbShowWeekday.Checked) labelTime.Text += $"\n{DateTime.Now.DayOfWeek}";
			notifyIcon.Text = labelTime.Text;
		}

		private void btnHideControls_Click(object sender, EventArgs e) => SetVisibility(tsmiShowControls.Checked = false);

		private void labelTime_DoubleClick(object sender, EventArgs e) => SetVisibility(tsmiShowControls.Checked = true);

		private void tsmiTopmost_CheckedChanged(object sender, EventArgs e) => this.TopMost = tsmiTopmost.Checked;

		private void tsmiShowControls_CheckedChanged(object sender, EventArgs e) => SetVisibility(tsmiShowControls.Checked);

		private void tsmiShowDate_CheckedChanged(object sender, EventArgs e) => cbShowDate.Checked = tsmiShowDate.Checked;

		private void tsmiShowWeekday_CheckedChanged(object sender, EventArgs e) => cbShowWeekday.Checked = tsmiShowWeekday.Checked;

		private void cbShowDate_CheckedChanged(object sender, EventArgs e) => tsmiShowDate.Checked = cbShowDate.Checked;

		private void cbShowWeekday_CheckedChanged(object sender, EventArgs e) => tsmiShowWeekday.Checked = cbShowWeekday.Checked;

		private void tsmiExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			if (this.TopMost == false)
			{
				this.TopMost = true;
				this.TopMost = false;
			}
		}

		private void tsmiBackgroundColor_Click(object sender, EventArgs e)
		{
			if (backgroundColorDialog.ShowDialog() == DialogResult.OK)
				labelTime.BackColor = backgroundColorDialog.Color;
		}

		private void tsmiForegroundColor_Click(object sender, EventArgs e)
		{
			if (foregroundColorDialog.ShowDialog() == DialogResult.OK)
				labelTime.ForeColor = foregroundColorDialog.Color;
		}

		private void tsmiFont_Click(object sender, EventArgs e)
		{
			if (fontDialog.ShowDialog() == DialogResult.OK)
				labelTime.Font = fontDialog.Font;
		}

		private void tsmiAutorun_CheckedChanged(object sender, EventArgs e)
		{
			string key_name = "Clock_P_418";
			//262144B (256kB)
			RegistryKey registryKey = Registry.CurrentUser
				.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);//true - позволяет записывать в открытую ветку
			if (tsmiAutorun.Checked) registryKey.SetValue(key_name, Application.ExecutablePath);
			else registryKey.DeleteValue(key_name, false);//false - НЕ бросать исключение (Exception) если ключ (запись в реестре) не найден(а).
			registryKey.Dispose();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			//string filename = "Settings.ini";
			//StreamWriter writer = new StreamWriter(filename);
			//writer.WriteLine(tsmiTopmost.Checked);
			//writer.WriteLine(tsmiShowControls.Checked);
			//writer.WriteLine(tsmiShowDate.Checked);
			//writer.WriteLine(tsmiShowWeekday.Checked);
			//writer.WriteLine(fontDialog.Filename);
			//writer.WriteLine(backgroundColorDialog.Color.ToArgb());
			//writer.WriteLine(foregroundColorDialog.Color.ToArgb());
			//writer.Close();
			//Process.Start("notepad", filename);
			SaveSettings();
		}
	}
}
