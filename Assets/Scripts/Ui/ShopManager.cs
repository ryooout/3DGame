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
    [SerializeField, Header("所持金を表示させるテキスト")] private Text haveMoneyText;

    /// <summary>ショップ内容</summary>
    [SerializeField] private Transform shopContent;
    /// <summary>ショップアイテムプレファブ</summary>
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
        //シーンをまたいでも値が保持される
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        foreach(ShopUpgrade upgrade in upgrades)
        {
            GameObject item = Instantiate(shopItemPrefab, shopContent);

            upgrade.itemRef = item;
            //ショップテキストの表示
            foreach(Transform child in item.transform)
            {
                if(child.gameObject.name =="Level")
                {
                    child.gameObject.GetComponent<Text>().text = "Lv."+upgrade.shopLevel.ToString();
                }
                else if(child.gameObject.name == "Cost")
                {
                    child.gameObject.GetComponent<Text>().text = upgrade.cost.ToString()+"ゴールド";
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
            //子オブジェクトを参照してショップのレベルを更新
            upgrade.itemRef.transform.GetChild(0).GetComponent<Text>().
            text = "Lv."+upgrade.shopLevel.ToString();

            ApplyUpgrade(upgrade);
        }
    }

    public void ApplyUpgrade(ShopUpgrade upgrade)
    {
        switch(upgrade.name)
        {
            case "射程増加":
                
                break;
            case "攻撃力増加":
                break;
            case "体力増加":
                break;
            case "防御力増加":
                break;
        }
    }
   
    private void OnGUI()
    {
        haveMoneyText.text = "所持金:" + GameManager.Instance.Money.ToString()+"ゴールド";
    }
}
[System.Serializable]
public class ShopUpgrade
{
    public string name;
    public int cost;
    public Sprite ShopBackGround;
    /// <summary>ショップのレベル</summary>
    [HideInInspector] public int shopLevel = 0;
    /// <summary>アイテム参照</summary>
    [HideInInspector] public GameObject itemRef;
}
