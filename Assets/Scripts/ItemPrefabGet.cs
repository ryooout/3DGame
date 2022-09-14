using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefabGet : MonoBehaviour
{
    public static ItemPrefabGet itemPrefabGet;
    public GameObject itemPrefab;
    UiController uiController;
    InventryUI inventry;
    [SerializeField] DropItem dropItem;
    private void Start()
    {
        inventry = GameObject.FindGameObjectWithTag("Player").GetComponent<InventryUI>();
        uiController = GameObject.Find("UiObj").GetComponent<UiController>();
        uiController.itemGet.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            dropItem.PickUpItem();
            Debug.Log("’Ê‚Á‚Ä‚é");
        }
    }
    public void GetItem()
    {

    }
}
