using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestFallingObject : MonoBehaviour
{
     public int numberOfFragments = 10;
    public Material fragmentMaterial;
    public float explosionForce = 10f;
    public float explosionRadius = 5f;
    public float disperseSpeed = 1f; // Speed of dispersion
    private List<GameObject> fragments = new List<GameObject>();

    void OnCollisionEnter(Collision collision)
    {
        // Check for a specific collision (optional)
        if (collision.relativeVelocity.magnitude > 1.0f) // Adjust threshold as necessary
        {
            Shatter();
        }

        Destroy(gameObject);
    }

    void Shatter()
    {
        // Get the original object's mesh filter and mesh
        MeshFilter originalMeshFilter = GetComponent<MeshFilter>();
        Mesh originalMesh = originalMeshFilter.mesh;

        // Get the original object's vertices
        Vector3[] vertices = originalMesh.vertices;

        // Check if we have enough vertices to create fragments
        if (vertices.Length < 4)
        {
            Debug.LogWarning("Not enough vertices to create fragments!");
            return;
        }

        // Create fragments
        for (int i = 0; i < numberOfFragments; i++)
        {
            // Create a new fragment GameObject
            GameObject fragment = new GameObject("Fragment");

            // Add components to the fragment
            MeshFilter fragmentMeshFilter = fragment.AddComponent<MeshFilter>();
            MeshRenderer fragmentMeshRenderer = fragment.AddComponent<MeshRenderer>();

            // Create a new mesh for the fragment
            Mesh fragmentMesh = new Mesh();

            // Create fragment vertices and triangles
            Vector3[] fragmentVertices = new Vector3[5];
            //triangle indices??? 
            int[] triangles = new int[] { 0, 1, 2, 0, 2, 3,2,3,4 }; // Simple quad

            // Assign positions for the fragment vertices
            // Using original vertices for this example
            fragmentVertices[0] = vertices[0] + Random.insideUnitSphere * 0.1f;
            fragmentVertices[1] = vertices[1] + Random.insideUnitSphere * 0.1f;
            fragmentVertices[2] = vertices[2] + Random.insideUnitSphere * 0.1f;
            fragmentVertices[3] = vertices[3] + Random.insideUnitSphere * 0.1f;

            // Assign vertices and triangles to the fragment mesh
            fragmentMesh.vertices = fragmentVertices;
            fragmentMesh.triangles = triangles; // This must be a multiple of 3

            // Calculate normals for lighting
            fragmentMesh.RecalculateNormals();

            // Assign the new fragment mesh to the fragment's mesh filter
            fragmentMeshFilter.mesh = fragmentMesh;

            // Set the material for the fragment
            fragmentMeshRenderer.material = fragmentMaterial;

            // Set a random scale for the fragment
            fragment.transform.localScale = new Vector3(1000,1000,1000);

            fragment.transform.position = new Vector3(0,0,0);

            // Optional: Set the fragment to be kinematic so it doesn't fall
            Rigidbody rb = fragment.AddComponent<Rigidbody>();
            fragment.AddComponent<BoxCollider>();
            fragment.GetComponent<BoxCollider>().size = new Vector3(0.01f,0.01f,0.01f);
            // rb.useGravity = false; 
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                        // Add the fragment to the list
            fragments.Add(fragment);
        }

        // Destroy the original object after shattering
        Destroy(gameObject);

        // Start the coroutine to disperse fragments
        StartCoroutine(DisperseFragments());
    }

    private IEnumerator DisperseFragments()
    {
        // While there are fragments to disperse
        while (fragments.Count > 0)
        {
            foreach (var fragment in fragments)
            {
                if (fragment != null)
                {
                    // Gradually apply force to disperse the fragment
                    Rigidbody rb = fragment.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        // Apply a small force in a random direction
                        Vector3 disperseDirection = Random.onUnitSphere; // Get a random direction
                        rb.AddForce(disperseDirection * disperseSpeed, ForceMode.Impulse);
                    }
                }
            }

            // Wait for a short time before applying more force
            yield return new WaitForSeconds(0.1f);
        }
    }


}
