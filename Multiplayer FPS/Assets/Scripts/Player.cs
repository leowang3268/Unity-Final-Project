using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour {

    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();    
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDamage(9999);
        }
    }

    [ClientRpc]
    public void RpcTakeDamage (int amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;
        Debug.Log(transform.name + " now has " + currentHealth + " health.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die ()
    {
        isDead = true;

        // DISABLE COMPONENTS
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }
        Collider collider = GetComponent<Collider>();
        if (collider != null)
            collider.enabled = false;

        Debug.Log(transform.name + "is Knock Out (by)"); // 待解決: 取得殺人的玩家名字???

        // CALL RESPAWN METHOD
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        SetDefaults();
        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;

        Debug.Log(transform.name + " respawned");
    }

    public void SetDefaults ()
    {
        currentHealth = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider collider = GetComponent<Collider>();
        if (collider != null)
            collider.enabled = true;
    }
}
