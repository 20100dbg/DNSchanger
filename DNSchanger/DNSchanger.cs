using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;


namespace DNSchanger
{
    public partial class DNSchanger : Form
    {
        List<ServiceDns> listDns = new List<ServiceDns>();
        List<Interface> listInterface = new List<Interface>();

        public DNSchanger()
        {
            InitializeComponent();

            LinkLabel.Link l = new LinkLabel.Link();
            l.LinkData = "https://wikileaks.org/wiki/Alternative_DNS";
            link.Links.Add(l);
            


            //Liste les interfaces
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                listInterface.Add(new Interface { Id = nic.Id, Name = nic.Name });

            cb_interfaces.Items.AddRange(listInterface.ToArray());


            //Charge et liste les services DNS
            StreamReader sr = new StreamReader("dns.csv");
            String line;
            Boolean headers = false;

            while ((line = sr.ReadLine()) != null)
            {
                if (!headers)
                {
                    headers = true;
                    line = sr.ReadLine();
                }

                String[] tab = line.Split(';');

                listDns.Add(new ServiceDns { Name = tab[0],
                                            PrimaryIP = IPAddress.Parse(tab[1]),
                                            SecondaryIP = IPAddress.Parse(tab[2]),
                                            Description = tab[3] });
            }
            
            cb_predefinedDNS.Items.AddRange(listDns.ToArray());
        }


        private Boolean checkIp(String strIP)
        {
            IPAddress ip;
            return IPAddress.TryParse(strIP, out ip);
        }

        //Set to auto
        private void ApplyDns(String description)
        {
            String cmd = "netsh interface ip set dns \"{0}\" dhcp";
            cmd = String.Format(cmd, description);
            String result = startCmd(cmd);
            result = result.Trim();

            if (String.IsNullOrWhiteSpace(result)) tb_log.AppendText("DNS has been set to auto");
            else tb_log.AppendText(result);
            tb_log.AppendText(Environment.NewLine);
        }

        private void ApplyDns(String description, String ip, int index)
        {
            String cmd = "netsh interface ipv4 add dnsserver \"{0}\" address={1} index={2}";
            cmd = String.Format(cmd, description, ip, index);
            String result = startCmd(cmd);
            result = result.Trim();

            if (String.IsNullOrWhiteSpace(result)) tb_log.AppendText("DNS has been updated");
            else tb_log.AppendText(result);
            tb_log.AppendText(Environment.NewLine);
        }

        private void ResetDns(String description)
        {
            String cmd = "netsh interface ipv4 delete dnsservers \"{0}\" all";
            cmd = String.Format(cmd, description);
            String result = startCmd(cmd);
            result = result.Trim();

            if (String.IsNullOrWhiteSpace(result))
                tb_log.AppendText("DNS has been reset" + Environment.NewLine);
        }

        private String startCmd(String cmd)
        {
            Process p = new Process();
            p.StartInfo.Verb = "runas"; //démarre en admin
            p.StartInfo.CreateNoWindow = true; //masque la console
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.StandardOutputEncoding = System.Text.Encoding.GetEncoding(850); //encodage par défaut de la console
            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = "/c " + cmd; // /c obligatoire pour que le process se finisse
            p.Start();
            String output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output;
        }


        private void cb_predefinedDNS_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = cb_predefinedDNS.SelectedIndex;
            if (idx > -1)
            {
                tb_newPri.Text = listDns[idx].PrimaryIP.ToString();
                tb_newSec.Text = listDns[idx].SecondaryIP.ToString();
                l_description.Text = listDns[idx].Description;
            }
        }

        private void cb_interfaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = cb_interfaces.SelectedIndex;
            if (idx > -1)
            {
                String id = listInterface[idx].Id;
                tb_curPri.Text = tb_curSec.Text = "";

                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.Id == id)
                    {
                        IPAddressCollection ipList = nic.GetIPProperties().DnsAddresses;

                        if (ipList.Count > 0) tb_curPri.Text = ipList[0].ToString();
                        if (ipList.Count > 1) tb_curSec.Text = ipList[1].ToString();
                    }
                }


            }
        }

        private void b_save_Click(object sender, EventArgs e)
        {
            int idx = cb_interfaces.SelectedIndex;
            if (idx > -1)
            {
                String description = listInterface[idx].Name;

                if (!String.IsNullOrWhiteSpace(tb_newPri.Text) && checkIp(tb_newPri.Text))
                {
                    ResetDns(description);

                    ApplyDns(description, tb_newPri.Text, 1);

                    if (!String.IsNullOrWhiteSpace(tb_newSec.Text) && checkIp(tb_newSec.Text))
                    {
                        ApplyDns(description, tb_newSec.Text, 2);
                    }
                }
                else
                    ApplyDns(description);

            }
        }

        private void b_saveAuto_Click(object sender, EventArgs e)
        {
            int idx = cb_interfaces.SelectedIndex;
            if (idx > -1)
            {
                String description = listInterface[idx].Name;
                ResetDns(description);
                ApplyDns(description);
            }
                
        }
    }

    class Interface
    {
        public String Id { get; set; }
        public String Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    class ServiceDns
    {
        public String Name { get; set; }
        public IPAddress PrimaryIP { get; set; }
        public IPAddress SecondaryIP { get; set; }
        public String Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
