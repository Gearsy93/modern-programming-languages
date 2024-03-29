import Data.Time

--Input graph
graph_0 :: [(Int, Int, Int)]
graph_0 = [(0,1,3),(0,2,2),(0,3,1),(1,3,9),(2,3,1)]

graph_1 :: [(Int, Int, Int)]
graph_1 = [(0,1,4),(0,2,6),(0,4,3),(1,3,1),(2,4,1)]

graph_2 :: [(Int, Int, Int)]
graph_2 = [(0,1,3),(0,2,4),(1,2,5)]

graph_3 :: [(Int, Int, Int)]
graph_3 = [(0,1,5),(0,2,20),(0,3,3),(0,4,0),(0,5,7),(1,2,5),(1,3,0),(1,4,44),(1,5,3),(2,3,0),(2,4,6),(2,5,2),(3,4,8),(3,5,33),(4,5,76)]

graph_4 :: [(Int, Int, Int)]
graph_4 = [(0,1,87),(0,2,26),(0,3,14),(0,4,27),(0,5,97),(0,6,26),(0,7,8),(0,8,77),(0,9,51),(1,2,12),(1,3,61),(1,4,69),(1,5,30),(1,6,79),(1,7,54),(1,8,51),(1,9,15),(2,3,12),(2,4,65),(2,5,40),(2,6,42),(2,7,42),(2,8,3),(2,9,72),(3,4,21),(3,5,14),(3,6,59),(3,7,9),(3,8,19),(3,9,61),(4,5,32),(4,6,67),(4,7,13),(4,9,92),(5,6,50),(5,7,39),(5,8,54),(5,9,98),(6,7,52),(6,8,72),(6,9,14),(7,8,86),(7,9,16),(8,9,17)]

