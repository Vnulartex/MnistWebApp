    var canvas, ctx, flag = false,
        prevX = 0,
        currX = 0,
        prevY = 0,
        currY = 0,
        dot_flag = false;

    var x = "black",
        y;
    
    function init() {
        canvas = document.getElementById("can");
        y = canvas.height/10;
        w = canvas.width;
        h = canvas.height;   
        ctx = canvas.getContext("2d");
        ctx.fillStyle = "white";
        ctx.fillRect(0, 0, w, h);
        canvas.addEventListener("mousemove", function (e) {
            findxy('move', e)
        }, false);
        canvas.addEventListener("mousedown", function (e) {
            findxy('down', e)
        }, false);
        canvas.addEventListener("mouseup", function (e) {
            findxy('up', e)
        }, false);
        canvas.addEventListener("mouseout", function (e) {
            findxy('out', e)
        }, false);
    }
    
    function draw() {
        ctx.beginPath();
        ctx.moveTo(prevX, prevY);
        ctx.lineTo(currX, currY);
        ctx.strokeStyle = x;
        ctx.lineWidth = y;
        ctx.lineCap="round";
        ctx.stroke();
        ctx.closePath();
    }
    
    function erase() {
        var m = confirm("Want to clear");
        if (m) {
            ctx.fillStyle = "white";
            ctx.fillRect(0, 0, w, h);
        }
    }
    
    function save() {
        var image = canvas.toDataURL("image/png").replace("data:image/png;base64,", "");  
        $.ajax({
            url: "Home/AjaxActionResult",
            method: "POST",           
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "imageString" : image.toString() }),
            success: function (data) {
                $(".pixels").html(data);
                // $(".pixels").html($($.parseHTML(data)).filter("#pixels").text());
                // $("#results").html(data);
            },
            error: function(jqxhr, status, exception) {
                alert("Exception:", exception);
            }
        });
    }
    
    function findxy(res, e) {
        if (res == 'down') {
            prevX = currX;
            prevY = currY;
            currX = e.clientX - canvas.offsetLeft;
            currY = e.clientY - canvas.offsetTop;
    
            flag = true;
            dot_flag = true;
            if (dot_flag) {
                ctx.beginPath();
                ctx.fillStyle = x;
                ctx.fillRect(currX, currY, 2, 2);
                ctx.closePath();
                dot_flag = false;
            }
        }
        if (res == 'up' || res == "out") {
            flag = false;
        }
        if (res == 'move') {
            if (flag) {
                prevX = currX;
                prevY = currY;
                currX = e.clientX - canvas.offsetLeft;
                currY = e.clientY - canvas.offsetTop;
                draw();
            }
        }
    }
