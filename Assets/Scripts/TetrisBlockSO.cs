using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TetrisBlockSO : ScriptableObject
{
    public Transform Block;
    public string BlockName;
    public Sprite BlockSprite;
    public int BlockNumber;
}
