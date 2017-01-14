using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TVSeries
{
    public partial class Form : System.Windows.Forms.Form
    {
        TVSeriesEntities tvse;

        public Form()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                TVSeries tv = new TVSeries();
                tvse.TVSeries.Add(tv);
                tVSeriesBindingSource.Add(tv);
                tVSeriesBindingSource.MoveLast();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            tVSeriesBindingSource.ResetBindings(false);

            foreach(DbEntityEntry entry in tvse.ChangeTracker.Entries())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                tVSeriesBindingSource.EndEdit();
                tvse.SaveChangesAsync();
                panel.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                tVSeriesBindingSource.ResetBindings(false);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            panel.Enabled = false;
            tvse = new TVSeriesEntities();
            tVSeriesBindingSource.DataSource = tvse.TVSeries.ToList();
        }
    }
}
