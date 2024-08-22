using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ジャンプゲーム
public class Main : MonoBehaviour
{
    // シーン定数
    public const int
        S_PLAY     = 1, // プレイ
        S_GAMEOVER = 2; // ゲームオーバー

    // 設定定数
    private const int BLOCK_NUM = 8; //ブロック数

    // 参照
    public GameObject player; // プレイヤー
    private Rigidbody rb; //Rigidbody;
    public GameObject blockBase; // ブロックベース
    public GameObject[] blocks; // ブロック群
    public TriggerListener frontTrigger; // 前方トリガー
    public TriggerListener bottomTrigger; // 下端トリガー
    public Text scoreText; // スコアテキスト
    public GameObject gameoverText; // ゲームオーバーテキスト

    // システム
    public int scene = S_PLAY; // シーン
    private float jumpPow; // ジャンプ力
    private int score; // スコア
    private  bool keyPress; // キー押下
    public  bool nextKeyPress; // 次キー押下

    // スタート
    void Start()
    {
        this.rb = player.GetComponent<Rigidbody>();
        SetScene(S_PLAY);
    }

    // シーンの指定
    public void SetScene(int scene)
    {
        this.scene = scene;
        if (this.scene == S_PLAY)
        {
            this.player.transform.position = new Vector3(-2f, -0.75f, 0f);
            this.blockBase.transform.position = new Vector3(0f, -1f, 0f);
            for (int i = 0; i < BLOCK_NUM-1; i++)
            {
                SetBlockY(i, 0);
            }
            this.jumpPow = 0f;
            this.score = 0;
            this.keyPress = false;
            this.gameoverText.SetActive(false);
        }
        else
        {
            this.gameoverText.SetActive(true);
        }
    }

    // 定期時間毎に更新
    void FixedUpdate()
    {
        if (this.scene == S_PLAY)
        {
            // スコアの更新
            this.score++;
            this.scoreText.text = this.score.ToString("00000000");

            // ブロック群の更新
            UpdateBlocks();

            // ジャンプ
            if (!this.bottomTrigger.IsHit())
            {
                this.player.transform.Translate(0f, this.jumpPow, 0f);
                this.jumpPow -= 0.005f;
            }

            // ゲームオーバー遷移
            if (frontTrigger.IsHit() || this.player.transform.position.y < -10f)
            {
                this.SetScene(S_GAMEOVER);
            }
        }
    }

    // 1フレーム毎に更新
    void Update()
    {
        // キー入力
        this.nextKeyPress = Input.GetKey(KeyCode.Space);

        if (this.scene == S_PLAY)
        {
            // ジャンプ
            if (this.bottomTrigger.IsHit() && !this.keyPress && this.nextKeyPress)
            {
                this.player.transform.Translate(0f, 0.1f, 0f);
                this.jumpPow = 0.2f;
            }
            else if (this.keyPress && !this.nextKeyPress)
            {
                if (this.jumpPow > 0f) this.jumpPow = this.jumpPow / 2f;
            }
        }
        else
        {
            // プレイ遷移
            if (this.keyPress && !this.nextKeyPress)
            {
                this.SetScene(S_PLAY);
            }
        }

        this.keyPress = this.nextKeyPress;
    }

    // ブロック群の更新
    private void UpdateBlocks()
    {
        this.blockBase.transform.Translate(-0.05f, 0f, 0f);
        if (this.blockBase.transform.position.x <= -1f)
        {
            // ブロック群のシフト
            for (int i = 0; i < BLOCK_NUM-1; i++)
            {
                SetBlockY(i, this.blocks[i+1].transform.localPosition.y);
            }
            this.blockBase.transform.position = new Vector3(0f, -1f, 0f);

            // 新規ブロックの高さの指定
            int idx = Random.Range(0, 12);
            float y = this.blocks[BLOCK_NUM-2].transform.localPosition.y;
            float y2 = this.blocks[BLOCK_NUM-3].transform.localPosition.y;
            // 1つ前が穴のときは2つ前の高さ
            if (y <= -10f)
            {
                SetBlockY(BLOCK_NUM-1, y2);
            }
            // 1/12の確率で穴
            else if (idx == 0)
            {
                SetBlockY(BLOCK_NUM-1, -10f);
            }
            // 1/12の確率で1段上
            else if (idx == 1)
            {
                SetBlockY(BLOCK_NUM-1, (y < 3f) ? y + 1f : 3f);
            }
            // 1/12の確率で1段下
            else if (idx == 2)
            {
                SetBlockY(BLOCK_NUM-1, (y > 0f) ? y - 1f : 0f);
            }
        }
    }

    // ブロックの高さの指定
    private void SetBlockY(int index, float y)
    {
        Vector3 pos = this.blocks[index].transform.localPosition;
        pos.y = y;
        this.blocks[index].transform.localPosition = pos;
    }
}