graph_5 :: [(Int, Int, Int)]
graph_5 = [(0,1,60),(0,2,94),(0,3,61),(0,4,19),(0,5,27),(0,6,21),(0,7,59),(0,8,19),(0,9,22),(0,10,28),(0,11,34),(0,12,21),(0,13,28),(0,14,20),(0,15,81),(0,16,88),(0,17,63),(0,18,67),(0,19,28),(0,20,89),(0,21,4),(0,22,49),(0,23,84),(0,24,93),(0,25,92),(0,26,58),(0,27,72),(0,28,44),(0,29,22),(1,2,25),(1,3,78),(1,4,56),(1,5,27),(1,6,2),(1,7,65),(1,8,8),(1,9,56),(1,10,34),(1,12,87),(1,13,73),(1,14,69),(1,15,23),(1,16,82),(1,17,28),(1,18,78),(1,19,7),(1,20,69),(1,21,85),(1,22,66),(1,23,65),(1,24,92),(1,25,56),(1,26,51),(1,27,4),(1,28,52),(1,29,27),(2,3,95),(2,4,88),(2,5,33),(2,6,46),(2,7,30),(2,8,73),(2,9,24),(2,10,32),(2,11,29),(2,12,39),(2,13,64),(2,14,95),(2,15,7),(2,16,20),(2,17,57),(2,18,53),(2,19,15),(2,21,56),(2,22,11),(2,23,96),(2,24,53),(2,25,2),(2,26,17),(2,27,98),(2,28,83),(2,29,95),(3,4,23),(3,5,71),(3,6,13),(3,7,82),(3,8,2),(3,9,21),(3,10,83),(3,11,49),(3,12,72),(3,13,76),(3,14,75),(3,15,79),(3,17,7),(3,18,40),(3,19,77),(3,20,28),(3,21,8),(3,22,3),(3,23,28),(3,24,22),(3,25,49),(3,26,15),(3,27,20),(3,28,61),(3,29,5),(4,5,57),(4,6,62),(4,7,87),(4,8,40),(4,9,80),(4,10,51),(4,11,58),(4,12,77),(4,13,50),(4,14,72),(4,15,87),(4,16,25),(4,17,32),(4,18,11),(4,19,54),(4,20,2),(4,21,87),(4,22,94),(4,23,37),(4,24,98),(4,25,95),(4,26,12),(4,27,95),(4,28,24),(4,29,68),(5,6,66),(5,7,70),(5,8,98),(5,9,98),(5,10,88),(5,11,76),(5,12,26),(5,13,70),(5,14,57),(5,15,24),(5,16,40),(5,17,22),(5,18,1),(5,19,31),(5,20,31),(5,21,66),(5,22,61),(5,23,7),(5,24,43),(5,26,64),(5,27,26),(5,28,49),(5,29,90),(6,7,50),(6,8,46),(6,9,6),(6,10,78),(6,11,28),(6,12,7),(6,13,31),(6,14,69),(6,15,78),(6,16,75),(6,17,66),(6,18,11),(6,19,83),(6,20,29),(6,21,11),(6,22,75),(6,23,20),(6,24,54),(6,25,83),(6,26,35),(6,27,35),(6,28,36),(6,29,37),(7,8,68),(7,9,65),(7,10,36),(7,11,85),(7,12,71),(7,13,42),(7,14,91),(7,15,50),(7,16,84),(7,17,45),(7,18,19),(7,19,67),(7,20,83),(7,21,75),(7,22,22),(7,23,79),(7,24,77),(7,25,84),(7,26,84),(7,27,2),(7,28,12),(7,29,40),(8,9,44),(8,10,22),(8,11,34),(8,12,78),(8,13,77),(8,14,86),(8,15,67),(8,16,66),(8,17,52),(8,18,60),(8,19,87),(8,20,23),(8,21,90),(8,22,12),(8,23,92),(8,24,14),(8,25,1),(8,26,15),(8,27,23),(8,28,95),(8,29,45),(9,10,61),(9,11,56),(9,12,98),(9,13,50),(9,14,50),(9,15,74),(9,16,67),(9,17,9),(9,18,31),(9,19,44),(9,20,89),(9,21,88),(9,22,12),(9,23,55),(9,24,7),(9,25,16),(9,26,99),(9,27,18),(9,28,38),(9,29,61),(10,11,18),(10,12,26),(10,13,45),(10,14,69),(10,15,1),(10,16,68),(10,17,41),(10,18,14),(10,19,49),(10,20,60),(10,21,20),(10,22,7),(10,23,18),(10,24,94),(10,25,31),(10,26,51),(10,27,54),(10,28,45),(10,29,19),(11,12,59),(11,13,87),(11,14,30),(11,15,55),(11,16,24),(11,17,92),(11,18,19),(11,19,88),(11,20,9),(11,21,63),(11,22,8),(11,23,87),(11,24,33),(11,25,66),(11,26,79),(11,27,4),(11,28,44),(11,29,34),(12,13,44),(12,14,36),(12,15,69),(12,16,6),(12,17,73),(12,18,78),(12,19,36),(12,20,7),(12,21,62),(12,22,14),(12,23,86),(12,24,95),(12,25,59),(12,26,4),(12,27,78),(12,28,57),(12,29,17),(13,14,32),(13,15,29),(13,16,53),(13,17,1),(13,18,83),(13,19,57),(13,20,30),(13,21,9),(13,22,58),(13,23,84),(13,24,2),(13,25,57),(13,26,15),(13,27,64),(13,28,82),(13,29,84),(14,15,32),(14,16,90),(14,17,48),(14,18,33),(14,19,92),(14,20,91),(14,21,2),(14,22,92),(14,23,92),(14,24,62),(14,25,79),(14,26,61),(14,27,56),(14,28,32),(14,29,28),(15,16,22),(15,17,5),(15,18,84),(15,19,87),(15,20,70),(15,21,83),(15,22,38),(15,23,27),(15,24,92),(15,25,98),(15,26,82),(15,27,46),(15,28,54),(15,29,65),(16,17,5),(16,18,48),(16,19,60),(16,20,75),(16,21,63),(16,22,10),(16,23,98),(16,24,73),(16,25,58),(16,26,24),(16,27,48),(16,28,40),(17,18,99),(17,19,67),(17,20,90),(17,21,4),(17,22,83),(17,23,7),(17,24,49),(17,25,92),(17,26,90),(17,27,35),(17,28,85),(17,29,34),(18,19,4),(18,20,57),(18,21,73),(18,22,99),(18,23,17),(18,24,39),(18,25,97),(18,26,79),(18,27,68),(18,28,51),(18,29,32),(19,20,67),(19,21,87),(19,22,98),(19,23,23),(19,24,35),(19,25,97),(19,26,14),(19,27,34),(19,28,79),(19,29,6),(20,21,7),(20,22,66),(20,23,82),(20,24,90),(20,25,29),(20,26,20),(20,27,76),(20,28,38),(20,29,17),(21,22,46),(21,23,2),(21,24,98),(21,25,87),(21,26,89),(21,27,97),(21,28,99),(21,29,15),(22,23,23),(22,24,82),(22,25,87),(22,26,60),(22,27,88),(22,28,68),(22,29,94),(23,24,55),(23,25,52),(23,26,41),(23,27,68),(23,28,67),(23,29,97),(24,25,73),(24,26,48),(24,27,5),(24,28,76),(24,29,96),(25,26,78),(25,27,92),(25,28,40),(25,29,41),(26,27,76),(26,28,37),(26,29,64),(27,28,64),(27,29,73),(28,29,16)]

