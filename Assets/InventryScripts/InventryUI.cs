using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class InventryUI : MonoBehaviour
{
    //�ڂŌ��Ă킩��
    /// <summary>�X���b�g���Ǘ�����e�I�u�W�F�N�g</summary>
    public Transform slotsParent;
    /// <summary>�X���b�g���擾</summary>
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
