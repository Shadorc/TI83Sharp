:ClrHome
:Output(1,4,"REFLEX V.1
:Disp "","","CE JEU VA METTRE","A RUDE EPREUVE","VOS REFLEXES
:Output(8,10,"SHADORC
:Pause
:Lbl 7
:Menu("DIFFICULTE","EASY",1,"NORMAL",2,"HARD",3,"VERY HARD",4,"IMPOSSIBLE",5
:Lbl 1:1→N
:Goto 6
:Lbl 2:2→N
:Goto 6
:Lbl 3:3→N
:Goto 6
:Lbl 4:4→N
:Goto 6
:Lbl 5:5→N
:Lbl 6
:ClrHome
:For(F,1,8
:Output(F,6,"I
:Output(F,11,"I
:End
:For(F,2,15
:Output(3,F,"-
:Output(6,F,"-
:End
:Output(3,6,"+
:Output(3,11,"+
:Output(6,6,"+
:Output(6,11,"+
:DelVar P
:While 1
:randInt(1,9→A
:If A≤3:1→Y
:If A>3 and A≤6
:4→Y
:If A>6:7→Y
:If A=1 or A=4 or A=7:2→X
:If A=2 or A=5 or A=8:7→X
:If A=3 or A=6 or A=9:12→X
:For(F,0,1
:For(G,0,3
:Output(Y+F,X+G,"X
:End:End
:For(F,1,75/N
:getKey→G
:If K
:(K=72)+2(K=73)+3(K=74)+4(K=82)+5(K=83)+6(K=84)+7(K=92)+8(K=93)+9(K=94)→B
:End
:If A≠B:Goto 0
:P+1→P
:For(F,0,1
:For(G,0,3
:Output(Y+F,X+G," "
:End:End:End
:Lbl 0
:ClrHome
:Output(2,5,"GAME OVER!
:Output(4,5,"SCORE:
:Output(4,11,P
:Pause
:Menu("CONTINUER?","OUI",7,"NON",8
:Lbl 8