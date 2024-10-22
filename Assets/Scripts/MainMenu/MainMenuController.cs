using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI highestScoreDisplay = GameObject.Find("BestScore").GetComponent<TextMeshProUGUI>();
        GameManager.Instance.LoadHighestScore();
        highestScoreDisplay.text = $"Name: {GameManager.Instance.playerName}\nBest Time: {GameManager.Instance.bestTime}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        GameManager.Instance.playerName = GameObject.Find("NameField").GetComponent<TextMeshProUGUI>().text.ToString();
        GameManager.Instance.bestTime = "00:00";
        GameManager.Instance.timeWhenGameStarted = Time.time;
        SceneManager.LoadScene("Game");
    }
    public void ExitGame() {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
