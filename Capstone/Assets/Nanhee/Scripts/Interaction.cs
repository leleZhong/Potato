using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{

    //4��?�� ť�긦 ���� 3���� ����, 1���� ����
    //���信 ��ȣ�ۿ��ϸ� ������ -> UI correct ǥ��
    //���信 ��ȣ�ۿ��ϸ� �� �ȿ��� -> ���� ǥ�ó����� > �г�Ƽ



    public Transform[] InterActionTransform;
    public GameObject[] blockPrefabs; // �� ������ �迭
    



    void Start()
    {


        Invoke("MakeQuestion", 0.1f);

    }

        void MakeQuestion()
        {
            List<int> InteractionNumbers = new List<int>() { 0, 1, 2, 3 }; // ���� �ĺ� ��ȣ��
            GameObject duplicatedBlock = GameObject.FindWithTag("CorrectNumber");

            if (duplicatedBlock != null)
            {

                int index1 = Random.Range(0, InterActionTransform.Length);
                GameObject correctAnswerBlock = Instantiate(duplicatedBlock, InterActionTransform[index1].position, Quaternion.identity); // ��ü ����
                InteractionNumbers.RemoveAt(index1); //�����鸮��Ʈ��������

                Debug.Log("Object instantiated at position: " + InterActionTransform[index1].position);

                correctAnswerBlock.transform.parent = InterActionTransform[index1];
                correctAnswerBlock.transform.localPosition = Vector3.zero;

                foreach (Transform cubeTransform in InterActionTransform)
                {
                    if (cubeTransform == InterActionTransform[index1]) continue; // �̹� �ߺ� ����� �Ҵ�� ��ġ�� �ǳʶٱ�
                    Debug.Log("Skipping duplicated block allocation at index: " + index1);

                    int randomIndex = Random.Range(0, InteractionNumbers.Count);
                    int selectedNumber = InteractionNumbers[randomIndex];


                    GameObject block = Instantiate(blockPrefabs[selectedNumber], cubeTransform.position, Quaternion.identity); //���õ� ���������� ��ġ�� �Ҵ�
                    block.transform.parent = cubeTransform;


                    InteractionNumbers.RemoveAt(randomIndex); //�Ҵ�� ���ڸ� ����Ʈ���� �����

                }
            }
            else
            {
                Debug.LogError("Duplicated block with tag 'DuplicatedBlock' not found.");
            }




            //���� �ٸ� 4�� �׸� ����, �´� �׸��� �±״޷����� > �±� �޷��ִ� �׸��� ����ó��

        }
    }


