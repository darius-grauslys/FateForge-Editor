using FateForge.Managers;
using FateForge.Managers.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FateForge
{
    public partial class SaveDialog : Form
    {
        /// <summary>
        /// Event triggerd when an export is happening. Use to stop user from editing during export.
        /// </summary>
        public static Action ExportLocking;

        /// <summary>
        /// Same as ExportLocking, but for unlocking. Wow.
        /// </summary>
        public static Action ExportUnlocking;

        public SaveDialog()
        {
            InitializeComponent();

            checkedListBox1.SelectedIndexChanged += CheckedListBox1_SelectedIndexChanged;
            FormClosing += ItemEditor_FormClosing;

            QuestManager.UpdateQuests += UpdateQuestMenu;
        }

        private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = QuestManager.GetQuestDescription(checkedListBox1.SelectedItem.ToString());
        }

        public void UpdateQuestMenu()
        {
            checkedListBox1.Items.Clear();
            QuestManager.GetQuestNames().ForEach((n) => checkedListBox1.Items.Add(n));
        }

        private void ExportLock()
        {
            ExportLocking();
            checkedListBox1.Enabled = false;
        }

        private void ExportUnlock()
        {
            ExportUnlocking();
            checkedListBox1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedItem == null)
                return;
            QuestEditor q = QuestManager.GetQuestEditor(checkedListBox1.SelectedItem.ToString());
            FolderBrowserDialog diag = new FolderBrowserDialog();
            diag.ShowDialog();
            string dialogPath = diag.SelectedPath;
            if (dialogPath == "")
                return;
            StreamWriter sw = new StreamWriter(String.Format("{0}//{1}.xml", dialogPath, q.QuestName));
            ExportManager.SerializeGenericBetonStructure(sw, q);
            sw.Close();
        }

        /// <summary>
        /// Prevent the form from disposing on close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
