using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class SpringDamperBehaviour : MonoBehaviour
    {
        [SerializeField]
        public SpringDamper springDamper;
        public ClothParticleBehaviour p1, p2;

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

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(p1.particle.Position, p2.particle.Position);
        }
    }
}
