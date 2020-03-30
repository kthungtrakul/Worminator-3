using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissileLauncherAmmoControl : MonoBehaviour
{
    public Image gaugeBacking;
    private Image gaugeImage { get; set; }
    public Image currentAmmoImage;
    public RectTransform gaugeTransform;
    public TextMeshProUGUI ammoText;

    public Color Active, Inactive;
    public GameObject weaponModel;

    public static int MaxAmmo { get; set; } = 5;
    public static int CurrentAmmo { get; set; } = 5;
    private float NoAmmoPos { get; set; } = -670;
    private float MaxAmmoPos { get; set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        gaugeImage = gaugeTransform.GetComponent<Image>();
    }

    private void Update()
    {
        if (MissileLauncher.WeaponID == WeaponManager.ActiveWeapon)
        {
            gaugeBacking.color = Active;
            currentAmmoImage.color = Active;
            ammoText.text = "" + CurrentAmmo;
            weaponModel.GetComponent<MissileLauncher>().enabled = true;
            weaponModel.SetActive(true);
        }
        else
        {
            gaugeBacking.color = Inactive;
            weaponModel.GetComponent<MissileLauncher>().enabled = false;
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
        float currentPos = CalculateFloatBar.Compute(CurrentAmmo, 0, MaxAmmo, NoAmmoPos, MaxAmmoPos);
        gaugeTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, currentPos, 670f);
        gaugeImage.color = new Color(1, CalculateFloatBar.Compute(CurrentAmmo, 0, MaxAmmo, 0, 1f), 0, 0.85f);
    }
}
