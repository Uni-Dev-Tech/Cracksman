using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Lock : MonoBehaviour
{
    // Три положения пина (слева направо)
    private int firstPinPosition, secondPinPosition, thirdPinPosition;

    [Header("Текстовая визуализация положения пинов:")] [SerializeField]
    private Text firstPinText;
    [SerializeField]
    private Text secondPinText,
                 thirdPinText;

    [Header("Первый инструмент(отмычка, свойства)")]
    [SerializeField]
    private int firstPinInfluenceMasterKey;
    [SerializeField]
    private int secondPinInfluenceMasterKey,
                 thirdPinInfluenceMasterKey;

    [Header("Второй инструмет(щуп, свойства)")]
    [SerializeField]
    private int firstPinInfluenceTester;
    [SerializeField]
    private int secondPinInfluenceTester, 
                 thirdPinInfluenceTester;

    [Header("Третий инструмент(нож, свойство)")]
    [SerializeField]
    private int firstPinInfluenceKnife;
    [SerializeField]
    private int secondPinInfluenceKnife,
                 thirdPinInfluenceKnife;

    [Header("Окно победы/проигрыша")]
    [SerializeField]
    private GameObject winWindow;
    [SerializeField]
    private GameObject loseWindow;

    [SerializeField] Timer timerSettings;

    private void Awake()
    {
        Initialization();
    }
    /// <summary>
    /// Настройки перед началом игры при запуске
    /// </summary>
    private void Initialization()
    {
        PinPositionCombination();
        winWindow.SetActive(false);
        loseWindow.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Считает комбинацию значения пинов
    /// </summary>
    private void PinPositionCombination()
    {
        firstPinPosition = 5;
        secondPinPosition = 5;
        thirdPinPosition = 5;

        // случайное количество инструментов
        int instrumentQuantity = Random.Range(1, 4);
        for (int i = 0; i < instrumentQuantity; i++)
        {
            // случайный инструмент 
            int currentInstrument = Random.Range(0, 3);
            if (currentInstrument == 0)
                ChangePinValue(-firstPinInfluenceMasterKey, -secondPinInfluenceMasterKey, -thirdPinInfluenceMasterKey);
            if (currentInstrument == 1)
                ChangePinValue(-firstPinInfluenceTester, -secondPinInfluenceTester, -thirdPinInfluenceTester);
            if (currentInstrument == 2)
                ChangePinValue(-firstPinInfluenceKnife, -secondPinInfluenceKnife, -thirdPinInfluenceKnife);
        }
        ChangePinValueText(firstPinPosition, secondPinPosition, thirdPinPosition);

        // выставляем время и ходы в зависимости от количества инуструментов 
        timerSettings.timeForLevel = instrumentQuantity * 30f;

        timerSettings.stepsLeft = instrumentQuantity * 3f;
    }

    /// <summary>
    /// Изменение значения пинов (исп. отмычки)
    /// </summary>
    public void MasterKeyOnClick()
    {
        ChangePinValue(firstPinInfluenceMasterKey, secondPinInfluenceMasterKey, thirdPinInfluenceMasterKey);
        TestForPinValue();
        ChangePinValueText(firstPinPosition, secondPinPosition, thirdPinPosition);
        CheckForWin();
        timerSettings.DecreaseSteps();
    }
    /// <summary>
    /// Изменение значения пинов (исп.щупа)
    /// </summary>
    public void TesterOnClick()
    {
        ChangePinValue(firstPinInfluenceTester, secondPinInfluenceTester, thirdPinInfluenceTester);
        TestForPinValue();
        ChangePinValueText(firstPinPosition, secondPinPosition, thirdPinPosition);
        CheckForWin();
        timerSettings.DecreaseSteps();
    }
    /// <summary>
    /// Изменение значения пинов (исп.ножа)
    /// </summary>
    public void KnifeOnClick()
    {
        ChangePinValue(firstPinInfluenceKnife, secondPinInfluenceKnife, thirdPinInfluenceKnife);
        TestForPinValue();
        ChangePinValueText(firstPinPosition, secondPinPosition, thirdPinPosition);
        CheckForWin();
        timerSettings.DecreaseSteps();
    }
    /// <summary>
    /// Удерживает значение пинов в диапазоне от 0 до 10
    /// </summary>
    private void TestForPinValue()
    {
        if (firstPinPosition < 0)
            firstPinPosition = 0;
        if (firstPinPosition > 10)
            firstPinPosition = 10;

        if (secondPinPosition < 0)
            secondPinPosition = 0;
        if (secondPinPosition > 10)
            secondPinPosition = 10;

        if (thirdPinPosition < 0)
            thirdPinPosition = 0;
        if (thirdPinPosition > 10)
            thirdPinPosition = 10;
    }
    /// <summary>
    /// Меняет значение пинов
    /// </summary>
    /// <param name="firstPinDifference">Первый пин</param>
    /// <param name="secondPinDifference">Второй пин</param>
    /// <param name="thirdPinDifference">Трейтий пин</param>
    private void ChangePinValue(int firstPinDifference, int secondPinDifference, int thirdPinDifference)
    {
        firstPinPosition += firstPinDifference;
        secondPinPosition += secondPinDifference;
        thirdPinPosition += thirdPinDifference;
    }
    /// <summary>
    /// Отображает новое значение пинов
    /// </summary>
    /// <param name="firstPinNewValue">Первый пин</param>
    /// <param name="secondPinNewValue">Второй пин</param>
    /// <param name="thirdPinNewValue">Третий пин</param>
    private void ChangePinValueText(int firstPinNewValue, int secondPinNewValue, int thirdPinNewValue)
    {
        firstPinText.text = firstPinNewValue.ToString();
        secondPinText.text = secondPinNewValue.ToString();
        thirdPinText.text = thirdPinNewValue.ToString();
    }
    /// <summary>
    /// Включает окно победы в случаи равенства значений пинов
    /// </summary>
    private void CheckForWin()
    {
        if (firstPinPosition == secondPinPosition && secondPinPosition == thirdPinPosition)
        {
            Time.timeScale = 0f;
            winWindow.SetActive(true);
        }
    }
    /// <summary>
    /// Включает окно проигрыша
    /// </summary>
    public void EnableLose()
    {
        Time.timeScale = 0f;
        loseWindow.SetActive(true);
    }
    /// <summary>
    /// Повторяет игру
    /// </summary>
    public void AgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
