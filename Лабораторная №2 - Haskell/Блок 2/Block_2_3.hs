participants = ["Sasha", "Katya", "Vlad", "Yulia", "Anya", "Grisha", "Vova", "Masha", "Ira", "Kostya", "Pasha", "Roma", "Dasha", "Valya", "Anton", "Sveta"]

check = ["Ira", "Kostya", "Pasha"]

search_fifth pos [] [] = ""
search_fifth pos [] [last] = last 
search_fifth pos [] second = search_fifth pos second []
search_fifth pos (h:t) second 
    | pos == 5 = search_fifth 1 t second
    | otherwise = search_fifth (pos + 1) t (second ++ [h])
    

find first = search_fifth 1 first []