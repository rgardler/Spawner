using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsCode.Models
{
    public class TreeHouse : MonoBehaviour
    {
        [Tooltip("The number of levels the this tree house will have.")]
        public int MinimumNumberOfLevels = 1;
        public int MaximumNumberOfLevels = 3;

        internal int levelHeight = 15;

        void Start()
        {
            Build();
        }

        /// <summary>
        /// Build this tree house to the correct configuration.
        /// </summary>
        public void Build()
        {
            GameObject level = transform.Find("Level 1").gameObject;
            Vector3 position;

            for (int i = 1; i < Random.Range(MinimumNumberOfLevels, MaximumNumberOfLevels + 1); i++)
            {
                GameObject newLevel = Instantiate(level, transform);
                newLevel.name = "Level " + (i + 1);

                position = transform.position;
                position.y += levelHeight * i;
                newLevel.transform.position = position;

                float yRotation = transform.rotation.y + Random.Range(30, 60);
                newLevel.transform.rotation = Quaternion.Euler(0, yRotation, 0);
                newLevel.transform.Find("Trunk").rotation = Quaternion.Euler(0, -yRotation, 0);
            }
        }
    }
}
