using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class InventryUI : MonoBehaviour
{
    //目で見てわかる
    /// <summary>スロットを管理する親オブジェクト</summary>
    public Transform slotsParent;
    /// <summary>スロットを取得</summary>
    public InventrySlot[]slots;
    Item item;
    Item.ItemId id;
    void Start()
    {
        slots = slotsParent.GetComponentsInChildren<InventrySlot>();   
    }
    public void UIUpdate()
    {
        Debug.Log("UptateUI");
      for(int i = 0;i<slots.Length;i++)
       {
            if(i<Inventry.instance.items.Count)
            {
                slots[i].AddItem(Inventry.instance.items[i]);
            }
            else
            {
                slots[i].RemoveItem();
            }
       }
    }
}
