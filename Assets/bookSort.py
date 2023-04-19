if __name__ == '__main__':
    readPath  = "Book.txt"
    writePath  = "BookFormatted.txt"
    
    w = open(writePath, "a", encoding='utf-8')
    #lens =[]
    f = open(readPath, "r", encoding='utf-8')
    line = ""
    lineLen = 0
    while 1:
        # read by character
    
        char = f.read(1)
        lineLen+=1
        #print(char)

        if char != "\n" and char != "\t":
            line+= char
        else:
            line+= " "
        if char == ".":
            line = line.strip()
            line+='\n'
            first = ord(line.lower()[0])
            if lineLen>=40 and lineLen<=80 and first>=97 and first<=122:
                w.write(line.replace("“","\"").replace("”","\""))
            lineLen = 0
            line = ""

        if not char:
            break  
        
 
    f.close()
    w.close()
    
# Đây là một đoạn code Python dùng để đọc và định dạng văn bản từ một tệp văn bản đầu vào và ghi kết quả vào một tệp văn bản đầu ra.

# Bước đầu tiên của chương trình là thiết lập đường dẫn cho tệp đầu vào và tệp đầu ra. Sau đó, chương trình mở tệp đầu vào và tệp đầu ra bằng các hàm open() trong chế độ đọc và ghi, tương ứng là "r" và "a" trong tham số thứ hai. Tham số thứ ba "encoding" được sử dụng để chỉ định bảng mã được sử dụng để đọc và ghi văn bản trong tệp. Ở đây, bảng mã được sử dụng là "utf-8".

# Tiếp theo, chương trình sử dụng một vòng lặp vô hạn để đọc tệp đầu vào một ký tự một lần. Trong vòng lặp, chương trình đọc một ký tự từ tệp đầu vào và thêm nó vào biến line. Nếu ký tự đó không phải là ký tự xuống dòng hoặc tab, chương trình sẽ thêm ký tự đó vào biến line. Nếu ký tự là dấu chấm, chương trình sẽ xử lý và ghi ra kết quả vào tệp đầu ra.

# Khi đoạn văn bản được đọc vào biến line, chương trình sẽ xóa các khoảng trống trước và sau chuỗi và thêm một ký tự xuống dòng vào cuối chuỗi. Sau đó, chương trình sử dụng len() để tính toán độ dài của đoạn văn bản và first để kiểm tra xem ký tự đầu tiên trong đoạn văn bản có phải là một chữ cái trong bảng chữ cái tiếng Anh không. Nếu độ dài đoạn văn bản nằm trong khoảng từ 40 đến 80 ký tự và ký tự đầu tiên là một chữ cái trong bảng chữ cái tiếng Anh, chương trình sẽ ghi đoạn văn bản đó vào tệp đầu ra.

# Nếu ký tự đang được đọc đã hết, chương trình sẽ thoát khỏi vòng lặp và đóng tệp đầu vào và tệp đầu ra bằng các hàm close().