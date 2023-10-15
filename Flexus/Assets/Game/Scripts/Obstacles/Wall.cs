using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private Texture2D hitTexture;
    [SerializeField] private int textureResolution = 1024;

    private void Awake()
    {
        RenderTexture.active = renderTexture;
        GL.Clear(false, true, Color.white);
        RenderTexture.active = null;
    }


    public void DrawHit(RaycastHit hit)
    {
        RenderTexture.active = renderTexture;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, textureResolution, textureResolution, 0);

        Vector2 uv = hit.textureCoord2;
        uv.y = 1 - uv.y;
        int x  = (int)(uv.x * textureResolution) - hitTexture.width / 2;
        int y  = (int)(uv.y * textureResolution) - hitTexture.height / 2;

        Graphics.DrawTexture(new Rect(x, y, hitTexture.width, hitTexture.height), hitTexture);
        GL.PopMatrix();
        RenderTexture.active = null;
    }
}