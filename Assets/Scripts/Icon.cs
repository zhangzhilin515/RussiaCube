using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    public TetrisBlockSO tetrisBlockSO;
    public Image blockImage;
    public Text blockNumber;
    public Button button;
    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            if(TetrisBlock.ableToGenerate)
            {
                Instantiate(tetrisBlockSO.Block, Player.Instance.GetSpawnPos(), Quaternion.identity);
                tetrisBlockSO.BlockNumber--;
                InventoryManager.RefreshIcon();
                TetrisBlock.ableToGenerate = false;
            }
        });
    }
}
