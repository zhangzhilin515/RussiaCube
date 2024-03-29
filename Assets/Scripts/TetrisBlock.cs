using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    public Vector3 nextPosition;
    private bool Drop;
    private int firstFoundRow;
    private float previousTime;
    public float fallTime = 0.75f;
    public static bool ableToGenerate=true;
    public static int height = 50;
    public static int width = 300;
    public static Transform[,] grid = new Transform[width, height];
    public List<Transform> DropList = new List<Transform>();


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForMagma();
                CheckForLines();
                this.enabled = false;
                ableToGenerate = true;
                //FindObjectOfType<SpawnTetromino>().NewTetromino();
            }
        }
        /*
        if(Time.time-previousTime>(Input.GetKeyDown(KeyCode.DownArrow)?fallTime/10:fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<SpawnTetromino>().NewTetromino();
            }
            previousTime = Time.time;
        }
        */
    }
    void CheckForLines()
    {
        for (int i = width-1; i >=0 ; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i,firstFoundRow);
                //RowDown(i);
            }
        }
    }
    bool HasLine(int i)
    {
        int hasDelete = 0;
        bool firstFound = false;
        for(int j=1;j<=height-1;j++)//遍历全部列数
        {
            if(grid[i,j]==null)//如果存储为空，则让消除数为0，重置发现列，继续遍历
            {
                hasDelete = 0;
                firstFound = false;
                continue;
            }
            if (grid[i, j].GetComponentInParent<Ground>()!=null)//如果发现方块存在ground脚本
            {
                if(firstFound==false)//如果未第一次发现，则记录第一列
                {
                    firstFound = true;
                    firstFoundRow = j;
                }
                Ground ground = grid[i, j].GetComponentInParent<Ground>();
                if(ground.state==Ground.State.Delete)
                {
                    hasDelete++;
                }
                else//如果方块并不是可消除方块，则让发现列归零,重置发现列
                {
                    hasDelete = 0;
                    firstFound = false;
                }
            }
            else//如果方块未发现ground脚本，则为生成的方块
            {
                if (firstFound == false)
                {
                    firstFound = true;
                    firstFoundRow = j;
                }
                hasDelete++;
            }
            if(hasDelete==11)
            {
                return true;
            }
        }
        return false;
    }
    void DeleteLine(int i,int firstFoundRow)
    {
        for (int j = firstFoundRow; j <firstFoundRow+11; j++)
        {
            Drop = false;
            Transform parent = grid[i, j].transform.parent;
            foreach (Transform children in parent)
            {
                int roundedX = Mathf.RoundToInt(children.transform.position.x);
                if (roundedX == i)
                {
                    Drop = true;
                    break;
                }
            }
            if (Drop == true)
            {
                foreach (Transform children in parent)
                {
                    int roundedX = Mathf.RoundToInt(children.transform.position.x);
                    if (roundedX != i)
                    {
                        DropList.Add(children);
                    }
                }
            }
        }
        /*
        for (int k = 0; k < DropList.Count; k++)
        {
            for (int l = 1; l < DropList.Count - k; l++)
            {
                if (DropList[k].transform.position.y > DropList[l].transform.position.y)
                {
                    Transform t = DropList[k];
                    DropList[k] = DropList[l];
                    DropList[l] = t;
                }
            }
        }
        for (int k = 0; k < DropList.Count; k++)
        {
            int roundedX = Mathf.RoundToInt(DropList[k].transform.position.x);
            int roundedY = Mathf.RoundToInt(DropList[k].transform.position.y);
            while (grid[roundedX, roundedY - 1] == null)
            {
                grid[roundedX, roundedY - 1] = grid[roundedX, roundedY];
                grid[roundedX, roundedY] = null;
                DropList[k].transform.position -= new Vector3(0, 1, 0);
            }
        }
        */
        for (int j = firstFoundRow; j < firstFoundRow+11; j++)
        {
            Destroy(grid[i, j].gameObject);
            grid[i, j] = null;
        }
        for (int k = 0; k < DropList.Count; k++)
        {
            for (int l = 0; l < DropList.Count; l++)
            {
                if (DropList[k].transform.position.y > DropList[l].transform.position.y)
                {
                    Transform t = DropList[k];
                    DropList[k] = DropList[l];
                    DropList[l] = t;
                }
            }
        }
        for (int k = DropList.Count-1; k >=0; k--)
        {
            int roundedX = Mathf.RoundToInt(DropList[k].transform.position.x);
            int roundedY = Mathf.RoundToInt(DropList[k].transform.position.y);
            while (grid[roundedX, roundedY - 1] == null)
            {
                grid[roundedX, roundedY - 1] = grid[roundedX, roundedY];
                grid[roundedX, roundedY] = null;
                DropList[k].transform.position -= new Vector3(0, 1, 0);
                bool destroy = false;//如果下方是岩浆，则摧毁方块
                foreach (Transform children in transform)
                {
                    if (grid[roundedX, roundedY - 1] != null)
                    {
                        if (grid[roundedX, roundedY - 1].GetComponentInParent<Ground>() != null)
                        {
                            Ground ground = grid[roundedX, roundedY - 1].GetComponentInParent<Ground>();
                            if (ground.state == Ground.State.Magma)
                            {
                                destroy = true;
                                break;
                            }
                        }
                    }
                }
                if (destroy == true)
                {
                    foreach (Transform children in transform)
                    {
                        Destroy(children.gameObject);
                        grid[roundedX, roundedY] = null;
                    }
                }//岩浆代码
                roundedX = Mathf.RoundToInt(DropList[k].transform.position.x);
                roundedY = Mathf.RoundToInt(DropList[k].transform.position.y);
            }
        }
    }
    void RowDown(int i)
    {
        for(int y=i;y<height;y++)
        {
            for(int j=0;j<width;j++)
            {
                if(grid[j,y]!=null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }
    void AddToGrid()
    {
        foreach(Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            grid[roundedX, roundedY] = children;
        }
    }
    bool ValidMove()
    {
        foreach(Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if(roundedX<=2||roundedX>=width||roundedY<0||roundedY>=height)
            {
                return false;
            }
            if (grid[roundedX, roundedY] != null)
                return false;
        }
        return true;
    }
    private void CheckForMagma()
    {
        bool destroy = false;
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if(roundedY-1>=0)
            {
                if (grid[roundedX, roundedY - 1] != null)
                {
                    if (grid[roundedX, roundedY - 1].GetComponentInParent<Ground>() != null)
                    {
                        Ground ground = grid[roundedX, roundedY - 1].GetComponentInParent<Ground>();
                        if (ground.state == Ground.State.Magma)
                        {
                            destroy = true;
                            break;
                        }
                    }
                }
            }
        }
        if(destroy==true)
        {
            foreach(Transform children in transform)
            {
                int roundedX = Mathf.RoundToInt(children.transform.position.x);
                int roundedY = Mathf.RoundToInt(children.transform.position.y);
                Destroy(children.gameObject);
                grid[roundedX, roundedY] = null;
            }
        }
    }
    List<Transform> RemoveNull(List<Transform> origin)
    {
        List<Transform> remove = new List<Transform>();
        for (int i = 0; i < origin.Count; i++)
        {
            if(origin[i]!=null)
            {
                remove.Add(origin[i]);
            }
        }
        return remove;
    }
}
