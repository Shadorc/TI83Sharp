:ClrHome
:For(Y,1,8)
	:Output(Y,1,".")
	:Output(Y,16,".")
	:Output(8,Y,".")
	:Output(8,17-Y,".")
:End
:7→X
:8→Y
:{randInt(1,3),randInt(2,15),1}→L₃
:1→dim(L₁)
:1→dim(L₂)
:DelVar [A]
:{8,16}→dim([A])
:Repeat K=45
	:getKey→K
	:If Ans:Then
		:Output(X,Y," ")
		:Y-(K=24 and Y>2)+(K=26 and Y<15)→Y
		:X-(K=25 and X>2)+(K=34 and X<7)→X
		:Output(X,Y,"W")
		:If K=21:Then
			:X-1→L₁(dim(L₁)+1)
			:Y→L₂(dim(L₂)+1)
		:End
	:End
	:1→A
	:While A≠dim(L₁) and 1≠dim(L₁)	
		:A+1→A	
		:Output(L₁(A),L₂(A)," ")
		:0→[A](L₁(A),L₂(A))
		:L₁(A)-1→L₁(A)	
		:If L₁(A)=0:Then
			:seq(L₁(B+(B≥A)),B,1,dim(L₁)-1)→L₁
			:seq(L₂(B+(B≥A)),B,1,dim(L₂)-1)→L₂
		:Else
			:Output(L₁(A),L₂(A),".")
			:1→[A](L₁(A),L₂(A))
		:End
	:End
	:If dim(L₃)≠0:Then
		:Output(L₃(1),L₃(2)," ")
		:L₃(2)-(L₃(3)=0)+(L₃(3)=1)→L₃(2)
		:L₃(3)-(L₃(2)=15)+(L₃(2)=2)→L₃(3)
		:If [A](L₃(1),L₃(2))=1:Then	
			:0→dim(L₃)	
		:Else		
			:Output(L₃(1),L₃(2),"H")	
		:End
	:End
:End