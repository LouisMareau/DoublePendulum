using UnityEngine;

public class PendulumRenderer : MonoBehaviour
{
    [Header("REFERENCES")]
    // Bobs
    public SpriteRenderer bobARenderer;
    public SpriteRenderer bobBRenderer;
    // Trails
    public TrailRenderer trailARenderer;
    public TrailRenderer trailBRenderer;

    // PendulumA Color (slightly darker)
    // PendulumB Color (slightly brighter)
    [Header("COLOR SETTINGS")]
    public Color globalColor;

    // Trail Width
    // Trail Color
    //* Trail color should match pendulum color
    //* Add possibility to render pendulumA trail (B should be rendered by default)
    [Header("TRAIL SETTINGS")]
    public bool isTrailRenderedPendulumA;
    public bool isTrailRenderedPendulumB;
    public Color trailColorPendulumA;
    public Color trailColorPendulumB;
    [Space]
    public float trailWidthPendulumA;
    public float trailWidthPendulumB;
    [Space]
    public float trailTimePendulumA;
    public float trailTimePendulumB;

    public void Init() {
        // Render trails
        RenderTrails();

        // Set pendulum colors
        SetDoublePendulumColor(this.globalColor);
    }

    public void Init(Color globalColor) {
        this.globalColor = globalColor; 

        Init();
    }

    #region SPRITE RENDERERS
    /// <summary>
    /// By setting the double pendulum color, it will set both bob's color and their respective trails, as well as the UI tab color. 
    /// </summary>
    /// <param name="color">The color that will be applied to the pendulum.</param>
    public void SetDoublePendulumColor(Color color) {
        // Setting up the colors
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);

        Color bobAColor = Color.HSVToRGB(h, s - (s / 10), v - (v / 10));
        Color bobBColor = color;
        trailColorPendulumA = bobAColor;
        trailColorPendulumB = bobBColor;
        
        // Update the renderers
        if (bobARenderer != null) { bobARenderer.color = bobAColor; }
        if (bobBRenderer != null) { bobBRenderer.color = bobBColor; }
        if (trailARenderer != null) {
            trailARenderer.startColor = trailColorPendulumA;
            trailARenderer.endColor = Color.HSVToRGB(h, .5f, .3f);
            trailARenderer.startWidth = trailWidthPendulumA;
            trailARenderer.time = trailTimePendulumA;
        }
        if (trailBRenderer != null) {
            trailBRenderer.startColor = trailColorPendulumB;
            trailBRenderer.endColor = Color.HSVToRGB(h, .5f, .3f);
            trailBRenderer.startWidth = trailWidthPendulumB;
            trailBRenderer.time = trailTimePendulumB;
        }
    }
    #endregion

    #region TRAIL RENDERERS
    public void RenderTrails() {
        trailARenderer.gameObject.SetActive(isTrailRenderedPendulumA);
        trailBRenderer.gameObject.SetActive(isTrailRenderedPendulumB);
    }
    #endregion


}