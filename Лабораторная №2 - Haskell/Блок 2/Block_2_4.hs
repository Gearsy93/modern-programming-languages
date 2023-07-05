months = [("January", 0), ("February", 0), ("March", 0), ("April", 0), ("May", 0), ("June", 0), ("July", 0), ("August", 0), ("September", 0), ("October", 0), ("November", 0), ("Decemnber", 0)]

non_work = [(4, "July"), (31, "March"), (10, "February"), (18, "March"), (17, "January")]

insert_day [] month = []
insert_day (h:t) month
    | (fst h) == month = (month, (snd h) + 1):t
    | otherwise = h:(insert_day t month)
    
filler months non_work = fill_list months non_work where
    fill_list months [] = months
    fill_list months (h:t) = fill_list (insert_day months (snd h)) t
    
fill = filler months non_work

find_max [] current_max = current_max
find_max (h:t) current_max
    | (snd h) > current_max = find_max t (snd h)
    | otherwise = find_max t current_max
    
max_num = find_max fill 0

run :: [(String, Int)] -> Int -> [String] -> [String]
run [] max_num list = list
run (h:t) max_num list
    | (snd h) == max_num = run t max_num list++[fst h]
    | otherwise = run t max_num list

create_final_list max_num fill = run fill max_num [] 
    
    
find = create_final_list max_num fill