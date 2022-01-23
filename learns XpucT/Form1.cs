using Svg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IP_info_for_XpucT
{
    public partial class IP_info : Form
    {
        public IP_info()
        {
            InitializeComponent();


        }
        int count = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            string line = "";
            string site = "http://free.ipwhois.io/xml" + $"/{textBox1.Text}.{textBox2.Text}.{textBox3.Text}.{textBox4.Text}";
            using (WebClient wc = new WebClient())
            {
                line = wc.DownloadString(site);
            }
                        
            label2.Text = "Continent\n" +
                "Continent_code\n" +
                "Country\n" +
                "Country_code\n" +
                "Country_flag\n" +
                "Country_capital\n" +
                "Country_phone\n" +
                "Country_neighbours\n" +
                "Region\n" +
                "City\n" +
                "Latitude\n" +
                "Longitude\n" +
                "Asn\n" +
                "Org\n" +
                "Isp\n" +
                "Timezone\n" +
                "Timezone_name\n" +
                "Timezone_dstOffset\n" +
                "Timezone_gmtOffset\n" +
                "Timezone_gmt\n" +
                "Currency\n" +
                "Currency_code\n" +
                "Currency_symbol\n" +
                "Currency_rates\n" +
                "Currency_plural\n";
            Match match = Regex.Match(line, "<continent>(.*?)</continent>" +
                "<continent_code>(.*?)</continent_code>" +
                "<country>(.*?)</country>" +
                "<country_code>(.*?)</country_code>" +
                "<country_flag>(.*?)</country_flag>" +
                "<country_capital>(.*?)</country_capital>" +
                "<country_phone>(.*?)</country_phone>" +
                "<country_neighbours>(.*?)</country_neighbours>" +
                "<region>(.*?)</region>" +
                "<city>(.*?)</city>" +
                "<latitude>(.*?)</latitude>" +
                "<longitude>(.*?)</longitude>" +
                "<asn>(.*?)</asn>" +
                "<org>(.*?)</org>" +
                "<isp>(.*?)</isp>" +
                "<timezone>(.*?)</timezone>" +
                "<timezone_name>(.*?)</timezone_name>" +
                "<timezone_dstOffset>(.*?)</timezone_dstOffset>" +
                "<timezone_gmtOffset>(.*?)</timezone_gmtOffset>" +
                "<timezone_gmt>(.*?)</timezone_gmt>" +
                "<currency>(.*?)</currency>" +
                "<currency_code>(.*?)</currency_code>" +
                "<currency_symbol>(.*?)</currency_symbol>" +
                "<currency_rates>(.*?)</currency_rates>" +
                "<currency_plural>(.*?)</currency_plural>");

            foreach (Group group in match.Groups)
            {
                if (group == match.Groups[0])
                {
                    continue;
                }
               
                label1.Text += group.Value + "\n";
            }
            string latitude;
            string longitude;
            match = Regex.Match(line, "<latitude>(.*?)</latitude>(.*?)<longitude>(.*?)</longitude>");
            latitude = match.Groups[1].Value;
            longitude = match.Groups[3].Value;
            //label1.Text = match.Groups[1].Value;
            match = Regex.Match(line, "<country_flag>(.*?)</country_flag>");
            
            string fileName = "";
            try
            {
                using(WebClient wc = new WebClient())
                {
                    count++;
                    //.svg
                    fileName = @"C:\Users\slavn\source\repos\learns XpucT\learns XpucT\Resources\" + $"image{count}.svg";
                    string webpath = match.Groups[1].Value;
                    wc.DownloadFile(webpath, fileName);
                }
                

                //FileStream fs = new FileStream(fileName, FileMode.Open);
                SvgDocument svgDoc = SvgDocument.Open(fileName);
                string newFileName = "";

                using (Bitmap image = new Bitmap(svgDoc.Draw()))
                {
                    newFileName = fileName.Remove(fileName.Length - 3) + "jpg";
                
                    image.Save(newFileName, ImageFormat.Jpeg);
                }
            
                pictureBox1.BackgroundImage = Image.FromFile(newFileName);
                pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
                //pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                webBrowser1.ScriptErrorsSuppressed = true;
                linkLabel1.Text = $"https://www.google.com/maps/search/?api=1&query={latitude},{longitude}";
                webBrowser1.Url = new Uri(linkLabel1.Text);
                //webBrowser1.Refresh();

            }
            catch (Exception)
            {
                
            }


        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox1.Text, "[^0-9]"))
            {
                MessageBox.Show("Только цифры!", Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length-1);
                textBox1.SelectionStart = textBox1.Text.Length;
            }
            try
            {
                if (textBox1.Text != null && Convert.ToInt32(textBox1.Text) > 255)
                {
                    MessageBox.Show("Не может быть больше 255!", Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    textBox1.Text = "255";
                    textBox1.SelectionStart = textBox1.Text.Length;
                }

            }
            catch (Exception)
            {

            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox2.Text, "[^0-9]"))
            {
                MessageBox.Show("Только цифры!", Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
                textBox2.SelectionStart = textBox2.Text.Length;
            }
            try
            {
                if (textBox2.Text != null && Convert.ToInt32(textBox2.Text) > 255)
                {
                    MessageBox.Show("Не может быть больше 255!", Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    textBox2.Text = "255";
                    textBox2.SelectionStart = textBox2.Text.Length;
                }

            }
            catch (Exception)
            {

            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox3.Text, "[^0-9]"))
            {
                MessageBox.Show("Только цифры!", Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox3.Text = textBox3.Text.Remove(textBox3.Text.Length - 1);
                textBox3.SelectionStart = textBox3.Text.Length;
            }
            try
            {
                if (textBox3.Text != null && Convert.ToInt32(textBox3.Text) > 255)
                {
                    MessageBox.Show("Не может быть больше 255!", Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    textBox3.Text = "255";
                    textBox3.SelectionStart = textBox3.Text.Length;
                }

            }
            catch (Exception)
            {

            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox1.Text, "[^0-9]"))
            {
                MessageBox.Show("Только цифры!", Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox4.Text = textBox4.Text.Remove(textBox4.Text.Length - 1);
                textBox4.SelectionStart = textBox4.Text.Length;
            }
            try
            {
                if(textBox4.Text != null && Convert.ToInt32(textBox4.Text) > 255)
                {
                    MessageBox.Show("Не может быть больше 255!", Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    textBox4.Text = "255";
                    textBox4.SelectionStart = textBox4.Text.Length;
                }

            }
            catch (Exception)
            {
                                
            }
        }

        private void IP_info_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                button1_Click(button1, null);
            }
     
        }

    }
}
