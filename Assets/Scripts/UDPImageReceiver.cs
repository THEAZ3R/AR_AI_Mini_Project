using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPImageReceiver : MonoBehaviour
{
    public int port = 5006;

    UdpClient client;
    Thread receiveThread;

    Texture2D texture;

    byte[] latestFrame;     // shared buffer
    bool newFrameAvailable = false;

    void Start()
    {
        texture = new Texture2D(2, 2);
        GetComponent<Renderer>().material.mainTexture = texture;

        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    void ReceiveData()
    {
        client = new UdpClient(port);

        while (true)
        {
            IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = client.Receive(ref anyIP);

            // Only store data — DO NOT touch Unity objects here
            latestFrame = data;
            newFrameAvailable = true;
        }
    }

    void Update()
    {
        if (newFrameAvailable && latestFrame != null)
        {
            texture.LoadImage(latestFrame);
            newFrameAvailable = false;
        }
    }

    void OnApplicationQuit()
    {
        receiveThread?.Abort();
        client?.Close();
    }
}
