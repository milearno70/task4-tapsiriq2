using System;
using System.IO;
using System.Windows.Forms;

namespace task4_tapsiriq2
{
    public partial class Form1 : Form
    {
        string fayl = "";
        bool deyisib = false, yuklenir = false;
        ToolStripStatusLabel simvol = new ToolStripStatusLabel();

        public Form1()
        {
            InitializeComponent();

            Text = "Qeydlər Dəftəri";
            rtbMetn.Dock = DockStyle.Fill;
            statusStrip1.Items.Add(simvol);

            rtbMetn.TextChanged += (s, e) =>
            {
                simvol.Text = "Simvol sayı: " + rtbMetn.Text.Length;
                if (!yuklenir) deyisib = true;
            };

            yeniToolStripMenuItem.Click += (s, e) =>
            {
                if (!Sor()) return;
                yuklenir = true;
                rtbMetn.Clear();
                yuklenir = false;
                fayl = "";
                deyisib = false;
            };

            acToolStripMenuItem.Click += (s, e) =>
            {
                if (!Sor()) return;
                if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

                yuklenir = true;
                fayl = openFileDialog1.FileName;
                rtbMetn.Text = File.ReadAllText(fayl);
                yuklenir = false;
                deyisib = false;
            };

            saxlaToolStripMenuItem.Click += (s, e) => Save();
            cixisToolStripMenuItem.Click += (s, e) => Close();

            sriftToolStripMenuItem.Click += (s, e) =>
            {
                if (fontDialog1.ShowDialog() == DialogResult.OK)
                    rtbMetn.SelectionFont = fontDialog1.Font;
            };

            rengToolStripMenuItem.Click += (s, e) =>
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                    rtbMetn.SelectionColor = colorDialog1.Color;
            };

            FormClosing += (s, e) => { if (!Sor()) e.Cancel = true; };
        }

        bool Save()
        {
            if (fayl == "")
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                    return false;
                fayl = saveFileDialog1.FileName;
            }

            File.WriteAllText(fayl, rtbMetn.Text);
            deyisib = false;
            return true;
        }

        bool Sor()
        {
            if (!deyisib) return true;

            DialogResult c = MessageBox.Show(
                "Saxlamaq istəyirsiniz?", "Qeydlər Dəftəri",
                MessageBoxButtons.YesNoCancel);

            if (c == DialogResult.Yes) return Save();
            return c == DialogResult.No;
        }
    }
}