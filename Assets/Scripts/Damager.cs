using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField]
    [Header("�^����_���[�W")] private float _attackDamage = 5.0f;
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
        // ���������I�u�W�F�N�g����ADamageInterFace ���Ă�
        var damagetarget = other.GetComponent<IDamageInterFace>();

        //IDamagable �� AddDamage �̏������K�{
        if (damagetarget != null)
        {
            other.GetComponent<IDamageInterFace>().AddDamage(_attackDamage);
        }
    }
}
