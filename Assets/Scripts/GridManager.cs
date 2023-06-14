using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridManager : MonoBehaviour
{
    public Camera cam;
    public GameObject gridObj;

    public int row, column;

    private float offsetY = 2.5f;

    public GameObject[,] grid;

    public GameObject diamondPrefab;

    public void GridCreate()
    {
        grid = new GameObject[row, column];

        for (int x = 0; x < row; x++)
        {
            for (int y = 0; y < column; y++)
            {
                grid[x,y] = Instantiate(gridObj, new Vector3(x, y), Quaternion.identity);
            }
        }

        cam.transform.position = new Vector3(row /2 - 0.5f, column /2 - 0.5f - offsetY, cam.transform.position.z);
    }

    public bool GridControl(Transform[] currentBlockChild)
    {
        for (int c = 0; c < currentBlockChild.Length; c++)
        {
            if (currentBlockChild[c].transform.position.x < 0 ||
                currentBlockChild[c].transform.position.x > row ||
                currentBlockChild[c].transform.position.y < 0 ||
                currentBlockChild[c].transform.position.y > column)
            {
                return false;
            }
        }

        return true;
    }

    [ContextMenu("Create Diamond!")]
    public void DiamondInstantiate()
    {
        GameObject[] selectionObjects = Selection.gameObjects;
        
        List<Vector2> diamondPosition = new List<Vector2>();

        foreach (GameObject obj in selectionObjects)
        {
            int childCount = obj.transform.childCount;  //Secilen objenin child sayisi
            int diamondCount = Random.Range(1, 3);      //Bir objedeki max diamond sayisi            

            List<int> childIndex = new List<int>();

            for (int i = 0; i < diamondCount; i++)
            {
                int rndmChild = Random.Range(0, childCount); //Secilen objenin indexi
                while (childIndex.Contains(rndmChild))
                {
                    rndmChild = Random.Range(0, childCount);
                }


                Vector3 childPosition = obj.transform.GetChild(rndmChild).transform.position;
                while (diamondPosition.Contains(childPosition))
                {
                    rndmChild = Random.Range(0, childCount);
                    childPosition = obj.transform.GetChild(rndmChild).transform.position;
                }

                childIndex.Add(rndmChild);
                diamondPosition.Add(childPosition);

                Instantiate(diamondPrefab, childPosition, Quaternion.identity);
            }
        }
    }
}
