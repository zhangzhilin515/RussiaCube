using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetromino : MonoBehaviour
{
    public GameObject[] Tetrominoes;
    public GameObject nextTetromino;
    public Vector3 nextPosition;
    private int current;
    private int nextone;
    private int i=0;
    private TetrisBlock tetrisBlock;
    void Start()
    {
        NewTetromino();
    }
    public  void NewTetromino()
    {
        if(i==0)
        {
            current = Random.Range(0, Tetrominoes.Length);
            nextone = Random.Range(0, Tetrominoes.Length);
            Instantiate(Tetrominoes[current], transform.position, Quaternion.identity);
            nextTetromino=Instantiate(Tetrominoes[nextone], nextPosition, Quaternion.identity);
            tetrisBlock = nextTetromino.GetComponent<TetrisBlock>();
            tetrisBlock.enabled = false;
            i=1;
        }
        else
        {
            current = nextone;
            nextone = Random.Range(0, Tetrominoes.Length);
            nextTetromino.transform.position = new Vector3(transform.position.x,transform.position.y,0);
            tetrisBlock.enabled = true;
            nextTetromino=Instantiate(Tetrominoes[nextone], nextPosition, Quaternion.identity);
            tetrisBlock = nextTetromino.GetComponent<TetrisBlock>();
            tetrisBlock.enabled = false;
        }
    }
}
