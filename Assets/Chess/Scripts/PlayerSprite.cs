using TMPro;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField, Tooltip("text component used to display the name of the player")]
    private TMP_Text name;

    public Player player
    {
        set => name.text = value.name;
    }
}
