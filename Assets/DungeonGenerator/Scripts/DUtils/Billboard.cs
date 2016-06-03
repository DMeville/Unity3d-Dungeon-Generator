using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Billboard : MonoBehaviour {

    public bool X = true;
    public bool Y = true;
    public bool Z = true;

	// Use this for initialization
	void Start () {
        Vector3 rot = Camera.main.transform.rotation.eulerAngles;
        if (!X) {
            rot.x = 0f;
        }
        if (!Y) {
            rot.y = 0f;
        }
        if (!Z) {
            rot.z = 0f;
        }

        Quaternion q = this.gameObject.transform.rotation;
        q.eulerAngles = rot;
        this.gameObject.transform.rotation = q;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 rot = Camera.main.transform.rotation.eulerAngles;
        if (!X) {
            rot.x = 0f;
        }
        if (!Y) {
            rot.y = 0f;
        }
        if (!Z) {
            rot.z = 0f;
        }

        Quaternion q = this.gameObject.transform.rotation;
        q.eulerAngles = rot;
        this.gameObject.transform.rotation = q;
	}
}
