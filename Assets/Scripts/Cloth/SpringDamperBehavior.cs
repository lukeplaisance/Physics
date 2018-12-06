using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Cloth
{
    public class SpringDamperBehavior : MonoBehaviour
    {
        [SerializeField]
        public SpringDamper springDamper;
        public ParticleBehavior p1, p2;

        // Use this for initialization
        void Start()
        {
            p1.particle.isAnchor = true;
            springDamper = new SpringDamper(p1.particle, p2.particle);
        }

        // Update is called once per frame
        void Update()
        {
            springDamper.Update();
        }
    }
}
