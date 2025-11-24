using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour {
    private GameManager _gameManager;

    [SerializeField]
    private Transform top;

    [SerializeField]
    private Transform bottom;

    private Stack<Disk> _disks;
    private Renderer rend;

    public GameManager gameManager {
        get => _gameManager;
        set => _gameManager = value;
    }

    public Stack<Disk> disks {
        get => _disks;
        set => _disks = value;
    }

    private void Awake() {
        disks = new Stack<Disk>();
        rend = GetComponent<Renderer>();

        top.hideFlags = HideFlags.HideInHierarchy;
        bottom.hideFlags = HideFlags.HideInHierarchy;
    }


    public void SelectDisk() {
        if (disks.Count < 1)
            return;
        Disk selectedDisk = disks.Pop();
        selectedDisk.transform.position = top.position;
        gameManager.selectedDisk = selectedDisk;
    }

    private bool CanStack(Disk toStack) {
        bool canStack = true;
        if (disks.TryPeek(out Disk topDisk)) canStack = topDisk.size < toStack.size;
        return canStack;
    }

    public void PutDisk(Disk toStack) {
        if (!CanStack(toStack))
            return;

        if (disks.Count <= 0)
            toStack.transform.position = bottom.position;
        else
            toStack.transform.position = disks.Peek().transform.position + gameManager.diskOffset;
        disks.Push(toStack);
        toStack.transform.parent = transform;
        if (gameManager.selectedDisk != null)
            gameManager.selectedDisk = null;
    }

    private void OnMouseEnter() {
        if (gameManager.selectedDisk != null && !CanStack(gameManager.selectedDisk))
            rend.material.SetColor("_Color", new Color(0.5f, 0f, 0f));
        else
            rend.material.SetColor("_Color", new Color(0.5f, 0.5f, 0.5f));
    }

    private void OnMouseExit() {
        rend.material.SetColor("_Color", new Color(1f, 1f, 1f));
    }

    private void OnMouseDown() {
        if (gameManager.selectedDisk != null)
            PutDisk(gameManager.selectedDisk);
        else
            SelectDisk();
    }
}