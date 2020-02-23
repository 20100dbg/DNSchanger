using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;


namespace DNSchanger
{
    public partial class DNSchanger : Form
    {
        List<ServiceDns> listDns = new List<ServiceDns>();
        List<Interface> listInterface = new List<Interface>();
        enum Method { Netsh1, Netsh2, Powershell };


        //netsh interface ip set dns name="Local Area Connection" static 208.67.222.222
        //netsh interface ip add dns name="Local Area Connection" 208.67.220.220 index=2
        //netsh interface ip set dnsservers name="Local Area Connection" source=dhcp
        //netsh interface ip set dns « Description » static %DNS%

        //Set-DnsClientServerAddress -InterfaceIndex 12 -ServerAddresses ("10.0.0.1","10.0.0.2")
        //Set-DnsClientServerAddress -InterfaceIndex 12 -ResetServerAddresses

        public DNSchanger()
        {
            InitializeComponent();

            InitInterface();
        }

        private void InitInterface()
        {
            LinkLabel.Link l = new LinkLabel.Link();
            l.LinkData = "https://wikileaks.org/wiki/Alternative_DNS";
            link.Links.Add(l);

            //Liste les interfaces

            /*
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                listInterface.Add(new Interface { Id = nic.Id, Name = nic.Name });
            */

            ListInterfaces();
            cb_interfaces.Items.AddRange(listInterface.ToArray());


            //Charge et liste les services DNS
            StreamReader sr = new StreamReader("dns.csv");
            sr.ReadLine(); //Ignore les headers

            String line;
            while ((line = sr.ReadLine()) != null)
            {
                String[] tab = line.Split(';');

                listDns.Add(new ServiceDns
                {
                    Name = tab[0],
                    PrimaryIP = IPAddress.Parse(tab[1]),
                    SecondaryIP = IPAddress.Parse(tab[2]),
                    Description = tab[3].Replace("- ", Environment.NewLine + "- ")
                });
            }

            cb_predefinedDNS.Items.AddRange(listDns.ToArray());

        }


        private void ListInterfaces()
        {
            PowerShell ps = PowerShell.Create();
            ps.AddCommand("Get-NetAdapter");
            var result = ps.Invoke();

            foreach (var pso in result)
            {
                listInterface.Add(new Interface { InterfaceIndex = Convert.ToUInt32(pso.Members["InterfaceIndex"].Value),
                                                    Name = pso.Members["Name"].Value.ToString(),
                                                    Id = pso.Members["InterfaceGuid"].Value.ToString(),
                });
            }

        }


        private void NshResetDns(String description)
        {
            String cmd = "netsh interface ipv4 delete dnsservers \"{0}\" all";
            cmd = String.Format(cmd, description);
            String result = startCmd(cmd);
            result = result.Trim();

            if (String.IsNullOrWhiteSpace(result))
                Log("DNS has been reset");
        }


        private void NshDefaultDns(String description)
        {
            String cmd = "netsh interface ip set dns \"{0}\" dhcp";
            cmd = String.Format(cmd, description);
            String result = startCmd(cmd);
            result = result.Trim();

            if (String.IsNullOrWhiteSpace(result)) Log("DNS has been set to auto");
            else Log(result);
        }


        private void NshApplyDns(String description, String ip, int index, Method m)
        {
            String cmd = "";

            if (m == Method.Netsh1)
                cmd = "netsh interface ipv4 add dnsserver \"{0}\" address={1} index={2}";
            else if (m == Method.Netsh2)
                cmd = "netsh interface ip set dns name=\"{0}\" {1} index={2}";

            cmd = String.Format(cmd, description, ip, index);
            String result = startCmd(cmd);
            result = result.Trim();

            if (String.IsNullOrWhiteSpace(result)) Log("DNS has been updated");
            else Log(result);
        }



        private void PsDefaultDns(int idxInterface)
        {
            PowerShell ps = PowerShell.Create();
            ps.AddCommand("Set-DnsClientServerAddress");
            ps.AddParameter("InterfaceIndex", idxInterface);
            ps.AddParameter("ResetServerAddresses");
            ps.Invoke();
        }

        private void PsApplyDns(UInt32 idxInterface, String[] ip, Boolean validate = true)
        {
            PowerShell ps = PowerShell.Create();
            ps.AddCommand("Set-DnsClientServerAddress");
            ps.AddParameter("InterfaceIndex", idxInterface);
            ps.AddParameter("ServerAddresses", ip);

            if (validate)
                ps.AddParameter("Validate");

            ps.Invoke();
        }


        private Boolean CheckAppliedDns(Interface iface, String ip)
        {
            Boolean flag = false;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.Id == iface.Id)
                {
                    IPAddressCollection ipList = nic.GetIPProperties().DnsAddresses;

                    for (int i = 0; i < ipList.Count; i++)
                        if (ip == ipList[i].ToString()) flag = true;
                }
            }

            return flag;
        }


        private void Log(String txt)
        {
            tb_log.AppendText(txt + Environment.NewLine);
        }

        private Boolean checkIp(String strIP)
        {
            IPAddress ip;
            return IPAddress.TryParse(strIP, out ip);
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
                l_description.Text = listDns[idx].Description.Trim(new char[] { '"' });
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
                        UnicastIPAddressInformationCollection ips = nic.GetIPProperties().UnicastAddresses;

                        for (int i = 0; i< ips.Count; i++)
                        {
                            if (ips[i].IPv4Mask != null)
                                l_ip.Text = ips[i].Address.ToString();
                        }
                        
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
                List<String> ips = new List<String>();
                
                if (!String.IsNullOrWhiteSpace(tb_newPri.Text) && checkIp(tb_newPri.Text))
                    ips.Add(tb_newPri.Text);

                if (!String.IsNullOrWhiteSpace(tb_newSec.Text) && checkIp(tb_newSec.Text))
                    ips.Add(tb_newSec.Text);

                if (ips.Count > 0)
                {
                    PsApplyDns(listInterface[idx].InterfaceIndex, ips.ToArray(), true);

                    if (CheckAppliedDns(listInterface[idx], ips[0]))
                        Log("Successfully set Primary DNS : " + ips[0]);
                    else
                        Log("Seems I couldn't set Primary DNS : " + ips[0]);

                    if (ips.Count > 1 && CheckAppliedDns(listInterface[idx], ips[1]))
                        Log("Successfully set Secondary DNS : " + ips[1]);
                    else
                        Log("Seems I couldn't set Secondary DNS : " + ips[1]);
                }
            }
        }

        private void b_saveAuto_Click(object sender, EventArgs e)
        {
            int idx = cb_interfaces.SelectedIndex;
            if (idx > -1)
            {
                PsDefaultDns(idx);
                Log("Set DNS to DHCP default");
            }
                
        }
    }

    class Interface
    {
        public UInt32 InterfaceIndex { get; set; }



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
