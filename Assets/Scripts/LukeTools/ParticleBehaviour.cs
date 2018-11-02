using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeTools
{
    public class ParticleBehaviour : MonoBehaviour
    {
        IMoveable moveable;
        public Transform t;
        public ParticleData pd;
       
        // Use this for initialization
        void Start()
        {
            moveable = new LinearMove(pd);
        }

        // Update is called once per frame
        void Update()
        {
            moveable.Move(t);
        }
    }
}
