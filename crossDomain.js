
/**
 * 跨域对象
 * toProxyUrl 跨域框架的代理地址
 * localProxyUrl 本地域的代理地址
 * crossType 跨域类型：toMain(给本地域发送消息)，toSub(本地给跨域框架发送消息)
 */
function crossDimain(toProxyUrl,localProxyUrl,crossType){
        var BUSMGR = {
    		localProxyUrl:localProxyUrl,
            /**
             * 消息管理的本地窗口列表
             */
            allWin: [],
            /**
             * 消息管理的代理窗口列表
             */
            proxyWin: [],
            /**
             *  本地给消息框架发送消息
             * @param {type} event 事件类型
             * @param {type} data 事件数据
             * @param {type} subUrl 跨域代理地址
             * @returns {unresolved}
             */
            _fire: function(event, data,subUrl){
                for(var q in this.proxyWin){
                	var v = this.proxyWin[q];
                	if(!v){
                		return;
                	}else {
                		try{
                			if(v.closed){
                				v = null;
                				delete this.proxyWin[q];
                				return;
                			}
                		}catch(e){
                			try{
                				v = null;
                				delete this.proxyWin[q];
                				return;
                			}catch(e){
                				return;
                			}
                		}
                	}
                	//给代理窗口window.name赋值为data
                	v.name=data;
                	//跳转到跨域框架代理
                	v.location = subUrl+"#toSub?"+event;
                }
            },
            /**
             *  消息框架给本地发送消息
             * @param {type} event 事件类型
             * @param {type} data 事件数据
             * @returns {unresolved}
             */
            _fireMain: function(event, data){
            	 for(var q in this.allWin){
                     var v = this.allWin[q];
                     var retOwn = false;
                     if(!v){
                         return;
                     }else {
                         try{
                             if(v.closed){
                                 v = null;
                                 delete this.allWin[q];
                                 return;
                             }
                         }catch(e){
                             try{
                                 v = null;
                                 delete this.allWin[q];
                                 return;
                             }catch(e){
                                 return;
                             }
                         }
                     }
                     try{
                         retOwn = v.hasOwnProperty('RFMGR'); //IE 6以下该行出错
                     }catch(err){
                         try{
                             retOwn = Object.prototype.hasOwnProperty.call(v, 'RFMGR'); //firefox跨窗口是该行返回值错误
                         }catch(err1){
                             //ie8时没有.closed属性，所以catch异常删除过期window
                             delete this.allWin[q];
                             retOwn = false;
                         }
                     }
                     //if (Object.prototype.hasOwnProperty.call(v, 'RFMGR') /*v.hasOwnProperty('RFMGR')*/ && v.RFMGR !== null && v.RFMGR !== undefined) {
                     if(retOwn&&v.RFMGR!==null&&v.RFMGR!==undefined){
                         try{
                             v.RFMGR._fire.call(v.RFMGR, event, data);
                         }catch(e){
                             try{
                                 delete this.allWin[q];
                                 return;
                             }catch(e){
                                 return;
                             }
                         }
                     }
                 }
            }
        };
        var RFMGR = {
    		toMainProxy:localProxyUrl,
    		toSubProxy:toProxyUrl,
    		crossType:crossType,
            /**
             * 消息总线所在窗口
             */
            BUSWINDOW: undefined,
            /**
             * 本窗口消息总线
             */
            eventBus: {},
            /**
             * 注册本地消息窗口
             * @param {type} win
             * @returns {undefined}
             */
            register: function(win){
                var busWindow = window.top;
                if(busWindow.BUSMGR===undefined||busWindow.BUSMGR===null){
                	busWindow.BUSMGR = BUSMGR;
                }
                var haswindow = false;
                var mpbus = busWindow.BUSMGR;
                for(var p in mpbus["allWin"]){
                    var a;
                    try{
                        a = mpbus["allWin"][p];
                        //这两句不能省，这句是为了ie8及以下判断window是否失效的
                        if(a.closed){
                            a = null;
                            delete mpbus["allWin"][p];
                        }
                        if(a===win)
                            haswindow = true;
                    }catch(e){
                        try{
                            delete mpbus["allWin"][p];
                        }catch(e){}
                    }
                }
                if(!haswindow)
                    busWindow.BUSMGR.allWin.push(win);
                this.BUSWINDOW = busWindow;
            },
            /**
             *  向消息门户发送事件
             * @param {type} event 事件类型
             * @param {type} data 事件数据
             * @returns {unresolved}
             */
            send: function(event,data){
            	var me=window.RFMGR;
            	 if(me.crossType==="toSub"){//本地给所有的代理框架发送消息
            		 if(jQuery.isWindow(me.BUSWINDOW)){
                         this.BUSWINDOW.BUSMGR._fire(event, data,me.toSubProxy);
                     }
                 }else if (me.crossType==="toMain"){//代理框架给本地发送消息
                	 
                	 var isFirst = true;
             		 var iframe = document.createElement('iframe');
                     iframe.style.display = 'none';
            		 iframe.src=me.toSubProxy;
        			 iframe.name=data;
        			 var callBack=function(){
        				 if(isFirst){//第一次跳转到本地代理地址，传递数据
        					 iframe.contentWindow.location = me.toMainProxy+"#toMain?"+event;
        					 isFirst = false;
        				 }else{//传递给本地消息后销毁代理iframe
        					 //iframe.contentWindow.document.write('');
    		                 //iframe.contentWindow.close();
    		                 document.body.removeChild(iframe);
    		                 //iframe.src = '';
    		                 iframe = null;
        				 }
        			 }
        			 
        			 if(iframe.attachEvent){
        				 iframe.attachEvent('onload',callBack);
        		     } else {
        		    	 iframe.onload = callBack;
        		     }
                  document.body.appendChild(iframe);
                 }
            },
            /**
             * 消息注册方法
             * @param {type} event
             * @param {type} func
             * @returns {undefined}
             */
            on: function(event, func){
        		this.eventBus[event] = func;
            },
            /**
             * 本窗口事件触发器
             * @param {type} event
             * @param {type} data
             */
            _fire: function(event, data){
                var me = this;
                jQuery.each(this.eventBus, function(k, v){
                    if(k===event){
                        v.call(me, data);
                        if(me.crossType==='toMain'){
                        	//执行完回调，启动本地消息代理
                        	var pIframe=document.getElementById("proxyIframe");
                        	pIframe.contentWindow.location=me.toMainProxy+"#register";
                        }
                    }
                });
            }
        };
        window.RFMGR = RFMGR;
        /**
         * crossType是toSub，将自身窗口注册到top中
         */
        if(crossType==="toSub"){
        	RFMGR.register(window);
        }else if(crossType==="toMain"){//如果是toMain，创建一个代理，将代理注册到top中
        	var iframe = document.createElement('iframe');
             iframe.style.display = 'none';
             iframe.id="proxyIframe";
			 iframe.src=localProxyUrl+"#register";
             document.body.appendChild(iframe);
        }
}