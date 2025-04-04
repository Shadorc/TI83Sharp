:Menu("TIME","CHRONOMETRE",1,"DECOMPTE",2
:Lbl 1
:ClrHome
:DelVar D
:DelVar E
:DelVar F
:DelVar K
:While 1
	:getKey→K
	:If K=105
	:Stop
	:D+.09→D
	:While 1≤D
		:E+1→E
		:DelVar D
	:End
	:While E=60
		:1+F→F
		:DelVar E
		:ClrHome
	:End
	:Output(3,6,"TEMPS :
	:Output(5,8,":
	:Output(5,7,F
	:If E<10
	:Then
		:Output(5,9,0
		:Output(5,10,E
	:Else
		:Output(5,9,E
	:End
:End
:Lbl 2
:DelVar B
:Repeat C<60
	:ClrHome
	:Input "MINUTES:",A
	:Input "SECONDES:",C
:End
:ClrHome
:While 1
	:While not(C
		:60+C→C
		:A-1→A
	:End
	:B+.125→B
	:While B≥1
		:DelVar B
		:C-1→C
		:ClrHome
	:End
	:If A≤0 and C≤0
	:Then
		:ClrHome
		:Output(4,3,"TEMPS ECOULE"
		:Stop
	:End	
	:Output(2,7,"TEMPS
	:Output(3,5,"RESTANTES :
	:If C<10
	:Then
		:Output(5,9,0
		:Output(5,10,C
	:Else
		:Output(5,9,C
	:End
	:Output(5,8,":
	:If A<10
	:Then
		:Output(5,7,A
	:Else
		:Output(5,6,A
	:End
:End