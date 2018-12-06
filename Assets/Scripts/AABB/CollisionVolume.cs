using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AABB
{
    public class CollisionVolume
    {
        public Vector3 min;
        public Vector3 max;
        public bool isColliding = false;
        public Vector3 cvCenter;
        public Vector3 volume;
    }
}
