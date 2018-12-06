using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LukeTools;


namespace AABB
{
    public class CollisionVolumeBehavior : MonoBehaviour
    {
        
        private CollisionVolume cv = new CollisionVolume();

        public Vector3 min;
        public Vector3 max;

        private void Start()
        {
            min = cv.min;
            max = cv.max;

            cv.volume = (min + max);
            cv.cvCenter = (min + max) / 2;
        }

        private void Update()
        {
            cv.cvCenter = transform.position;
            Debug.Log(min);
            Debug.Log(max);
        }
    }
}
