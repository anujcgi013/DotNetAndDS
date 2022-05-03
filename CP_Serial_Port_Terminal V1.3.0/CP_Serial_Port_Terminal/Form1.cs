using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using MySql.Data.MySqlClient;


#region About CP Serial Port Terminal
/// <summary>
/// Version 1.0.5 Date 04/05/2020
/// Version 1.0.6 Date 10/05/2020
/// Version 1.1.0 Date 16/05/2020
/// Version 1.1.5 Date 23/05/2020
/// Version 1.2.0 Date 31/05/2020
/// Version 1.3.0 Date 06/08/2020
/// </summary>
#endregion

namespace CP_Serial_Port_Terminal
{
    public partial class Form1 : Form
    {
        string dataOUT;
        string sendWith;
        string dataIN;
        int dataINLength;
        int[] dataInDec;

        StreamWriter objStreamWriter;
        string pathFile;
        bool state_AppendText = true;

        MySqlConnection myConnection;
        MySqlCommand myCommand;


        #region My Own Method
        private void SaveDataToTxtFile()
        {
            if(saveToTxtFileToolStripMenuItem.Checked)
            {
                try
                {
                    objStreamWriter = new StreamWriter(pathFile, state_AppendText);
                    if (toolStripComboBox_writeLineOrwriteText.Text == "WriteLine")
                    {
                        objStreamWriter.WriteLine(dataIN);
                    }
                    else if (toolStripComboBox_writeLineOrwriteText.Text == "Write")
                    {
                        objStreamWriter.Write(dataIN + " ");
                    }

                    objStreamWriter.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }
        private void SaveDataToMySqlDatabase()
        {
            if(saveToMySQLDatabaseToolStripMenuItem.Checked)
            {
                try
                {
                    myConnection = new MySqlConnection("server=localhost; username=root; password=; port=3306; database=database01");
                    myConnection.Open();

                    myCommand = new MySqlCommand(string.Format("INSERT INTO `table01` VALUES('{0}')", dataIN), myConnection);
                    myCommand.ExecuteNonQuery();

                    myConnection.Close();

                    RefreshDataGridViewForm2();
                }
                catch(Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        #region Custom EventHandler
        public delegate void UpdateDelegate(object sender, UpdateDataEventArgs args);

        public event UpdateDelegate UpdateDataEventHandler;
        public class UpdateDataEventArgs : EventArgs
        {

        }
        protected void RefreshDataGridViewForm2()
        {
            UpdateDataEventArgs args = new UpdateDataEventArgs();
            UpdateDataEventHandler.Invoke(this, args);
        }
        #endregion

        #region RX Data Format
        private string RxDataFormat(int[] dataInput)
        {
            string strOut = "";

            if(toolStripComboBox4.Text == "Hex")
            {
                foreach(int element in dataInput)
                {
                    strOut += Convert.ToString(element, 16) + "\t";
                }
            }
            else if (toolStripComboBox4.Text == "Decimal")
            {
                foreach (int element in dataInput)
                {
                    strOut += Convert.ToString(element) + "\t";
                }
            }
            else if (toolStripComboBox4.Text == "Binary")
            {
                foreach (int element in dataInput)
                {
                    strOut += Convert.ToString(element, 2) + "\t";
                }
            }
            else if (toolStripComboBox4.Text == "Char")
            {
                foreach (int element in dataInput)
                {
                    strOut += Convert.ToChar(element);
                }
            }

            return strOut;
        }
        #endregion

        #endregion

        #region GUI Method
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chBoxDtrEnable.Checked = false;
            serialPort1.DtrEnable = false;
            chBoxRTSEnable.Checked = false;
            serialPort1.RtsEnable = false;
            btnSendData.Enabled = true;
            sendWith = @"Both (\r\n)";
            toolStripComboBox3.Text = "BOTTOM";
            toolStripComboBox1.Text = "Add to Old Data";
            toolStripComboBox2.Text = @"Both (\r\n)";

            toolStripComboBox_appendOrOverwriteText.Text = "Append Text";
            toolStripComboBox_writeLineOrwriteText.Text = "WriteLine";

            pathFile = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            pathFile += @"\_My Source File\SerialData.txt";

            saveToTxtFileToolStripMenuItem.Checked = false;
            saveToMySQLDatabaseToolStripMenuItem.Checked = false;

            toolStripComboBox4.Text = "Char";
        }

        private void oPENToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cBoxCOMPORT.Text;
                serialPort1.BaudRate = Convert.ToInt32(CBoxBaudRate.Text);
                serialPort1.DataBits = Convert.ToInt32(cBoxDataBits.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxStopBits.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxParityBits.Text);

                serialPort1.Open();
                progressBar1.Value = 100;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cLOSEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                progressBar1.Value = 0;
            }
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                dataOUT = tBoxDataOut.Text;
                if (sendWith == "None")
                {
                    serialPort1.Write(dataOUT);
                }
                else if(sendWith==@"Both (\r\n)")
                {
                    serialPort1.Write(dataOUT + "\r\n");
                }
                else if (sendWith == @"New Line (\n)")
                {
                    serialPort1.Write(dataOUT + "\n");
                }
                else if (sendWith == @"Carriage Return (\r)")
                {
                    serialPort1.Write(dataOUT + "\r");
                }

            }
            if(clearToolStripMenuItem.Checked)
            {
                if (tBoxDataOut.Text != "")
                {
                    tBoxDataOut.Text = "";
                }
            }
        }

        private void toolStripComboBox2_DropDownClosed(object sender, EventArgs e)
        {
            //None
            //Both
            //New Line
            //Carriage Return

            if (toolStripComboBox2.Text == "None")
            {
                sendWith = "None";
            }
            else if (toolStripComboBox2.Text == @"Both (\r\n)")
            {
                sendWith = @"Both (\r\n)";
            }
            else if (toolStripComboBox2.Text == @"New Line (\n)")
            {
                sendWith = @"New Line (\n)";
            }
            else if (toolStripComboBox2.Text == @"Carriage Return (\r)")
            {
                sendWith = @"Carriage Return (\r)";
            }
        }

        private void chBoxDtrEnable_CheckedChanged(object sender, EventArgs e)
        {
            if(chBoxDtrEnable.Checked)
            {
                serialPort1.DtrEnable = true;
                MessageBox.Show("DTR Enable", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else { serialPort1.DtrEnable = false; }
        }

        private void chBoxRTSEnable_CheckedChanged(object sender, EventArgs e)
        {
            if(chBoxRTSEnable.Checked)
            {
                serialPort1.RtsEnable = true;
                MessageBox.Show("RTS Enable", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else { serialPort1.RtsEnable = false; }
        }

        private void tBoxDataOut_TextChanged(object sender, EventArgs e)
        {
            int dataOUTLength = tBoxDataOut.TextLength;
            lblDataOutLength.Text = string.Format("{0:00}", dataOUTLength);
        }

        private void tBoxDataOut_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.doSomething();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void doSomething()
        {
            if (serialPort1.IsOpen)
            {
                dataOUT = tBoxDataOut.Text;
                if (sendWith == "None")
                {
                    serialPort1.Write(dataOUT);
                }
                else if (sendWith == @"Both (\r\n)")
                {
                    serialPort1.Write(dataOUT + "\r\n");
                }
                else if (sendWith == @"New Line (\n)")
                {
                    serialPort1.Write(dataOUT + "\n");
                }
                else if (sendWith == @"Carriage Return (\r)")
                {
                    serialPort1.Write(dataOUT + "\r");
                }
            }
            if (clearToolStripMenuItem.Checked)
            {
                if (tBoxDataOut.Text != "")
                {
                    tBoxDataOut.Text = "";
                }
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //dataIN = serialPort1.ReadExisting();

            List<int> dataBuffer = new List<int>();

            while(serialPort1.BytesToRead > 0)
            {
                try
                {
                    dataBuffer.Add(serialPort1.ReadByte());
                }
                catch(Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }

            dataINLength = dataBuffer.Count();
            dataInDec = new int[dataINLength];
            dataInDec = dataBuffer.ToArray();

            this.Invoke(new EventHandler(ShowData));
        }

        private void ShowData(object sender, EventArgs e)
        {
            //int dataINLength = dataIN.Length;

            dataIN = RxDataFormat(dataInDec);

            lblDataInLength.Text = string.Format("{0:00}", dataINLength);

            if(toolStripComboBox1.Text == "Always Update")
            {
                tBoxDataIN.Text = dataIN;
            }
            else if(toolStripComboBox1.Text == "Add to Old Data")
            {
                if(toolStripComboBox3.Text == "TOP")
                {
                    tBoxDataIN.Text = tBoxDataIN.Text.Insert(0, dataIN);
                }
                else if(toolStripComboBox3.Text=="BOTTOM")
                {
                    tBoxDataIN.Text += dataIN;
                }      
            }

            SaveDataToTxtFile();
            SaveDataToMySqlDatabase();
        }

        private void clearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tBoxDataIN.Text != "")
            {
                tBoxDataIN.Text = "";
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Catur P", "Creator", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            groupBox12.Width = panel1.Width - 213;
            groupBox12.Height = panel1.Height - 40;

            tBoxDataIN.Height = panel1.Height - 88;
        }

        private void toolStripComboBox_appendOrOverwriteText_DropDownClosed(object sender, EventArgs e)
        {
            if(toolStripComboBox_appendOrOverwriteText.Text == "Append Text")
            {
                state_AppendText = true;
            }
            else
            {
                state_AppendText = false;
            }
        }

        private void showDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 objForm2 = new Form2(this);
            objForm2.Show();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 objForm3 = new Form3(this);
            objForm3.Show();
            this.Hide();
        }
        
        private void tBoxDataIN_TextChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox3.Text == "BOTTOM")
            {
                tBoxDataIN.SelectionStart = tBoxDataIN.Text.Length;
                tBoxDataIN.ScrollToCaret();
            }
        }

        private void cBoxCOMPORT_DropDown(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxCOMPORT.Items.Clear();
            cBoxCOMPORT.Items.AddRange(ports);
        }

        #endregion


    }
}
