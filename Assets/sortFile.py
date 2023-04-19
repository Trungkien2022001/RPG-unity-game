if __name__ == '__main__':
    readPath  = "words_alpha.txt"
    writePath  = "wordsByLen.txt"
    
    w = open(writePath, "a")
    #lens =[]
    for i in range(4, 36):
        f = open(readPath, "r")
        #lens.append([])
        for line in f:
            #print(len("a"))
            if len(line) == i:
                #while(len(line)<35): 
                    #line+=" "
                #print(len(line))
                w.write(line)
                #lens[i-3].append(line[:-1]) 
        f.close()
    w.close()
    #print(lens[3])
    
# Đoạn code này đọc một tệp văn bản chứa danh sách từ điển, sau đó ghi các từ theo độ dài của từ vào các tệp khác nhau.

# Cụ thể, đoạn code sẽ đọc các từ trong tệp "words_alpha.txt" và ghi các từ có độ dài từ 4 đến 35 vào các tệp khác nhau với tên tệp là "wordsByLen.txt".

# Đoạn code sử dụng một vòng lặp for để lặp qua các độ dài từ 4 đến 35 và với mỗi độ dài, nó mở tệp "words_alpha.txt", đọc từng dòng trong tệp, và nếu chiều dài của dòng đó bằng với độ dài đang xét, nó sẽ ghi dòng đó vào tệp tương ứng. Sau khi lặp xong, tất cả các từ có độ dài từ 4 đến 35 sẽ được lưu trong các tệp khác nhau theo độ dài của từ.

# Nếu tệp "words_alpha.txt" không tồn tại, hoặc không có quyền ghi vào tệp "wordsByLen.txt", hoặc các tệp khác nhau tùy vào điều kiện, các hoạt động trong đoạn code sẽ gây ra lỗi.