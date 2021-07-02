using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace HDD_InfoTest
{
    public partial class Form1 : Form
    {
        string path = "TestFile";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo masDrive in allDrives)
            {
                cmbHdd.Items.Add(masDrive.Name);
            }
            for (int i = 1; i < 1025; i++)
                comboBox1.Items.Add(i);
        }

        private void CmbHdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            for (int i = 0; i < allDrives.Length; i++)
            {
                if (allDrives[i].Name == cmbHdd.Text)
                {
                    lblType.Text = "Тип диска: " + allDrives[i].DriveType;
                    label2.Text = "Тип файловой системы: " + allDrives[i].DriveFormat;
                    if (allDrives[i].VolumeLabel == "")
                        label3.Text = "Метка диска: отсутствует";
                    else
                        label3.Text = "Метка диска: " + allDrives[i].VolumeLabel;
                    label4.Text = "Обьем диска: " + allDrives[i].TotalSize + " Байт";
                    label5.Text = "Свободный обьем диска: " + allDrives[i].TotalFreeSpace + " Байт";
                    label6.Text = "Занятый обьем диска: " + (allDrives[i].TotalSize - allDrives[i].TotalFreeSpace) + " Байт";
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "МегаБайт")
            {
                Stopwatch sh = new Stopwatch();
                string a;
                int aLen;
                if (radioButton2.Checked)
                { a = new string('a', Convert.ToInt32(radioButton2.Text)); aLen = Convert.ToInt32(radioButton2.Text); }
                else { a = new string('a', Convert.ToInt32(radioButton1.Text) * 1048576); aLen = Convert.ToInt32(radioButton1.Text) * 1048576; }
                int Mb = Convert.ToInt32(comboBox1.Text) * 1048576;
                for (int i = 0; i < 4; i++)
                {
                    sh.Reset();
                    sh.Start();
                    StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path), false, Encoding.ASCII);
                    for (int j = 1; (j * aLen) <= Mb; j++)
                        sw.Write(a);
                    sh.Stop();
                    sw.Close();
                    string WriteRes = string.Format("Тест {0}: = {1:f} Мбайт/сек", (i + 1), Mb / 1024 / 1024 / sh.Elapsed.TotalSeconds);
                    listBox1.Items.Add(WriteRes);
                }
                sh.Reset();
                for (int i = 0; i < 4; i++)
                {
                    sh.Reset();
                    sh.Start();
                    FileStream fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path), FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);
                    long Mbr = new FileInfo(path).Length;
                    for (int j = 0; j < Mbr; j += aLen)
                    {
                        char[] buffer = new char[aLen];
                        sr.ReadBlock(buffer, 0, aLen);
                    }
                    sr.Close();
                    fs.Close();
                    sh.Stop();
                    string ReadRes = string.Format("Тест {0}: = {1:f} Мбайт/сек", (i + 1), Mbr / 1024 / 1024 / sh.Elapsed.TotalSeconds);
                    listBox2.Items.Add(ReadRes);
                }
            }
        }
    }
}
