

pairs = [("Vladimir", "Roman"), ("Vadim", "Petr"), ("Maxim", "Kirill"), ("Michail", "Vladimir"), ("Artem", "Egor"), ("Denis", "Georgiy"), ("Vadim", "Alexey"), ("Vladimir", "Dmitriy"), ("Alexey", "Michail"), ("Matvey", "Artem"), ("Michail", "Alexander"), ("Yaroslav", "Denis"), ("Vladimir", "Boris"), ("Artem", "Andrey")]

double_concat first second = first ++ second
triple_concat first second third = first ++ double_concat second third
forth_concat first second third forth = first ++ triple_concat second third forth
fifth_concat first second third forth fifth = first ++ forth_concat second third forth fifth

get_son :: (String, String) -> String
get_son (father, son) = son

get_father :: (String, String) -> String
get_father (father, son) = father

run_list :: String -> [(String, String)] -> [String] -> [String]
run_list name [] direct_find_descendent = direct_find_descendent
run_list name (h:t) direct_find_descendent
    | get_father h == name = run_list name t updated_list
    | otherwise  = run_list name t direct_find_descendent
    where updated_list = direct_find_descendent ++ [(get_son h)]
    
direct_descendent :: [String] -> [(String, String)] -> String
direct_descendent names pairs
    | names == [] = ""
    | x == [] = triple_concat (head names) " " (direct_descendent (tail names) pairs)
    | otherwise = fifth_concat (head names) " " (direct_descendent x pairs) " " (direct_descendent (tail names) pairs)
    where x = run_list (head names) pairs []
    
boot name pairs = direct_descendent [name] pairs

find_descendent name = boot name pairs