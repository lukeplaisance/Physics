using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeTools
{
    public class ParticleBehaviour : MonoBehaviour
    {
        IMovable movable;
        public ParticleData pd;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            movable.Move(transform.position, pd.Velocity, Time.deltaTime);
        }
    }
}
