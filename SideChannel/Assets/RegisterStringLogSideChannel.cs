using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.SideChannels;

public class RegisterStringLogSideChannel : MonoBehaviour
{
    StringLogSideChannel stringChannel;

    // ゲームオブジェクトの生成時に呼ばれる (1)
    public void Awake()
    {
        // サイドチャネルの生成
        stringChannel = new StringLogSideChannel();

        // サイドチャネルの登録
        SideChannelManager.RegisterSideChannel(stringChannel);
    }

    // ゲームオブジェクトの破棄時に呼ばれる
    public void OnDestroy()
    {
        // サイドチャネルの登録を解除
        if (Academy.IsInitialized)
        {
            SideChannelManager.UnregisterSideChannel(stringChannel);
        }
    }

    // フレーム毎に呼ばれる
    public void Update()
    {
        // スペースキーを押した時 (3)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Pythonへのデータの送信
            stringChannel.SendString("Send Data from Unity");
        }
    }
}