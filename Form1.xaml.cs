using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
//using System.Configuration;
// using EntLibContrib.Data.MySql;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
namespace Ext { }
namespace DBEditor
{


    public partial class Form1 : Window
    {
        public byte[] resx;
        public int selecter;
        public string MysqlConn;
        protected static string quote = "\"";
        public string login;
        public int check;
        public int LoginID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool LogType { get; set; }
        public Form1()
        {

            InitializeComponent();
            System.Windows.Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            string appSp = System.Windows.Forms.Application.StartupPath + "\\";
            if (!System.IO.File.Exists(appSp + "mysql.data.dll"))
            {
                byte[] resd = DBEditor.Properties.Resources.mysql_data;
                System.IO.File.WriteAllBytes(appSp + "mysql.data.dll", resd);
            }
           
           //job_titan = System.IO.File.ReadAllBytes("/Resources/titan.jpg");
            resx = DBEditor.Properties.Resources.gam1232;
            //job_titan = DBEditor.Properties.Resources.titan;
            System.IO.File.WriteAllBytes(appSp + "cur.ani", resx);
            this.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.idtextbox.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.kill_box.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.carm_box.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.root_level.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.str_box.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.dex_box.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.int_box.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.con_box.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.IPtextBox.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.passwdtextb.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.acc_id_textb.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.LoginBox.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.lvltextbox.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.nicknamebox.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.nicktextbox.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.cashtextbox.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.emailtextbox.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.goldbox.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.chanelbox.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.zonebox.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.logintextb.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.iptextb.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.dbtextb.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            this.authtextb.Cursor = new System.Windows.Input.Cursor(appSp + "cur.ani");
            System.IO.File.Delete(appSp + "cur.ani");
            iptextb.Text = Properties.Settings.Default.IpSetting.ToString().Trim();
            logintextb.Text = Properties.Settings.Default.LoginSetting.ToString().Trim();
            passwdtextb.Text = Properties.Settings.Default.PassSetting.ToString().Trim();
            dbtextb.Text = Properties.Settings.Default.DBSetting.ToString().Trim();
            authtextb.Text = Properties.Settings.Default.DBAuthSetting.ToString().Trim();
            System.Windows.Forms.Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            button_findout.Click += new System.Windows.RoutedEventHandler(findoutButtonClickEventHandler);
            button_ban.Click += new System.Windows.RoutedEventHandler(banButtonClickEventHandler);
            activatebutton.Click += new System.Windows.RoutedEventHandler(ActivateButtonHandler);
            accinfo.Click += new System.Windows.RoutedEventHandler(accinfoButtonHandler);
            connectionButton.Click += new System.Windows.RoutedEventHandler(connectEventHandler);
            refreshbutton.Click += new System.Windows.RoutedEventHandler(refreshButtonHandler);
            unban_button.Click += new System.Windows.RoutedEventHandler(unban_button_handler);
            gods_stone_button.Click += new System.Windows.RoutedEventHandler(gods_stone_button_Click);
            chaos_stone_button.Click += new System.Windows.RoutedEventHandler(chaos_stone_button_Click);
            dar_op_button.Click += new System.Windows.RoutedEventHandler(dar_op_button_Click);
            good_gods_stone_button.Click += new System.Windows.RoutedEventHandler(good_gods_stone_button_Click);
            jaguar_button.Click += new System.Windows.RoutedEventHandler(jaguar_button_Click);
            mana_stealer_button.Click += new System.Windows.RoutedEventHandler(mana_stealer_button_Click);
            osheinic_button.Click += new System.Windows.RoutedEventHandler(osheinic_button_Click);
            rune_of_protection_1_button.Click += new System.Windows.RoutedEventHandler(rune_of_protection_1_button_Click);
            super_stone_button.Click += new System.Windows.RoutedEventHandler(super_stone_button_Click);
            exp_elexir_button.Click += new System.Windows.RoutedEventHandler(exp_elexir_button_Click);
            iris_button.Click += new System.Windows.RoutedEventHandler(iris_button_Click);
            jivoder_box_button.Click += new System.Windows.RoutedEventHandler(jivoder_box_button_Click);
            medic_box_button.Click += new System.Windows.RoutedEventHandler(medic_box_button_Click);
            platinum_iris_button.Click += new System.Windows.RoutedEventHandler(platinum_iris_button_Click);
            health_stealer_button.Click += new System.Windows.RoutedEventHandler(health_stealer_button_Click);
            adrenaline_button.Click += new System.Windows.RoutedEventHandler(adrenaline_button_Click);

            
        }
       
    private static BitmapImage Set_image(string job)
        {
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            switch (job)
            {
                case "ti":
                    myBitmapImage.UriSource = new Uri("/Resources/titan.jpg", UriKind.Relative);
                    break;
                case "kni":
                    myBitmapImage.UriSource = new Uri("/Resources/rytsar.jpg", UriKind.Relative);
                    break;
                case "zri":
                    myBitmapImage.UriSource = new Uri("/Resources/zritsa.jpg", UriKind.Relative);
                    break;
                case "ma":
                    myBitmapImage.UriSource = new Uri("/Resources/mag.jpg", UriKind.Relative);
                    break;
                case "ass":
                    myBitmapImage.UriSource = new Uri("/Resources/razba.jpg", UriKind.Relative);
                    break;
                case "sorc":
                    myBitmapImage.UriSource = new Uri("/Resources/kold.jpg", UriKind.Relative);
                    break;
                case "mist":
                    myBitmapImage.UriSource = new Uri("/Resources/mist.jpg", UriKind.Relative);
                    break;
                case "band":
                    myBitmapImage.UriSource = new Uri("/Resources/band.jpg", UriKind.Relative);
                    break;
                case "med":
                    myBitmapImage.UriSource = new Uri("/Resources/medium.jpg", UriKind.Relative);
                    break;
                default :
                    myBitmapImage.UriSource = new Uri("/Resources/none.jpg", UriKind.Relative);
                    break;
            }
                    myBitmapImage.DecodePixelWidth = 89;
                    myBitmapImage.EndInit();
                    return myBitmapImage;
            
        }
   

