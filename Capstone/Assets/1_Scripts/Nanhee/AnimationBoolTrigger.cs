using UnityEngine;

public class AnimationBoolTrigger : MonoBehaviour
{
    public Animator animator; // Animator 컴포넌트 연결
    public string boolParameterName = "True"; // 설정할 bool 파라미터 이름

    private void Start()
    {
        // 15초 후에 SetBoolTrue 메서드 실행
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
            Debug.LogWarning("Animator가 연결되어 있지 않습니다.");
        }
    }
}
