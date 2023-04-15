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
    
