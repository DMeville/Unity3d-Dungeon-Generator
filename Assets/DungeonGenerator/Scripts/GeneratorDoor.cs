using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EL.Dungeon {
    public class GeneratorDoor : MonoBehaviour {
        public bool open = true;
        public GameObject voxelOwner;
        public GeneratorDoor sharedDoor;

        public Door door;
    }
}
