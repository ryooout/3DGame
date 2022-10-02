using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    public static ShopManager shopInstance; 
    [SerializeField] GameObject _playerSword = default;
    [SerializeField] Damager damager;
    [SerializeField] Guard guard;
    Vector3 swordScale;
    [SerializeField] private ShopUpgrade[] upgrades;
    [SerializeField, Header("��������\��������e�L�X�g")] private Text haveMoneyText;

    /// <summary>�V���b�v���e</summary>
    [SerializeField] private Transform shopContent;
    /// <summary>�V���b�v�A�C�e���v���t�@�u</summary>
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private PlayerController playerController;
    private void Awake()
    {
        swordScale = _playerSword.transform.localScale;
        if(shopInstance ==null)
        {
            shopInstance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
        //�V�[�����܂����ł��l���ێ������
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        foreach(ShopUpgrade upgrade in upgrades)
        {
            GameObject item = Instantiate(shopItemPrefab, shopContent);

            upgrade.itemRef = item;
            //�V���b�v�e�L�X�g�̕\��
            foreach(Transform child in item.transform)
            {
                if(child.gameObject.name =="Level")
                {
                    child.gameObject.GetComponent<Text>().text = "Lv."+upgrade.shopLevel.ToString();
                }
                else if(child.gameObject.name == "Cost")
                {
                    child.gameObject.GetComponent<Text>().text = upgrade.cost.ToString()+"�S�[���h";
                }
                else if (child.gameObject.name == "ItemName")
                {
                    child.gameObject.GetComponent<Text>().text = upgrade.name;
                }
            }
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuyUpgrade(upgrade);
            });
        }

    }
    public void BuyUpgrade(ShopUpgrade upgrade)
    {
        if(GameManager.Instance.Money>=upgrade.cost)
        {
            GameManager.Instance.Money-=upgrade.cost;
            upgrade.shopLevel++;
            //�q�I�u�W�F�N�g���Q�Ƃ��ăV���b�v�̃��x�����X�V
            upgrade.itemRef.transform.GetChild(0).GetComponent<Text>().
            text = "Lv."+upgrade.shopLevel.ToString();

            ApplyUpgrade(upgrade);
        }
    }

    public void ApplyUpgrade(ShopUpgrade upgrade)
    {
        switch(upgrade.name)
        {
            case "�˒�����":
                
                break;
            case "�U���͑���":
                break;
            case "�̗͑���":
                break;
            case "�h��͑���":
                break;
        }
    }
   
    private void OnGUI()
    {
        haveMoneyText.text = "������:" + GameManager.Instance.Money.ToString()+"�S�[���h";
    }
}
[System.Serializable]
public class ShopUpgrade
{
    public string name;
    public int cost;
    public Sprite ShopBackGround;
    /// <summary>�V���b�v�̃��x��</summary>
    [HideInInspector] public int shopLevel = 0;
    /// <summary>�A�C�e���Q��</summary>
    [HideInInspector] public GameObject itemRef;
}
