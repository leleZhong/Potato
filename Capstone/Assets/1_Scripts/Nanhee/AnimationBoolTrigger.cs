using UnityEngine;

public class AnimationBoolTrigger : MonoBehaviour
{
    public Animator animator; // Animator ������Ʈ ����
    public string boolParameterName = "True"; // ������ bool �Ķ���� �̸�

    private void Start()
    {
        // 15�� �Ŀ� SetBoolTrue �޼��� ����
        Invoke("SetBoolTrue", 16f);
    }

    private void SetBoolTrue()
    {
        if (animator != null)
        {
            animator.SetBool(boolParameterName, true);
        }
        else
        {
            Debug.LogWarning("Animator�� ����Ǿ� ���� �ʽ��ϴ�.");
        }
    }
}
