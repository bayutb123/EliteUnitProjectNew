using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchingUI : MonoBehaviour
{
    public GameObject shotgunUI;
    public GameObject pistolUI;
    public GameObject grenadeUI;
    public GameObject rifleUI;

    private int currentWeaponIndex = 0;
    private GameObject[] weaponUIArray;

    void Start()
    {
        // Atur referensi UI senjata ke dalam array
        weaponUIArray = new GameObject[] { shotgunUI, pistolUI, grenadeUI, rifleUI };

        // Nonaktifkan semua UI senjata kecuali Shotgun
        for (int i = 1; i < weaponUIArray.Length; i++)
        {
            weaponUIArray[i].SetActive(false);
        }
    }

    void Update()
    {
        // Deteksi input mouse scroll
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput > 0)
        {
            // Scroll ke atas
            SwitchToNextWeapon();
        }
        else if (scrollInput < 0)
        {
            // Scroll ke bawah
            SwitchToPreviousWeapon();
        }
    }

    void SwitchToNextWeapon()
    {
        // Nonaktifkan UI senjata saat ini
        weaponUIArray[currentWeaponIndex].SetActive(false);

        // Pindah ke senjata berikutnya
        currentWeaponIndex = (currentWeaponIndex + 1) % weaponUIArray.Length;

        // Aktifkan UI senjata yang sesuai
        weaponUIArray[currentWeaponIndex].SetActive(true);
    }

    void SwitchToPreviousWeapon()
    {
        // Nonaktifkan UI senjata saat ini
        weaponUIArray[currentWeaponIndex].SetActive(false);

        // Pindah ke senjata sebelumnya
        currentWeaponIndex = (currentWeaponIndex - 1 + weaponUIArray.Length) % weaponUIArray.Length;

        // Aktifkan UI senjata yang sesuai
        weaponUIArray[currentWeaponIndex].SetActive(true);
    }
}
