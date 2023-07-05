data BinaryTree element =
    None
    | Node element (BinaryTree element) (BinaryTree element)

--Creating binary tree - ex 1
create_tree line = print_tree (create_subtree (words line)) where
    print_tree None = ""
    print_tree (Node value None None) = value
    print_tree (Node value sub_ sub_right) = "(" ++ print_tree sub_ ++ ")" ++ value ++ "(" ++ print_tree sub_right ++ ")"
    
--Reverse polish notation - ex 2
end_bypass line = print_end_bypass (create_subtree (words line)) where
    print_end_bypass None = ""
    print_end_bypass (Node value None None) = value ++ " " 
    print_end_bypass (Node value sub_left sub_right) = print_end_bypass sub_left ++ " "  ++ print_end_bypass sub_right ++ " "  ++ value ++ " "
   
--Calc original string with no brackets - ex 3
calc_tree line = head (foldl foldingFunction [] (words (end_bypass line))) where
    foldingFunction (first:second:remain) "*" = (first * second):remain
    foldingFunction (first:second:remain) "/" = (second `div` first):remain
    foldingFunction (first:second:remain) "+" = (first + second):remain
    foldingFunction (first:second:remain) "-" = (second - first):remain
    foldingFunction xs numberString = read numberString:xs 

--Operation is ancestor, values are descendants
create_new_node sub_tree [] = (sub_tree, [])
create_new_node sub_tree (operation:value:tail) = 
    if operation == "+" || operation == "-" then (sub_tree, (operation:(value:tail)))
    else create_new_node (Node operation sub_tree (Node value None None)) tail

--hmmm
add_node rest_tree [] = rest_tree
add_node rest_tree (operation:value:tail) = add_node (Node operation rest_tree sub_tree) rest where
    (sub_tree, rest) = create_new_node (Node value None None) tail 

--Creating tree from lowest level - first priority
create_subtree symbols_list = first_node symbols_list where
    --Extract symbols for the first node
    first_node [] = None
    first_node (head:tail) = add_node new_node rest_list where
        (new_node, rest_list) = create_new_node (Node head None None) tail
