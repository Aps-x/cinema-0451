using System;
using UnityEngine;

public class SimpleShell : MonoBehaviour {
    [SerializeField] Mesh shellMesh;
    [SerializeField] Shader shellShader;

    [SerializeField] bool updateStatics = true;

    // These variables and what they do are explained on the shader code side of things
    // You can see below (line 70) which shader uniforms match up with these variables
    [Range(1, 256)]
    [SerializeField] int shellCount = 16;

    [Range(0.0f, 1.0f)]
    [SerializeField] float shellLength = 0.15f;

    [Range(0.01f, 3.0f)]
    [SerializeField] float distanceAttenuation = 1.0f;

    [Range(1.0f, 1000.0f)]
    [SerializeField] float density = 150.0f;

    [Range(0.0f, 1.0f)]
    [SerializeField] float noiseMin = 0.5f;

    [Range(0.0f, 1.0f)]
    [SerializeField] float noiseMax = 1.0f;

    [Range(0.0f, 10.0f)]
    [SerializeField] float thickness = 3.5f;

    [Range(0.0f, 10.0f)]
    [SerializeField] float curvature = 1.0f;

    [Range(0.0f, 1.0f)]
    [SerializeField] float displacementStrength = 0.0f;

    [SerializeField] Color shellColor;

    [Range(0.0f, 5.0f)]
    [SerializeField] float occlusionAttenuation = 2.0f;
    
    [Range(0.0f, 1.0f)]
    [SerializeField] float occlusionBias = 0.1f;

    private Material shellMaterial;
    private GameObject[] shells;

    private Vector3 displacementDirection = new Vector3(0, 0, 0);

    void OnEnable() {
        shellMaterial = new Material(shellShader);

        shells = new GameObject[shellCount];

        for (int i = 0; i < shellCount; ++i) {
            shells[i] = new GameObject("Shell " + i.ToString());
            shells[i].AddComponent<MeshFilter>();
            shells[i].AddComponent<MeshRenderer>();
            
            shells[i].GetComponent<MeshFilter>().mesh = shellMesh;
            shells[i].GetComponent<MeshRenderer>().material = shellMaterial;
            shells[i].transform.SetParent(this.transform, false);

            // In order to tell the GPU what its uniform variable values should be, we use these "Set" functions which will set the
            // values over on the GPU. 
            shells[i].GetComponent<MeshRenderer>().material.SetInt("_ShellCount", shellCount);
            shells[i].GetComponent<MeshRenderer>().material.SetInt("_ShellIndex", i);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_ShellLength", shellLength);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_Density", density);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_Thickness", thickness);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_Attenuation", occlusionAttenuation);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_ShellDistanceAttenuation", distanceAttenuation);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_Curvature", curvature);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_DisplacementStrength", displacementStrength);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_OcclusionBias", occlusionBias);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_NoiseMin", noiseMin);
            shells[i].GetComponent<MeshRenderer>().material.SetFloat("_NoiseMax", noiseMax);
            shells[i].GetComponent<MeshRenderer>().material.SetVector("_ShellColor", shellColor);
        }
    }

    void Update() {
        // In order to avoid setting this variable on every single shell's material instance, we instead set this is as a global shader variable
        // That every shader will have access to, which sounds bad, because it kind of is, but just be aware of your global variable names and it's not a big deal.
        // Regardless, setting the variable one time instead of 256 times is just better.
        Shader.SetGlobalVector("_ShellDirection", displacementDirection);

        // Generally it is bad practice to update statics that do not need to be updated every frame
        // You can see the performance difference between updating 256 shells of statics by disabling the updateStatics parameter in the script
        // So it obviously matters at the extreme ends, but something above like setting the directional vector each frame is not going to make an insane diff
        // You will see in my other shaders and scripts that I do not always do this, because I'm lazy, but it's best practice to not update what doesn't need to be
        // updated.
        if (updateStatics) {
            for (int i = 0; i < shellCount; ++i) {
                shells[i].GetComponent<MeshRenderer>().material.SetInt("_ShellCount", shellCount);
                shells[i].GetComponent<MeshRenderer>().material.SetInt("_ShellIndex", i);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_ShellLength", shellLength);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_Density", density);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_Thickness", thickness);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_Attenuation", occlusionAttenuation);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_ShellDistanceAttenuation", distanceAttenuation);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_Curvature", curvature);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_DisplacementStrength", displacementStrength);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_OcclusionBias", occlusionBias);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_NoiseMin", noiseMin);
                shells[i].GetComponent<MeshRenderer>().material.SetFloat("_NoiseMax", noiseMax);
                shells[i].GetComponent<MeshRenderer>().material.SetVector("_ShellColor", shellColor);
            }
        }
    }

    void OnDisable() {
        for (int i = 0; i < shells.Length; ++i) {
            Destroy(shells[i]);
        }

        shells = null;
    }
}
