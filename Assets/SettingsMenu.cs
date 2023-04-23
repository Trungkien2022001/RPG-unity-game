using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i] + " x " + resolutions[i].height;
            options.Add(option);

            if (
                resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height
            )
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}


/*
Đoạn code trên là một script của game Unity, nơi cho phép người dùng tùy chỉnh các cài đặt trong game như âm lượng,
chất lượng hình ảnh, độ phân giải và chế độ toàn màn hình.

Đầu tiên, nó khai báo một AudioMixer, một Dropdown và một mảng Resolution,
sau đó nó gán giá trị của Screen.resolutions cho mảng Resolution để có danh sách các độ phân giải khả dụng.

Trong hàm Start (), nó tạo một danh sách các tùy chọn độ phân giải và lấy độ phân giải hiện tại của
màn hình để đặt giá trị ban đầu cho Dropdown. Sau đó, nó đăng ký phương thức SetResolution () để được gọi
khi người dùng chọn một tùy chọn độ phân giải khác nhau.

Hàm SetVolume () sử dụng AudioMixer để đặt giá trị âm lượng trong game.

Hàm SetQuality () sử dụng QualitySettings.SetQualityLevel để đặt chất lượng hình ảnh của game.

Hàm SetFullscreen () đặt chế độ toàn màn hình của game bằng cách đặt giá trị của Screen.fullScreen.
*/
