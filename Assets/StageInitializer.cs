using UnityEngine;

public class StageInitializer : MonoBehaviour
{
    void Start()
    {
        // 1. プレイヤーをタグで見つける
        GameObject player = GameObject.FindWithTag("Player");
        // 2. 自分（プレハブ）の子にあるSpawnPointを見つける
        Transform spawnPoint = transform.Find("PlayerSpawnPoint");

        if (player != null && spawnPoint != null)
        {
            // 3. プレイヤーをその位置へワープさせる
            player.transform.position = spawnPoint.position;
            Debug.Log("プレイヤーを配置しました！");
        }
    }
}