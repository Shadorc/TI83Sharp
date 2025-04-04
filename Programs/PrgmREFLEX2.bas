:ClrHome
:Output(1,4,"REFLEX V.2
:Disp "","","APPUYEZ SUR","ENTRER DES","QUE VOUS VOYEZ","LE NOMBRE 42.
:Output(8,10,"SHADORC
:Pause
:Lbl E
:Menu("DIFFICULTE:","LENT",1,"MOYEN",2,"RAPIDE",3,"TRES RAPIDE",4,"IMPOSSIBRU",5
:Lbl 1:1→N
:Goto A
:Lbl 2:2→N
:Goto A
:Lbl 3:3→N
:Goto A
:Lbl 4:4→N
:Goto A
:Lbl 5:5→N
:Lbl A
:ClrHome
:DelVar S
:While 1
:randInt(1,3→R
:If R=1:Output(4,8,42
:If R=2:Output(4,8,24
:If R=3:Output(4,8,54
:DelVar K
:For(T,1,40/N
:getKey→K
:If K=105 and R≠1:Goto 0
:If K=0 and T=40/N and R=1
:Goto 0
:If K=105 and R=1
:40/N→T
:End
:If R=1:S+1=S
:End
:Lbl 0
:ClrHome
:Output(4,5,"SCORE:
:Output(4,12,S
:Pause
:Menu("CONTINUER ?","OUI",E,"NON",F
:Lbl F