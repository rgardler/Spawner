using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WizardsCode.Spawner
{
    [CustomEditor(typeof(Spawner))]
    public class SpawnerEditor : Editor
    {
        Spawner m_spawner;
        bool m_destroyBeforeSpawn = true;

        SerializedProperty spawnableObjects;
        SerializedProperty spawnPattern;
        SerializedProperty quantity;

        // Grid Settings
        SerializedProperty gridSpacing;
        SerializedProperty rowLength;

        // Random Within Radius
        SerializedProperty radius;

        private void OnEnable()
        {
            m_spawner = (Spawner)target;
            spawnableObjects = serializedObject.FindProperty("SpawnableObjects");
            spawnPattern = serializedObject.FindProperty("SpawnPattern");
            quantity = serializedObject.FindProperty("Quantity");

            gridSpacing = serializedObject.FindProperty("GridSpacing");
            rowLength = serializedObject.FindProperty("RowLength");

            radius = serializedObject.FindProperty("Radius");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(spawnableObjects, true);
            EditorGUILayout.PropertyField(spawnPattern);
            EditorGUILayout.PropertyField(quantity);

            switch (m_spawner.SpawnPattern)
            {
                case Spawner.Pattern.Grid:
                    EditorGUILayout.PropertyField(gridSpacing);
                    EditorGUILayout.PropertyField(rowLength);
                    break;

                case Spawner.Pattern.RandomWithinRadius:
                    EditorGUILayout.PropertyField(radius);
                    break;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Editor Only", EditorStyles.boldLabel);

            m_destroyBeforeSpawn = GUILayout.Toggle(m_destroyBeforeSpawn, "Destroy Before Spawn?");

            if (GUILayout.Button("Spawn"))
            {
                if (m_destroyBeforeSpawn)
                {
                    m_spawner.DestroyObjects();
                }
                m_spawner.Spawn();
            }


            if (GUILayout.Button("Destroy All"))
            {
                m_spawner.DestroyObjects();
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
