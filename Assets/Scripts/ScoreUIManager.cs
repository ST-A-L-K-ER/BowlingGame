using UnityEngine;
using TMPro;

// ����� ��� ���������� ����������� ������������ (UI), ������������ ���� ����
public class ScoreUIManager : MonoBehaviour
{
    // ��������� �������� ��� ����������� �������� ����� ������, ���������� � ������� �������� ����
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI computerScoreText;
    public TextMeshProUGUI currentPlayerText;

    // ������� ��������� ��������� ��� ����������� ����� �� ������� ������ (10 �������) ��� ������ � ����������
    public TextMeshProUGUI[] playerFrameScores; // ������ ��� ������
    public TextMeshProUGUI[] computerFrameScores; // ������ ��� ����������

    // �������� ����������� ����� ��� ����������� ����������� ��������
    private int playerSkip = 0;
    private int computerSkip = 0;

    // ������ �� ���������� GameManager � BallController
    private GameManager GM;
    private BallController ball;

    // �����, ���������� ��� ������, ��� ��������� ������ �� ����������� ����������
    void Start()
    {
        GM = FindObjectOfType<GameManager>(); // ������� ������ GameManager � �����
        ball = FindObjectOfType<BallController>(); // ������� ������ BallController � �����
    }

    // ����� ����������, ���������� ������ ����, ��������� UI
    void Update()
    {
        UpdateUI(); // ��������� ��������� �������� ����������
    }

    // ����� ���������� ��������� ��������� ����������
    public void UpdateUI()
    {
        playerScoreText.text = $"�����: {GM.playerScore}"; // ��������� ���� ������
        computerScoreText.text = $"���������: {GM.computerScore}"; // ��������� ���� ����������
        currentPlayerText.text = ball.isPlayerTurn ? "��� ������" : "��� ����������"; // ��������� ������� ���
    }

    // ����� ��� ����������� ����� ������ � ��������� ���������
    public void DisplayPlayerScore(int rollIndex, int pinsKnockedDown, bool isSpare = false)
    {
        // ��������� ��������� ������� ��� ���������� ������ ������
        if (pinsKnockedDown == 10 && GM.currentRoll == 1) // ������
        {
            playerFrameScores[rollIndex + playerSkip].text = "X";
            playerSkip++; // ����������� ������� ����������� �����
        }
        else if (pinsKnockedDown == 0) // ������ ������
        {
            playerFrameScores[rollIndex + playerSkip].text = "-";
        }
        else if (isSpare) // ����
        {
            playerFrameScores[rollIndex + playerSkip].text = "/";
        }
        else // ���������� ������ ������
        {
            playerFrameScores[rollIndex + playerSkip].text = pinsKnockedDown.ToString();
        }
    }

    // ����� ��� ����������� ����� ���������� � ��������� ���������
    public void DisplayComputerScore(int rollIndex, int pinsKnockedDown, bool isSpare = false)
    {
        // ��������� ��������� ������� ��� ���������� ������ ������
        if (pinsKnockedDown == 10 && GM.currentRoll == 1) // ������
        {
            computerFrameScores[rollIndex + computerSkip].text = "X";
            computerSkip++; // ����������� ������� ����������� �����
        }
        else if (pinsKnockedDown == 0) // ������ ������
        {
            computerFrameScores[rollIndex + computerSkip].text = "-";
        }
        else if (isSpare) // ����
        {
            computerFrameScores[rollIndex + computerSkip].text = "/";
        }
        else // ���������� ������ ������
        {
            computerFrameScores[rollIndex + computerSkip].text = pinsKnockedDown.ToString();
        }
    }
}