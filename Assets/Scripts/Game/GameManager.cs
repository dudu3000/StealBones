using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameRunning = true;
    public GameObject oceanTile;

    private float oceanTileSize = 30f;
    private float mapSize = 3000f;
    // Start is called before the first frame update
    void Start()
    {
        for (float i = -1500; i < 1500; i+=oceanTileSize) {
            for (float j = -1500; j < 1500; j+=oceanTileSize) {
                Instantiate(oceanTile, new Vector3(i, 0, j), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameRunning) {
            Debug.Log("GAME OVER");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }
}
