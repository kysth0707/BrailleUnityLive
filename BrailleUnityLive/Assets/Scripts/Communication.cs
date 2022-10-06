using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System;
using System.IO;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Communication : MonoBehaviour
{
    public BallGenerator BallCreater;
    List<GameObject> Balls = new List<GameObject>();

    TcpClient Client;

    string ServerIP = "127.0.0.1";
    int Port = 8000;

    byte[] ReceivedBuffer;
    StreamReader Reader;
    bool SocketReady = false;
    NetworkStream Stream;

    string BallState = "";

    void Start()
    {
        Balls = BallCreater.CreateBalls();
        CheckReceive();
    }

    // Update is called once per frame
    void Update()
    {
        if (SocketReady)
        {
            if (Stream.DataAvailable)
            {
                ReceivedBuffer = new byte[1500];

                Stream.Read(ReceivedBuffer, 0, ReceivedBuffer.Length);
                string Message = Encoding.UTF8.GetString(ReceivedBuffer, 0, ReceivedBuffer.Length);

                //Debug.Log(Message);
                BallState = Message;
                SetBalls();
            }
        }
    }

    void SetBalls()
    {
        for (int x = 0; x < 32; x++)
        {
            for (int z = 0; z < 32; z++)
            {
                int num = (31 - x) + z * 32;
                if (BallState[x + z * 32] == '1')
                {
                    Balls[num].transform.position = new Vector3(Balls[num].transform.position.x, 0, Balls[num].transform.position.z);
                }
                else
                {
                    Balls[num].transform.position = new Vector3(Balls[num].transform.position.x, -0.18f, Balls[num].transform.position.z);
                }
            }
        }
    }

    void CheckReceive()
    {
        if (SocketReady)
            return;

        try
        {
            Client = new TcpClient(ServerIP, Port);

            if (Client.Connected)
            {
                Stream = Client.GetStream();

                Debug.Log("접속 성공");
                SocketReady = true;
            }
        }
        catch
        {
            
        }
    }

    void OnApplicationQuit()
    {
        CloseSocket();
    }

    void CloseSocket()
    {
        if (!SocketReady)
            return;

        Reader.Close();
        Client.Close();
        SocketReady = false;
    }
}
