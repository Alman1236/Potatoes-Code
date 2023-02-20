using UnityEngine;
using EnumsAndStructs;

public class CharacterKiller : MonoBehaviour
{
    [SerializeField] deathCause typeOfKiller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            collision.GetComponent<CharacterDeath>().Die(typeOfKiller);
        }
    }
}
