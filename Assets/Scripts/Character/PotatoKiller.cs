using UnityEngine;
using EnumsAndStructs;

public class PotatoKiller : MonoBehaviour
{
    Character me;

    private void Awake()
    {
        me = GetComponent<Character>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Character character))
        {
            character.deathManager.Die(deathCause.badPotato);
        }
    }
}
