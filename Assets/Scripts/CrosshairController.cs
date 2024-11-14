using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    private Image crosshair;

    [Header("Crosshair Sprites")]
    public Sprite blankCrosshair;
    public Sprite bluePortalCrosshair;
    public Sprite orangePortalCrosshair;
    public Sprite bothPortalCrosshair;

    [Header("Portals")]
    public GameObject bluePortal;
    public GameObject orangePortal;

    private void Start()
    {
        crosshair = GetComponent<Image>();
    }

    private void Update()
    {
        Sprite newCrosshair;
        if (bluePortal.activeSelf && orangePortal.activeSelf)
        {
            newCrosshair = bothPortalCrosshair;
        }
        else if (bluePortal.activeSelf)
        {
            newCrosshair = bluePortalCrosshair;
        }
        else if (orangePortal.activeSelf)
        {
            newCrosshair = orangePortalCrosshair;
        }
        else
        {
            newCrosshair = blankCrosshair;
        }
        crosshair.sprite = newCrosshair;
    }
}