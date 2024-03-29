using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Inventory PlayerBag;
    public GameObject iconContainer;
    public Icon IconPrefab;
    private void Awake()
    {
        if(Instance!=null)
        {
            Destroy(this);
        }
        Instance = this;
    }
    private void OnEnable()
    {
        RefreshIcon();
    }
    public static void CreateNewIcon(TetrisBlockSO tetrisBlockSO)
    {
        Icon newIcon = Instantiate(Instance.IconPrefab, Instance.iconContainer.transform);
        newIcon.tetrisBlockSO = tetrisBlockSO;
        newIcon.blockImage.sprite = tetrisBlockSO.BlockSprite;
        newIcon.blockNumber.text = tetrisBlockSO.BlockNumber.ToString();
    }
    public static void RefreshIcon()
    {
        for (int i = 0; i < Instance.iconContainer.transform.childCount; i++)
        {
            Destroy(Instance.iconContainer.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < Instance.PlayerBag.tetrisBlockSOList.Count; i++)
        {
            if(Instance.PlayerBag.tetrisBlockSOList[i].BlockNumber!=0)
            {
                CreateNewIcon(Instance.PlayerBag.tetrisBlockSOList[i]);
            }
        }
    }
}
