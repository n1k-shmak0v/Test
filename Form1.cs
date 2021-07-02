using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;

namespace HDDInfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ManagementObjectSearcher mosDisks = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject moDisk in mosDisks.Get())
            {
                cmbHdd.Items.Add(moDisk["Model"].ToString());
            }
        }

        private void cmbHdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            ManagementObjectSearcher mosDisks = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE Model = '" + cmbHdd.SelectedItem + "'");
            foreach (ManagementObject moDisk in mosDisks.Get())
            {
                lblType.Text = "Type: " + moDisk["MediaType"].ToString();
                lblModel.Text = "Model: " + moDisk["Model"].ToString();
                lblSerial.Text = "Serial: " + moDisk["SerialNumber"].ToString();
                lblInterface.Text = "Interface: " + moDisk["InterfaceType"].ToString();
                lblCapacity.Text = "Capacity: " + moDisk["Size"].ToString() + " bytes (" + Math.Round(((((double)Convert.ToDouble(moDisk["Size"]) / 1024) / 1024) / 1024), 2) + " GB)";
                lblPartitions.Text = "Partitions: " + moDisk["Partitions"].ToString();
                lblSignature.Text = "Signature: " + moDisk["Signature"].ToString();
                lblFirmware.Text = "Firmware: " + moDisk["FirmwareRevision"].ToString();
                lblCylinders.Text = "Cylinders: " + moDisk["TotalCylinders"].ToString();
                lblSectors.Text = "Sectors: " + moDisk["TotalSectors"].ToString();
                lblHeads.Text = "Heads: " + moDisk["TotalHeads"].ToString();
                lblTracks.Text = "Tracks: " + moDisk["TotalTracks"].ToString();
                lblBytesPerSect.Text = "Bytes per Sector: " + moDisk["BytesPerSector"].ToString();
                lblSectorsPerTrack.Text = "Sectors per Track: " + moDisk["SectorsPerTrack"].ToString();
                lblTracksPerCyl.Text = "Tracks per Cylinder: " + moDisk["TracksPerCylinder"].ToString();
            }
        }
    }
}