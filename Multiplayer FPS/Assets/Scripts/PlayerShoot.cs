using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : MonoBehaviour {

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera gun; // cam   

    [SerializeField]
    private LayerMask mask;

    void Start()
    {
        if (gun == null)
        {
            Debug.LogError("PlaterShoot: No camera referenced");
            this.enabled = false;
        }

    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;

        if(Physics.Raycast(gun.transform.position, gun.transform.forward, out hit, weapon.range, mask))
        {
            Debug.Log("We hit " + hit.collider.name);
        }
    }
}
