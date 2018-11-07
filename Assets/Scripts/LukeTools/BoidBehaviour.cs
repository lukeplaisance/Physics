using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeTools
{
    //Change the argument of the Move function to an interface called IParticle
    public class BoidBehaviour : MonoBehaviour
    {
        [SerializeField]
        private List<ParticleData> Boids;
        public List<GameObject> gameObjects;

        private void Start()
        {
            foreach(var p in Boids)
            {
                p.Position = Vector3.zero;
                p.Position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
            }
        }

        private void LateUpdate()
        {
            MoveBoidsToNewPosition();
        }

        public void MoveBoidsToNewPosition()
        {
            Vector3 v1;
            Vector3 v2;
            Vector3 v3;

            foreach (var b in Boids)
            {
                v1 = Boid_Cohesion(b);
                v2 = Boid_Dispersion(b);
                v3 = Boid_Alignment(b);

                b.Velocity = b.Velocity + v1 + v2 + v3;

                if (b.Velocity.magnitude > 5)
                    b.Velocity = b.Velocity.normalized;

                b.Position = b.Position + b.Velocity;
                gameObjects[Boids.IndexOf(b)].transform.position = b.Position;
            }
        }

        public Vector3 Boid_Cohesion(ParticleData b)
        {
            //the number of boids
            var N = Boids.Count;

            //the perceived center
            Vector3 pc = new Vector3(0,0,0);

            //finds the average position of each boid
            foreach (var item in Boids)
            {
                if (item != b)
                {
                    pc += item.Position;
                }   
            }
            pc = pc / (N - 1);
            return (pc - b.Position) / 50;
        }

        public Vector3 Boid_Dispersion(ParticleData b)
        {
            //the displacement of each boid
            Vector3 c = new Vector3(0, 0, 0);

            //if the position of a boid is less thatn 1 units away from another, 
            //the boid will go the opposite way
            foreach (var item in Boids)
            {
                if(item != b)
                    if ((item.Position - b.Position).magnitude <= 1)
                    {
                        c = c - (item.Position - b.Position);
                    }
            }
            return c;
        }

        public Vector3 Boid_Alignment(ParticleData b)
        {
            //the number of boids
            var N = Boids.Count;

            //percieved velocity
            Vector3 pv = new Vector3(0,0,0);

            //finds the average velocity of each boid
            foreach(var item in Boids)
            {
                if(item != b)
                {
                    pv += b.Velocity;
                }
            }
            pv = pv / (N - 1);
            return (pv - b.Velocity) / 8;
        }
    }
}
