using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeTools
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        IMovable movable;
        public Transform t;
        public ProjectileMovement pm;

        public Vector3 velocity;
        public float angle;
        public Vector3 height;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            movable.Move(t);
            pm.ProjectileMove(velocity, angle, height);
        }
    }
}
