using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour {
    
    public static List<string> ItemName = new List<string>();
    public static List<float> ItemQuantity = new List<float>();
    private static Backpack _instance;
    
    //Create Singleton Constructor
    public Backpack Instance()
    {
        if(_instance == null) { _instance = GetComponent<Backpack>(); }
        return _instance;
    }//End Singleton Constructor

    //----------------------------------------------------------------------------
    public void NewPickup(string strItem,float floQuantity)
    {
        if (HaveItem(strItem))
        {
            int i = ItemName.IndexOf(strItem);
            ItemQuantity[i] += floQuantity;

        }
        else
        {
            ItemName.Add(strItem);
            ItemQuantity.Add(floQuantity);
        }

    }//End NewPickup Method

    //----------------------------------------------------------------------------
    public float GetQuantity(string strItem)
    {
        float floQ = -1;
        if (HaveItem(strItem))
        {
            int i = ItemName.IndexOf(strItem);
            floQ = ItemQuantity[i];
        }
        return floQ;
    }//End GetQuantity Method

    //----------------------------------------------------------------------------
    public void UpdateQuantity(string strItem,float floQuant)
    {
        if (HaveItem(strItem))
        {
            int i = ItemName.IndexOf(strItem);
            ItemQuantity[i] += floQuant;
        }
    }//End UpdateQuantity Method

    //----------------------------------------------------------------------------
    public bool HaveItem(string strItem)
    {
        bool booH = false;
        if (ItemName.Contains(strItem))
        {
            booH = true;
        }
        return booH;
    }//End HaveItem Method


}
