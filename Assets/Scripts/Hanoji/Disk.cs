using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour {
    public int size;

    [SerializeField]
    private float _scale;

    public float scale {
        get => _scale;
        set {
            _scale = value;
            ScaleDisk();
        }
    }

    private void ScaleDisk() {
        transform.localScale = new Vector3(scale, 0.1f, scale);
    }
}