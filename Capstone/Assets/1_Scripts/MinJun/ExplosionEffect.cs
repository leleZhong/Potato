using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float explosionForce = 1000f; // 폭발의 힘
    public float explosionRadius = 5f; // 폭발 반경
    public float upwardsModifier = 1f; // 폭발력의 위쪽 증가값
    public GameObject explosionEffectPrefab; // 폭발 효과 프리팹
    public float effectScaleMultiplier = 30f; // 이펙트 크기 배수
    public Vector3 effectRotation = new Vector3(90f, 0f, 0f); // 이펙트 회전값

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 물체의 태그가 "letter"인 경우에만 폭발
        // Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("letter"))
        {
            Explode();
        }
    }

    void Explode()
    {
        // 폭발 위치
        Vector3 explosionPosition = transform.position;

        // 폭발 효과 생성
        if (explosionEffectPrefab != null)
        {
            Debug.Log("Instantiating explosion effect: " + explosionEffectPrefab.name);

            // 지정된 회전값을 사용하여 이펙트 인스턴스화
            GameObject instantiatedEffect = Instantiate(explosionEffectPrefab, explosionPosition, Quaternion.Euler(effectRotation));

            // 이펙트 크기 조정
            instantiatedEffect.transform.localScale *= effectScaleMultiplier;
        }
        else
        {
            Debug.LogWarning("No explosion effect prefab assigned.");
        }

        // 폭발 반경 내의 모든 콜라이더 검색
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // 폭발력 적용
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
