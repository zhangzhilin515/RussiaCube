using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public event EventHandler OnBagToggle;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            OnBagToggle?.Invoke(this, EventArgs.Empty);
        }
    }
    public Vector3 GetSpawnPos()
    {
        return new Vector3(Mathf.RoundToInt(transform.position.x),Mathf.RoundToInt(transform.position.y)+10,0);
    }
}
