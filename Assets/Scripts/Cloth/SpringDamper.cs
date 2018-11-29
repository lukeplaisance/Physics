using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LukeTools;

namespace Cloth
{
    [System.Serializable]
    public class SpringDamper
    {
        private float ks, kd; //spring constant, damping factor
        private float lo; //Rest length
        public float l; //unit length
        public Particle p1, p2; //particle 1, particle 2
        private Vector3 e; //unit length vector
        private Vector3 ePrime;

        public SpringDamper(Particle a, Particle b)
        {
            p1 = a;
            p2 = b;
            lo = Vector3.Distance(p1.Position, p2.Position);
            ks = 50;
            kd = 2;
        }

        public void Update()
        {
            //calculate the unit length vector between the two particle
            ePrime = p2.Position - p1.Position;
            l = ePrime.magnitude;
            e = ePrime.normalized;

            //calculate the 1D velocities
            var v1 = Vector3.Dot(e, p1.Velocity);
            var v2 = Vector3.Dot(e, p2.Velocity);

            //convert from 1D to 3D
            var hooksLaw = -ks * (lo - l) - kd * (v1 - v2);
            float fs = -ks * (lo - l);
            float fd = -kd * (v1 - v2);
            var f1 = (fs + fd) * e;
            var f2 = -f1;

            p1.AddForce(f1);
            p2.AddForce(f2);            
        }
    }
}
