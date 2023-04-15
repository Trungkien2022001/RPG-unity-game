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
    
