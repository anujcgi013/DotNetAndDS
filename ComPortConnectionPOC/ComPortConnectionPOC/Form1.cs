using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace ComPortConnectionPOC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                cboPort.Items.AddRange(ports);
                cmbBaudRate.SelectedIndex = 6;
                cmbDataBites.SelectedIndex = 3;
                cmbStopBits.SelectedIndex = 0;
                cmbParity.SelectedIndex = 0;
                //cmbStopBits.Enabled = false;
                //cmbParity.Enabled = false;
                btnClose.Enabled = false;
            }
            else
            {
                MessageBox.Show("No COM Ports are not detected in your Machine !");
                cmbBaudRate.SelectedIndex = 6;
                cmbDataBites.SelectedIndex = 3;
                cmbStopBits.SelectedIndex = 0;
                cmbParity.SelectedIndex = 0;
                //cmbStopBits.Enabled = false;
                cmbParity.SelectedIndex = 0;
                //cmbParity.Enabled = false;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            String dtn = dt.ToShortTimeString();
            btnOpen.Enabled = false;
            btnClose.Enabled = true;
            try
            {
                serialPort1.PortName = cboPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(cmbBaudRate.Text);
                serialPort1.DataBits = Convert.ToInt32(cmbDataBites.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
                //serialPort1.StopBits = StopBits.One;
                //serialPort1.Parity = Parity.None;
                serialPort1.Open();
                txtReceive.AppendText("[" + dtn + "] " + "Connected\n");
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(sport_DataReceived);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error While Opening Port:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            String dtn = dt.ToShortTimeString();

            txtReceive.AppendText("[" + dtn + "] " + "Received: " + serialPort1.ReadExisting() + "\n");
        }


        private void btnSend_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            String dtn = dt.ToShortTimeString();
            try
            {
                if (serialPort1.IsOpen)
                {
                    // Convert the user's string of hex digits (ex: B4 CA E2) to a byte array
                    byte[] data = HexStringToByteArray(txtSend.Text);

                    // Send the binary data out the port
                    serialPort1.Write(data, 0, data.Length);
                    //serialPort1.WriteLine(txtSend.Text + Environment.NewLine);
                    //MessageBox.Show(null, "Sent Byte data is:" + data, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtReceive.AppendText("[" + dtn + "] " + "Sent: " + ByteArrayToHexString(data) + "\n");
                    txtSend.Clear();
                }
                else
                {
                    MessageBox.Show(null, "There is no Port Open", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary> Convert a string of hex digits (ex: E4 CA B2) to a byte array. </summary>
        /// <param name="s"> The string containing the hex digits (with or without spaces). </param>
        /// <returns> Returns an array of bytes. </returns>
        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = true;
            btnClose.Enabled = false;
            try
            {
                serialPort1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    // Obtain the number of bytes waiting in the port's buffer
                    int bytes = serialPort1.BytesToRead;
                    //MessageBox.Show(null, "Received Byte data from Port:" + bytes, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Create a byte array buffer to hold the incoming data
                    byte[] buffer = new byte[bytes];

                    // Read the data from the port and store it in our buffer
                    serialPort1.Read(buffer, 0, bytes);

                    // Show the user the incoming data in hex format
                    //Log(LogMsgType.Incoming, ByteArrayToHexString(buffer));
                    txtReceive.Text = ByteArrayToHexString(buffer);
                }
                else
                {
                    MessageBox.Show(null, "There is no Port Open", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
        }
    }
}
