using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*Đoạn code trên có nhiệm vụ tạo ra một đối tượng Timer trong trò chơi. Timer này sẽ hiển thị thời gian đếm lùi hoặc đếm thời gian 
lên tùy chọn, có thể có giới hạn thời gian đếm ngược. Timer cũng có thể định dạng hiển thị thời gian, cho phép lựa chọn định dạng 
đếm thời gian từ dạng số nguyên đến dạng số thập phân. Đối tượng Timer hiển thị thời gian được thể hiện trên thành phần UI TextMeshProUGUI.*/

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    [Header("Format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    // Start is called before the first frame update
    void Start()
    {
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrethsDecimal, "0.00");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        if(hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = Color.red;
            enabled = false;
        }

        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
    }
}

public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HundrethsDecimal
}
