gcd_newref n m =
    if (m == 0) then n
    else gcd_newref m (n `rem` m)