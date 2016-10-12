using UnityEngine;
using System.Collections;

namespace Inferances{
    public interface IBoid
    {
        float mass { get; set; }
        Vector3 velocity { get; set; }
        Vector3 position { get; set; }
    }

}
