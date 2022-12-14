using System;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace LitePlacer {
    delegate void SerialRXDelegate(string msg);
    class SerialComm {
        const int ReadBufferSize = 10000;
        private string RxString = string.Empty;
        SerialPort Port = new SerialPort();
        public SerialRXDelegate serialDelegate = null;


        public bool IsOpen { get { return Port.IsOpen; } }


        public void Close() {
            try {
                if (Port.IsOpen) {
                    Port.DiscardInBuffer();
                    Port.DiscardOutBuffer();
                }
                // Known issue: Sometimes serial port hangs in app closing. Google says that 
                // the workaround is to close in another thread
                Thread t = new Thread(delegate() { Port.Close(); });
                t.Start();
            } catch { }
        }

        public bool Open(string Com) {
            Close();
            try {
                Port.PortName = Com;
                Port.BaudRate = 115200;
                Port.Parity = Parity.None;
                Port.StopBits = StopBits.One;
                Port.DataBits = 8;
                Port.Handshake = Handshake.RequestToSend;
                Port.DtrEnable = true;  // prevent hangs on some drivers
                Port.RtsEnable = true;
                Port.Open();
                if (Port.IsOpen) {
                    Port.DiscardOutBuffer();
                    Port.DiscardInBuffer();
                }
                Port.DataReceived += DataReceived;
                return Port.IsOpen;
            } catch {
                return false;
            }
        }


        public void Write(string TxText) {
            if (Port.IsOpen) {
                Port.Write(TxText + "\r\n");
                Global.Instance.DisplayText("> " + TxText, Color.Green);
            }
        }

        void DataReceived(object sender, SerialDataReceivedEventArgs e) {
            //Initialize a buffer to hold the received data 
            byte[] buffer = new byte[ReadBufferSize];
            string WorkingString;

            try {
                //There is no accurate method for checking how many bytes are read 
                //unless you check the return from the Read method 
                int bytesRead = Port.Read(buffer, 0, buffer.Length);

                //The received data is ASCII
                RxString += Encoding.ASCII.GetString(buffer, 0, bytesRead);
                //Process each line
                while (RxString.IndexOf("\n") > -1) {
                    //Even when RxString does contain terminator we cannot assume that it is the last character received 
                    WorkingString = RxString.Substring(0, RxString.IndexOf("\n") + 1);
                    //Remove the data and the terminator from tString 
                    RxString = RxString.Substring(RxString.IndexOf("\n") + 1);
                    if (serialDelegate != null) serialDelegate(WorkingString);
                }
            } catch (Exception ex) {
                Global.Instance.DisplayText("########## " + ex, Color.Red);
            }
        }

    }
}
