using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GameSetup {
    private Disk _selectedDisk;

    [Header("UI")]
    [SerializeField]
    private GameObject completionPanel;

    public Disk selectedDisk {
        get => _selectedDisk;
        set => _selectedDisk = value;
    }

    public override void Awake() {
        base.Awake();
        for (int i = 0; i < poles.Length; i++) poles[i].gameManager = this;
    }

    public void Update()
    {
        CheckForCompletion();
    }

    public void CheckForCompletion()
    {
        if (poles[poleCount - 1].disks.Count >= poleCount)
        {
            Debug.Log("Gameover!");
            Time.timeScale = 0f;
            if (completionPanel != null)
            {
                completionPanel.SetActive(true);
            }
        }
    }

    public void ReloadScene() {
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}