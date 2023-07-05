string_1 = ['k', 'r', 'e', 's', 'r', 'e', 's', 'r', 'e', 's', 'j', '1', '2']
string_2 = ['r', 'e', 's', 'r', 'e', 's', 'j']
 
find_whole [] [] original_1 original_2 = True
find_whole stump_1 [] original_1 original_2 = True
find_whole [] stump_2 original_1 original_2 = False
find_whole (h1:t1) (h2:t2) original_1 original_2
    | h1 == h2 = find_whole t1 t2 original_1 original_2
    | otherwise = starts original_1 original_2 original_2

starts [] [] original_2 = False
starts [] string_2 original_2 = False
starts (h1:t1) (h2:t2) original_2
    | h1 == h2 = find_whole t1 t2 t1 original_2
    | otherwise = starts t1 (h2:t2) original_2
    
check = starts string_1 string_2 string_2