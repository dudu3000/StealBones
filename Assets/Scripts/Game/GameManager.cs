using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameRunning = true;
    public static bool gameFinished = false;
    public static bool mainMenu = true;
    public static float gameTime = 0f;
    public bool gamePaused = false;
    public GameObject oceanTile;
    public AudioClip[] backgroundSounds;
    public string bestTime;
    public string playerName;
    public static GameManager Instance;
    public int noOfChests = 4;
    public int chestsCollected = 0;
    public float timeWhenGameStarted = 0f;
    public int currentRunTimeInSeconds = 0;
    public int bestTimeInSeconds = 0;
    public int bestTimeReadInSeconds = 0;
    public string bestTimeRead = "00:00";

    private float oceanTileSize = 30f;
    private float mapSize = 3000f;
    private AudioSource gameAudioSource;
    private float backgroundSoundsTime = 5f;
    private float timeCounter = 0f;
    private int numberOfBackgroundSounds = 0;
    private float menuDelay = 1f;
    private float menuTimePassed = 0f;
    private bool oceanSpawned = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gameAudioSource = GetComponent<AudioSource>();
        Instance.bestTimeReadInSeconds = ConvertToSeconds(ReadHighestScore().bestTime);
        Instance.bestTimeRead = ReadHighestScore().bestTime;
    }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        GameLost();
        GameCompleted();
        UpdateStatsRealTime();
        GameMenuControl();
        SpawnOcean();
        RandomBackgroundSoundsController();

        gameTime += Time.deltaTime;

    }

    private void RandomBackgroundSoundsController() {
        timeCounter += Time.deltaTime;

        if (timeCounter > backgroundSoundsTime) {
            backgroundSoundsTime = UnityEngine.Random.Range(3f, 8f);
            timeCounter = 0f;
            int indexOfClipToPlay = UnityEngine.Random.Range(0, numberOfBackgroundSounds);
            gameAudioSource.PlayOneShot(backgroundSounds[indexOfClipToPlay]);
        }
    }

    private void SpawnOcean() {
        if (SceneManager.GetActiveScene().name == "Game" && !oceanSpawned) {
            numberOfBackgroundSounds = backgroundSounds.Length;

            float halfMapSize = mapSize / 2;
            for (float i = -halfMapSize; i < halfMapSize; i+=oceanTileSize) {
                for (float j = -halfMapSize; j < halfMapSize; j+=oceanTileSize) {
                    Instantiate(oceanTile, new Vector3(i, 0, j), Quaternion.identity);
                }
            }
            oceanSpawned = true;
        }
    }

    private void GameCompleted () {
        if (Instance.chestsCollected == Instance.noOfChests) {
            Instance.bestTime = GetTimeFormatted();
            SaveData data = ReadHighestScore();

            int currentRunTime = ConvertToSeconds(GetTimeFormatted());
            currentRunTimeInSeconds = currentRunTime;
            int bestRunTime = ConvertToSeconds(data.bestTime);
            bestTimeInSeconds = bestRunTime;

            if (currentRunTime < bestRunTime) {
                SaveHighestScore();
            }

            gameRunning = false;
            gameFinished = true;
        }
    }    
    
    public static int ConvertToSeconds(string timeString) {
        string[] timeParts = timeString.Split(':');
        int minutes = int.Parse(timeParts[0]);
        int seconds = int.Parse(timeParts[1]);
        int totalSeconds = (minutes * 60) + seconds;

        return totalSeconds;
    }

    private void GameLost() {
        if (!gameRunning) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void GameMenuControl() {
        if (SceneManager.GetActiveScene().name == "Game") {
            if (GameObject.Find("Player") == null) {
                if (!gameRunning) {
                    menuTimePassed = 0f;
                    Instance.gamePaused = true;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    return;
                }
            }

            menuTimePassed += Time.deltaTime;
            if (Input.GetKey(KeyCode.Escape) && menuTimePassed > menuDelay && !Instance.gamePaused) {
                menuTimePassed = 0f;
                Instance.gamePaused = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                return;
            }

            if (Instance.gamePaused) {
                timeWhenGameStarted += Time.deltaTime;

                if (Input.GetKey(KeyCode.Escape) && menuTimePassed > menuDelay) {
                    ResumeGame();
                }
            }
        }
    }

    public void ResumeGame() {
        menuTimePassed = 0f;
        Instance.gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        return;
    }

    public void UpdateStatsRealTime() {
        GameObject nameDisplay = GameObject.Find("Name");
        GameObject timeDisplay = GameObject.Find("Time");
        GameObject scoreDisplay = GameObject.Find("Score");
        if (nameDisplay != null && GameObject.Find("Player") != null) {
            nameDisplay.GetComponent<TextMeshProUGUI>().text = $"{playerName} - HP: {GameObject.Find("Player").GetComponent<PlayerController>().health}/100";
            timeDisplay.GetComponent<TextMeshProUGUI>().text = GetTimeFormatted();
            scoreDisplay.GetComponent<TextMeshProUGUI>().text = $"Chests: {GameManager.Instance.chestsCollected}/{GameManager.Instance.noOfChests}";
        }
    }

    public string GetTimeFormatted() {
        float timeSinceStart = gameTime - timeWhenGameStarted;

        int minutes = Mathf.FloorToInt(timeSinceStart / 60);
        int seconds = Mathf.FloorToInt(timeSinceStart % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddCollectedChest() {
        chestsCollected += 1;
    }

    [System.Serializable]
    public class SaveData {
        public string playerName;
        public string bestTime;
    }

    public void LoadHighestScore() {
        string path = Application.persistentDataPath + "/savedata.json";

        if (File.Exists(path)) {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestTime = data.bestTime;
            playerName = data.playerName;
        } else {
            bestTime = "00:00";
            playerName = "Jhon Doe";
        }
    }

    public SaveData ReadHighestScore() {
        string path = Application.persistentDataPath + "/savedata.json";

        if (File.Exists(path)) {
            string json = File.ReadAllText(path);

            return JsonUtility.FromJson<SaveData>(json);
        }
        SaveData data = new SaveData();
        data.playerName = "Jhon Doe";
        data.bestTime = "00:00";

        return data;
    }

    public void SaveHighestScore() {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.bestTime = bestTime;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public void RestartGame() {
        Instance.timeWhenGameStarted = 0;
        gameTime = 0f;
        SceneManager.LoadScene(1);
        LoadHighestScore();
        Instance.bestTimeReadInSeconds = ConvertToSeconds(ReadHighestScore().bestTime);
        Instance.bestTimeRead = ReadHighestScore().bestTime;
        Instance.chestsCollected = 0;
        Instance.gamePaused = false;
        gameFinished = false;
    }
}
