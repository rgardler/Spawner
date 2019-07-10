using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WizardsCode.Spawner
{
    public class Spawner : MonoBehaviour
    {
        public enum Pattern { RandomWithinRadius, Grid }

        [Tooltip("The prefabs to spawn. One will be selected at random, based on the settings in the SpawnableObject.")]
        public SpawnableObject[] SpawnableObjects;

        [Header("Spawn Characteristics")]
        [Tooltip("The pattern used to spawn objects")]
        public Pattern SpawnPattern;

        [Tooltip("The number of this object to spawn.")]
        public int Quantity = 1;

        [Header("Grid Settings")]
        [Tooltip("Spacing between spawn points of objects in the grid.")]
        public float GridSpacing = 1;
        [Tooltip("Row length. Only used for Grid based systems.")]
        public int RowLength = 5;

        [Header("Radius Settings")]
        [Tooltip("The radius of the circle within which this spawner will create objects. Set to 0 to spawn only on the position above.")]
        public float Radius = 0;

        List<GameObject> m_spawnedObjects = new List<GameObject>();

        int m_totalWeight = 0;

        void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            m_totalWeight = 0;
            for (int i = 0; i < SpawnableObjects.Length; i++)
            {
                m_totalWeight += SpawnableObjects[i].weight;
            }

            switch (SpawnPattern)
            {
                case Pattern.RandomWithinRadius:
                    RandomWithinRadius();
                    break;
                case Pattern.Grid:
                    Grid();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void Grid()
        {
            int numOfCompleteRows = (Quantity / RowLength);
            int numOfColumns;
            if (numOfCompleteRows > 0)
            {
                numOfColumns = (Quantity / (numOfCompleteRows * RowLength));
            }
            else
            {
                numOfColumns = 1;
            }
            int lastRowCount = Quantity - ((numOfCompleteRows * RowLength) * numOfColumns);

            float xCornerOffset;
            if (lastRowCount > 0)
            {
                xCornerOffset = (numOfCompleteRows * GridSpacing) / 2;
            } else
            {
                xCornerOffset = ((numOfCompleteRows - 1) * GridSpacing) / 2;
            }
            float zCornerOffset;
            if (numOfCompleteRows == 0)
            {
                zCornerOffset = ((lastRowCount - 1) * GridSpacing) / 2;
            }
            else
            {
                zCornerOffset = ((RowLength - 1) * GridSpacing) / 2;
            }

            for (int i = 0; i < numOfColumns; i++)
            {
                for (int x = 0; x < numOfCompleteRows; x++)
                {
                    for (int z = 0; z < RowLength; z++)
                    {
                        Vector3 pos = transform.position;
                        pos.x += (GridSpacing * x) - xCornerOffset;
                        pos.z += (GridSpacing * z) - zCornerOffset;
                        SpawnAt(pos);
                    }
                }
            }

            if (lastRowCount > 0)
            {
                for (int z = 0; z < lastRowCount; z ++)
                {
                    Vector3 pos = transform.position;
                    pos.x += (GridSpacing * numOfCompleteRows) - xCornerOffset;
                    pos.z += (GridSpacing * z) - zCornerOffset;
                    SpawnAt(pos);

                }
            }
        }


        private void RandomWithinRadius()
        {
            for (int i = 0; i < Quantity; i++)
            {
                Vector3 pos = transform.position;
                if (Radius > 0)
                {
                    Vector2 circlePos = Random.insideUnitCircle * Radius;
                    pos.x += circlePos.x;
                    pos.z += circlePos.y;
                }
                SpawnAt(pos);
            }
        }

        /// <summary>
        /// Spawn an object at the supplied position after applying any offset required.
        /// </summary>
        /// <param name="pos"></param>
        private void SpawnAt(Vector3 pos)
        {
            // Select the object to spawn
            int weight = 0;
            int itemWeight = Random.Range(1, m_totalWeight);
            int idx;
            for (idx = 0; idx < SpawnableObjects.Length; idx++)
            {
                weight += SpawnableObjects[idx].weight;
                if (itemWeight <= weight)
                {
                    break;
                } 
            }

            // Calculate Rotation
            Quaternion rotation;
            if (SpawnableObjects[idx].IsRandomRotation)
            {
                rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            }
            else
            {
                rotation = Quaternion.identity;
            }

            // Calculate position
            pos.y += SpawnableObjects[idx].yOffset;

            // Spawn object
            GameObject go = Instantiate(SpawnableObjects[idx].Prefab);
            go.transform.localRotation = rotation;
            go.transform.position = pos;
            go.transform.SetParent(transform);

            m_spawnedObjects.Add(go);
        }

        /// <summary>
        /// Destroy all previously spawned objects.
        /// </summary>
        public void DestroyObjects()
        {
            for(int i = 0; i < m_spawnedObjects.Count; i++)
            {
                DestroyImmediate(m_spawnedObjects[i]);
            }
        }
    }
}
