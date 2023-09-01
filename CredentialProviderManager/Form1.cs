using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CredentialProviderManager
{
    public partial class Form1 : Form
    {
        private LoginProvidersModel loginProviderModel = new LoginProvidersModel("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Authentication\\Credential Providers");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridLoginProviders.DataSource = loginProviderModel.getEntries();
            dataGridLoginProviders.Update();
            //dataGridLoginProviders.Size = this.Size;
        }

        private void OnCurrentCellDirtyChanged(object sender, EventArgs e)
        {
            if (this.dataGridLoginProviders.CurrentCell is DataGridViewCheckBoxCell);
            {
                this.dataGridLoginProviders.EndEdit();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //dataGridLoginProviders.Size = this.Size;
            //this.Text = this.Size.ToString();
        }
    }
}
