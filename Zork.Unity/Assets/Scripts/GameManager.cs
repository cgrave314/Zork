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

    [SerializeField]
    private TextMeshProUGUI ScoreText;

    [SerializeField]
    private TextMeshProUGUI MovesText;

    void Awake()
    {
        TextAsset gameJsonAsset = Resources.Load<TextAsset>(ZorkGameFileAssetName);

        Game.StartGame(gameJsonAsset.text, InputService, OutputService);

        Game.Instance.Output.WriteLine(Game.Instance.Player.CurrentRoom);
        Game.Instance.Output.WriteLine(Game.Instance.Player.CurrentRoom.Description);
    }

    void Start()
    {
        InputService.InputField.Select();
    }

    void Update()
    {
        LocationText.text = Game.Instance.Player.CurrentRoom.ToString();
        ScoreText.text = $"Score: {Game.Instance.Player.Score}";
        MovesText.text = $"Moves: {Game.Instance.Player.MovesCount}";

        if(Game.Instance.IsRunning == false)
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
