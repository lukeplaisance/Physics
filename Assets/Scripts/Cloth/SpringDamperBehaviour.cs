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
            springDamper = new SpringDamper();
            springDamper.p1 = p1.particle;
            springDamper.p2 = p2.particle;
        }

        // Update is called once per frame
        void Update()
        {
            springDamper.Update();
        }
    }
}
