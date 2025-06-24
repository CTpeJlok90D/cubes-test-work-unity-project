using UnityEngine;

namespace Game
{
    [Icon(EditorIcons.Note)]
    public class Note : MonoBehaviour
    {
        [TextArea(3, 100)]
        [SerializeField] private string _text;
    }
}
