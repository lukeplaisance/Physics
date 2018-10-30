using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeTools
{
    public interface IMovable
    {
        void Move(Vector3 pos, Vector3 vel, float dt);
    }
}
