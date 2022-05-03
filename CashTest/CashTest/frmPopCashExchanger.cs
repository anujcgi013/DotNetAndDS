using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.IO;

namespace CashTest
{
    public partial class frmPopCashExchanger : Form
    {
        int txt_income = 0;
        int txt_destination_amount = 0;
        int txt_exchange = 0;

        int coin_out_cnt_err = 0;

        public volatile bool flag_coin = false;
        public volatile bool flag_bill = false;
        public volatile bool flag_insert_complete = false;

        public string ReturnValue { get; set; }

        public delegate void AddDataDelegate(int income);
        public AddDataDelegate myDelegate;

        System.Windows.Forms.Timer reload = new System.Windows.Forms.Timer();//////////////////////////////////////////////////일정 시간마다 바뀐것 감지
        System.Windows.Forms.Timer timeout = new System.Windows.Forms.Timer();

        System.Windows.Forms.Timer background_animation = new System.Windows.Forms.Timer();
        int background_animation_count = 0;
        private int timeout_flag = 0;


        //170919 지폐,동전쪽 타임아웃 타이머에서 대기하게 변경
        System.Windows.Forms.Timer coin_timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer bill_timer = new System.Windows.Forms.Timer();
        private int bill_timer_cnt, coin_timer_cnt = 0;
        private int bill_cnt, coin_cnt = 0;

        //취소flag
        private int cancel_flag = 0;

        public frmPopCashExchanger(int amount)
        {
            txt_destination_amount = amount;
            InitializeComponent();
        }

        private void init_label()
        {
            lblIncome.Text = "0";
            lblDestination.Text = string.Format("{0:N0}", txt_destination_amount);
        }