        #region Хендлеры тут
        //private void HelpButtonClicked(object sender, EventArgs e) { System.Windows.Forms.MessageBox.Show("Нажатие кнопки Да - забанит/разбанит аккаунт. \nНажатие кнопки Нет - забанит/разбанит персонажа. \nНажатие кнопки Отмена - отменяет операцию","",MessageBoxButtons.OK); }
    private void adrenaline_button_Click(object sender, EventArgs e)
    {
        dupe_list_updater(2138);

    }
        private void gods_stone_button_Click(object sender, EventArgs e)
        {
            dupe_list_updater(85);

        }
        private void chaos_stone_button_Click(object sender, EventArgs e)
        {
            dupe_list_updater(2844);

        }
        private void dar_op_button_Click(object sender, EventArgs e)
        {
            dupe_list_updater(882);

        }
        private void good_gods_stone_button_Click(object sender, EventArgs e)
        {
            dupe_list_updater(974);

        }
        private void jaguar_button_Click(object sender, EventArgs e)
        {
            dupe_list_updater(5118);

        }
        private void mana_stealer_button_Click(object sender, EventArgs e)
        {
            dupe_list_updater(2356);

        }
        private void osheinic_button_Click(object sender, EventArgs e)
        {
            dupe_list_updater(4969);

        }
        private void rune_of_protection_1_button_Click(object sender, EventArgs e)
        {
            dupe_list_updater(2394);

        }
        private void super_stone_button_Click(object sender, EventArgs e)
        {
            dupe_list_updater(6643);

        }
        private void exp_elexir_button_Click(object sender, EventArgs e)
        {
            dupe_list_updater(1287);

        }
        private void iris_button_Click(object sender, EventArgs e)
        {
            dupe_list_updater(1288);

        }
        private void jivoder_box_button_Click(object sender, EventArgs e)
        {
            dupe_list_updater(4915);

        }
        private void medic_box_button_Click(object sender, EventArgs e)
        {
            dupe_list_updater(4916);

        }
        private void platinum_iris_button_Click(object sender, EventArgs e)
        {
            dupe_list_updater(1416);

        }
        private void health_stealer_button_Click(object sender, EventArgs e)
        {
            dupe_list_updater(2357);

        }
        public void connectEventHandler(object sender, EventArgs e)
        {

            if (checkconnection())
            {
                MysqlConn = "Database=" + Properties.Settings.Default.DBSetting.ToString().Trim() + ";Data Source=" + Properties.Settings.Default.IpSetting.ToString().Trim() + ";User Id=" + Properties.Settings.Default.LoginSetting.ToString().Trim() + ";Password=" + Properties.Settings.Default.PassSetting.ToString().Trim() + "; Connection Lifetime = 200";
                LoginBox.IsEnabled = true;
                nicknamebox.IsEnabled = true;
                listBox1.IsEnabled = true;
                accinfo.IsEnabled = true;
                activatebutton.IsEnabled = true;
                button_findout.IsEnabled = true;
                idtextbox.IsEnabled = true;
                nicktextbox.IsEnabled = true;
                cashtextbox.IsEnabled = true;
                emailtextbox.IsEnabled = true;
                acc_id_textb.IsEnabled = true;
                goldbox.IsEnabled = true;
                IPtextBox.IsEnabled = true;
                zonebox.IsEnabled = true;
                chanelbox.IsEnabled = true;
                str_box.IsEnabled = true;
                con_box.IsEnabled = true;
                int_box.IsEnabled = true;
                dex_box.IsEnabled = true;
                root_level.IsEnabled = true;
                carm_box.IsEnabled = true;
                kill_box.IsEnabled = true;
                acc_id_textb.IsEnabled = true;
                refreshbutton.IsEnabled = true;
                button_ban.IsEnabled = true;
                unban_button.IsEnabled = true;
                lvltextbox.IsEnabled = true;

                dataGridView1.IsEnabled = true;
                chaos_stone_button.IsEnabled = true;
                dar_op_button.IsEnabled = true;
                good_gods_stone_button.IsEnabled = true;
                jaguar_button.IsEnabled = true;
                mana_stealer_button.IsEnabled = true;
                osheinic_button.IsEnabled = true;
                rune_of_protection_1_button.IsEnabled = true;
                super_stone_button.IsEnabled = true;
                exp_elexir_button.IsEnabled = true;
                gods_stone_button.IsEnabled = true;
                iris_button.IsEnabled = true;
                jivoder_box_button.IsEnabled = true;
                medic_box_button.IsEnabled = true;
                platinum_iris_button.IsEnabled = true;
                health_stealer_button.IsEnabled = true;
                adrenaline_button.IsEnabled = true;
                tabControl1.SelectedIndex = 1;
                
            }
            else
            {
                tabControl1.SelectedIndex = 0;
                lvltextbox.IsEnabled = false;
                LoginBox.IsEnabled = false;
                nicknamebox.IsEnabled = false;
                listBox1.IsEnabled = false;
                accinfo.IsEnabled = false;
                activatebutton.IsEnabled = false;
                button_findout.IsEnabled = false;
                idtextbox.IsEnabled = false;
                nicktextbox.IsEnabled = false;
                cashtextbox.IsEnabled = false;
                emailtextbox.IsEnabled = false;
                acc_id_textb.IsEnabled = false;
                goldbox.IsEnabled = false;
                IPtextBox.IsEnabled = false;
                zonebox.IsEnabled = false;
                chanelbox.IsEnabled = false;
                str_box.IsEnabled = false;
                con_box.IsEnabled = false;
                int_box.IsEnabled = false;
                dex_box.IsEnabled = false;
                root_level.IsEnabled = false;
                carm_box.IsEnabled = false;
                kill_box.IsEnabled = false;
                refreshbutton.IsEnabled = false;
                button_ban.IsEnabled = false;
                unban_button.IsEnabled = false;
                dataGridView1.IsEnabled = false;
                chaos_stone_button.IsEnabled = false;
                dar_op_button.IsEnabled = false;
                good_gods_stone_button.IsEnabled = false;
                jaguar_button.IsEnabled = false;
                mana_stealer_button.IsEnabled = false;
                osheinic_button.IsEnabled = false;
                rune_of_protection_1_button.IsEnabled = false;
                super_stone_button.IsEnabled = false;
                exp_elexir_button.IsEnabled = false;
                gods_stone_button.IsEnabled = false;
                iris_button.IsEnabled = false;
                jivoder_box_button.IsEnabled = false;
                medic_box_button.IsEnabled = false;
                platinum_iris_button.IsEnabled = false;
                health_stealer_button.IsEnabled = false;
                adrenaline_button.IsEnabled = false;
                System.Windows.Forms.MessageBox.Show("Подключение прошло неудачно, попробуйте еще раз", "Ошибка!", MessageBoxButtons.OK);
            }
        }
        private void help_event_handler(object sender, EventArgs e) { }
        private void unban_button_handler(object sender, EventArgs e)
        {
            check = 3;
            login = LoginBox.Text;
            if (listBox1.SelectedIndex == -1) { Mysql_Character_findout(login, "-1", check, MysqlConn); }
            else { string lbox = listBox1.SelectedItem.ToString(); Mysql_Character_findout(login, lbox, check, MysqlConn); }
        }
        private void OnApplicationExit(object sender, EventArgs e)
        {
            System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + "/mysql_data.dll");
        }
        public void ActivateButtonHandler(object sender, EventArgs e)
        {
            ActMysqlQuery();
        }

