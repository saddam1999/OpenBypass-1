using System;
using System.Diagnostics;
using System.IO;
using Renci.SshNet;
using System.Windows.Forms;

namespace OpenBypass
{
    public partial class MDM : Form
    {
        public MDM()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string host = "127.0.0.1";
            string pass = "alpine";
            string user = "root";
            KilliProxy();
            StartiProxy();
            SshClient sshclient = new SshClient(host, user, pass);
            ScpClient scpClient = new ScpClient("127.0.0.1", "root", "alpine");
            sshclient.Connect();
            scpClient.Connect();
            sshclient.CreateCommand("rm -rf /var/containers/Shared/SystemGroup/systemgroup.com.apple.configurationprofiles/Library/ConfigurationProfiles/*").Execute();
            sshclient.CreateCommand("killall backboardd && sleep 7").Execute();
            scpClient.Upload(new FileInfo(Application.StartupPath + "/Required/mdm"), "/var/containers/Shared/SystemGroup/systemgroup.com.apple.configurationprofiles/Library/ConfigurationProfiles/CloudConfigurationDetails.plist");
            sshclient.CreateCommand("chflags uchg /var/containers/Shared/SystemGroup/systemgroup.com.apple.configurationprofiles/Library/ConfigurationProfiles/CloudConfigurationDetails.plist").Execute();
            sshclient.CreateCommand("killall backboardd").Execute();
            sshclient.Disconnect();
            scpClient.Disconnect();
            MessageBox.Show("MDM is now bypassed & device is respringing! Make sure NOT to erase or update your device cause it will relock if done!");
        }
        public void KilliProxy()
        {
            foreach (var process in Process.GetProcessesByName("iproxy"))
            {
                process.Kill();
            }
        }
        public static void StartiProxy()
        {
            try
            {

                new Process
                {
                    StartInfo =
                        {
                            FileName = Environment.CurrentDirectory + "/Required/iproxy.exe",
                            Arguments = "22 44",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            CreateNoWindow = true
                        }
                }.Start();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("iProxy not found! Is your reference folder deleted?");
            }
        }

    }
}
