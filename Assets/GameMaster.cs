using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    public enum GameState { Title, Playing, Pause, Settings, Clear, GameOver }
    public GameState CurrentState { get; private set; }

    [Header("Game Progress")]
    public int currentStage = 1;
    public bool hasKey = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ResumeGame();
    }

    // 社長の仕事：世界の時間と状態を操る
    public void PauseGame()
    {
        CurrentState = GameState.Pause;
        Time.timeScale = 0f;
        Debug.Log("Game Paused");
    }

    public void OpenSettingsState()
    {
        CurrentState = GameState.Settings;
    }

    public void ResumeGame()
    {
        CurrentState = GameState.Playing;
        Time.timeScale = 1f;
        Debug.Log("Game Resumed");
    }

    // 鍵の管理（ゲームのルール）
    public void AddKey() { hasKey = true; }
    public void UseKey() { hasKey = false; }

    [Header("UI References")]
    [SerializeField] private GameObject resultCanvas;

    public void GoalReached()
    {
        // 1. 状態をクリアに変更（これで二重ゴールなどを防ぐ）
        if (CurrentState == GameState.Clear) return;
        CurrentState = GameState.Clear;

        // 2. 物理演算や時間を必要に応じて止める（またはスローにする演出もアリ）
        // Time.timeScale = 0f; // 完全に止めるならこれ

        // 3. リザルトUIを表示
        if (resultCanvas != null)
        {
            Time.timeScale = 0f;

            resultCanvas.SetActive(true);
            Debug.Log($"🏆 ステージ {currentStage} クリア！リザルトを表示します。");
        }
        else
        {
            Debug.LogError("⚠️ リザルトUIがGameMasterに登録されていません！");
        }

        // 4. ここでセーブデータを更新
        // PlayerPrefs.SetInt("MaxClearedStage", currentStage);
    }
}