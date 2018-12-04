using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeTools
{
    [System.Serializable]
    public class Particle
    {
        public Vector3 Velocity { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Acceleration { get; set; }
        public Vector3 Displacement { get; set; }
        public Vector3 Force { get; set; }
        public float Mass { get; set; }
        public bool isAnchor;
        public bool isGrabbed;
        public bool isActive;

        public Particle(Vector3 pos)
        {
            Position = pos;
            Velocity = new Vector3(0, -1, 0);
            Mass = 1;
            isActive = true;
            isGrabbed = false;
        }

        public void AddForce(Vector3 force)
        {
            Force += force;            
        }

        public void Update(float dt)
        {
            var distance = Vector3.Distance(Position, Input.mousePosition);
            if(distance <= .1f && Input.GetMouseButton(0))
            {
                isGrabbed = true;
            }

            if (isAnchor)
            {
                Force = Vector3.zero;
                return;
            }
            Acceleration = Force * Mass;
            Velocity = Velocity + Acceleration * dt;
            Position += Velocity * dt;
               
            Force = Vector3.zero;
        }
    }
}
