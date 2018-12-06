using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AABB
{
    public class CollisionVolume : MonoBehaviour
    {
        public Vector3 min;
        public Vector3 max;
        public Vector3 size;
        public bool isColliding = false;

        private void Update()
        {
            min.x = transform.position.x - size.x;
            max.x = transform.position.x + size.x;
            min.y = transform.position.x - size.y;
            max.y = transform.position.x + size.y;
        }
    }
}
