using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public GameObject[] weapons;
    
    public int activeWeaponIndex;

    private Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        activeWeaponIndex = 0;
        for(int i =1;i < weapons.Length;i++){
            weapons[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if(scroll != 0f)
        {
            UnityEngine.Debug.Log("scroll");
            SwitchWeapon();
        }

        weapon = weapons[activeWeaponIndex].GetComponent<Weapon>();

        if (Input.GetMouseButtonDown(0)){
            weapon.Attack();
        }
        if(Input.GetKeyDown(KeyCode.R)){
            if(weapon is RangedWeapon rangedWeapon){
                rangedWeapon.Reload();
            }
        }
        
    }

    void SwitchWeapon(){
        if(activeWeaponIndex + 1 >= weapons.Length)
        {
            DeactivateWeapon();
            activeWeaponIndex = 0;
            ActivateWeapon();
        }
        else
        {
            DeactivateWeapon();
            activeWeaponIndex++;
            ActivateWeapon();
        }
    }

    void ActivateWeapon(){
        weapons[activeWeaponIndex].SetActive(true);
    }
    void DeactivateWeapon(){
        weapons[activeWeaponIndex].SetActive(false);
    }
}
