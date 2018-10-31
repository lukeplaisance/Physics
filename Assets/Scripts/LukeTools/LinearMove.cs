using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeTools
{
    public class LinearMove : IMovable
    {
        public ParticleData pd;
       
        public void Move(Transform t)
        {
            pd.Acceleration = pd.Force * pd.Mass;
            pd.Velocity = pd.Velocity + pd.Acceleration * Time.deltaTime;
            t.position = pd.Velocity * Time.deltaTime;
        }
    }
}
