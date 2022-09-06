using UnityEngine;
using UnityEngine.UI;
//unityEditorÇ©ÇÁåƒÇ—èoÇπÇÈÇÊÇ§Ç…ÇµÇΩ
[CreateAssetMenu(fileName ="New Item",menuName = "ScriptableObject/Create Item")]
public class Item : ScriptableObject
{
    public ItemId Id = default;
    public static Item itemInstance;
    new public string name = "New Item";
    public Sprite itemIcon = null;
    public enum ItemId
    {
        None,
        Atk,
        Def,
        Heal,
    }
    public string ItemName()
    {
        return name;
    }

}
