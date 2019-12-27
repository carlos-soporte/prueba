using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Parking_Pascualino
{
    public partial class List_options : Form
    {
        SqlConnection connection;
        SqlDataReader dataReader;
        SqlCommand command;
        int[] estado = new int[10];
        Button[] buttons = new Button[10];
        

        public List_options()
        {
            InitializeComponent();
        }

        private void List_options_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection("server=CARLITOS-PC; database=Parking_Pascualino ; integrated security = true");
            buttons[0] = btn1;
            buttons[1] = btn2;
            buttons[2] = btn3;
            buttons[3] = btn4;
            buttons[4] = btn5;
            buttons[5] = btn6;
            buttons[6] = btn7;
            buttons[7] = btn8;
            buttons[8] = btn9;
            buttons[9] = btn10;
            try
            {
                connection.Open();
                //MessageBox.Show("se abrio la base de datos");
            }
            catch (Exception ex)
            {
                MessageBox.Show("el servidor no existe");

            }

            string query = "SELECT number_celda,estado FROM Celdas";
            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            int i = 0;

            while (dataReader.Read())
            {
                
                estado[i] = Convert.ToInt32(dataReader["estado"]);
                i++;
               
            }
            dataReader.Close();
            
            for(int j=0; j < 10; j++)
            {
                if (estado[j] == 0)
                {
                    buttons[j].BackColor = Color.LightSkyBlue;
                }
                else
                {
                    buttons[j].BackColor = Color.Red;
                    buttons[j].Enabled = false;
                }
            }

        }

        private void Button11_Click(object sender, EventArgs e)
        {
            Get_in get_In = new Get_in();
            get_In.Show();
            this.Hide();
            connection.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Get_out get_Out = new Get_out();
            get_Out.Show();
            this.Hide();
            connection.Close();
        }
    }
}
