using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondObject : MonoBehaviour
{
    private SpriteRenderer sp;
    private int  blockCount = 0;
    public bool isGreen = false;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();

        GameManager.instance.SubscribeDiamond(this);
        DiamondPassive();
    }

    public void DiamondActive()
    {
        sp.color = Color.green;
        isGreen = true;
    }

    
    public void  DiamondPassive()
    { 
        sp.color= Color.red;
        isGreen = false;
    }

    //public void OnBlockAdded()
    //{
    //    blockCount++;   
    //    UpdateColor();
    //}
    //public void OnBlockRemoved()
    //{
    //    blockCount--; 
    //    UpdateColor();
    //}
    //private void UpdateColor()
    //{
    //    if(blockCount > 0)
    //    {
    //        DiamondActive();
    //    }
    //    else
    //    {
    //        DiamondPassive();
    //    }
    //}
}
