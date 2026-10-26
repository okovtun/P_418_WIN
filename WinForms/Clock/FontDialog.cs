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
using System.IO;

namespace Clock
{
	public partial class FontDialog : Form
	{
		public Font Font { get; set; }
		Form parentWindow;
		public FontDialog()
		{
			InitializeComponent();
			Console.WriteLine(Directory.GetCurrentDirectory());
			Console.WriteLine($"{Application.ExecutablePath}");
			Directory.SetCurrentDirectory($"{Application.ExecutablePath}\\..\\..\\..\\Fonts");
			Console.WriteLine(Directory.GetCurrentDirectory());

			LoadFonts("*.ttf");
			LoadFonts("*.otf");
			comboBoxFont.SelectedIndex = 0;
		}
		public FontDialog(Form parentWindow) : this()
		{
			this.parentWindow = parentWindow;
			this.StartPosition = FormStartPosition.Manual;
		}
		private void FontDialog_Load(object sender, EventArgs e)
		{
			this.Location = new Point
			(
				parentWindow.Location.X - this.Width/4,
				parentWindow.Location.Y + 128
			);
		}
		void LoadFonts(string pattern)
		{
			string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), pattern);
			for (int i = 0; i < files.Length; i++)
			{
				files[i] = files[i].Split('\\').Last();
			}
			comboBoxFont.Items.AddRange(files);
		}

		private void comboBoxFont_SelectionChangeCommitted(object sender, EventArgs e) => ApplyFont();

		private void counterFontSize_ValueChanged(object sender, EventArgs e) => ApplyFont();
		void ApplyFont()
		{
			PrivateFontCollection pfc = new PrivateFontCollection();
			pfc.AddFontFile(comboBoxFont.SelectedItem.ToString());
			labelExample.Font = new Font(pfc.Families[0], (int)counterFontSize.Value);
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.Font = labelExample.Font;
		}
	}
}
