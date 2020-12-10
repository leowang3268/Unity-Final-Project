using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    private const string PLAYER_TAG = "Player";

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam; // cam   

    [SerializeField]
    private LayerMask mask;

    //public ParticleSystem muzzleFlash;

    void Start()
    {
        if (cam == null)
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
            Debug.DrawRay(cam.transform.position, cam.transform.forward * 10, Color.green);
        }
    }

    [Client]
    private void Shoot ()
    {
        RaycastHit hit;
        //muzzleFlah.Play();
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
        {
            //Debug.Log("hit");
            if (hit.collider.tag == PLAYER_TAG)
                CmdPlayerShot(hit.collider.name, weapon.damage);
        }
    }

    [Command]
    void CmdPlayerShot (string _playerID, int damage)
    {
        Debug.Log(_playerID + " has been shot");

        Player player = GameManager.GetPlayer(_playerID);
        player.RpcTakeDamage(damage);
    }
}
