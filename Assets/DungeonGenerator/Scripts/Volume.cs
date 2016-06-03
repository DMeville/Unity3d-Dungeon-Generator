using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EL.Dungeon {
    [System.Serializable]
    public class Volume : MonoBehaviour {

        public Vector3 generatorSize = new Vector3(1f, 1f, 1f);
        public List<GameObject> voxels = new List<GameObject>();

        public static bool drawVolume = false;

        public GameObject gameObjectContainer;
        public Bounds bounds;

        public float voxelScale = 10f;

       
        public void OnDrawGizmos() {
            if (!drawVolume) {
                if (bounds == null) return;
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(bounds.center, bounds.size);
            } else {
                if (gameObjectContainer == null) return;
                for (int i = 0; i < gameObjectContainer.transform.childCount; i++) {
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireCube(gameObjectContainer.transform.GetChild(i).transform.position, Vector3.one * voxelScale);
                }
            }
        }

       
        [ContextMenu("Generate Voxel Grid")]
        public void GenerateVoxelGrid() {
            if(voxels.Count != 0){
                for (int i = 0; i < voxels.Count; i++) {
                    DestroyImmediate(voxels[i]);
                }
            }

            if (gameObjectContainer == null) {
                gameObjectContainer = new GameObject("Voxels");
                gameObjectContainer.transform.parent = this.transform;
            }

            voxels = new List<GameObject>();
            for (int i = 0; i < generatorSize.x; i++) {
                for (int j = 0; j < generatorSize.y; j++) {
                    for (int k = 0; k < generatorSize.z; k++) {
                        CreateVoxel(i * (int)voxelScale,
                                    j * (int)voxelScale,
                                    k * (int)voxelScale);
                    }
                }
            }
        }

        private void CreateVoxel(int i, int j, int k) {
            GameObject voxel = new GameObject(string.Format("Voxel - ({0}, {1}, {2})", i, j, k));
            voxel.transform.position = new Vector3(i, j, k);
            voxel.transform.parent = gameObjectContainer.transform;
        }

        [ContextMenu("Assign Voxels")]
        public void AssignVoxelsToList() {

            Vector3 min = new Vector3(gameObjectContainer.transform.GetChild(0).transform.position.x,
                                            gameObjectContainer.transform.GetChild(0).transform.position.y,
                                            gameObjectContainer.transform.GetChild(0).transform.position.z);

            Vector3 max = new Vector3(gameObjectContainer.transform.GetChild(0).transform.position.x,
                                            gameObjectContainer.transform.GetChild(0).transform.position.y,
                                            gameObjectContainer.transform.GetChild(0).transform.position.z);

            for (int i = 0; i < gameObjectContainer.transform.childCount; i++) {
                voxels.Add(gameObjectContainer.transform.GetChild(i).gameObject);
                Vector3 pos = gameObjectContainer.transform.GetChild(i).transform.position;

                if (pos.x < min.x) min.x = pos.x;
                if (pos.y < min.y) min.y = pos.y;
                if (pos.z < min.z) min.z = pos.z;

                if (pos.x > max.x) max.x = pos.x;
                if (pos.y > max.y) max.y = pos.y;
                if (pos.z > max.z) max.z = pos.z;
            }

            Debug.Log("Voxel::AssignVoxelsToList() | " + min + " : " + max);

            Vector3 size = new Vector3(0.5f * voxelScale, 0.5f * voxelScale, 0.5f * voxelScale);
            bounds = new Bounds((min + max)/2f, ((max + size) - (min - size))); 
        }

        public void RecalculateBounds() {

            Vector3 min = new Vector3(voxels[0].transform.position.x,
                                      voxels[0].transform.position.y,
                                      voxels[0].transform.position.z);

            Vector3 max = new Vector3(voxels[0].transform.position.x,
                                      voxels[0].transform.position.y,
                                      voxels[0].transform.position.z);

            for (int i = 0; i < voxels.Count; i++) { 
                    Vector3 pos = voxels[i].transform.position;

                    if (pos.x < min.x) min.x = pos.x;
                    if (pos.y < min.y) min.y = pos.y;
                    if (pos.z < min.z) min.z = pos.z;

                    if (pos.x > max.x) max.x = pos.x;
                    if (pos.y > max.y) max.y = pos.y;
                    if (pos.z > max.z) max.z = pos.z;
                }

            //Debug.Log("Voxel::RecalculateBounds() | " + min + " : " + max);

            Vector3 size = new Vector3(0.5f * voxelScale, 0.5f * voxelScale, 0.5f * voxelScale);
            bounds = new Bounds((min + max) / 2f, ((max + size) - (min - size))); 
        }
        
        [ContextMenu("Toggle Gizmo Mode")]
        public void ToggleGizmoToDraw() {
            Volume.drawVolume = !Volume.drawVolume;
        }
    }
}
