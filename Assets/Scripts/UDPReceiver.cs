using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPReceiver : MonoBehaviour
{
    public int port = 5005;

    UdpClient client;
    Thread receiveThread;
    string latestMessage;

    void Start()
    {
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
        Debug.Log("✅ UDP Receiver started");
    }

    void ReceiveData()
    {
        client = new UdpClient(port);

        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);
                latestMessage = Encoding.UTF8.GetString(data);
            }
            catch { }
        }
    }

    public string GetLatestMessage()
    {
        return latestMessage;
    }

    void OnApplicationQuit()
    {
        receiveThread?.Abort();
        client?.Close();
    }
}
