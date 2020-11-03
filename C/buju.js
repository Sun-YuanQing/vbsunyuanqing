
    $(function () {
        var leftOffset, inx, nextW2, nextW, thisObject;
        var dragging = false;
        var doc = document;
        var labBtn = $("#hj_wrap").find('label');
        var wrapWidth = $("#hj_wrap").width();
        //定义一个对象
        function PointerObject() {
            this.el = null;
            this.grabx = null;
            this.left = null;
            this.pointerPosition = 0;
            this.clickX = 0;
        }

        labBtn.bind('mousedown', function (e) {
            dragging = true;
            thisObject = new PointerObject();
            thisObject.el = this;
            thisObject.pointerPosition = $(this).offset().left;
            thisObject.clickX = e.pageX;
        }
        );

        doc.onmousemove = function (e) {
            if (dragging) {
                if (thisObject != null) {
                    var changeDistance = 0;
                    var nextWidth = $(thisObject.el).next().width();
                    var prevWidth = $(thisObject.el).prev().width();
                    if (thisObject.clickX >= e.pageX) {
                        //鼠标向左移动
                        changeDistance = Number(thisObject.clickX) - Number(e.pageX);
                        if ($(thisObject.el).prev().width() - changeDistance < 20) {

                        } else {
                            $(thisObject.el).prev().width($(thisObject.el).prev().width() - changeDistance);
                            $(thisObject.el).next().width($(thisObject.el).next().width() + changeDistance);
                            thisObject.pointerPosition = (thisObject.pointerPosition - changeDistance);
                            thisObject.clickX = e.pageX;
                            $(thisObject.el).offset({ left: e.pageX - 1 });
                      }

                    } else {
                        //鼠标向右移动
                        changeDistance = Number(e.pageX) - Number(thisObject.clickX);
                        if ($(thisObject.el).next().width() - changeDistance < 30) {

                        } else {
                            $(thisObject.el).prev().width($(thisObject.el).prev().width() + changeDistance);
                            $(thisObject.el).next().width($(thisObject.el).next().width() - changeDistance);
                            thisObject.pointerPosition = (thisObject.pointerPosition + changeDistance);
                            thisObject.clickX = e.pageX;
                            $(thisObject.el).offset({ left: e.pageX -1});
                        }
                    }
                }
            }
        };

        $(doc).mouseup(function (e) {
            if (thisObject != null) {
                thisObject = null;
            }
            dragging = false;
            e.cancelBubble = true;
        })
    })
