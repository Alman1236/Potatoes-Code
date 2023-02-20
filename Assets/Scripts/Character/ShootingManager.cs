using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumsAndStructs;
using UnityEngine.InputSystem;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] bool canCharacterShoot = false;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab, pointer;

    bool isReadyToShoot = true;
    Character me;
    Camera mainCam;

    List<GameObject> shotBullets = new List<GameObject>();

    private void Awake()
    {
        mainCam = Camera.main;
        me = GetComponent<Character>();
    }

    public void OnResettingCharacter()
    {
        for (ushort i = 0; i < shotBullets.Count; i++)
        {
            if (shotBullets[i] != null)
            {
                Destroy(shotBullets[i]);
            }
        }

        StopAllCoroutines();
        isReadyToShoot = true;
        shotBullets.Clear();
    }

    public void TryToShoot()
    {
        if (canCharacterShoot && isReadyToShoot && !me.deathManager.isDead)
        {
            if (!pointer.activeSelf)
            {
                Shoot(mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
            }
            else
            {
                Shoot(pointer.transform.GetChild(0).transform.position);      
            }

        }
    }

    public void Shoot(Vector3 where)
    {
        me.audioManager.PlaySound(AudioClipsNames.rockThrown);
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = firePoint.position;

        where.z = 0;
        bullet.GetComponent<Bullet>().direction = (where - firePoint.position).normalized;
        shotBullets.Add(bullet);

        PhasesManager.instance.OnSendingCommandToCharacter();
        me.actionsRecorder.RecordAction(actionTypes.shot, 0, where);

        StartCoroutine(StartCooldown());
    }


    IEnumerator StartCooldown()
    {
        isReadyToShoot = false;
        yield return new WaitForSeconds(Data.ShootingCooldown);
        isReadyToShoot = true;
    }

    public void RotatePointer(float xInput)
    {
        float currentZ = pointer.transform.rotation.eulerAngles.z;
        float newZ = currentZ + xInput * Data.PointerRotationSpeed;

        pointer.transform.rotation = Quaternion.Euler(0, 0, newZ);
    }

    public void OnUsingGamepad()
    {
        SetPointerActive(true);
        pointer.transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    public void OnUsingKeyboardOrMouse()
    {
        SetPointerActive(false);
    }

    void SetPointerActive(bool value)
    {
        pointer.SetActive(value);
    }

}
