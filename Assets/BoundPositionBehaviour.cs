using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeTools
{
    public class BoundPositionBehaviour : MonoBehaviour
    {
        private BoidBehaviour b = new BoidBehaviour();
        private Vector3 v;

        
        public Vector3 Bound_Position(ParticleData b)
        {
            //min and max positions
            float Xmin = 0, Xmax = 1, Ymin = 0, Ymax = 1, Zmin = 0, Zmax = 1;

            float groundLevel = 0;

            Vector3 v = Vector3.zero;

            if (b.Position.y < groundLevel)
            {
                b.Position.y = groundLevel;
                b.isPerching = true;
            }

            if (b.Position.x < Xmin)
            {
                v.x = 1;
            }
            else if (b.Position.x > Xmax)
            {
                v.x = -1;
            }

            if (b.Position.y < Ymin)
            {
                v.y = 1;
            }
            else if (b.Position.x > Ymax)
            {
                v.y = -1;
            }

            if (b.Position.y < Zmin)
            {
                v.z = 1;
            }
            else if (b.Position.x > Zmax)
            {
                v.z = -1;
            }

            return v;
        }
    }
}
