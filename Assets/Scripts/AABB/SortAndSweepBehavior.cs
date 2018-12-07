using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AABB
{
    public class SortAndSweepBehavior : MonoBehaviour
    {
        public List<CollisionVolume> AllVolumes = new List<CollisionVolume>();
        public List<CollisionVolume> activeList = new List<CollisionVolume>();
        public List<CollisionVolume> closedList = new List<CollisionVolume>();

        private void Start()
        {
            activeList = new List<CollisionVolume>();
            closedList = new List<CollisionVolume>();
        }

        public void Update()
        {
            CheckCollision();
        }

        public void CheckCollision()
        {
            for(int i = 0; i < AllVolumes.Count; i++)
            {
                if (!activeList.Contains(AllVolumes[i]))
                    activeList.Add(AllVolumes[i]);

                if (activeList.Count >= 2)
                {
                    if (activeList[0].min.x < activeList[1].max.x && activeList[0].max.x > activeList[1].min.x) 
                    {
                        if(activeList[0].min.y < activeList[1].max.y && activeList[0].max.y > activeList[1].min.y)
                        {
                            Debug.Log("collision");
                            activeList[0].isColliding = true;
                            activeList[1].isColliding = true;
                            closedList.Add(activeList[0]);
                            activeList.Remove(activeList[0]);
                        }
                        else
                        {
                            Debug.Log("collision on Y but no collision on X");
                            activeList[0].isColliding = false;
                            activeList[1].isColliding = false;
                            closedList.Add(activeList[0]);
                            activeList.Remove(activeList[0]);
                        }
                    }
                    else if (activeList[0].min.y < activeList[1].max.y && activeList[0].max.y > activeList[1].min.y)
                    {
                        Debug.Log("collision on X but no collision on Y");
                        activeList[0].isColliding = false;
                        activeList[1].isColliding = false;
                        closedList.Add(activeList[0]);
                        activeList.Remove(activeList[0]);
                    }
                    else
                    {
                        Debug.Log("No Collision");
                        activeList[0].isColliding = false;
                        activeList[1].isColliding = false;
                        closedList.Remove(AllVolumes[i]);
                    }
                }
            }
        }
    }
}
