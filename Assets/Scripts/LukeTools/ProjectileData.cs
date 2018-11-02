using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LukeTools
{
    [CreateAssetMenu(menuName = "ProjectileData")]
    public class ProjectileData : ScriptableObject
    {
        public Vector3 initial_velocity;
        public Vector3 current_velocity;

        public Vector3 initial_position;
        public Vector3 current_position;

        public Vector3 velocity;
        public Vector3 accel;
        public Vector3 initial_height;
        public Vector3 current_height;

        //gravity is constant
        public Vector3 gravity = new Vector3(0, -9.81f, 0);

        public float angle;
        public float time;
        public float speed;
    }

    [CustomEditor(typeof(ProjectileData))]
    public class ProjectileMovementEditor : Editor
    {
        private ProjectileBehaviour pb = new ProjectileBehaviour();
        public ProjectileData pd;

        public override void OnInspectorGUI()
        {
            pd = (ProjectileData)target;

            if (GUILayout.Button("Calculate"))
            {
                pb.ProjectileMove(pd.initial_velocity, pd.angle, pd.initial_height);
            }
            GUILayout.Box(" End Position " + pd.current_position.ToString());
        }
    }
}

