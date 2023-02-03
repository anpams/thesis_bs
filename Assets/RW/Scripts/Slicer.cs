using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour
{
    public GameObject gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (gameManager){
            gameManager.GetComponent<GameManager>().score += 100;//slicing adds to the score
        }

        //haptic event
        ControllerHaptics haptics = GetComponentInParent<ControllerHaptics>();
        if(haptics){
            haptics.HapticEvent();
        }

        SplitMesh(other.gameObject);
        Destroy(other.gameObject);
    }

    // Get a cutting plane from the rotation/position of the saber
    private Plane GetPlane(GameObject go)
    {
        Plane rv = new Plane();

        //Unity has function that creates plane from 3 points on the plane
        //multiplying points by the quaternion rotation of the saber
        Vector3 pt1 = transform.rotation * new Vector3(0,0,0);
        Vector3 pt2 = transform.rotation * new Vector3(0,1,0);
        Vector3 pt3 = transform.rotation * new Vector3(0,0,1);

        //new plane to turn into cutting plane
        rv.Set3Points(pt1,pt2,pt3);

        return rv;
    }

    // Clone a Mesh "half"
    private Mesh CloneMesh(Plane p, Mesh oMesh, bool halve)
    {
        //
        Mesh cMesh = new Mesh();
        cMesh.name="slicedMesh";
        Vector3[] vertices = oMesh.vertices;
        for (int i=0; i< vertices.Length; i++)
        {
        // determine with side the the vertex falls on.
            bool side = p.GetSide(vertices[i]);
            if(side == halve){
                //if isn't on the side you want, map it back to the cutting plane
                vertices[i] = p.ClosestPointOnPlane(vertices[i]);
            }//the new mesh that appears represents only half of the object
        }

        //reassings cloned vertices, triangles, normals and UV
        //returns mesh as half.
        cMesh.vertices= vertices;
        cMesh.triangles = oMesh.triangles;
        cMesh.normals = oMesh.normals;
        cMesh.uv = oMesh.uv; //uv -> texture coordinates. 2d position within a texture

        return cMesh;
    }

    // Configure the GameObject
    GameObject MakeHalf(GameObject go, bool isLeft)
    {

        float sign = isLeft ? -1 : 1;
        GameObject half = Instantiate(go); //create copy with instantiate
        MeshFilter filter = half.GetComponent<MeshFilter>();

        Plane cuttingPlane = GetPlane(go);// Determines the cutting plane
        filter.mesh = CloneMesh(cuttingPlane, filter.mesh, isLeft); //clones the mesh tusing the cutting plane as slice

        half.transform.position = go.transform.position + transform.rotation * new Vector3(sign * 0.05f, 0, 0);
        half.GetComponent<Rigidbody>().isKinematic = false; //reenable physics by disabling isKinematic
        half.GetComponent<Rigidbody>().useGravity = true; //and enabling useGravity
        half.GetComponent<Collider>().isTrigger = false; //disable further subslicing (to prevent rapid cloning of subslices)

        Destroy(half, 2);
    	return half;
    }


    // Make two GameObjects with "halves" of the original
    private void SplitMesh(GameObject go)
    {
        GameObject leftHalf= MakeHalf(go, true); //makes two copies of the mesh for the illusion
        GameObject righHalf= MakeHalf(go, false);

        GetComponent<AudioSource>().Play(); //hit sound
     }


}