        public void findoutButtonClickEventHandler(object sender, EventArgs e)
        {
            check = 1;
            login = LoginBox.Text;
            listBox1.Items.Clear();
           Mysql_Character_findout(login, "-1", check, MysqlConn);



        }

        public void banButtonClickEventHandler(object sender, EventArgs e)
        {
            check = 2;
            login = LoginBox.Text;
            if (listBox1.SelectedIndex == -1) {Mysql_Character_findout(login, "-1", check, MysqlConn); }
            else { string lbox = listBox1.SelectedItem.ToString(); Mysql_Character_findout(login, lbox, check, MysqlConn); }
        }
        public void accinfoButtonHandler(object sender, EventArgs e)
        {
           // groupBox1.Visibility = true;
           getcharinfo();

        }
        public void refreshButtonHandler(object sender, EventArgs e)
        {
            updatecharinfo();
        }
        #endregion
        #region connection_check
        public bool checkconnection()
        {

            string ipstr = iptextb.Text.Trim().ToString();
            string logstr = logintextb.Text.Trim().ToString();
            string passstr = passwdtextb.Text.Trim().ToString();
            string dbstr = dbtextb.Text.Trim().ToString();
            string dbauthstr = authtextb.Text.Trim().ToString();
            Properties.Settings.Default.IpSetting = ipstr;
            Properties.Settings.Default.LoginSetting = logstr;
            Properties.Settings.Default.PassSetting = passstr;
            Properties.Settings.Default.DBSetting = dbstr;
            Properties.Settings.Default.DBAuthSetting = dbauthstr;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
            MysqlConn = "Database=" + Properties.Settings.Default.DBSetting.ToString().Trim() + ";Data Source=" + Properties.Settings.Default.IpSetting.ToString().Trim() + ";User Id=" + Properties.Settings.Default.LoginSetting.ToString().Trim() + ";Password=" + Properties.Settings.Default.PassSetting.ToString().Trim() + "; Connection Lifetime = 200";
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = MysqlConn;
            MySqlCommand command = connection.CreateCommand();
            command.Connection.Close();
            if (!(ipstr == "" | ipstr == null) && !(logstr == "" | logstr == null) && !(dbstr == "" | dbstr == null) && !(dbauthstr == "" | dbauthstr == null))
            {
                try
                {
                    command.Connection.Open();
                    if (command.Connection.Ping()) { System.Windows.Forms.MessageBox.Show("Подключение успешно", "Success!", MessageBoxButtons.OK); }
                    else { System.Windows.Forms.MessageBox.Show("Ping не удался", "Failed", MessageBoxButtons.OK); }

                }

                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                    command.Connection.Close();
                    command.Connection.Dispose();
                    return false;
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Неудачное подключение", "Failed", MessageBoxButtons.OK); command.Connection.Close();
                return false;
            }


            return true;
        }
        #endregion
        
        #region update char
        public void updatecharinfo()
        {


            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = MysqlConn;
            MySqlCommand command = connection.CreateCommand();
            if (cashtextbox.Text == "" | cashtextbox.Text == null) { cashtextbox.Text = "0"; }
            if (emailtextbox.Text == "" | emailtextbox.Text == null) { emailtextbox.Text = "отсутствует"; }
            if (nicktextbox.Text == "" | nicktextbox.Text == null) { nicktextbox.Text = "0"; }
            if (lvltextbox.Text == "" | lvltextbox.Text == null) { lvltextbox.Text = "1"; }
            if (LoginBox.Text == "" | LoginBox.Text == null) { System.Windows.Forms.MessageBox.Show(" Логин не может равняться нулю! \n Лучше его вообще не трогать \n (Должен быть уникальным)", "Ошибка!", MessageBoxButtons.OK); return; }
            if (root_level.Text == "" | root_level.Text == null) { root_level.Text = "0"; }
            cashtextbox.Text = MysqlString(cashtextbox.Text);
            emailtextbox.Text = MysqlString(emailtextbox.Text);
            idtextbox.Text = MysqlString(idtextbox.Text);
            nicktextbox.Text = MysqlString(nicktextbox.Text);
            lvltextbox.Text = MysqlString(lvltextbox.Text);
            LoginBox.Text = MysqlString(LoginBox.Text);
            root_level.Text = MysqlString(root_level.Text);
            idtextbox.Text = MysqlString(idtextbox.Text);
            acc_id_textb.Text = MysqlString(acc_id_textb.Text);
            str_box.Text = MysqlString(str_box.Text);
            dex_box.Text = MysqlString(dex_box.Text);
            int_box.Text = MysqlString(int_box.Text);
            con_box.Text = MysqlString(con_box.Text);
            carm_box.Text = MysqlString(carm_box.Text);
            kill_box.Text = MysqlString(kill_box.Text);
            IPtextBox.Text = MysqlString(IPtextBox.Text);
            try
            {
                connection.Open();
                command.CommandText = "UPDATE `bg_user` SET `user_id` = " + quote + LoginBox.Text + quote + ", `email` = " + quote + emailtextbox.Text + quote + " ,`cash` = " + quote + cashtextbox.Text + quote + ", `ip` = " + quote + IPtextBox.Text + quote + " WHERE `user_code` = " + idtextbox.Text + ";";
                command.Connection.ChangeDatabase(authtextb.Text);
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return;
            }
            command.CommandText = "UPDATE `t_characters` SET `a_index` = " + quote + idtextbox.Text + quote + ", `a_user_index` = " + quote + acc_id_textb.Text + quote + ", `a_nick` =" + quote + nicktextbox.Text + quote + ",`a_admin` = " + quote + root_level.Text + quote + ", `a_level` = " + quote + lvltextbox.Text + quote + ", `a_str` = " + quote + str_box.Text + quote + ",`a_dex` = " + quote + dex_box.Text + quote + ", `a_int` = " + quote + int_box.Text + quote + ", `a_con` = " + quote + con_box.Text + quote + ", `a_pkpenalty` = " + quote + carm_box.Text + quote + ", `a_pkcount` = " + quote + kill_box.Text + quote + " WHERE `a_nick` = " + quote + listBox1.SelectedItem.ToString() + quote + ";";
            try
            {
                connection.Open();
                command.Connection.ChangeDatabase(dbtextb.Text);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return;
            }
            System.Windows.Forms.MessageBox.Show("Успешно обновлено!", "Успех!", MessageBoxButtons.OK);
            connection.Close();


        }
        #endregion
       
