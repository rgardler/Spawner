using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WizardsCode.Spawner
{
    [CreateAssetMenu(fileName = "Spawnable Object", menuName = "Digital Painting/Spawner/Spawnable Object")]
    public class SpawnableObject : ScriptableObject
    {
        [Tooltip("The prefab to spawn.")]
        public GameObject Prefab;

        [Tooltip("Weight affects the likelihood of this object being spawned in any given location. The higher the weight, relative to other Spawnable Objects the more likley it is to be spawned.")]
        [Range(1, 100)]
        public int weight = 50;

        [Tooltip("The Y axies offset to use when deciding the spawn location. This will be aded to the Y spwarn coordinate.")]
        public float yOffset = 0;

        [Tooltip("Randomize the rotation or keep all spawned units uniform.")]
        public bool IsRandomRotation = true;
    }
}
