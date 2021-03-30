using System;
using Renci.SshNet;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace OpenBypass
{
    public partial class susicivus : Form
    {

        public susicivus()
        {
            InitializeComponent();
        } 
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("If your device is iOS 12, select Yes. \nIf not, select No", "OpenBypass", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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
                sshclient.CreateCommand("mount -o rw,union,update /").Execute();
                scpClient.Upload(new FileInfo(Application.StartupPath + "\\Required\\erase.dll"), "/bin/erase");
                sshclient.CreateCommand("cd /bin/erase").Execute();
                sshclient.CreateCommand("chmod 755  /bin/erase").Execute();
                MessageBox.Show("Device erased!");
                sshclient.CreateCommand("erase -command 8a5fbd968b4f16624ecb5713744028fefabe8a20de10dfc58c4ede37212ac3da").Execute();
                sshclient.Disconnect();
                scpClient.Disconnect();
                KilliProxy();

            }
            else
            {
                string host = "127.0.0.1";
                string pass = "alpine";
                string user = "root";
                KilliProxy();
                StartiProxy();
                SshClient sshclient = new SshClient(host, user, pass);
                ScpClient scpClient = new ScpClient("127.0.0.1", "root", "alpine");
                sshclient.Connect();
                scpClient = new ScpClient("127.0.0.1", "root", "alpine");
                scpClient.Connect();
                sshclient.CreateCommand("mount -o rw,union,update /").Execute();
                scpClient.Upload(new FileInfo(Application.StartupPath + "\\Reference\\erase"), "/bin/erase");
                sshclient.CreateCommand("chmod -R 777 /bin").Execute();
                sshclient.CreateCommand("launchctl load -w /System/Library/LaunchDaemons/com.apple.mobile.obliteration.plist").Execute();
                scpClient.Disconnect();
                sshclient.Disconnect();
                MessageBox.Show("Device erased!");
                KilliProxy();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("If your device is iOS 12, select Yes. \nIf not, select No", "OpenBypass", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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
                scpClient.Upload(new FileInfo(Application.StartupPath + "/Reference/General.plist"), "/System/Library/PrivateFrameworks/PReferenceerencesUI.framework/General.plist");
                MessageBox.Show("OTA disabled!");
                sshclient.Disconnect();
                scpClient.Disconnect();
                KilliProxy();

            }
            else
            {
                string host = "127.0.0.1";
                string pass = "alpine";
                string user = "root";
                KilliProxy();
                StartiProxy();
                SshClient sshclient = new SshClient(host, user, pass);
                ScpClient scpClient = new ScpClient("127.0.0.1", "root", "alpine");
                sshclient.Connect();
                scpClient = new ScpClient("127.0.0.1", "root", "alpine");
                scpClient.Connect();
                scpClient.Upload(new FileInfo(Application.StartupPath + "/Reference/extermina"), "/System/Library/PrivateFrameworks/Settings/GeneralSettingsUI.framework/General.plist");
                scpClient.Disconnect();
                sshclient.Disconnect();
                MessageBox.Show("OTA disabled!");
                KilliProxy();
            }
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
