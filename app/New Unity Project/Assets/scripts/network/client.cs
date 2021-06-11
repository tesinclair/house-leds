using System.Collections;
using System.Collections.Generic;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class client : MonoBehaviour
{
    private TcpClient socketConnection;
    private Thread clientRecieveThread;

    void Start(){
        ConnectToTcpServer();
    }

    private void ConnectToTcpServer(){
        try {
            clientRecieveThread = new Thread(new ThreadStart(ListenForData));
            clientRecieveThread.IsBackground = true;
            clientRecieveThread.Start();
        }
        catch (Exception e){
            Debug.Log("On Client connect exception " + e);
        }
    }

    private void ListenForData(){
        try{
            socketConnection = new TcpClient("localhost", 3333);
            
            SendMessage("false\n");
            
            Byte[] bytes = new Byte[1024];

            while (true){
                using (NetworkStream stream = socketConnection.GetStream()){
                    int length;
                    while ((length  = stream.Read(bytes, 0, bytes.Length)) != 0){
                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);

                        string serverMessage = Encoding.ASCII.GetString(incommingData);
                        Debug.Log("server message: " + serverMessage);
                    }
                }
            }
        }
        catch (SocketException socketException){
            Debug.Log("Socket exception: " + socketException);
        }
    }

    public void SendMessage(string message){
        if (socketConnection == null){
            return;
        }
        try{
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite){
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(message);

                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("SentMessage");
            }
        }
        catch (SocketException socketException){
            Debug.Log("Socket exception: " + socketException);
        }
    }
}
