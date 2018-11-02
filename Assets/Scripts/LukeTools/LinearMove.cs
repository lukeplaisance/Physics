using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeTools
{
    public class LinearMove : IMoveable
    {
        public ParticleData pd;

        public LinearMove(ParticleData particleData)
        {
            pd = particleData;
        }
      
        public void Move(Transform t)
        {
            pd.Acceleration = pd.Force * pd.Mass;
            pd.Velocity = pd.Velocity + pd.Acceleration * Time.deltaTime;
            t.position = pd.Velocity * Time.deltaTime;
        }
    }
}
