using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class WordGenerator : MonoBehaviour
{
    /** private static string[] overRide =
    {
        "they end will another much could go you day person need help this through much be same over off those with year by",
        "between way over all which from begin govern after system mean some program how through public long use should ",
        "but then call however part program take very never should increase day without fact think give end might both right",
        "down need line could much the those hand think what most like see what of such great system again you could must make look help no",
        "general through feel go head point at some play new lead they see be keep most hand all system old after lead out" ,
        "show each word long most which real hold where over public off year with more that during after small way there be come public place but",
        "may late run develop need people such to person be long around back never large before home want down never state small" ,
        "most back present give what long other people right a public up right increase as for that little feel real large use",
    z};**/

    public static String[] GenerateDict()
    {
        int dictLen = 961;
        string[] words = new string[dictLen];
        System.Random rand = new System.Random();
        String bookpath = Path.Combine(Application.streamingAssetsPath, "BookFormatted.txt");
        StreamReader reader = new StreamReader(bookpath);
        string line;
        // Read and display lines from the file until the end of
        // the file is reached.
        int i = 0;
        while ((line = reader.ReadLine()) != null)
        {
            words[i]= line.TrimEnd('\n');
            i++;
        }
        return words;
    }

    public static string GenerateSentence(String[] sentences)
    {
        System.Random rand = new System.Random();
        int index = rand.Next(960);
        return sentences[index];
    }
}

// Đoạn code trên định nghĩa một class `WordGenerator` để sinh ra các câu hỏi / từ vựng cho trò chơi.
// Hàm `GenerateDict()` tạo ra một mảng các từ vựng ngẫu nhiên trong một tệp văn bản, được lưu trữ dưới dạng một mảng các chuỗi `words`. Đặc biệt, hàm sử dụng `stream reader` để đọc và hiển thị các dòng từ tệp văn bản cho đến khi hết dữ liệu được đọc vào.
// Hàm `GenerateSentence()` sẽ chọn ngẫu nhiên một câu hỏi của trò chơi từ tệp văn bản được truyền vào qua đối số `sentences`. Sau đó, hàm sử dụng đối tượng `System.Random` để chọn ra index ngẫu nhiên phù hợp với câu hỏi đã định nghĩa, từ đó trả về một chuỗi câu hỏi ngẫu nhiên từ `sentences`.