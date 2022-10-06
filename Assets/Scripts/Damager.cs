using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField]
    [Header("与えるダメージ")] private float _attackDamage = 5.0f;
    private void Awake()
    {
    }
    public float AttackDamage
    {
        get { return _attackDamage; }
        set { _attackDamage = value; }
    }
    private void OnTriggerEnter(Collider other)
    {
        // あたったオブジェクトから、DamageInterFace を呼ぶ
        var damagetarget = other.GetComponent<IDamageInterFace>();

        //IDamagable は AddDamage の処理が必須
        if (damagetarget != null)
        {
            other.GetComponent<IDamageInterFace>().AddDamage(_attackDamage);
        }
    }
}
