using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LukeTools
{
    [CreateAssetMenu(menuName = "ProjectileMovement")]
    public class ProjectileMovement : ScriptableObject
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
        private float time;
        private float speed;

        public Vector3 ProjectileMove(Vector3 velocity, float angle, Vector3 initial_height)
        {
            time = Time.deltaTime;
            current_velocity.x = initial_velocity.x * Mathf.Cos(angle);
            current_velocity.y = initial_velocity.y * Mathf.Sin(angle);

            //calulating the magnitude of the velocity
            float velocity_mag = (current_velocity - initial_velocity).magnitude;

            //calculating the speed
            speed = velocity_mag / (2.0f * gravity).magnitude;
            velocity_mag = 2.0f * gravity.magnitude * speed;

            //calculating the current position
            current_position.x = initial_position.x + (initial_velocity.x * time); 
            current_position.y = initial_position.y + (initial_velocity.y * time) +
                                  (1 / 2 * (gravity.magnitude * time));

            return current_position;
        }

    }

    [CustomEditor(typeof(ProjectileMovement))]
    public class ProjectileMovementEditor : Editor
    {
        private ProjectileMovement pm;

        public override void OnInspectorGUI()
        {
            pm = (ProjectileMovement)target;

            if (GUILayout.Button("Calculate"))
            {
                pm.ProjectileMove(pm.initial_velocity, pm.angle, pm.initial_height);
            }
            GUILayout.Box(" End Position " + pm.current_position.ToString());
        }
    }
}

