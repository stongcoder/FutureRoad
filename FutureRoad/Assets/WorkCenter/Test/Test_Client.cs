using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class Test_Client : MonoBehaviour
{
    public Button sendBtn;//发送
    public InputField inputfield;//信息
    Socket clientSocket;

    private static byte[] result = new byte[1024];
    void Start()
    {
        sendBtn.onClick.AddListener(SetMessage);

        //要连接的服务器IP地址  
        IPAddress ip = IPAddress.Parse("192.168.3.97");//本地IP地址
        //IPAddress ip = IPAddress.Any;//本地IP地址
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(new IPEndPoint(ip, 59999)); //配置服务器IP与端口 ，并且尝试连接
            Debug.Log("Connect Success"); //没有出错 打印连接成功
        }
        catch (Exception ex)
        {
            Debug.Log("Connect lose" + ex); //打印连接失败
            return;
        }

        int receiveLength = clientSocket.Receive(result);//接收服务器回复消息，成功则说明已经接通
        Debug.Log("开始" + receiveLength);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //开始发送数据
        try
        {
            if (isShow)
            {
                isShow = false;//发送后 变为false 直到按钮再次点击发送
                string sendMessage = inputfield.text;
                Debug.Log("发送信息" + sendMessage);
                clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));//传送信息
            }
        }
        catch (Exception ex)
        {
            Debug.Log("发送失败：" + ex);
            //clientSocket.Shutdown(SocketShutdown.Both); //出现问题关闭socket
        }
    }

    //单击按钮发送信息
    bool isShow = false; //用来判断是否发送信息的
    void SetMessage()
    {
        //输入框不为空 发送消息
        if (!inputfield.text.Equals(string.Empty))
            isShow = true;
    }
}
