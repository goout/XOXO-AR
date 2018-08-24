using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public GameObject boardObject;

    private Board board;

    [SerializeField] private GameObject[] playerMarkers;
    [SerializeField] private TextMesh winnerText;
    [SerializeField] private GameObject fireworkPrefab;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip loseSoundFx;
    [SerializeField] private AudioClip winSoundFx;
    [SerializeField] private AudioClip drawSoundFx;
    [SerializeField] private AudioClip xSound;
    [SerializeField] private AudioClip oSound;

    private AudioClip currentPlayerSound;
    private Material currentPlayerMaterial;
    private int currentPlayer = X_PLAYER;
    private int _turnsCount;
    private GameObject fireworkClone;

    private const string WIN_MESSAGE = "You WIN!!!";
    private const string LOSE_MESSAGE = "You LOSE!!!";
    private const string DRAW_MESSAGE = "DRAW xD";
    private const string X_PLAYER_WIN_MESSAGE = "X WIN!!!";
    private const string O_PLAYER_WIN_MESSAGE = "O WIN!!!";
    private const int X_PLAYER = 0;
    private const int O_PLAYER = 1;
    private const int MIN_COUNT_FOR_WIN = 4;
    private const int MARKERS_IN_LINE = 3;
    private const int CELLS_COUNT = 9;


    public int turnsCount { get { return _turnsCount; } }

    void Start() {
        board = boardObject.GetComponent<Board>();
        currentPlayerSound = xSound;
        GameSetup();
    }

    public void Restart() {
        GameSetup();
        if (!winnerText.gameObject.activeSelf && StaticData.AiPlayerMode && currentPlayer == 1) {
            board.SetColiders(false);
            StartCoroutine(PseudoAiTap());
        }
    }

    void GameSetup() {
        _turnsCount = 1;
        winnerText.text = "";
        winnerText.gameObject.SetActive(false);

        board.RemoveMarkers();
        Destroy(fireworkClone);
    }

    public void TapOnCell(GameObject tappedObject) {
        Cell tappedCell = tappedObject.transform.GetComponent<Cell>();

        if (!tappedCell.IsMarked()) {
            tappedCell.Mark(playerMarkers[currentPlayer]);
            currentPlayerMaterial = playerMarkers[currentPlayer].GetComponent<Renderer>().sharedMaterial;
            audioSource.PlayOneShot(currentPlayerSound);
            _turnsCount++;
            NextTurn();

            if (!winnerText.gameObject.activeSelf && StaticData.AiPlayerMode) {
                board.SetColiders(false);
                StartCoroutine(PseudoAiTap());
            }
        }
    }

    IEnumerator PseudoAiTap() {
        yield return new WaitForSeconds(.4f);

        Cell tappedCell = PseudoAI.instance.AITap(board);
        tappedCell.Mark(playerMarkers[currentPlayer]);

        currentPlayerMaterial = playerMarkers[currentPlayer].GetComponent<Renderer>().sharedMaterial;
        audioSource.PlayOneShot(currentPlayerSound);
        _turnsCount++;
        board.SetColiders(true);

        NextTurn();
    }

    public void NextTurn() {
        if (_turnsCount > MIN_COUNT_FOR_WIN) {
            WinnerCheck();
        }

        if (currentPlayer == X_PLAYER) {
            currentPlayer = O_PLAYER;
            currentPlayerSound = oSound;
        } else {
            currentPlayer = X_PLAYER;
            currentPlayerSound = xSound;
        }
    }

    void WinnerCheck() {
        int[] solutions = board.GetLinesSum();

        for (int i = 0; i < solutions.Length; i++) {

            if (solutions[i] == MARKERS_IN_LINE * (currentPlayer + 1)) {

                if (currentPlayer == X_PLAYER) {
                    if (StaticData.AiPlayerMode) {
                        GameOver(Color.yellow, WIN_MESSAGE, winSoundFx);
                    } else {
                        GameOver(Color.blue, X_PLAYER_WIN_MESSAGE, winSoundFx);
                    }
                    break;
                } else if (currentPlayer == O_PLAYER) {
                    if (StaticData.AiPlayerMode) {
                        GameOver(Color.red, LOSE_MESSAGE, loseSoundFx);
                    } else {
                        GameOver(Color.cyan, O_PLAYER_WIN_MESSAGE, winSoundFx);
                    }
                    break;

                }
              } else if (turnsCount > CELLS_COUNT && i == (solutions.Length - 1) ) {
                GameOver(Color.magenta, DRAW_MESSAGE, drawSoundFx);
            }
        }
    }

    void GameOver(Color color, string text, AudioClip gameOverSoundFX) {
        fireworkClone = Instantiate(fireworkPrefab, Vector3.zero, Quaternion.Euler(-90, 0, 0));
        fireworkClone.GetComponent<ParticleSystemRenderer>().material = currentPlayerMaterial;
        winnerText.gameObject.SetActive(true);
        winnerText.color = color;
        winnerText.text = text;
        audioSource.PlayOneShot(gameOverSoundFX);
    }

}
