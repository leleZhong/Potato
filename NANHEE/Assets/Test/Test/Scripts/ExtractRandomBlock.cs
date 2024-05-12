using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yeon
{
    public class ExtractRandomBlock : MonoBehaviour
    {
        public BlockQuizController blockQuiz;

        public List<Block> blocks = new List<Block>();

        public Transform blockPos;
        public Block randomBlock;


        private void Start()
        {
            Init();
        }

        public void Init()
        {
            if (randomBlock != null)
            {
                Destroy(randomBlock);
            }

            System.Random random = new System.Random();
            int blocksLength = blocks.Count;
            int randomInt = random.Next(0, blocksLength);
            randomBlock = Instantiate(blocks[randomInt]);

            randomBlock.transform.SetParent(blockPos);
            randomBlock.transform.localPosition = Vector3.zero;
            randomBlock.gameObject.SetActive(true);
            randomBlock.boxCollider.enabled = false;

            blockQuiz.questBlock = randomBlock;
        }
    }
}
