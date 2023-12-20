using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private GameObject playerPrefab;

    private GameObject currentLevel;
    private int levelNum = 1;
    private bool isPaused = false;


    private void Start()
    {
        LoadLevel();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                PauseGame();
                UIManager.Instance.EnablePanel(UIManager.Instance.pausePanel);
            }
            else
            {
                UnpauseGame();
                UIManager.Instance.DisablePanel(UIManager.Instance.pausePanel);
            }
        }
    }

    private void EnableCameraAndControls()
    {
        CameraFollow.Instance.SetCameraTarget();
        SwipeDetection.Instance.SetControlsTarget();
    }

    private void CleanUp()
    {
        CameraFollow.Instance.DisableCameraTarget();
        SwipeDetection.Instance.DisableControlsTarget();
        Destroy(currentLevel);
    }

    private void LoadLevel()
    {
        currentLevel = (GameObject) Instantiate(Resources.Load(MyConst.Path.LEVEL_PATH + levelNum.ToString()));
        EnableCameraAndControls();
        UIManager.Instance.EnableScore();
    }

    public void RetryLevel()
    {
        if(isPaused)
        {
            UnpauseGame();
        }
        CleanUp();
        LoadLevel();
    }

    public void LoadNextLevel()
    {
        levelNum++;
        CleanUp();
        LoadLevel();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PrepareForMenu()
    {
        Debug.Log("CLEANUP");
    }
}
