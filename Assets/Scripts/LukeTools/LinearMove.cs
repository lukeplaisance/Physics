using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeTools
{
    public class LinearMove : IMovable
    {
        public Transform t;
       
        public void Move(Vector3 pos, Vector3 vel, float dt)
        {
            t.position = vel;
        }
    }
}
