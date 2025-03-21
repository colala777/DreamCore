using UnityEngine;

public class OutlineController : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    private Material originalMaterial;
    private Renderer targetRenderer;

    private void Update()
    {
        if (Camera.main == null) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == transform)
            {
                if (targetRenderer == null)
                {
                    targetRenderer = hit.transform.GetComponent<Renderer>();
                    originalMaterial = targetRenderer.material;
                    targetRenderer.material = outlineMaterial;
                }
            }
            else
            {
                ResetMaterial();
            }
        }
        else
        {
            ResetMaterial();
        }
    }

    private void ResetMaterial()
    {
        if (targetRenderer != null)
        {
            targetRenderer.material = originalMaterial;
            targetRenderer = null;
        }
    }
}