using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EL.Dungeon { 
    public class Room : MonoBehaviour {

        public List<EL.Dungeon.GeneratorDoor> doors;

        //WARNING - when doing the dungeon gen we sometimes Instantiate a room, check if it will fit and if it doesn't
        //we IMMEDIATLY destroy it.  Awake() is called with instantiation - Start() waits until the function returns..
        //SO to be safe, don't use Awake if you don't have to.  Put all enemy and room specific instantiation in START!
        void Awake() {
            //DungeonGenerator.roomsCalledStart++;
            //Debug.Log("AWAKE");
        }

	    void Start () {
            DungeonGenerator.roomsCalledStart++;
            //Debug.Log("START");

	    }
	
	    void Update () {
	
	    }

         private void OnDrawGizmos() {
             for (int i = 0; i < doors.Count; i++) {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(doors[i].transform.position, 0.1f);

                Gizmos.DrawRay(new Ray(doors[i].transform.position, doors[i].transform.right));

                Gizmos.color = Color.green;
                Gizmos.DrawRay(new Ray(doors[i].transform.position, doors[i].transform.up));

                Gizmos.color = Color.blue;
                Gizmos.DrawRay(new Ray(doors[i].transform.position, doors[i].transform.forward));
             }
        }

         public GeneratorDoor GetRandomDoor(DRandom random) {
             doors.Shuffle(random.random);
             for (int i = 0; i < doors.Count; i++) {
                 if (doors[i].open) return doors[i];
             }
             Debug.LogError("Room::GetRandomDoor() - No open doors...");
             return null;
         }

         public bool hasOpenDoors() {
             for (int i = 0; i < doors.Count; i++) {
                 if (doors[i].open) return true;
             }
             return false;
         }

         public GeneratorDoor GetFirstOpenDoor() {
             for (int i = 0; i < doors.Count; i++) {
                 if (doors[i].open) return doors[i];
             }
             Debug.LogError("Room::GetFirstOpenDoor() - No open doors...");
             return null;
         }
    }
}
