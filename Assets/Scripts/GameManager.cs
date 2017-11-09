using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public GameObject gameOverUI;
    public Text resultText;

    private EnemySpawn enemySpawn;

    private void Awake()
    {
        Instance = this;
        enemySpawn = GetComponent<EnemySpawn>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Win()
    {
        gameOverUI.SetActive(true);
        resultText.text = "WIN";
    }

    public void Failed()
    {
        enemySpawn.StopSpwan();
        gameOverUI.SetActive(true);
        resultText.text = "GAME OVER";
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }
}
