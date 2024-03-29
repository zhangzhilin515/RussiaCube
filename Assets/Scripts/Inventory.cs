using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Inventory : ScriptableObject
{
    public List<TetrisBlockSO> tetrisBlockSOList = new List<TetrisBlockSO>();
}
