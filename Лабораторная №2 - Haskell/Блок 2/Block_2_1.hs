triples = [("Владивосток", "Екатеринбург", 7379), ("Тюмень", "Сочи", 3102), ("Канск", "Сочи", 5267), ("Грозный", "Сочи", 4000), ("Грозный", "Екатеринбург", 2608), ("Южно-Сахалинск", "Сочи", 10177)]

get_distance (x, y, z) = z

algorithm :: [(String, String, Int)] -> Int -> Int
algorithm [] current = current
algorithm (h:t) current
    | ((get_distance h) < current) = algorithm t (get_distance h)
    | otherwise = algorithm t current

find_shortest = 
    algorithm triples 99999
    