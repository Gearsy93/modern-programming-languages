fac n =
    if (n == 0) then 1
    else n * fac (n - 1)
soch n m = 
    if (n < m) then -1
    else (fac n)/((fac (n-m))*(fac m))