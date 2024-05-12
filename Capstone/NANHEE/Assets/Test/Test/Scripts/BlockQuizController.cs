using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Yeon
{
    public enum BlockColor
    {
        White,
        Red,
        Blue,
        Null = -1
    }

    public class BlockQuizController : MonoBehaviour
    {
        public GameObject blockGroup;

        public List<Block> blocks = new List<Block>();

        [HideInInspector]
        public Block questBlock;


        private void Start()
        {
            InitBlockQuiz();
        }

        public void InitBlockQuiz()
        {
            int childLength = blockGroup.transform.childCount;

            for (int i = 0; i < childLength; i++)
            {
                Block _block = blockGroup.transform.GetChild(i).GetComponent<Block>();
                blocks.Add(_block);
            }
        }
    }
}

