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
    public int playerSkip = 0;
    public int computerSkip = 0;

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
    public void DisplayPlayerScore(int rollIndex, int pinsKnockedDown)
    {
        bool isSpare = rollIndex > 0 && (rollIndex + playerSkip) % 2 != 0 && (GM.playerRolls[rollIndex - 1] + pinsKnockedDown == 10); // ��������� �� ���� 

        // ��������� ��������� ������� ��� ���������� ������ ������
        if (pinsKnockedDown == 10 && (rollIndex + playerSkip) % 2 == 0) // ������
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
    public void DisplayComputerScore(int rollIndex, int pinsKnockedDown)
    {
        bool isSpare = rollIndex > 0 && (rollIndex + computerSkip) % 2 != 0 && (GM.computerRolls[rollIndex - 1] + pinsKnockedDown == 10); // ��������� �� ����

        // ��������� ��������� ������� ��� ���������� ������ ������
        if (pinsKnockedDown == 10 && (rollIndex + computerSkip) % 2 == 0) // ������
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