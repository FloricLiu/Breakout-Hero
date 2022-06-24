using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemies;
    public BallScript ball { get; private set; }
    public MovementScript paddle { get; private set; }
    public Brick[] bricks { get; private set; }

    const int NUM_LEVELS = 2;

    public int level = 1;
    public int score = 0;
    public int lives = 3;

    [SerializeField] public GameObject FinishScreen;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
    }
    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Checks if enemies are available with tag "Enemy". Note that you should set this to your enemies in the inspector.
        if (enemies.Length == 0)
        {
            SceneManager.LoadScene("OtherSceneName"); // Load the scene with name "OtherSceneName"
        }
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;

        SceneManager.LoadScene("Level1");
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<BallScript>();
        this.paddle = FindObjectOfType<MovementScript>();
        this.bricks = FindObjectsOfType<Brick>();
    }
    public void OnPlayButton()
    {
        NewGame();
        Time.timeScale = 1;
        FinishScreen.SetActive(false);
    }
    public void OnExitButton()
    {
        Application.Quit();
    }

    public void Miss()
    {
        if (this.lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }

    private void ResetLevel()
    {
        this.paddle.ResetPaddle();
        this.ball.ResetBall();

        // Resetting the bricks is optional
        // for (int i = 0; i < this.bricks.Length; i++) {
        //     this.bricks[i].ResetBrick();
        // }
    }

    private void GameOver()
    {
        // Start a new game immediately
        // You can also load a "GameOver" scene instead
        NewGame();
    }

    public void Hit(Brick brick)
    {
        this.score += brick.points;

        if (Cleared())
        {
            //LoadLevel(this.level + 1);
            Time.timeScale = 0;
            FinishScreen.SetActive(true);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < this.bricks.Length; i++)
        {
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable)
            {
                return false;
            }
        }

        return true;
    }

}
