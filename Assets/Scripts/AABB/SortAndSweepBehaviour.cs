using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AABB
{
    public class SortAndSweepBehaviour : MonoBehaviour
    {
        public List<CollisionVolume2D> xValues;
        private List<CollisionVolume2D> yValues;
        public List<CollisionVolume2D> activeList;
        public List<CollisionVolume2D> closedList;

        // Use this for initialization
        void Start()
        {
            xValues = new List<CollisionVolume2D>(GetComponents<CollisionVolume2D>());
            yValues = new List<CollisionVolume2D>(GetComponents<CollisionVolume2D>());
        }

        public void CheckCollisionForXAxis()
        {
            xValues.OrderBy(v => v.min);
            for(int i = 0; i < xValues.Count; i++)
            {
                foreach(var volume in xValues)
                {
                    activeList.Add(volume);
                    if(activeList.Count >= 2)
                    {
                        if(activeList[0].min.x < activeList[1].max.x)
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
                    activeList.Add(volume);
                    if (activeList.Count >= 2)
                    {
                        if (activeList[0].min.y < activeList[1].max.y)
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
