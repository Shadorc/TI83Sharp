:DelVar J
:DelVar I
:DelVar A
:DelVar L₁
:5→dim(L₁)
:While 0≥J
:ClrHome
:Input "NOMBRE TIRAGES :",J
:End
:ClrHome
:Disp "RESULTAT
:For(A,2,6
:Output(A,1,A-1
:Output(A,2,":
:End
:Output(8,1,"TOTAL :
:Repeat I=J
:randInt(1,5→A
:Output(1,15,A
:L₁(A)+1→L₁(A
:For(Z,2,6
:Output(Z,3,L₁(Z-1
:End
:I+1→I
:Output(8,9,I
:End