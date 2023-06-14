using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Transform[] childBlocks;

    private void Start()
    {
        childBlocks = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            childBlocks[i] = transform.GetChild(i);
        }

        GameManager.instance.SubscribeBlock(this);
    }

    public bool ControlPosition(Vector3Int controlPosition)
    {
       
        for (int i = 0;i < childBlocks.Length;i++)
        {
            Vector3Int childPos = new Vector3Int();
            childPos.x = Mathf.RoundToInt(childBlocks[i].position.x);
            childPos.y = Mathf.RoundToInt(childBlocks[i].position.y);
            childPos.z = 0;

            if (childPos == controlPosition)
            {
                return true;
            }
        }
        return false;
    }

    public Vector3 SelectionChild(int index)
    {
        return childBlocks[index].localPosition + transform.position;
    }
}
