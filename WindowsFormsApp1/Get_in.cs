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
    public partial class Get_in : Form
    {
        SqlConnection connection;
        SqlDataReader dataReader;
        SqlCommand command;
        int[] estado = new int[10];
        Button[] buttons = new Button[10];
        public Get_in()
        {
            InitializeComponent();
        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void Get_in_Load(object sender, EventArgs e)
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
            for (int j = 0; j < 10; j++)
            {
                if (estado[j] == 0)
                {
                    buttons[j].BackColor = Color.LightSkyBlue;
                }
                else
                {
                    buttons[j].BackColor = Color.Red;
                    buttons[j].Enabled = false;
                    if (estado[9] == 1)
                    {
                        btnSave.Enabled = false;
                    }
                }
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            
            connection = new SqlConnection("server=CARLITOS-PC; database=Parking_Pascualino ; integrated security = true");
            try
            {
                connection.Open();
                //MessageBox.Show("se abrio la base de datos");
            }
            catch (Exception ex)
            {
                MessageBox.Show("el servidor no existe");

            }

            string query = "INSERT INTO Vehículo(Placa,Tipo_vehiculo,Hora_entrada) VALUES('";
            string plate = txtPlate.Text + "','";
            string vehicle;
            bool bandera = false;
            if (checkMotorcycle.Checked == true)
            {
                vehicle = "moto','";
            }
            else
            {
                vehicle = "carro','";

            }

            string hora_ingreso = txtMin.Text + ":" + txtSeg.Text + "')";
      
            string query2 =query+plate+vehicle+hora_ingreso;
            
            SqlCommand command = new SqlCommand(query2, connection);
            
            try
            {
                
                command.ExecuteNonQuery();
                MessageBox.Show("Los datos se guardaron correctamente");
                bandera = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error en los datos enviados", ex.ToString());
            }
            finally
            {
                txtPlate.Text = "";
                txtMin.Text = "";
                txtSeg.Text = "";

            }
            

            


            if (bandera == true)
            {
                for(int p = 0; p < 10; p++)
                {
                    if (estado[p] == 0)
                    {
                        
                        string query3 = "UPDATE Celdas SET estado=1 WHERE number_celda="+(p+1);
                        SqlCommand command2 = new SqlCommand(query3, connection);

                        try
                        {

                            command2.ExecuteNonQuery();
                            this.Close();
                            Get_in get_In = new Get_in();
                            get_In.Show();
                            
                            //MessageBox.Show("Los datos se guardaron correctamente");
                            break;

                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Error en los datos enviados", ex.ToString());
                        }

                        


                    }
                    
                }

                
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            List_options list_Options = new List_options();
            list_Options.Show();
        }
    }
}
