using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Clock
{
	public partial class FontDialog : Form
	{
		public FontDialog()
		{
			InitializeComponent();
			Console.WriteLine(Directory.GetCurrentDirectory());
			Console.WriteLine($"{Application.ExecutablePath}");
			Directory.SetCurrentDirectory($"{Application.ExecutablePath}\\..\\..\\..\\Fonts");
			Console.WriteLine(Directory.GetCurrentDirectory());

			LoadFonts("*.ttf");
			LoadFonts("*.otf");
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
	}
}
