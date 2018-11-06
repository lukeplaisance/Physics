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
        public Vector3 gravity;

        public float angle;
        public float time;
        public float speed;
    }
}
