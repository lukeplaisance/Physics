using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LukeTools
{
    public class BoidBehaviour : MonoBehaviour
    {
        [SerializeField]
        private List<ParticleData> Boids;
        public List<GameObject> gameObjects;

        //sets the positons of the boids to be in a random spot
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
            //declare vectors for the 3 boid rules methods
            Vector3 v1;
            Vector3 v2;
            Vector3 v3;

            //calls the boid rules methods
            foreach (var b in Boids)
            {
                Bound_Position(b);
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
            return (pc - b.Position) / 100;
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

        public Vector3 Bound_Position(ParticleData b)
        {
            float Xmin = 0, Xmax = 0, Ymin = 0, Ymax = 0, Zmin = 0, Zmax = 0;
            Vector3 v = new Vector3(0,0,0);

            if (b.Position.x < Xmin)
            {
                v.x = Screen.width;
            }
            else if(b.Position.x > Xmax)
            {
                v.x = -10;
            }

            if(b.Position.y < Ymin)
            {
                v.y = Screen.height;
            }
            else if (b.Position.x > Ymax)
            {
                v.y = -10;
            }

            if (b.Position.y < Zmin)
            {
                v.z = 10;
            }
            else if (b.Position.x > Zmax)
            {
                v.z = -10;
            }

            return v;
        }
    }
}
