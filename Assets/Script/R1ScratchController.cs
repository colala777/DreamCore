// Scripts/ScratchController.cs
using UnityEngine;

public class R1ScratchController : MonoBehaviour
{
    public Camera mainCamera;
    public RenderTexture maskTexture;
    public float brushSize = 0.05f;

    private Texture2D _brushTex;
    private Material _scratchMat;

    void Start()
    {
        // 初始化遮罩纹理
        Graphics.Blit(Texture2D.whiteTexture, maskTexture);

        // 创建笔刷纹理
        _brushTex = CreateBrushTexture();
        _scratchMat = GetComponent<Renderer>().material;
        _scratchMat.SetTexture("_MaskTex", maskTexture);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector2 uv = hit.textureCoord;
                DrawOnMask(uv);
            }
        }
    }

    void DrawOnMask(Vector2 uv)
    {
        // 临时切换到 RenderTexture
        RenderTexture.active = maskTexture;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, 1, 0, 1);

        // 绘制白色圆形（表示刮除区域）
        Graphics.DrawTexture(
            new Rect(uv.x - brushSize / 2, uv.y - brushSize / 2, brushSize, brushSize),
            _brushTex
        );

        GL.PopMatrix();
        RenderTexture.active = null;
    }

    Texture2D CreateBrushTexture()
    {
        Texture2D tex = new Texture2D(64, 64);
        float center = 0.5f;
        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                float dx = (x / (float)tex.width) - center;
                float dy = (y / (float)tex.height) - center;
                float alpha = Mathf.Clamp01(1 - (dx * dx + dy * dy) * 4);
                tex.SetPixel(x, y, new Color(1, 1, 1, alpha));
            }
        }
        tex.Apply();
        return tex;
    }
}