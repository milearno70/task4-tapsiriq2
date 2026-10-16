
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace task4_tapsiriq2
{
    public partial class Form1 : Form
    {
        string fayl = "";
        bool deyisib = false, yuklenir = false;

        public Form1()
        {
            InitializeComponent();

            Text = "Qeydlər Dəftəri";
            rtbMetn.Dock = DockStyle.Fill;
            ToolStripStatusLabel simvolLabel = new ToolStripStatusLabel("Simvol sayı: 0");
            statusStrip1.Items.Add(simvolLabel);
            lblSimvolSayi.Text = "Simvol sayı: 0";

            rtbMetn.TextChanged += (s, e) =>
            {
                lblSimvolSayi.Text =
                    "Simvol sayı: " + rtbMetn.Text.Length;
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
                Text = "Qeydlər Dəftəri";
            };

            acToolStripMenuItem.Click += (s, e) =>
            {
                if (!Sor()) return;
                openFileDialog1.Filter = "Mətn faylları|*.txt";
                if (openFileDialog1.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    yuklenir = true;
                    fayl = openFileDialog1.FileName;
                    rtbMetn.Text = File.ReadAllText(fayl);
                    deyisib = false;
                    Text = "Qeydlər Dəftəri - " + Path.GetFileName(fayl);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                finally { yuklenir = false; }
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
                saveFileDialog1.Filter = "Mətn faylları|*.txt";
                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                    return false;
                fayl = saveFileDialog1.FileName;
            }

            try
            {
                File.WriteAllText(fayl, rtbMetn.Text);
                deyisib = false;
                Text = "Qeydlər Dəftəri - " + Path.GetFileName(fayl);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        bool Sor()
        {
            if (!deyisib) return true;

            DialogResult c = MessageBox.Show(
                "Dəyişiklikləri saxlamaq istəyirsiniz?",
                "Qeydlər Dəftəri",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (c == DialogResult.Yes) return Save();
            return c == DialogResult.No;
        }
    }
}
