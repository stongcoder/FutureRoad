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
    public Button sendBtn;//����
    public InputField inputfield;//��Ϣ
    Socket clientSocket;

    private static byte[] result = new byte[1024];
    void Start()
    {
        sendBtn.onClick.AddListener(SetMessage);

        //Ҫ���ӵķ�����IP��ַ  
        IPAddress ip = IPAddress.Parse("192.168.3.97");//����IP��ַ
        //IPAddress ip = IPAddress.Any;//����IP��ַ
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(new IPEndPoint(ip, 59999)); //���÷�����IP��˿� �����ҳ�������
            Debug.Log("Connect Success"); //û�г��� ��ӡ���ӳɹ�
        }
        catch (Exception ex)
        {
            Debug.Log("Connect lose" + ex); //��ӡ����ʧ��
            return;
        }

        int receiveLength = clientSocket.Receive(result);//���շ������ظ���Ϣ���ɹ���˵���Ѿ���ͨ
        Debug.Log("��ʼ" + receiveLength);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //��ʼ��������
        try
        {
            if (isShow)
            {
                isShow = false;//���ͺ� ��Ϊfalse ֱ����ť�ٴε������
                string sendMessage = inputfield.text;
                Debug.Log("������Ϣ" + sendMessage);
                clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));//������Ϣ
            }
        }
        catch (Exception ex)
        {
            Debug.Log("����ʧ�ܣ�" + ex);
            //clientSocket.Shutdown(SocketShutdown.Both); //��������ر�socket
        }
    }

    //������ť������Ϣ
    bool isShow = false; //�����ж��Ƿ�����Ϣ��
    void SetMessage()
    {
        //�����Ϊ�� ������Ϣ
        if (!inputfield.text.Equals(string.Empty))
            isShow = true;
    }
}
