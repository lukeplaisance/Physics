using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeTools
{
    [CreateAssetMenu (menuName = "ParticleData")]
    public class ParticleData : ScriptableObject, IMovable
    {
        public IMovable movable_impl;
        public Vector3 Position;
        public Vector3 Velocity;
        public Vector3 Acceleration;
        public Vector3 Displacement;
        public Vector3 Force;
        public float Mass;

        public void Move(Vector3 pos, Vector3 vel, float dt)
        {
            movable_impl.Move(Position, Velocity, dt);
        }
    }
}
