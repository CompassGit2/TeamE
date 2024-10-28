using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public GameObject destructionEffect;
    public void DestroyObject()
    {
        // �j��G�t�F�N�g�𐶐��i�K�v�ȏꍇ�j
        if (destructionEffect != null)
        {
            Instantiate(destructionEffect, transform.position, transform.rotation);
        }

        // �I�u�W�F�N�g��j��
        Destroy(gameObject);
    }
}