        #region get char info
        public void getcharinfo()
        {
            
            if (!(listBox1.SelectedItem == null))
            {
                string chars = listBox1.SelectedItem.ToString();
                chars = MysqlString(chars);

                MySqlConnection connection = new MySqlConnection();
                connection.ConnectionString = MysqlConn;
                MySqlCommand command = connection.CreateCommand();
                MySqlDataReader Reader;
                try
                {
                    connection.Open();
                    command.CommandText = "SELECT a_index,a_user_index,a_nick,a_enable,a_admin,a_job,a_level,a_str,a_dex,a_int,a_con,a_pkpenalty,a_pkcount,a_job2 from t_characters WHERE a_nick = " + quote + chars + quote + ";";

                    try
                    {

                        Reader = command.ExecuteReader();


                        if (Reader.Read())
                        {


                            if (Convert.IsDBNull(Reader["a_user_index"]) == false | Convert.IsDBNull(Reader["a_index"]) == false)
                            {
                                string bunss = Reader["a_enable"].ToString();
                                if (bunss == "0") { char_bun.Visibility = System.Windows.Visibility.Visible; }
                                else { char_bun.Visibility = System.Windows.Visibility.Hidden; }
                                acc_id_textb.Text = Reader["a_user_index"].ToString();
                                idtextbox.Text = Reader["a_index"].ToString();
                                nicktextbox.Text = Reader["a_nick"].ToString();
                                lvltextbox.Text = Reader["a_level"].ToString();
                                root_level.Text = Reader["a_admin"].ToString();
                                str_box.Text = Reader["a_str"].ToString();
                                dex_box.Text = Reader["a_dex"].ToString();
                                int_box.Text = Reader["a_int"].ToString();
                                con_box.Text = Reader["a_con"].ToString();
                                carm_box.Text = Reader["a_pkpenalty"].ToString();
                                kill_box.Text = Reader["a_pkcount"].ToString();
                                string jobs = Reader["a_job"].ToString();
                                string jobs2 = Reader["a_job2"].ToString();
                                jobs = jobs + "," + jobs2;
                                switch (jobs)
                                {
                                    case "0,0":
                                       // job.Foreground = System.Windows.Media.Brushes.Red;
                                        job.Content = "";
                                        job_image.Source = Set_image("ti");
                                       // job_image.UpdateLayout();

           // job_image.Source = LoadImage(job_titan);
                                        break;
                                    case "0,1":
                                        job.Foreground = System.Windows.Media.Brushes.Red;
                                        job.Content = "Горец";
                                        job_image.Source = Set_image("ti");
                                        
                                        break;
                                    case "0,2":
                                        job.Foreground = System.Windows.Media.Brushes.Red;
                                        job.Content = "МС";
                                        job_image.Source = Set_image("ti");
                                        break;
                                    case "1,0":
                                     //   job.Foreground = System.Windows.Media.Brushes.Blue;
                                        job.Content = "";
                                        job_image.Source = Set_image("kni");
                                        break;
                                    case "1,1":
                                        job.Foreground = System.Windows.Media.Brushes.Blue;
                                        job.Content = "Роял";
                                        job_image.Source = Set_image("kni");
                                        break;
                                    case "1,2":
                                        job.Foreground = System.Windows.Media.Brushes.Blue;
                                        job.Content = "Храм";
                                        job_image.Source = Set_image("kni");
                                        break;
                                    case "2,0":
                                      //  job.Foreground = System.Windows.Media.Brushes.Green;
                                        job.Content = "";
                                        job_image.Source = Set_image("zri");
                                        break;
                                    case "2,1":
                                        job.Foreground = System.Windows.Media.Brushes.Green;
                                        job.Content = "Лучница";
                                        job_image.Source = Set_image("zri");
                                        break;
                                    case "2,2":
                                        job.Foreground = System.Windows.Media.Brushes.Green;
                                        job.Content = "Целительница";
                                        job_image.Source = Set_image("zri");
                                        break;
                                    case "3,0":
                                      //  job.Foreground = System.Windows.Media.Brushes.DarkViolet;
                                        job.Content = "";
                                        job_image.Source = Set_image("ma");
                                        break;
                                    case "3,1":
                                        job.Foreground = System.Windows.Media.Brushes.DarkViolet;
                                        job.Content = "Волшебница";
                                        job_image.Source = Set_image("ma");
                                        break;
                                    case "3,2":
                                        job.Foreground = System.Windows.Media.Brushes.DarkViolet;
                                        job.Content = "Ведьма";
                                        job_image.Source = Set_image("ma");
                                        break;
                                    case "4,0":
                                       // job.Foreground = System.Windows.Media.Brushes.Yellow;
                                        job.Content = "";
                                        job_image.Source = Set_image("ass");
                                        break;
                                    case "4,1":
                                        job.Foreground = System.Windows.Media.Brushes.Yellow;
                                        job.Content = "Убийца";
                                        job_image.Source = Set_image("ass");
                                        break;
                                    case "4,2":
                                        job.Foreground = System.Windows.Media.Brushes.Yellow;
                                        job.Content = "Арбалетчица";
                                        job_image.Source = Set_image("ass");
                                        break;
                                    case "5,0":
                                      //  job.Foreground = System.Windows.Media.Brushes.Violet;
                                        job.Content = "";
                                        job_image.Source = Set_image("sorc");
                                        break;
                                    case "5,1":
                                        job.Foreground = System.Windows.Media.Brushes.Violet;
                                        job.Content = "Материалист";
                                        job_image.Source = Set_image("sorc");
                                        break;
                                    case "5,2":
                                        job.Foreground = System.Windows.Media.Brushes.Violet;
                                        job.Content = "Стихийник";
                                        job_image.Source = Set_image("sorc");
                                        break;
                                    case "6,0":
                                       // job.Foreground = System.Windows.Media.Brushes.Purple;
                                        job.Content = "";
                                        job_image.Source = Set_image("mist");
                                        break;
                                    case "7,0":
                                      //  job.Foreground = System.Windows.Media.Brushes.DarkBlue;
                                        job.Content = "";
                                        job_image.Source = Set_image("band");
                                        break;
                                    case "7,1":
                                        job.Foreground = System.Windows.Media.Brushes.DarkBlue;
                                        job.Content = "Убийца";
                                        job_image.Source = Set_image("band");
                                        break;
                                    case "7,2":
                                        job.Foreground = System.Windows.Media.Brushes.DarkBlue;
                                        job.Content = "Арбалетчица";
                                        job_image.Source = Set_image("band");
                                        break;
                                    case "8,0":
                                      //  job.Foreground = System.Windows.Media.Brushes.Pink;
                                        job.Content = "";
                                        job_image.Source = Set_image("med");
                                        break;
                                    case "8,1":
                                        job.Foreground = System.Windows.Media.Brushes.Pink;
                                        job.Content = "Волшебница";
                                        job_image.Source = Set_image("med");
                                        break;
                                    case "8,2":
                                        job.Foreground = System.Windows.Media.Brushes.Pink;
                                        job.Content = "Ведьма";
                                        job_image.Source = Set_image("med");
                                        break;
                                    default:
                                        job.Foreground = System.Windows.Media.Brushes.Black;
                                        job.Content = "Неизвестный класс";
                                        job_image.Source = Set_image("none");
                                        break;
                                }


                            }
                        }
                        Reader.Close();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.ToString());
                    }
                    chars = MysqlString(chars);
                    command.CommandText = "SELECT cash,email,ip from " + authtextb.Text + ".bg_user WHERE user_code IN(SELECT a_user_index FROM t_characters WHERE a_nick = " + quote + chars + quote + ");";
                    try
                    {
                        Reader = command.ExecuteReader();
                        if (Reader.Read())
                        {
                            cashtextbox.Text = Reader["cash"].ToString();
                            emailtextbox.Text = Reader["email"].ToString();
                            IPtextBox.Text = Reader["ip"].ToString();

                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.ToString());
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());

                }
                connection.Close();
                try
                {
                    connection.Open();
                    string o_account_login = LoginBox.Text;
                    o_account_login = MysqlString(o_account_login);
                    command.CommandText = "SELECT a_subnum,a_zone_num from " + dbtextb.Text + ".t_users WHERE a_idname = " + quote + o_account_login + quote + ";";
                    try
                    {
                        Reader = command.ExecuteReader();
                        if (Reader.Read())
                        {
                            zonebox.Text = Reader["a_zone_num"].ToString();
                            chanelbox.Text = Reader["a_subnum"].ToString();

                        }
                        if (zonebox.Text == "-1") { statuslabel.Content = "Оффлайн"; statuslabel.Foreground = System.Windows.Media.Brushes.Red; }
                        else { statuslabel.Content = "Онлайн"; statuslabel.Foreground = System.Windows.Media.Brushes.Green; }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.ToString());
                    }

                    if (totalgoldlabel.Content.ToString() == "" | totalgoldlabel.Content == null | totalgoldlabel.Content.ToString() == "000000000000")
                    {


                        ActMysqlQuery();

                    }
                    long o_second_gold_check = 0;
                    o_second_gold_check += ActMysqlQuery();
                    MysqlActWorker2 ActMysqlWorker2 = new MysqlActWorker2();
                    idtextbox.Text = MysqlString(idtextbox.Text);
                    ActMysqlWorker2.Run_gold_check2(idtextbox.Text);
                    long o_gold_query_result2 = ActMysqlWorker2.gold_result2;
                    goldbox.Text = String.Format("{0:0,0}", o_gold_query_result2);
                    unchecked
                    {
                        double o_math_gold_result = System.Convert.ToDouble(o_gold_query_result2) / System.Convert.ToDouble(o_second_gold_check);
                        o_math_gold_result = Math.Round(o_math_gold_result, 7);
                        label10.Content = o_math_gold_result + " %";
                    }





                    connection.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                }

            }
            else System.Windows.Forms.MessageBox.Show("Ничего не выбрано!", "Ошибка!", MessageBoxButtons.OK);
        }
        #endregion
        #region character_findout
        public void Mysql_Character_findout(string login, string lbox, int check, string MysqlConn)
        {

            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = MysqlConn;
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            try
            {
                connection.Open();
                if (check == 1)
                {
                    if (LoginBox.Text.ToString() == "")
                    {


                        nicknamebox.Text = MysqlString(nicknamebox.Text.ToString());
                        command.CommandText = "SELECT `user_id` FROM " + authtextb.Text + ".bg_user WHERE `user_code` IN (SELECT `a_user_index` FROM `t_characters` WHERE `a_nick` = " + quote + nicknamebox.Text + quote + ");";

                        Reader = command.ExecuteReader();

                        while (Reader.Read())
                        {
                            object result_for_reader = Reader["user_id"];
                            if (!result_for_reader.Equals(System.DBNull.Value))
                            {
                                string thisrow = "";
                                for (int i = 0; i < Reader.FieldCount; i++)
                                    thisrow += Reader.GetString(i).ToString();
                                LoginBox.Text = thisrow;


                            }

                        }


                        if (!(LoginBox.Text == "") & !(LoginBox.Text == null))
                        {
                            Mysql_Character_findout(LoginBox.Text, "-1", 1, MysqlConn);
                        }
                        else System.Windows.Forms.MessageBox.Show("Ничего не найдено", "Ошибка!", MessageBoxButtons.OK);

                    }

                    else
                    {
                        authtextb.Text = MysqlString(authtextb.Text);
                        login = MysqlString(login);
                        command.CommandText = "SELECT `a_nick` FROM t_characters WHERE `a_user_index` IN (SELECT `user_code` FROM " + authtextb.Text + ".bg_user WHERE `user_id` = " + quote + login + quote + ");";
                        Reader = command.ExecuteReader();
                        while (Reader.Read())
                        {
                            string thisrow = "";
                            for (int i = 0; i < Reader.FieldCount; i++)
                                thisrow += Reader.GetValue(i).ToString();
                            listBox1.Items.Add(thisrow);
                        }


                        Reader.Close();
                        command.CommandText = "SELECT `a_enable` FROM " + authtextb.Text + ".t_users WHERE `a_idname` = " + quote + LoginBox.Text + quote + ";";

                        Reader = command.ExecuteReader();
                        if (Reader.Read())
                        {
                            string buns = Reader["a_enable"].ToString();
                            if (buns == "0" | buns == "2")
                            { acc_bun.Visibility = System.Windows.Visibility.Visible; }
                            else
                            { acc_bun.Visibility = System.Windows.Visibility.Hidden; }
                        }
                    }
                }

                else if (check == 2)
                {
                    //string dilem = System.Windows.Forms.MessageBox.Show("Забанить аккаунт?", "Дилема", MessageBoxButtons.YesNoCancel, icon:default(MessageBoxIcon),defaultButton:default(MessageBoxDefaultButton),options:0,displayHelpButton:true).ToString();

                    string dilem = System.Windows.Forms.MessageBox.Show("Забанить аккаунт?", "Дилема", MessageBoxButtons.YesNoCancel).ToString();
                    if (dilem == "Yes")
                    {
                        command.Connection.ChangeDatabase(authtextb.Text);
                        login = MysqlString(login);
                        command.CommandText = "UPDATE `t_users` SET `a_enable` = 0 WHERE `a_idname` = " + quote + login + quote + ";";
                        command.ExecuteNonQuery();
                        System.Windows.Forms.MessageBox.Show("BUNNED! " + login, "Результат", MessageBoxButtons.OK);
                        acc_bun.Visibility = System.Windows.Visibility.Visible;
                    }

                    else if (dilem == "No")
                    {
                        lbox = MysqlString(lbox);
                        command.CommandText = "UPDATE `t_characters` SET `a_enable` = 0 WHERE `a_nick` = " + quote + lbox + quote + ";";
                        command.ExecuteNonQuery();
                        System.Windows.Forms.MessageBox.Show("BUNNED! " + lbox, "Результат", MessageBoxButtons.OK);
                        char_bun.Visibility = System.Windows.Visibility.Hidden;
                    }


                }
                else if (check == 3)
                {

                    string dilem = System.Windows.Forms.MessageBox.Show("Разбанить аккаунт?", "Дилема", MessageBoxButtons.YesNoCancel).ToString();
                    if (dilem == "Yes")
                    {
                        command.Connection.ChangeDatabase(authtextb.Text);
                        login = MysqlString(login);
                        command.CommandText = "UPDATE `t_users` SET `a_enable` = 1 WHERE `a_idname` = " + quote + login + quote + ";";
                        command.ExecuteNonQuery();
                        System.Windows.Forms.MessageBox.Show("Разбанен " + login, "Результат", MessageBoxButtons.OK);
                        acc_bun.Visibility = System.Windows.Visibility.Hidden;
                    }

                    else if (dilem == "No")
                    {
                        lbox = MysqlString(lbox);
                        command.CommandText = "UPDATE `t_characters` SET `a_enable` = 1 WHERE `a_nick` = " + quote + lbox + quote + ";";
                        command.ExecuteNonQuery();
                        System.Windows.Forms.MessageBox.Show("Разбанен " + lbox, "Результат", MessageBoxButtons.OK);
                        char_bun.Visibility = System.Windows.Visibility.Hidden;
                    }


                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());

            }
            connection.Close();

        }

        #endregion
        #region gold check
        public long ActMysqlQuery()
        {

            MysqlActWorker ActMysqlWorker = new MysqlActWorker();
            ActMysqlWorker.Run_gold_check();
            long fas = ActMysqlWorker.gold_result;
            totalgoldlabel.Content = String.Format("{0:0,0}", fas);
            return fas;

        }

        class MysqlActWorker
        {
            public long gold_result;
            public object gold_result_Locker = new object();
            public void Run_gold_check()
            {
                for (int count_i = 0; count_i < 10; count_i++)
                {

                    object inven_param_obj = count_i;
                    Thread thread = new Thread(ThreadFunction);
                    thread.Name = "Thread" + count_i.ToString();
                    thread.Start(inven_param_obj);
                    thread.Join();

                }


            }
            public void ThreadFunction(Object inven_param_obj)
            {
                string MysqlConn = "Database=" + Properties.Settings.Default.DBSetting.ToString().Trim() + ";Data Source=" + Properties.Settings.Default.IpSetting.ToString().Trim() + ";User Id=" + Properties.Settings.Default.LoginSetting.ToString().Trim() + ";Password=" + Properties.Settings.Default.PassSetting.ToString().Trim() + "; Connection Lifetime = 200";
                int inven_i = (int)inven_param_obj;
                MySqlConnection connection = new MySqlConnection();
                connection.ConnectionString = MysqlConn;
                MySqlCommand command = connection.CreateCommand();
                MySqlDataReader Reader;
                try
                {
                    connection.Open();
                    object result_for_reader;
                    long result_for_value = 0;

                    for (int count_i = 0; count_i < 5; count_i++)
                    {

                        command.CommandText = "(SELECT sum(a_count" + count_i + ") AS count from t_inven0" + inven_i + " Where a_item_idx" + count_i + " = 19);";

                        Reader = command.ExecuteReader();
                        while (Reader.Read())
                        {


                            result_for_reader = Reader["count"];
                            if (!result_for_reader.Equals(System.DBNull.Value))
                            {

                                result_for_value = Convert.ToInt64((decimal)result_for_reader);

                                lock (gold_result_Locker)
                                {
                                    gold_result = gold_result + result_for_value;
                                }
                            }
                        }

                        Reader.Close();
                    }


                }

                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());

                }
                connection.Close();

            }




        }
        #endregion
        #region gold_percent_checker
        public class MysqlActWorker2
        {
            public long gold_result2;
            public object gold_result_Locker2 = new object();
            public void Run_gold_check2(string id)
            {
                for (int count_i2 = 0; count_i2 < 10; count_i2++)
                {

                    // object inven_param_obj2 = count_i2;
                    string[] led = { count_i2.ToString(), id };
                    object inven_param_object2 = led;

                    Thread thread2 = new Thread(ThreadFunction2, 0);
                    thread2.Name = "Thread2" + count_i2.ToString();
                    thread2.Start(inven_param_object2);
                    thread2.Join();
                }
            }
            public void ThreadFunction2(Object inven_param_obj2)
            {
                string MysqlConn = "Database=" + Properties.Settings.Default.DBSetting.ToString().Trim() + ";Data Source=" + Properties.Settings.Default.IpSetting.ToString().Trim() + ";User Id=" + Properties.Settings.Default.LoginSetting.ToString().Trim() + ";Password=" + Properties.Settings.Default.PassSetting.ToString().Trim() + "; Connection Lifetime = 200";
                // int inven_i2 = (int)inven_param_obj2;
                string[] led = (string[])inven_param_obj2;
                string inven_i2 = led[0];
                string id = led[1];
                MySqlConnection connection = new MySqlConnection();
                connection.ConnectionString = MysqlConn;
                MySqlCommand command = connection.CreateCommand();
                MySqlDataReader Reader2;
                try
                {
                    connection.Open();
                    object result_for_reader2;
                    long result_for_value2 = 0;

                    for (int count_i2 = 0; count_i2 < 5; count_i2++)
                    {

                        command.CommandText = "(SELECT sum(a_count" + count_i2 + ") AS count from t_inven0" + inven_i2 + " Where a_char_idx = " + quote + id + quote + " AND a_item_idx" + count_i2 + " = 19);";

                        Reader2 = command.ExecuteReader();
                        while (Reader2.Read())
                        {
                            result_for_reader2 = Reader2["count"];
                            if (result_for_reader2.Equals(System.DBNull.Value)) { }
                            else
                            {

                                result_for_value2 = Convert.ToInt64((decimal)result_for_reader2);

                                lock (gold_result_Locker2)
                                {
                                    gold_result2 = gold_result2 + result_for_value2;
                                }
                            }
                        }

                        Reader2.Close();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());

                }

                connection.Close();

            }
        }
        #endregion
        public string MysqlString(string value)
        {
            char[] trim = { ' ' };
            value = value.Trim(trimChars: trim);
            char[] test = value.ToCharArray();
            int i = test.GetLength(0);
            value = "";
            for (int i_n = 0; i_n < i; i_n++)
            {

                //test[i_n] = System.Convert.ToChar(test[i_n].ToString().Replace('/', '?'));
                //MessageBox.Show(test[i_n].ToString(), "testeda", MessageBoxButtons.OK);.Replace('/', '\u0001')
                string temp = test[i_n].ToString();
                if (temp.Contains("/") | temp.Contains(" ") | temp.Contains("!") | temp.Contains("&") | temp.Contains("\\") | temp.Contains("'") | temp.Contains("\"") | temp.Contains("?") | temp.Contains("^") | temp.Contains("*") | temp.Contains("%") | temp.Contains("#") | temp.Contains("$") | temp.Contains("(") | temp.Contains(")") | temp.Contains("-") | temp.Contains("+") | temp.Contains("=") | temp.Contains("№") | temp.Contains(":") | temp.Contains("<") | temp.Contains(">") | temp.Contains("|") | temp.Contains("||") | temp.Contains(";") | temp.Contains("{") | temp.Contains("}") | temp.Contains("[") | temp.Contains("]") | temp.Contains("€") | temp.Contains("`") | temp.Contains("~")) { value += test[i_n].ToString().Remove(0); }
                else value += test[i_n];
            }
            //value.Replace("/[^a-zA-Z0-9@.]/", "");

            //  value.Normalize();
            //MessageBox.Show(value, "", MessageBoxButtons.OK);
            value = value.Trim(trimChars: trim);
            //value = System.Convert.ToString(test);
            return value;
        }
        #region dupe_list
        public void dupe_list_updater(int sender)
        {
            System.Windows.Forms.MessageBox.Show(sender.ToString(),"Id предмета",MessageBoxButtons.OK);
            try
            {
                dupe dupe = new dupe();
               dupe.Run_dupe_check(sender);
               List<KeyValuePair<int, int>> q = new List<KeyValuePair<int, int>>();
              // Dictionary<int, int> qw = new Dictionary<int, int>();
              // qw = dupe.q;
               q = dupe.q;
              // Array array;
              
               //array = q.ToArray();
               // dataGridView1.AutoGenerateColumns = true;
               dataGridView1.ItemsSource = q;
              
               /*int n = array.Length;
               int[,] mas = new int[n, n];
               for (int i = 0; i < n; i++)
               {
                 for (int j = 0; j < n; j++)
                 { mas[i, j] = (array as int[,])[i,j]; }


               }
                */
              // dupe_list_box.Sort(Key, ListSortDirection.Descending);
              // dataGridView1.TopLeftHeaderCell.Value = "qqqq";
              // bindingSource1.Sort = "";
              dataGridView1.Columns[0].Header = "Id персонажа";
              dataGridView1.Columns[1].Header = "Количество";
              //dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
               //dataGridView1.Columns.CopyTo(array,27);
               

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        #endregion
      /*  public void DataUpdater(object idx, object count, int flag)
        {
            if (flag ==2 ){
                this.dataGridView1.Columns.Add("a_char_idx", "User ID");
                this.dataGridView1.Columns["a_char_idx"].Width = 20;
                this.dataGridView1.Columns.Add("a_count", "Count");
                this.dataGridView1.Columns["a_count"].Width = 50;
            dataGridView1.Rows.Add(idx.ToString(), idx.ToString());
            dataGridView1.Rows.Add(2);
                       
            }
            else 
            {
                dataGridView1.Rows.Add(idx.ToString(), idx.ToString());
                dupe_list_box.Rows.Add(idx.ToString(), idx.ToString());
                dupe_list_box.Rows.Add("1", "2");
            }
        } */
        class dupe:Form1
        {
            public List<KeyValuePair<int, int>> q = new List<KeyValuePair<int, int>>();
           // public Dictionary<int, int> q = new Dictionary<int, int>();
            public object dupe_result_Locker = new object();
           
            DataSet dataset = new DataSet();
            public void Run_dupe_check(int sender)
            {
                for (int count_i = 0; count_i < 10; count_i++)
                {
                    int[] temp = {count_i,sender };
                    object inven_param_obj = temp;
                    Thread thread = new Thread(ThreadFunction);
                    
                    thread.Name = "Thread" + count_i.ToString();
                    thread.Start(inven_param_obj);


                    thread.Join();

                }

        }
            public void ThreadFunction(Object inven_param_obj)
            {
                string MysqlConn = "Database=" + Properties.Settings.Default.DBSetting.ToString().Trim() + ";Data Source=" + Properties.Settings.Default.IpSetting.ToString().Trim() + ";User Id=" + Properties.Settings.Default.LoginSetting.ToString().Trim() + ";Password=" + Properties.Settings.Default.PassSetting.ToString().Trim() + "; Connection Lifetime = 200";
                int[] temp = (int[])inven_param_obj;
                int inven_i = temp[0];
                int item_idx = temp[1];
                MySqlConnection connection = new MySqlConnection();
                connection.ConnectionString = MysqlConn;
                MySqlCommand command = connection.CreateCommand();
                MySqlDataReader Reader;
                
                //MySqlDataAdapter adapter = new MySqlDataAdapter();

                try
                {
                    //adapter.TableMappings.Add("Table", "Suppliers");
                    connection.Open();

                    int count = 0;
                    int count_v = 0;
                    object result;
                    for (int count_i = 0; count_i < 5; count_i++)
                    {
                    command.CommandText = "Select a_char_idx, sum(a_count"+count_i+") as count from t_inven0"+inven_i+" Where a_item_idx"+count_i+" = "+item_idx+" AND a_count"+count_i+" <> 0 group by a_char_idx;";

                             //command.CommandText = "SELECT a_char_idx,sum(a_count" + count_i + ") from t_inven0" + inven_i + " Where a_item_idx" + count_i + " = 882 AND a_char_idx IN(SELECT a_char_idx from t_inven0" + inven_i + " WHERE a_item_idx" + count_i + " = 882);";
                        //command.CommandText = "(SELECT a_char_idx,a_count" + count_i + " from t_inven0" + inven_i + " Where a_item_idx" + count_i + " = 882);";
                        //MySqlDataAdapter DA = new MySqlDataAdapter(command);
                        //command.CommandType = CommandType.Text;
                        //adapter.SelectCommand = command;
                        //DataSet dataSet = new DataSet("Suppliers");
                        //adapter.Fill(dataSet);
                        Reader = command.ExecuteReader();
                        //if (Reader.HasRows)
                        //{
                            //DataTable dt = new DataTable();
                            //dt.Load(Reader);
                            //dupe_list_box.DataSource = dt;
                           
                        //}
                        while (Reader.Read())
                        {

                            //for (int i = 0; i < Reader; i++)
                            //{
                            //    if (!(Reader[1] == System.DBNull.Value) & !(Reader[0] == System.DBNull.Value))
                            //    {
                            
                             lock (dupe_result_Locker)
                            {
                                string[] res = { Reader[0].ToString(), Reader[1].ToString() };
                                result = res;
                                count = Convert.ToInt32(Reader[0]);
                                count_v = Convert.ToInt32(Reader[1]);
                                var w = new KeyValuePair<int,int>(count,count_v);

                                q.Add(w);
                                //q.Add(w);
                            }
                                    //foreach (int temps in Reader[i])
                                    //{
                                    //}
                                //}

                        }
                            // dupe_list_box.Rows.Add(Reader[0].ToString(), Reader[1].ToString());
                            //DataUpdater(Reader[0], Reader[1], count_i);
                           // result = Reader[0];
                            //count = Reader[1];
                            
                            //if (!count.Equals(System.DBNull.Value))
                           //{

                                //count_v =(int)count;
                                //lock (dupe_result_Locker)
                                //{
                                    //dupe_result = dupe_result + count_v;
                                    //result = (object)count;
                                    //result = (object)Reader["a_char_idx"];
                                   // dupe_list_box.Items.Add(result);
                        //dupe_list_box.Rows.Add(result, count);
                                //}
                           // }
                        //}

                        Reader.Close();
                    }


                }

                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());

                }
                connection.Close();

                
            }




        }

      

       

  

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            getcharinfo();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          //  richTextBox1.Text = HWIDGrabber.GetUHI;
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Внимание! Эта программа - бесплатная. Если по какой-либо причине вы ее приобрели, то разработчик настоятельно рекомендует вам потребовать деньги назад."); 
        
        }




        /*    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
            {
            
                System.IO.File.Delete(Application.StartupPath + "\\mysql.data.dll");
            
            }*/


        /* void OnApplicationExit()
         {
             System.IO.File.Delete(Application.StartupPath + "\\mysql_data.dll");
             System.Windows.Forms.MessageBox.Show("OnAppExit", "", MessageBoxButtons.OK);
         }*/
    }
}













