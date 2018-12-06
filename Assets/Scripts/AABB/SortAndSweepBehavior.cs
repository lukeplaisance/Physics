using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AABB
{
    public class SortAndSweepBehavior : MonoBehaviour
    {
        public List<CollisionVolume> xValues = new List<CollisionVolume>();
        public List<CollisionVolume> yValues = new List<CollisionVolume>();
        public List<CollisionVolume> activeList = new List<CollisionVolume>();
        public List<CollisionVolume> closedList = new List<CollisionVolume>();

        void Awake()
        {
            foreach(var volume in xValues)
            {
                activeList.Add(volume);
            }

            foreach(var volume in yValues)
            {
                activeList.Add(volume);
            }
        }

        private void OnDrawGizmos()
        {
            foreach (var cv in activeList)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(cv.volume, new Vector3(1.1f, 1.1f, 1.1f));
            }
        }


        public void Update()
        {
            CheckCollisionForXAxis();
            CheckCollisionForYAxis();
        }

        public void CheckCollisionForXAxis()
        {
            xValues.OrderBy(v => v.min);
            for(int i = 0; i < xValues.Count; i++)
            {
                foreach(var volume in xValues)
                {
                    if(activeList.Count >= 2)
                    {
                        if(activeList[0].min.x <= activeList[1].max.x)
                        {
                            closedList.Add(activeList[0]);
                            activeList.Remove(activeList[0]);
                            Debug.Log("collision on X axis");
                        }
                    }
                }
            }
        }

        public void CheckCollisionForYAxis()
        {
            yValues.OrderBy(v => v.min);
            for (int i = 0; i < yValues.Count; i++)
            {
                foreach (var volume in yValues)
                {
                    if (activeList.Count >= 2)
                    {
                        if (activeList[0].min.y <= activeList[1].max.y)
                        {
                            closedList.Add(activeList[0]);
                            activeList.Remove(activeList[0]);
                            Debug.Log("collision on Y axis");
                        }
                    }
                }
            }
        }
    }
}
