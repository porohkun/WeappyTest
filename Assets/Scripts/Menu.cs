using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] _charactersOutline;

    private int _selectedCharacter = 1;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _selectedCharacter = (_selectedCharacter + 1) % _charactersOutline.Length;
            for (int i = 0; i < _charactersOutline.Length; i++)
                _charactersOutline[i].enabled = i == _selectedCharacter;
        }
    }
}
