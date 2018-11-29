using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LukeTools;

namespace Cloth
{
    public class ClothGenerator : MonoBehaviour
    {
        private List<Particle> Particles = new List<Particle>();
        private List<SpringDamper> Springs = new List<SpringDamper>();
        public float width;
        public float height;


        // Use this for initialization
        void Awake()
        {
            for (float x = 0; x < width; x++)
            {
                for (float y = 0; y < height; y++)
                {
                    Particles.Add(new Particle(new Vector3(x, y, 0)));
                }
            }

            var allSprings = (width - 1) * height + (height - 1) * width;

            for (int i = 0; i < Particles.Count; i++)
            {
                Springs.Add(new SpringDamper(Particles[i], Particles[i++]));
            }

            foreach(var particle in Particles)
            {
                if (particle.Position.y == height - 1)
                {
                    particle.isAnchor = true;
                }
            }
        }
        void OnDrawGizmos()
        {
            foreach(var p in Particles)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(p.Position, .25f);
            }
           
        }

        // Update is called once per frame
        void Update()
        {
            foreach(var s in Springs)
            {
                s.Update();
            }

            foreach (var particle in Particles)
            {
                var gravity = new Vector3(0, -9.81f, 0);
                particle.AddForce(gravity * 0.25f);
                particle.Update(Time.deltaTime);
                transform.position = particle.Position;
            }
        }

    }
}
