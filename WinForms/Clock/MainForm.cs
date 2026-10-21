using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
			backgroundColorDialog = new ColorDialog();
			foregroundColorDialog = new ColorDialog();
			fontDialog = new FontDialog();

			this.Location = new Point
			(
				Screen.PrimaryScreen.Bounds.Right - this.Width - 50,
				Screen.PrimaryScreen.Bounds.Bottom / 20
			);
			SetVisibility(false);
		}

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
			if (fontDialog.ShowDialog() == DialogResult.OK) ;
		}
	}
}
