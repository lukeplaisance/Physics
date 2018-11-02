using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeTools
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        public ProjectileMovement pm;
        public ParticleData pd;

        public Vector3 velocity;
        public float angle;
        public Vector3 height;

        // Use this for initialization
        void Start()
        {
            pm = new ProjectileMovement();
            velocity = pm.initial_velocity;
            angle = pm.angle;
            height = pm.initial_height;
        }

        // Update is called once per frame
        void Update()
        {
            pm.ProjectileMove(velocity, angle, height);
        }
    }
}
