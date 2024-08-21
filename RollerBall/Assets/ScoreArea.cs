using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スコアエリア
public class ScoreArea : MonoBehaviour
{
    public GameManager gameManager;
    public int agentId;

    // ボールがスコアエリアに進入した時に呼ばれる
    void OnTriggerEnter (Collider other)
    {
        gameManager.EndEpisode(agentId);
    }
}
