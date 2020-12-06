using UnityEngine;
using UnityEngine.Networking;
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

    void Awake()
    {
        SetDefault();    
    }

    public void TakeDamage (int _amount)
    {
        currentHealth -= _amount;

        Debug.Log(transform.name + "now has " + currentHealth + "health.");
    }

    public void SetDefault()
    {
        currentHealth = maxHealth;
    }
}
