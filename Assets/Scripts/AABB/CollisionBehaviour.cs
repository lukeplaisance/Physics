using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AABB
{
    public class CollisionBehaviour : MonoBehaviour
    {
        public CollisionVolume2D cv;

        private List<Vector2> xValues;
        private List<Vector2> yValuse;
        public List<CollisionVolume2D> activeList;
        public List<CollisionVolume2D> closedList;

        public void Empty()
        {
            
        }

        //adds a point to the AABB if necessary to contain the shape
        public void Add(Vector3 vec)
        {
            if (vec.x < cv.min.x)
                cv.min.x = vec.x;

            if (vec.y < cv.min.y)
                cv.min.y = vec.y;
        }
    }
}
