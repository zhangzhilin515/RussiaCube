using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public enum State
    { Ground, Magma, Delete,Treasure }
    public State state;
    private SpriteRenderer sprite;
    public Inventory treasure;

    void Start()
    {
        AddToGrid();
        if(state==State.Magma)
        {
            sprite = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
            sprite.color = Color.red;
        }
        if (state == State.Delete)
        {
            sprite = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
            sprite.color = Color.blue;
        }
        if(state == State.Treasure)
        {
            sprite = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
            sprite.color = Color.yellow;
        }
    }
    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            TetrisBlock.grid[roundedX, roundedY] = children;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state==State.Treasure&&collision.gameObject.CompareTag("Player"))
        {
            for(int i=0;i<treasure.tetrisBlockSOList.Count;i++)
            {
                AddItem(i);
            }
            InventoryManager.RefreshIcon();
            Destroy(this.gameObject);
        }
    }
    private void AddItem(int i)
    {
        bool HasInBag = false;
        for (int j = 0; j < InventoryManager.Instance.PlayerBag.tetrisBlockSOList.Count; j++)
        {
            if (InventoryManager.Instance.PlayerBag.tetrisBlockSOList[j].BlockName == treasure.tetrisBlockSOList[i].BlockName)
            {
                InventoryManager.Instance.PlayerBag.tetrisBlockSOList[j].BlockNumber += treasure.tetrisBlockSOList[i].BlockNumber;
                HasInBag = true;
                break;
            }
        }
        if(!HasInBag)
        {
            InventoryManager.Instance.PlayerBag.tetrisBlockSOList.Add(treasure.tetrisBlockSOList[i]);
            InventoryManager.CreateNewIcon(treasure.tetrisBlockSOList[i]);
        }
    }
}
