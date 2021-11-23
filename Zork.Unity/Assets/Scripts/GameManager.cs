using UnityEngine;
using Zork.Common;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private string ZorkGameFileAssetName = "Zork";

    [SerializeField]
    private UnityInputService InputService;

    [SerializeField]
    private UnityOutputService OutputService;

    [SerializeField]
    private TextMeshProUGUI LocationText;

    void Awake()
    {
        TextAsset gameJsonAsset = Resources.Load<TextAsset>(ZorkGameFileAssetName);

        Game.StartGame(gameJsonAsset.text, InputService, OutputService);

        Game.Instance.Output.WriteLine(Game.Instance.Player.CurrentRoom);
        Game.Instance.Output.WriteLine(Game.Instance.Player.CurrentRoom.Description);
    }

    void Start()
    {
        
    }

    void Update()
    {
        LocationText.text = Game.Instance.Player.CurrentRoom.ToString();

        if(Game.Instance.IsRunning == false)
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
