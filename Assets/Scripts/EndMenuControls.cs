using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Класс для управления экраном окончания игры
public class EndMenuControls : MonoBehaviour
{
    public TextMeshProUGUI winnerText; // Текстовый элемент для отображения результата игры

    // Метод, вызываемый каждый кадр
    void Update()
    {
        DisplayWinner(); // Обновляем текст с результатом игры
    }

    // Метод для обработки нажатия кнопки "Играть снова"
    public void PlayPressed()
    {
        if (File.Exists(Application.dataPath + "/savegame.json"))
        {
            File.Delete(Application.dataPath + "/savegame.json");
            Saves data = new Saves
            {
                currentFrame = 1,
                currentRoll = 1,
                playerScore = 0,
                computerScore = 0,
                playerRolls = new int[21],
                computerRolls = new int[21],
                pinHasFallen = new bool[10],
                pinIsFallen = new bool[10],
                playerRollIndex = 0,
                computerRollIndex = 0
            };
        }

        SceneManager.LoadScene("Game"); // Загружаем сцену с игрой
    }

    // Метод для обработки нажатия кнопки "В меню"
    public void MenuPressed()
    {
        SceneManager.LoadScene("Menu"); // Загружаем сцену главного меню
    }

    // Метод для обработки нажатия кнопки "Выход"
    public void ExitPressed()
    {
        Application.Quit(); // Завершаем приложение
    }

    // Метод для отображения победителя или сообщения о ничьей
    private void DisplayWinner()
    {
        if (EndScore.playerScore > EndScore.computerScore) // Если счет игрока выше
        {
            winnerText.text = $"Игрок победил со счётом: {EndScore.playerScore}:{EndScore.computerScore}";
        }
        else if (EndScore.playerScore < EndScore.computerScore) // Если счет компьютера выше
        {
            winnerText.text = $"Компьютер победил со счётом: {EndScore.computerScore}:{EndScore.playerScore}";
        }
        else // Если счет равный
        {
            winnerText.text = $"Ничья! Счёт: {EndScore.computerScore}:{EndScore.playerScore}";
        }
    }
}