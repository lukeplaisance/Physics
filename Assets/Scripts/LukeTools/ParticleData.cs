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
        public bool isPerching = false;
        public float perch_timer;

        private void OnEnable()
        {
            moveable_impl = new LinearMove(this);
            perch_timer = Random.Range(1, 5);
        }
        public void Move(Transform t)
        {
            moveable_impl.Move(t);
        }
    }
}
