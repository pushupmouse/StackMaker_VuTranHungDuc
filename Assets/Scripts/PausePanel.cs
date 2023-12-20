using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        resumeButton.onClick.AddListener(ResumeOnClick);
        retryButton.onClick.AddListener(RetryOnClick);
        menuButton.onClick.AddListener(MenuOnClick);
    }

    private void ResumeOnClick()
    {
        UIManager.Instance.DisablePanel(UIManager.Instance.pausePanel);
        GameManager.Instance.UnpauseGame();
    }
    private void RetryOnClick()
    {
        GameManager.Instance.RetryLevel();
        UIManager.Instance.DisablePanel(UIManager.Instance.pausePanel);
    }

    private void MenuOnClick()
    {
        GameManager.Instance.PrepareForMenu();
        Debug.Log("ENABLE MENU PANEL");
    }
}
