using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.SideChannels;
using System.Text;
using System;

public class StringLogSideChannel : SideChannel // (1)
{
    // コンストラクタ
    public StringLogSideChannel()
    {
        // ChannelIDの指定 (2)
        ChannelId = new Guid("621f0a70-4f87-11ea-a6bf-784f4387d1f7");
    }

    // Python側からのデータの受信 (3)
    protected override void OnMessageReceived(IncomingMessage msg)
    {
        Debug.Log(msg.ReadString());
    }

    // Python側へのデータの送信 (4)
    public void SendString(string sendString)
    {
        using (var msgOut = new OutgoingMessage())
        {
            msgOut.WriteString(sendString);
            QueueMessageToSend(msgOut);
        }
    }
}