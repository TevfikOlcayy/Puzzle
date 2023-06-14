using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GridManager gridManager;
    public Camera cam;
    public LayerMask blockLayer;

    public Block currentBlock;
    private Vector3 blockStartPosition;

    private float zDepth = 0;

    private List<DiamondObject> diamonds = new List<DiamondObject>();
    private List<Block> blocks = new List<Block>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BlockSelect();
        }
        else if (Input.GetMouseButton(0))
        {
            MoveBlock();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            PutBlock();
            currentBlock = null;
        }

    }

    public void SubscribeBlock(Block block)
    {
        blocks.Add(block);
    }

    public void SubscribeDiamond(DiamondObject diamond)
    {
        diamonds.Add(diamond);
    }

    private void PutBlock()
    {
        if(currentBlock != null)
        {
            Vector3Int vectorInt = new Vector3Int();
            vectorInt.x = Mathf.RoundToInt(currentBlock.transform.position.x);
            vectorInt.y = Mathf.RoundToInt(currentBlock.transform.position.y);

            currentBlock.transform.position = new Vector3(vectorInt.x, vectorInt.y, zDepth);

            if (!gridManager.GridControl(currentBlock.childBlocks))
            {
                currentBlock.transform.position = blockStartPosition;
            }

            for (int i = 0; i < diamonds.Count; i++)
            {
                  
                for (int j = 0; j < blocks.Count; j++)
                {
                    
                    Vector3Int diamondPos = new Vector3Int();
                    diamondPos.x = Mathf.RoundToInt(diamonds[i].transform.position.x);
                    diamondPos.y = Mathf.RoundToInt(diamonds[i].transform.position.y);
                    diamondPos.z = 0;

                    if (blocks[j].ControlPosition(diamondPos))
                    {
                        diamonds[i].DiamondActive();
                        break;
                    }
                    else
                    {
                        diamonds[i].DiamondPassive();
                    }
                    
                   //var grid = gridManager.grid[childPos.x, childPos.y];
                   // if(grid != null)
                   // {
                   //     grid.Diomond ulaþýlacak 
                   // }
                    //if (diamondPos == childPos)
                    //{
                    //    diamonds[i].DiamondActive();
                    //    break;
                    //
                    //}
                }
            }

            if (DiamondControl())
            {
                int currentIndex = SceneManager.GetActiveScene().buildIndex;

                if (currentIndex + 1 < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(currentIndex + 1);
                }
            }

        }
    }
    
    public bool DiamondControl()
    {
        int greenCount = 0;

        for (int i = 0; i < diamonds.Count; i++)
        {
            if (diamonds[i].isGreen == true)
            {
                greenCount++;
            }
        }

        return greenCount >= diamonds.Count;
    }

    private void MoveBlock()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = zDepth;

        if (currentBlock != null)
        {
            currentBlock.transform.position = Vector3.Lerp(currentBlock.transform.position, mousePosition, 0.1f);
        }
    }

    private void BlockSelect()
    {
        if (currentBlock != null) return;

        Ray2D ray = new Ray2D(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1f, blockLayer);

        if(hit.collider != null)
        {
            Transform parent = hit.collider.gameObject.transform.parent;
            currentBlock = parent.GetComponent<Block>();
            blockStartPosition = currentBlock.transform.position;

            zDepth -= 0.0001f; 
            currentBlock.transform.position = new Vector3(currentBlock.transform.position.x, currentBlock.transform.position.y, zDepth);

        }
    }


}
