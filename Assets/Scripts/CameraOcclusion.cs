using System.Collections.Generic;
using UnityEngine;

public class CameraOcclusion : MonoBehaviour
{
    public Transform target; // Il player
    public LayerMask occluderLayer; // Layer degli oggetti da rendere trasparenti
    public float fadeAlpha = 0.3f;

    private readonly List<Renderer> fadedObjects = new();
    private readonly Dictionary<Renderer, Material[]> originalMaterials = new();

    void Update()
    {
        ClearOldFades();

        Vector3 dir = target.position - transform.position;
        float dist = Vector3.Distance(transform.position, target.position);

        // Trova TUTTI gli oggetti in mezzo
        RaycastHit[] hits = Physics.RaycastAll(transform.position, dir, dist, occluderLayer);

        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null && !fadedObjects.Contains(rend))
            {
                FadeOut(rend);
            }
        }
    }

    // Rendi trasparente
    void FadeOut(Renderer rend)
    {
        // Salva materiali originali
        originalMaterials[rend] = rend.materials;

        Material[] newMats = new Material[rend.materials.Length];

        for (int i = 0; i < rend.materials.Length; i++)
        {
            Material m = new Material(rend.materials[i]);
            m.SetFloat("_Mode", 2); // Transparent (Standard Shader)
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            m.SetInt("_ZWrite", 0);
            m.DisableKeyword("_ALPHATEST_ON");
            m.EnableKeyword("_ALPHABLEND_ON");
            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            m.renderQueue = 3000;

            Color c = m.color;
            c.a = fadeAlpha;
            m.color = c;

            newMats[i] = m;
        }

        rend.materials = newMats;
        fadedObjects.Add(rend);
    }

    // Ripristina materiali
    void ClearOldFades()
    {
        foreach (Renderer rend in fadedObjects)
        {
            if (rend != null)
                rend.materials = originalMaterials[rend];
        }

        fadedObjects.Clear();
        originalMaterials.Clear();
    }
}
