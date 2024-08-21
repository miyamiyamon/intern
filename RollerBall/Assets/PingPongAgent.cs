using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;

// PingPongAgent
public class PingPongAgent : Agent
{
    public int agentId;
    public GameObject ball;
    Rigidbody ballRb;

    // ゲームオブジェクトの生成時に呼ばれる
    public override void Initialize()
    {
        this.ballRb = this.ball.GetComponent<Rigidbody>();
    }

    // 観察取得時に呼ばれる
    public override void CollectObservations(VectorSensor sensor)
    {
        float dir = (agentId == 0) ? 1.0f : -1.0f;
        sensor.AddObservation(this.ball.transform.localPosition.x * dir); // ボールのX座標
        sensor.AddObservation(this.ball.transform.localPosition.z * dir); // ボールのZ座標
        sensor.AddObservation(this.ballRb.velocity.x * dir); // ボールのX速度
        sensor.AddObservation(this.ballRb.velocity.z * dir); // ボールのZ速度
        sensor.AddObservation(this.transform.localPosition.x * dir); // パドルのX座標
    }

    // ボールとパドルの衝突開始時に呼ばれる
    void OnCollisionEnter(Collision collision)
    {
        // 報酬
        AddReward(0.1f);
    }

    // 行動実行時に呼ばれる
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // PingPongAgentの移動
        float dir = (agentId == 0) ? 1.0f : -1.0f;
        int action = actionBuffers.DiscreteActions[0];
        Vector3 pos = this.transform.localPosition;
        if (action == 1)
        {
            pos.x -= 0.2f * dir;
        }
        else if (action == 2)
        {
            pos.x += 0.2f * dir;
        }
        if (pos.x < -4.0f) pos.x = -4.0f;
        if (pos.x > 4.0f) pos.x = 4.0f;
        this.transform.localPosition = pos;
    }

    // ヒューリスティックモードの行動決定時に呼ばれる
    public override void Heuristic(in ActionBuffers actionBuffers)
    {
        var actionsOut = actionBuffers.DiscreteActions;
        actionsOut[0] = 0;
        if (Input.GetKey(KeyCode.LeftArrow)) actionsOut[0] = 1;
        if (Input.GetKey(KeyCode.RightArrow)) actionsOut[0] = 2;
    }
}
