using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerListener : MonoBehaviour
{
    private Dictionary<string, string> hit = new Dictionary<string, string>();

    // 衝突中かどうか
    public bool IsHit()
    {
        return this.hit.Count > 0;
    }

    // 他のオブジェクトとのすり抜け開始
    void OnTriggerEnter(Collider other)
    {
        this.hit.Add(other.gameObject.name, "");
    }

    // 他のオブジェクトとのすり抜け開始
    void OnTriggerExit(Collider other) {
        this.hit.Remove(other.gameObject.name);
    }
}
