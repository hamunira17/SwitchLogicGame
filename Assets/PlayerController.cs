using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player"; // プレイヤーをタグで探す

    void Start()
    {
        // 1. シーン内のプレイヤーを探す
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        // 2. このオブジェクト（SpawnPoint）の場所を取得
        if (player != null)
        {
            player.transform.position = transform.position;
            Debug.Log("🎬 プレイヤーを舞台へ招待しました。");
        }
        else
        {
            Debug.LogWarning("⚠️ タグ 'Player' がついたオブジェクトが見つかりません！");
        }
    }
}