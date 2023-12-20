using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPanel : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;
    private void Start()
    {
        nextLevelButton.onClick.AddListener(NextLevelOnClick);
        retryButton.onClick.AddListener(RetryOnClick);
        menuButton.onClick.AddListener(MenuOnClick);
    }

    private void NextLevelOnClick()
    {
        GameManager.Instance.LoadNextLevel();
        UIManager.Instance.DisablePanel(UIManager.Instance.victoryPanel);
    }
    private void RetryOnClick()
    {
        GameManager.Instance.RetryLevel();
        UIManager.Instance.DisablePanel(UIManager.Instance.victoryPanel);
        UIManager.Instance.ResetScore();
    }

    private void MenuOnClick()
    {
        GameManager.Instance.PrepareForMenu();
        Debug.Log("ENABLE MENU PANEL");
    }
}