        private void frmPopCashExchanger_Load(object sender, EventArgs e)
        {

            SP.PortName = "COM" + Convert.ToString(frmMain.port_exchanger);
            SP.BaudRate = frmMain.baud_exchanger;
            SP.WriteTimeout = 3000;
            SP.ReadTimeout = 13000;
            

            init_label();

            try
            {
                SP.Open();
            }
            catch
            {
                MessageBox.Show("금전 교환기 시리얼 통신 'PORT OPEN' 오류 입니다.", "통신 PORT OPEN 오류", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (SP.IsOpen)
                    SP.Close();
                this.ReturnValue = "cancel";
                Close();
            }

            this.myDelegate = new AddDataDelegate(AddDataMethod);

            reload.Interval = (500);
            reload.Tick += new EventHandler(check_amount);
            reload.Start();

            Send_start_command();

            timeout.Interval = 90000;
            timeout.Tick += new EventHandler(timeout_Tick);
            timeout.Start();

            coin_timer.Interval = 1000;
            bill_timer.Interval = 1000;
            bill_timer.Tick += new EventHandler(bill_timer_Tick);
            coin_timer.Tick += new EventHandler(coin_timer_Tick);

            background_animate_start();
        }

        void timeout_Tick(object sender, EventArgs e)
        {
            btnCancel_Click(sender, null);
        }

        private void check_amount(object sender, EventArgs e)
        {
            if (cancel_flag == 0)
            {
                if (flag_insert_complete)//금액 투입이 완료 되었을 경우
                {
                    ExchangeDispense();
                    btnCancel.Enabled = false;
                    flag_insert_complete = false;
                }

                if (flag_coin && flag_bill)//거스름돈 전체 완료 되었을 경우
                {
                    reload.Stop();
                    Send_finish_command();
                    Thread.Sleep(100);
                    if (SP.IsOpen)
                    {
                        SP.Close();
                    }
                    this.ReturnValue = "ok";
                    ////sqlite_helper.change_money_in_out_status("정상완료");
                    timeout.Stop();
                    background_animation.Stop();
                    Close();
                }
            }
            else //돌아가기 눌렀을 경우.
            {

                if (flag_coin && flag_bill)//거스름돈 전체 완료 되었을 경우
                {
                    reload.Stop();
                    Send_finish_command();
                    Thread.Sleep(100);
                    if (SP.IsOpen)
                    {
                        SP.Close();
                    }
                    this.ReturnValue = "cancel";
                    //sqlite_helper.change_money_in_out_status("거래중단");
                    //sqlite_helper.insert_money_log("거래중단", "4", txt_income, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 1);
                    timeout.Stop();
                    background_animation.Stop();
                    Close();
                }
            }
        }

        public void setIncome()
        {
            lblIncome.Text = string.Format("{0:N0}", txt_income);
            txt_exchange = txt_income - txt_destination_amount;

            if (txt_income >= txt_destination_amount)
            {
                btnCancel.Enabled = false;
                flag_insert_complete = true;
            }
        }

        private void Send_start_command()
        {
            byte[] start_command = { 0x02, 0x49, 0x53, 0x03 };
            byte[] status_command = { 0x02, 0x00, 0x00, 0x03 };
            try
            {
                SP.WriteTimeout = 2000;
                SP.Write(status_command, 0, status_command.Length);
                Log("Send       ", make_hexString(status_command[0]) + "  " + make_hexString(status_command[1]) + "  " + make_hexString(status_command[2]) + "  " + make_hexString(status_command[3]) + "  ");
                byte[] read_buf = new byte[6];
                Thread.Sleep(1000);
                SP.Read(read_buf, 0, 6);

                if (read_buf[4] == 0xff)//금전교환기 에러
                {
                    MessageBox.Show("금전 교환기 통신 오류입니다.\n연결상태 확인 후 재시도 해주세요.\n", "금전교환기 오류", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (SP.IsOpen)
                        SP.Close();
                    this.ReturnValue = "cancel";
                    //frmMain.ThrowError(0);
                    Close();
                    return;
                }

                SP.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(SP_DataReceived);
                SP.Write(start_command, 0, start_command.Length);
                Log("Send       ", make_hexString(status_command[0]) + "  " + make_hexString(status_command[1]) + "  " + make_hexString(status_command[2]) + "  " + make_hexString(status_command[3]) + "  ");

            }
            catch (Exception ex_err)
            {
                MessageBox.Show("금전 교환기 통신 오류입니다.\n연결상태 확인 후 재시도 해주세요.\n", "금전교환기 오류", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine(ex_err);
                if (SP.IsOpen)
                    SP.Close();
                this.ReturnValue = "cancel";
                //frmMain.print_supervisor_call("금전교환기 에러");
                Close();
            }
        }

        private void Send_finish_command()
        {
            byte[] finish_command = { 0x02, 0x49, 0x43, 0x03 };
            try
            {
                SP.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(SP_DataReceived);
                SP.Write(finish_command, 0, finish_command.Length);
                Log("Send       ", make_hexString(finish_command[0]) + "  " + make_hexString(finish_command[1]) + "  " + make_hexString(finish_command[2]) + "  " + make_hexString(finish_command[3]) + "  ");
            }
            catch (Exception ex_err)
            {
                MessageBox.Show("금전 교환기 통신 오류입니다.\n연결상태 확인 후 재시도 해주세요.\n", "금전교환기 오류", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine(ex_err);
                if (SP.IsOpen)
                    SP.Close();
                this.ReturnValue = "cancel";
                //frmMain.print_supervisor_call("금전교환기 에러");
                Close();
            }
        }

        private void Send_coin_command(int count)
        {
            if (count < 1)
            {
                flag_coin = true;
                return;
            }
            coin_out_cnt_err = count;

            byte dispense_cnt = 0x3F;//1개 뽑는게 0x40 이기 때문
            for (int i = 0; i < count; i++)
            {
                dispense_cnt += 0x01;
            }

            byte[] hopper_command = { 0x02, 0x4F, 0x53, 0x48, dispense_cnt, 0x03 };
            SP.Write(hopper_command, 0, hopper_command.Length);
            Log("Send       ", make_hexString(hopper_command[0]) + "  " + make_hexString(hopper_command[1]) + "  " + make_hexString(hopper_command[2]) + "  " + make_hexString(hopper_command[3]) + "  " + make_hexString(hopper_command[4]) + " " + make_hexString(hopper_command[5]) + "  ");
            /*
            int temp_cnt = 0;
            while (!flag_coin)
            {
                Thread.Sleep(100);
                temp_cnt++;
                if (temp_cnt > 300)
                {
                    frmMain.mode_exchanger_error = 2;
                    frmMain.flag_exchanger_error = true;
                }
            }
            */
        }

        private void Send_bill_command(int count)
        {
            if (count < 1)
            {
                flag_bill = true;
                return;
            }

            byte dispense_cnt = 0x00;//1개 뽑는게 0x01이기 때문
            for (int i = 0; i < count; i++)
            {
                dispense_cnt += 0x01;
            }

            byte[] bill_command = { 0x02, 0x4F, 0x53, 0x50, dispense_cnt, 0x03 };
            SP.Write(bill_command, 0, bill_command.Length);
            Log("Send       ", make_hexString(bill_command[0]) + "  " + make_hexString(bill_command[1]) + "  " + make_hexString(bill_command[2]) + "  " + make_hexString(bill_command[3]) + "  " + make_hexString(bill_command[4]) + " " + make_hexString(bill_command[5]) + "  ");
            /*
            int temp_cnt = 0;
            while (!flag_bill)
            {
                Thread.Sleep(100);
                temp_cnt++;
                if (temp_cnt > 300)
                {
                    frmMain.mode_exchanger_error = 3;
                    frmMain.flag_exchanger_error = true;
                }
            }
            */
        }

        private void coin_timer_Tick(object sender, EventArgs e)
        {
            if (coin_timer_cnt == 0)
            {
                Send_coin_command(coin_cnt);
                coin_timer_cnt++;
            }
            else
            {
                if (!flag_coin)
                {
                    coin_timer_cnt++;
                    if (coin_timer_cnt > 30)
                    {
                        //frmMain.mode_exchanger_error = 2;
                        //frmMain.flag_exchanger_error = true;
                        flag_coin = true;
                        coin_timer.Stop();
                    }
                }
                else
                    coin_timer.Stop();
            }
        }
        private void bill_timer_Tick(object sender, EventArgs e)
        {
            if (bill_timer_cnt == 0 && flag_coin) //코인 상태 완료후 빌로 넘어옴.
            {
                Send_bill_command(bill_cnt);
                bill_timer_cnt++;
            }

            if (bill_timer_cnt != 0)
            {
                if (!flag_bill)
                {
                    bill_timer_cnt++;
                    if (bill_timer_cnt > 30)
                    {
                        //frmMain.mode_exchanger_error = 3;
                        //frmMain.flag_exchanger_error = true;
                        flag_bill = true;
                        bill_timer.Stop();
                    }
                }
                else
                    bill_timer.Stop();
            }
        }

        private string make_hexString(byte data)
        {
            string aa = string.Format(CultureInfo.InvariantCulture, "{0:X2}", data);
            return aa;
        }
        private void SP_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (SP.ReadBufferSize > 5)
            {
                byte[] buffer = new byte[SP.ReadBufferSize + 1];
                SP.Read(buffer, 0, buffer.Length);
                Log("Receive    ", make_hexString(buffer[0]) + "  " + make_hexString(buffer[1]) + "  " + make_hexString(buffer[2]) + "  " + make_hexString(buffer[3]) + "  " + make_hexString(buffer[4]) + " " + make_hexString(buffer[5]) + "  ");
                Console.WriteLine(make_hexString(buffer[0]) + "  " + make_hexString(buffer[1]) + "  " + make_hexString(buffer[2]) + "  " + make_hexString(buffer[3]) + "  " + make_hexString(buffer[4]) + " " + make_hexString(buffer[5]) + "  ");
                if (buffer[0] == 0x02 && buffer[5] == 0x03)//stx etx
                {

                    if (buffer[2] == 0x46)                                                              //정상응답
                    {
                        if (buffer[1] == 0x49 && buffer[3] == 0x42)                  //지폐 삽입
                        {
                            timeout_flag = 1;
                            int add_amount = 0;

                            if (buffer[4] == 0x40)
                            {
                                add_amount = 1000;
                                ////sqlite_helper.change_money_in_out(1, 1);
                            }
                            else if (buffer[4] == 0x41)
                            {
                                add_amount = 5000;
                                ////sqlite_helper.change_money_in_out(2, 1);
                            }
                            else if (buffer[4] == 0x42)
                            {
                                add_amount = 10000;
                                ////sqlite_helper.change_money_in_out(3, 1);
                            }
                            else if (buffer[4] == 0x43)
                            {
                                add_amount = 50000;
                                ////sqlite_helper.change_money_in_out(4, 1);
                            }
                            lblIncome.Invoke(myDelegate, new Object[] { add_amount });

                        }
                        else if (buffer[1] == 0x4F && buffer[3] == 0x48)             //동전 배출
                        {
                            flag_coin = true;
                            ////sqlite_helper.change_money_in_out(6, buffer[4] - 63);
                        }
                        else if (buffer[1] == 0x4F && buffer[3] == 0x50)             //지폐 배출
                        {
                            flag_bill = true;
                            ////sqlite_helper.change_money_in_out(5, buffer[4]);
                        }
                    }
                    else if (buffer[2] == 0x45 && buffer[4] == 0xff)                                     //에러
                    {
                        //에러처리
                        if (buffer[1] == 0x49 && buffer[3] == 0x42)                  //지폐 삽입
                        {
                            //frmMain.ThrowError(1);
                        }
                        else if (buffer[1] == 0x4F && buffer[3] == 0x48)             //동전 배출
                        {
                            //frmMain.mode_exchanger_error = 2;
                            //frmMain.flag_exchanger_error = true;
                            flag_coin = true;
                            ////sqlite_helper.change_money_in_out(6, coin_out_cnt_err); //모자라는 에러 떠도 배출 했다고 가정.
                        }
                        else if (buffer[1] == 0x4F && buffer[3] == 0x50)             //지폐 배출
                        {
                            //frmMain.mode_exchanger_error = 3;
                            //frmMain.flag_exchanger_error = true;
                            flag_bill = true;
                        }
                    }
                    else if (buffer[2] == 0x45 && buffer[4] >= 0x00) // 에러후 FF말고 지폐 량 찍어줌...바뀐것.
                    {
                        if (buffer[1] == 0x4F && buffer[3] == 0x50)             //지폐 배출
                        {
                            //frmMain.mode_exchanger_error = 3;
                            //frmMain.flag_exchanger_error = true;
                            flag_bill = true;
                            ////sqlite_helper.change_money_in_out(5, buffer[4]);
                        }
                        else if (buffer[1] == 0x4F && buffer[3] == 0x48)             //동전 배출
                        {
                            //frmMain.mode_exchanger_error = 2;
                            //frmMain.flag_exchanger_error = true;
                            flag_coin = true;
                            int err_coin = 0;

                            err_coin = Convert.ToInt32(buffer[4]) - 63;
                            ////sqlite_helper.change_money_in_out(6, err_coin);
                        }
                    }
                }

                if (buffer[0] == 0x06)
                    bill_in_response();
            }
        }
        public void Log(string status, string msg)
        {
            string FilePath = Directory.GetCurrentDirectory() + @"\CashTableLog\" + DateTime.Today.ToString("yyyyMMdd") + ".log";
            string DirPath = Directory.GetCurrentDirectory() + @"\CashTableLog";
            string temp;
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DirectoryInfo di = new DirectoryInfo(DirPath);
            FileInfo fi = new FileInfo(FilePath);
            try
            {
                if (di.Exists != true) Directory.CreateDirectory(DirPath);

                if (fi.Exists != true)
                {
                    using (StreamWriter sw = new StreamWriter(FilePath))
                    {
                        temp = string.Format("[{0}] {1} {2}", now, status, msg);
                        sw.WriteLine(temp);
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(FilePath))
                    {
                        temp = string.Format("[{0}] {1} {2}", now, status, msg);
                        sw.WriteLine(temp);
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Log 쌓는함수 에러");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //reload.Stop();

            if (cancel_flag == 1)
                return;

            if (txt_income != 0)
            {
                cancel_flag = 1;
                IncomeDispense();
                ////sqlite_helper.insert_money_log("거래중단", "4", txt_income, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                Send_finish_command();
                SP.Close();
                this.ReturnValue = "cancel";
                Close();
            }
        }

        public void AddDataMethod(int income)
        {
            txt_income += income;
            setIncome();
        }

        private void ExchangeDispense()
        {
            bill_cnt = txt_exchange / frmMain.exchanger_type;
            coin_cnt = 0;
            if (frmMain.coin_500)
                coin_cnt = (txt_exchange - (bill_cnt * frmMain.exchanger_type)) / 500;
            else
                coin_cnt = (txt_exchange - (bill_cnt * frmMain.exchanger_type)) / 100;

            //Send_coin_command(coin_cnt);
            //Send_bill_command(bill_cnt);


            coin_timer.Start();
            bill_timer.Start();
        }

        private void IncomeDispense()
        {
            bill_cnt = txt_income / frmMain.exchanger_type;
            coin_cnt = 0;
            if (frmMain.coin_500)
                coin_cnt = (txt_income - (bill_cnt * frmMain.exchanger_type)) / 500;
            else
                coin_cnt = (txt_income - (bill_cnt * frmMain.exchanger_type)) / 100;

            //Send_coin_command(coin_cnt);
            //Send_bill_command(bill_cnt);

            coin_timer.Start();
            bill_timer.Start();
        }

        private string getSubByte(string src, int startIndex, int byteCount)
        {
            System.Text.Encoding myEncoding = System.Text.Encoding.GetEncoding("ks_c_5601-1987");
            byte[] buf = myEncoding.GetBytes(src);

            return myEncoding.GetString(buf, startIndex, byteCount);
        }


        private string teltime_to_timestamp(string teltime)
        {
            string temp = "";
            //temp = 20 + frmMenu.getSubByte(teltime, 0, 2) + "-" + frmMenu.getSubByte(teltime, 2, 2) + "-" + frmMenu.getSubByte(teltime, 4, 2) + " " + frmMenu.getSubByte(teltime, 6, 2) + ":" + frmMenu.getSubByte(teltime, 8, 2) + ":" + frmMenu.getSubByte(teltime, 10, 2) + ".000";
            return temp;
        }

        void background_animation_Tick(object sender, EventArgs e)
        {
            if (this.BackgroundImage != null)
                this.BackgroundImage.Dispose();

            switch (background_animation_count)
            {
                case 0:
                    //if (frmMain.current_language == LANGUAGE.KOR)
                    //    this.BackgroundImage = global::UNOSPAY_Simple.Properties.Resources.payment_cash_udk1;
                    //else
                    background_animation_count++;
                    break;
                case 1:
                    //if (frmMain.current_language == LANGUAGE.KOR)
                    //    this.BackgroundImage = global::UNOSPAY_Simple.Properties.Resources.payment_cash_udk2;
                    //else
                    background_animation_count++;
                    break;
                case 2:
                    //if (frmMain.current_language == LANGUAGE.KOR)
                    //    this.BackgroundImage = global::UNOSPAY_Simple.Properties.Resources.payment_cash_udk3;
                    //else
                    background_animation_count++;
                    break;
                case 3:
                    //if (frmMain.current_language == LANGUAGE.KOR)
                    //    this.BackgroundImage = global::UNOSPAY_Simple.Properties.Resources.payment_cash_udk4;
                    //else
                    //{
                    //background_animation_count = 0;
                    //break;
                    //}
                    background_animation_count++;
                    break;
                case 4:
                    //if (frmMain.current_language == LANGUAGE.KOR)
                    //    this.BackgroundImage = global::UNOSPAY_Simple.Properties.Resources.payment_cash_udk5;
                    //else
                    background_animation_count = 0;
                    break;
            }

            if (timeout_flag == 1)
            {
                timeout_flag = 0;
                timeout.Stop();
                timeout.Start();
            }
        }
      
        void background_animate_start()
        {
            background_animation.Interval = (300);
            background_animation.Tick += new EventHandler(background_animation_Tick);
            background_animation.Start();
        }

        private void frmPopCashExchanger_FormClosed(object sender, FormClosedEventArgs e)
        {
            reload.Stop();
            timeout.Stop();
            background_animation.Stop();

            if (this.BackgroundImage != null)
                this.BackgroundImage.Dispose();

            foreach (Control con in this.Controls)
            {
                if (con.GetType() == typeof(Button))
                {
                    if (((Button)con).Image != null)
                        ((Button)con).Image.Dispose();
                    if (((Button)con).BackgroundImage != null)
                        ((Button)con).BackgroundImage.Dispose();
                }
                else if (con.GetType() == typeof(PictureBox))
                {
                    if (((PictureBox)con).Image != null)
                        ((PictureBox)con).Image.Dispose();
                    if (((PictureBox)con).BackgroundImage != null)
                        ((PictureBox)con).BackgroundImage.Dispose();
                }
                else
                {
                    if (con.BackgroundImage != null)
                        con.BackgroundImage.Dispose();
                }
            }

            this.Dispose(true);
        }

        private void bill_in_response()
        {
            byte[] command = { 0x06 };
            try
            {
                SP.Write(command, 0, command.Length);
            }
            catch
            {

            }
        }
    }
}
