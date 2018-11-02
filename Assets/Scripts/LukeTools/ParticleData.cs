using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeTools
{
    [CreateAssetMenu (menuName = "ParticleData")]
    public class ParticleData : ScriptableObject, IMoveable
    {
        public IMoveable moveable_impl;
        public Vector3 Displacement;
        public Vector3 Force;
        public float Mass;
        public Vector3 Position;
        public Vector3 Velocity;
        public Vector3 Acceleration;

        private void OnEnable()
        {
            moveable_impl = new LinearMove(this);
        }
        public void Move(Transform t)
        {
            moveable_impl.Move(t);
        }
    }
}
