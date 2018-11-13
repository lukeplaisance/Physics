using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AABB
{
    public class CollisionVolume2DBehaviour : MonoBehaviour
    {
        
        private CollisionVolume2D cv = new CollisionVolume2D();

        public Vector2 min;
        public Vector2 max;
        private Vector2 c;

        private void Start()
        {
            min = cv.min;
            max = cv.max;

            c = (min + max) / 2;
        }

    }
}
