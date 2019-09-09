using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField, Tooltip("text component used to display the name of the player")]
    private TMP_Text name;

    private Color disabledColor;

    [SerializeField, Tooltip("what color should the background be when it's the player's turn''")] 
    private Color enabledColor = Color.green;
  
    
    private Player _player;
    public Player player
    {
        set
        {
            _player = value;
            name.text = value.name;
            
        }
    }
    private void Awake()
    {
        disabledColor = GetComponent<Image>().color;
    }


    private void OnEnable()
    {
        GameManager.OnNextPlayer += GameManagerOnOnNextPlayer;
    }

    private void OnDisable()
    {
        GameManager.OnNextPlayer -= GameManagerOnOnNextPlayer;
    }

    private void GameManagerOnOnNextPlayer(Player obj)
    {
        GetComponent<Image>().color = (obj.Equals(_player)) ? enabledColor : disabledColor;
    }


}
