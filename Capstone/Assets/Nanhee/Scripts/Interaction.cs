using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    //4개?의 큐브를 놓고 3개는 오답, 1개는 정답
    //정답에 상호작용하면 문열림 -> UI correct 표시
    //오답에 상호작용하면 문 안열림 -> 에러 표시나오게 > 패널티

   
    public GameObject interactionUI; // 상호작용 UI 오브젝트 참조
    public Transform[] InterActionTransform;
    public GameObject[] blockPrefabs; // 블럭 프리팹 배열
    private Animator animator;


    void Start()
    {

        //interactionUI.SetActive(false); // 초기에는 UI를 비활성화

        animator = GetComponent<Animator>();


        Invoke("MakeQuestion", 0.1f);
    }

    //void OnTriggerEnter(Collider other)
    //{
        
    
    // bool 
    //    // "null" 태그를 가진 오브젝트에 접촉하면 UI를 활성화
    //    if (other.CompareTag("interaction"))
    //    {
    //        interactionUI.SetActive(true);


    // TODO : 오브젝트 상호작용 후
            
    //if(Interaction.tag == 'CorrectNumber'){
    //      animator.SetBool("Open",true);
        //}
        //else
        //UI에 틀린 상호작용이라고 표시하기


    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    // 오브젝트가 범위를 벗어나면 UI를 비활성화
    //    if (other.CompareTag("null"))
    //    {
    //        interactionUI.SetActive(false);
    //    }
    //}

    void MakeQuestion()
    {
        List<int> InteractionNumbers = new List<int>() { 0, 1, 2, 3 }; // 생성 후보 번호들
        GameObject duplicatedBlock = GameObject.FindWithTag("CorrectNumber");

        if (duplicatedBlock != null)
        {

            int index1 = Random.Range(0, InterActionTransform.Length);
            GameObject correctAnswerBlock = Instantiate(duplicatedBlock, InterActionTransform[index1].position, Quaternion.identity); // 객체 생성
            InteractionNumbers.RemoveAt(index1); //프리펩리스트에서지움

            Debug.Log("Object instantiated at position: " + InterActionTransform[index1].position);

            correctAnswerBlock.transform.parent = InterActionTransform[index1];
            correctAnswerBlock.transform.localPosition = Vector3.zero;

            foreach (Transform cubeTransform in InterActionTransform)
            {
                if (cubeTransform == InterActionTransform[index1]) continue; // 이미 중복 블록이 할당된 위치는 건너뛰기
                Debug.Log("Skipping duplicated block allocation at index: " + index1);

                int randomIndex = Random.Range(0, InteractionNumbers.Count); 
                int selectedNumber = InteractionNumbers[randomIndex]; 
                                                                    

                GameObject block = Instantiate(blockPrefabs[selectedNumber], cubeTransform.position, Quaternion.identity); //선택된 블럭프리펩을 위치에 할당
                block.transform.parent = cubeTransform;


                InteractionNumbers.RemoveAt(randomIndex); //할당된 숫자를 리스트에서 지우기

            }
        }
        else
        {
            Debug.LogError("Duplicated block with tag 'DuplicatedBlock' not found.");
        }




        //서로 다른 4개 그림 생성, 맞는 그림에 태그달려있음 > 태그 달려있는 그림만 정답처리

    }
}

