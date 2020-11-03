/**
 * name:   monitorSplitWindows.js
 * route:   /static/js/control/ui
 * author:  luozhubang
 * date:    2016-10-26
 * function: 鐩戝惉绐楀彛鎷栧姩浜嬩欢锛屼緷璧栦簬/static/js/control/ui/DynamicWindow.js,
 * 
*/
$(function(){
	//榧犳爣妯悜銆佺珫鍚戞搷浣滃璞�
	var thisTransverseObject,thisVerticalObject;
	//鏂囨。瀵硅薄
	var doc 	  = document;
	//妯悜鍒嗗壊鏍�
	var transverseLabels 	  = $(".hj-wrap").find('.hj-transverse-split-label');
	//绔栧悜鍒嗗壊鏍�
	var verticalLabels 	  = $(".hj-wrap").find('.hj-vertical-split-label');

	if($(".hj-wrap").length>0){
		for(var i=0;i<$(".hj-wrap").length;i++){
			//console.log($(".hj-wrap")[i]);
			//console.log($($(".hj-wrap")[i]));
			var hjDivNums = $($(".hj-wrap")[i]).children(".hj-transverse-split-div");
			var defaultWidth =Math.floor(100/hjDivNums.length);
			$($(".hj-wrap")[i]).children(".hj-transverse-split-div").width(defaultWidth-1+"%");
		}
	}
	//瀹氫箟涓€涓璞�
	function PointerObject(){
		this.el = null;//褰撳墠榧犳爣閫夋嫨鐨勫璞�		
		this.clickX =0;//榧犳爣妯悜鍒濆浣嶇疆
		this.clickY =0;//榧犳爣绔栧悜鍒濆浣嶇疆
		this.transverseDragging=false;//鍒ゆ柇榧犳爣鍙惁妯悜鎷栧姩
		this.verticalDragging=false;//鍒ゆ柇榧犳爣鍙惁绔栧悜鎷栧姩
	}
	//妯悜鍒嗛殧鏍忕粦瀹氫簨浠�
	transverseLabels.bind('mousedown',function(e){
			thisTransverseObject = new PointerObject();
			thisTransverseObject.transverseDragging  = true;//榧犳爣鍙í鍚戞嫋鍔�
			thisTransverseObject.el = this;
			thisTransverseObject.clickX = e.pageX;//璁板綍榧犳爣妯悜鍒濆浣嶇疆
	});
	
	//绔栧悜鍒嗛殧鏍忕粦瀹氫簨浠�
	verticalLabels.bind('mousedown',function(e){
			//console.log("mousedown");
			thisVerticalObject = new PointerObject();
			thisVerticalObject.verticalDragging  = true;//榧犳爣鍙珫鍚戞嫋鍔�
			thisVerticalObject.el = this;
			thisVerticalObject.clickY = e.pageY;//璁板綍榧犳爣绔栧悜鍒濆浣嶇疆
	});

	doc.onmousemove = function(e){
		//榧犳爣妯悜鎷栧姩
		if(thisTransverseObject != null){
			if (thisTransverseObject.transverseDragging) {
				var changeDistance = 0;
				var nextWidth = $(thisTransverseObject.el).next().width();
				var prevWidth = $(thisTransverseObject.el).prev().width();
				if(thisTransverseObject.clickX>=e.pageX){
					//榧犳爣鍚戝乏绉诲姩
					changeDistance = Number(thisTransverseObject.clickX)-Number(e.pageX);
					if($(thisTransverseObject.el).prev().width()-changeDistance<20){
						
					}else{
						$(thisTransverseObject.el).prev().width($(thisTransverseObject.el).prev().width()-changeDistance);
						$(thisTransverseObject.el).next().width($(thisTransverseObject.el).next().width()+changeDistance);
						thisTransverseObject.clickX=e.pageX;
						$(thisTransverseObject.el).offset({left:e.pageX-4});	
					}
				}else{
					//榧犳爣鍚戝彸绉诲姩
					changeDistance = Number(e.pageX)-Number(thisTransverseObject.clickX);
					if($(thisTransverseObject.el).next().width()-changeDistance<20){
							
					}else{
						$(thisTransverseObject.el).prev().width($(thisTransverseObject.el).prev().width()+changeDistance);
						$(thisTransverseObject.el).next().width($(thisTransverseObject.el).next().width()-changeDistance);
						thisTransverseObject.clickX=e.pageX;
						$(thisTransverseObject.el).offset({left:e.pageX-4});
					}
				}
				$(thisTransverseObject.el).width(10);
			}
		}
		//榧犳爣绔栧悜鎷栧姩
		if(thisVerticalObject != null){
			if (thisVerticalObject.verticalDragging) {
				var changeDistance = 0;
				var nextheight = $(thisVerticalObject.el).next().height();
				var prevheight = $(thisVerticalObject.el).prev().height();
				if(thisVerticalObject.clickY>=e.pageY){
					//榧犳爣鍚戜笂绉诲姩
					changeDistance = Number(thisVerticalObject.clickY)-Number(e.pageY);
					if($(thisVerticalObject.el).prev().height()-changeDistance<20){
						
					}else{
						$(thisVerticalObject.el).prev().height($(thisVerticalObject.el).prev().height()-changeDistance);
						$(thisVerticalObject.el).next().height($(thisVerticalObject.el).next().height()+changeDistance);
						thisVerticalObject.clickY=e.pageY;
						$(thisVerticalObject.el).offset({top:e.pageY-4});	
					}
				}else{
					//榧犳爣鍚戜笅绉诲姩
					changeDistance = Number(e.pageY)-Number(thisVerticalObject.clickY);
					if($(thisVerticalObject.el).next().height()-changeDistance<20){
							
					}else{
						$(thisVerticalObject.el).prev().height($(thisVerticalObject.el).prev().height()+changeDistance);
						$(thisVerticalObject.el).next().height($(thisVerticalObject.el).next().height()-changeDistance);
						thisVerticalObject.clickY=e.pageY;
						$(thisVerticalObject.el).offset({top:e.pageY-4});
					}
				}
				$(thisVerticalObject.el).height(10);
			}
		}
	};

	$(doc).mouseup(function(e) {
		if (thisTransverseObject != null) {
			thisTransverseObject.transverseDragging = false;
	  		thisTransverseObject = null;
	 	}
	 	if (thisVerticalObject != null) {
			thisVerticalObject.verticalDragging = false;
	  		thisVerticalObject = null;
	 	}
	    
	    e.cancelBubble = true;
	});
});