--Isolate tuple part
get_first :: (Int, Int, Int) -> Int
get_first (first, second, value) = first

get_second :: (Int, Int, Int) -> Int
get_second (first, second, value) = second

get_value :: (Int, Int, Int) -> Int
get_value (first, second, value) = value

--Finding minimum edge
find_min_edge :: [(Int, Int, Int)] -> (Int, Int, Int) -> (Int, Int, Int)
find_min_edge [] current_min = current_min
find_min_edge (head:tail) current_min
    | get_value head < get_value current_min = find_min_edge tail head
    | otherwise = find_min_edge tail current_min
    
--Get graph size
get_size :: [(Int, Int, Int)] -> Int -> Int
get_size [] size = size
get_size (head:tail) size = get_size tail (size + 1)

--Create ancestors list
create_anc_list :: Int -> Int -> [(Int, Int)] -> [(Int, Int)]
create_anc_list size current anc
    | current < size = create_anc_list size (current + 1) [(current, current)] ++ anc
    | otherwise = anc

--Get value from ancestors list
get_value_anc :: [(Int, Int)] -> Int -> Int
get_value_anc (head:tail) vertex
    | fst head == vertex = snd head
    | otherwise = get_value_anc tail vertex

--Get most distant ancestor
loop_check :: [(Int, Int)] -> Int -> Int
loop_check ancestors vertex 
    | get_value_anc ancestors vertex == vertex = vertex
    | otherwise = loop_check ancestors (get_value_anc ancestors vertex)

--This edge creates cycle, to be deleted
delete_edge:: [(Int, Int, Int)] -> (Int, Int, Int) -> [(Int, Int, Int)]
delete_edge (head:tail) edge
    | head == edge = tail
    | otherwise = head:(delete_edge tail edge)

--Update list of ancestors
update_ancestors :: [(Int, Int)] -> Int -> Int -> [(Int, Int)]
update_ancestors (head:tail) vertex_1 vertex_2
    | (snd head) == vertex_1 = [(fst head, vertex_2)] ++ tail
    | otherwise = head:(update_ancestors tail vertex_1 vertex_2)
    

--While all vertex not in tree
main_cycle :: [(Int, Int, Int)] -> [(Int, Int)] -> Int -> Int -> [(Int, Int)] -> Int -> ([(Int, Int)], Int)
main_cycle graph ancestors size edge_done output total_weight
    | edge_done < size - 1 = 
        if loop_check ancestors (get_first current_min) == loop_check ancestors (get_second current_min) then 
            main_cycle (delete_edge graph current_min) ancestors size edge_done output total_weight
        else 
            main_cycle (delete_edge graph current_min) new_anc size (edge_done + 1) (output ++ [((get_first current_min) + 1, (get_second current_min) + 1)]) (total_weight + (get_value current_min))
    | otherwise = (output, total_weight)
    where 
        current_min = find_min_edge graph (maxBound::Int, maxBound::Int, maxBound::Int)
        new_anc = update_ancestors ancestors (loop_check ancestors (get_first current_min)) (loop_check ancestors (get_second current_min))

--Main function
main_ :: [(Int, Int, Int)] -> Int -> ([(Int, Int)], Int)
main_ graph size =
   main_cycle graph ancestors size 0 [] 0
   where
       ancestors = create_anc_list size 0 []
   
time :: [(Int, Int, Int)] -> Int -> IO ()
time graph size = do {
    start <- getCurrentTime;
    print $ main_ graph size;
    stop <- getCurrentTime;
    print $ diffUTCTime stop start
}