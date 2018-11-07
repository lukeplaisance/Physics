using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeTools
{
    //Change the argument of the Move function to an interface called IParticle
    public class BoidBehaviour : MonoBehaviour
    {
        public List<ParticleData> Boids;


        public void MoveBoidsToNewPosition()
        {
            Vector3 v1;
            Vector3 v2;
            Vector3 v3;

            foreach (var b in Boids)
            {
                v1 = Boid_Cohesion(b);
                v2 = Boid_Dispersion(b);
                v2 = Boid_Allignment(b);

                b.Velocity = b.Velocity + v1 + v2 + v3;
                b.Position = b.Position + b.Velocity;
            }
        }

        public Vector3 Boid_Cohesion(ParticleData b)
        {
            var N = Boids.Count;

            Vector3 pc = new Vector3(0,0,0);

            foreach (var item in Boids)
            {
                if(item != b)
                    if((item.Position - b.Position).magnitude <= 1)
                    {
                        pc += item.Position;
                    }
            }
            pc = pc / (N - 1);
            return (pc - b.Position) / 100;
        }
    }
}
