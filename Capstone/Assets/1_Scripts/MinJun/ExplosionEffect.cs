using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float explosionForce = 1000f; // ������ ��
    public float explosionRadius = 5f; // ���� �ݰ�
    public float upwardsModifier = 1f; // ���߷��� ���� ������
    public GameObject explosionEffectPrefab; // ���� ȿ�� ������
    public float effectScaleMultiplier = 30f; // ����Ʈ ũ�� ���
    public Vector3 effectRotation = new Vector3(90f, 0f, 0f); // ����Ʈ ȸ����

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��ü�� �±װ� "letter"�� ��쿡�� ����
        // Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("letter"))
        {
            Explode();
        }
    }

    void Explode()
    {
        // ���� ��ġ
        Vector3 explosionPosition = transform.position;

        // ���� ȿ�� ����
        if (explosionEffectPrefab != null)
        {
            Debug.Log("Instantiating explosion effect: " + explosionEffectPrefab.name);

            // ������ ȸ������ ����Ͽ� ����Ʈ �ν��Ͻ�ȭ
            GameObject instantiatedEffect = Instantiate(explosionEffectPrefab, explosionPosition, Quaternion.Euler(effectRotation));

            // ����Ʈ ũ�� ����
            instantiatedEffect.transform.localScale *= effectScaleMultiplier;
        }
        else
        {
            Debug.LogWarning("No explosion effect prefab assigned.");
        }

        // ���� �ݰ� ���� ��� �ݶ��̴� �˻�
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // ���߷� ����
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
