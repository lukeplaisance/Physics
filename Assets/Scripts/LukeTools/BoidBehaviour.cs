using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeTools
{
    //Change the argument of the Move function to an interface called IParticle
    public class BoidBehaviour : MonoBehaviour, IMoveable
    {
        public List<ParticleData> Boids;

        public void Move(Transform t)
        {
            
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
