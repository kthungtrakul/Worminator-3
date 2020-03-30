using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShotBeamAmmoControl : MonoBehaviour
{
    private Image GaugeBacking { get; set; }
    private Image GaugeImage { get; set; }
    private Image CurrentAmmoImage { get; set; }
    private RectTransform gaugeTransform;
    private TextMeshProUGUI AmmoText { get; set; }

    public Color Active, Inactive;

    public GameObject weaponModel;

    public static int MaxAmmo { get; set; } = 10;
    public static int CurrentAmmo { get; set; } = 10;
    private float NoAmmoPos { get; set; } = -670;
    private float MaxAmmoPos { get; set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        CurrentAmmoImage = GameObject.FindWithTag("Current Ammo Image").GetComponent<Image>();
        AmmoText = GameObject.FindWithTag("Current Ammo Text").GetComponent<TextMeshProUGUI>();
        GaugeBacking = GameObject.FindWithTag("SB Ammo Gauge").GetComponent<Image>();
        GaugeImage = GameObject.FindWithTag("SB Ammo Bar").GetComponent<Image>();
        gaugeTransform = GameObject.FindWithTag("SB Ammo Bar").GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (ShotBeam.WeaponID == WeaponManager.ActiveWeapon)
        {
            GaugeBacking.color = Active;
            CurrentAmmoImage.color = Active;
            AmmoText.text = "" + CurrentAmmo;
            weaponModel.GetComponent<ShotBeam>().enabled = true;
            weaponModel.SetActive(true);
        }
        else
        {
            GaugeBacking.color = Inactive;
            weaponModel.GetComponent<ShotBeam>().enabled = false;
            weaponModel.SetActive(false);
        }
    }

    void LateUpdate()
    {
        if (CurrentAmmo > MaxAmmo) CurrentAmmo = MaxAmmo;
        MoveAmmoBar();
    }

    void MoveAmmoBar()
    {
        float currentPos = CalculateAmmoBar(CurrentAmmo, 0, MaxAmmo, NoAmmoPos, MaxAmmoPos);
        gaugeTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, currentPos, 670f);
        GaugeImage.color = new Color(1, CalculateAmmoBar(CurrentAmmo, 0, MaxAmmo, 0, 1f), 0, 0.85f);
    }

    float CalculateAmmoBar(float x, float inMin, float inMax, float outMin, float outMax)
    {
        /*
         * x = current ammo
         * inMin = minimum ammo (e.g. 0)
         * inMax = maximum ammo
         * outMin = min X position (no ammo)
         * outMax = max X position (max ammo)
         */
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
