using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace online_marketplace
{
    public partial class addproduct : Form
    {
        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet ds;

        string ordb = "Data Source=orcl; User Id=scott; Password=tiger;";
        OracleConnection Conn;
        OracleConnection conn;
        public addproduct()
        {
            InitializeComponent();
        }

        private void addproduct_Load(object sender, EventArgs e)
        {
            //filling comboBoxes with data

            Conn = new OracleConnection(ordb);
            Conn.Open();
            OracleCommand c = new OracleCommand();
            c.Connection = Conn;
            c.CommandText = "select s.username from sellers s";
            c.CommandType = CommandType.Text;

            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);


            }
            dr.Close();

            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cm = new OracleCommand();
            cm.Connection = conn;
            cm.CommandText = "select c.categoryname from productcategory c";
            cm.CommandType = CommandType.Text;

            OracleDataReader r = cm.ExecuteReader();
            while (r.Read())
            {
                comboBox2.Items.Add(r[0]);


            }
            dr.Close();


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //view data in data grid view
            string constr = "Data Source=orcl; User Id=scott; Password=tiger;";
            string cmd = "select * from product where seller_username =:x and categoryname=:y";
            adapter = new OracleDataAdapter(cmd, constr);
            adapter.SelectCommand.Parameters.Add("x", comboBox1.SelectedItem.ToString());
            adapter.SelectCommand.Parameters.Add("y", comboBox2.SelectedItem.ToString());
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //save the changes which happened in the grid view by user
            builder = new OracleCommandBuilder(adapter);
            adapter.Update(ds.Tables[0]);
        }

        private void addproduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            Conn.Dispose();
            Conn.Dispose();
        }
    }
}
