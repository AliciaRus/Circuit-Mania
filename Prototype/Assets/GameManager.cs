using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlayerLose()
    {
        Debug.Log("Player has lost!");
        // Loads the lose screen scene
        SceneManager.LoadScene("LoseScreen");
    }
}