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

        private void OnEnable()
        {
            m_spawner = (Spawner)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

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
        }
    }
}
