using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Yeon
{
    public class Player : MonoBehaviour
    {
        public BlockQuizController blockQuiz;


        private void Update()
        {
            OnMouseDown();
        }

        public void OnMouseDown()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.collider.CompareTag("Block"))
                {
                    return;
                }

                Block _hitBlock = hit.transform.GetComponent<Block>();
                _hitBlock.OntriggerBlock();

                if (blockQuiz.questBlock.blockColor == _hitBlock.blockColor)
                {
                    Debug.Log("¡§¥‰~");
                }
                else
                {
                    Debug.Log("∆≤∑»¿Ω;;");
                }

            }
        }
    }
}
