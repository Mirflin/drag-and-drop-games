using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GameSetup {
    private Disk _selectedDisk;

    public Disk selectedDisk {
        get => _selectedDisk;
        set => _selectedDisk = value;
    }

    public override void Awake() {
        base.Awake();
        for (int i = 0; i < poles.Length; i++) poles[i].gameManager = this;
    }

    public void CheckForCompletion() {
        if (poles[poleCount - 1].disks.Count >= poleCount) {
            Debug.Log("Gameover!");
            StartCoroutine(ReloadScene());
        }
    }

    private IEnumerator ReloadScene() {
        yield return new WaitForSecondsRealtime(1.6f);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}